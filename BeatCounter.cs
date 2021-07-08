using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace BeatCounter
{
    public partial class BeatCounter : Form, IDisposable
    {
        #region private
        /// XMLファイルの情報を格納する。
        private XDocument _xml;
        private XMLClass _cls = new XMLClass();

        /// コントローラの情報
        private Joystick _joy;

        // 鍵盤or皿が操作された回数を格納
        private long _s_upnum;
        private long _s_downnum;
        private long _key1num;
        private long _key2num;
        private long _key3num;
        private long _key4num;
        private long _key5num;
        private long _key6num;
        private long _key7num;
        private long _todaynum;
        private long _alldaynum;

        // 前フレームと現フレームのアナログ軸の位置を格納する
        private int _s_rel_old_x;
        private int _s_rel_now_x;
        private int _s_rel_old_y;
        private int _s_rel_now_y;

        // フレームチェック用
        private ulong _counter = 1;

        // スクラッチが回っているか
        private bool _isActive = false;

        // スクラッチがRightへ回っているか
        private bool _isRight = false;

        // アナログ軸の最大値/最小値
        private int _rangeMax = 1000;
        private int _rangeMin = 0;

        // 鍵盤が押されっぱなしか
        private bool _joy1b = false;
        private bool _joy2b = false;
        private bool _joy3b = false;
        private bool _joy4b = false;
        private bool _joy5b = false;
        private bool _joy6b = false;
        private bool _joy7b = false;
        private bool _joyUp = false;
        private bool _joyDown = false;

        // 初期設定用(1度しか動かさない処理用)
        private bool _onceAction1 = false;
        private bool _onceAction2 = false;
        private bool _onceAction3 = false;

        // カウント変更モードか
        private bool _keyChangeMode = false;

        // 現在の背景色
        private Color _keyBackColor = new Color();

        // バックグラウンドでキーボードの打鍵を処理するためのリスト
        private List<string> _list = new List<string>();

        // バックグラウンドでキーボードの打鍵を処理するためのDLLをインポート
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern short GetAsyncKeyState(int nVirtKey);

        #endregion

        #region main処理群
        /// <summary>
        /// Formの処理
        /// </summary>
        public BeatCounter()
        {
            // XMLファイルの読み込みで失敗したらアプリを終了する。
            try
            {
                //xmlファイルを指定する
                _xml = XDocument.Load("Config.xml");
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                DialogResult dialog = MessageBox.Show(
                    "Config.xmlファイルが見つかりませんでした。" +
                    "アプリケーションを終了します。",
                    "エラー",
                    MessageBoxButtons.OK);
                Close();
                return;
            }

            InitializeComponent();

            // 起動時の変数を初期化
            TodayInit(false);

            // 最大化を無効に
            MaximizeBox = false;
        }

        /// <summary>
        /// Formの処理
        /// </summary>
        public void Exec()
        {
            // DirectXデバイスの初期化処理
            DirectXInit();

            // 設定の読み込み
            SettingInit();

            // フォームの生成
            Show();

            // フォームが作成されている間、実行する。
            while (Created)
            {
                MainLoop();

                // イベントがある場合は処理する
                Application.DoEvents();

                // CPUがフル稼働しないようにFPSの制限をかける。(簡易的に、おおよそ秒間60フレーム程度に制限)
                // 皿の処理が不安定になるのでコメントアウト。
                //Thread.Sleep(16);
            }
        }

        /// <summary>
        /// メインループ処理
        /// </summary>
        public void MainLoop()
        {
            try
            {
                if (!KeyBoardTips.Checked)
                {
                    // GamePad系の処理に遷移
                    UpdateForPad();
                }
                else
                {
                    // キーボード用の処理に遷移
                    UpdateForKeyBoard();
                }
            }
            catch (SharpDX.SharpDXException)
            {
                // アプリ起動時に読み込まれていたGamePadが切断された場合、閉じる処理を呼び出し、アプリを終了する。
                BeatCounter_Close(new object(), new FormClosingEventArgs(new CloseReason(), false));
                Application.Exit();
            }
        }

        /// <summary>
        /// 解放処理
        /// </summary>
        public new void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// パッド入力処理
        /// </summary>
        public void UpdateForPad()
        {
            // 数値変更モードの時は処理しない。
            if (_keyChangeMode) { return; }

            // 初期化されていない場合、処理を終了させる。
            if (_joy == null) { return; }

            // キャプチャするデバイスを取得する。
            _joy.Acquire();
            _joy.Poll();

            // ゲームパッドのデータ取得する。
            var jState = _joy.GetCurrentState();

            // 取得できない場合、処理終了する。
            if (jState == null) { return; }

            // アナログ軸の初期化処理。
            if (_onceAction1 == false)
            {
                _s_rel_old_x = jState.X;
                _s_rel_now_x = jState.X;
                _s_rel_old_y = jState.Y;
                _s_rel_now_y = jState.Y;
                _onceAction1 = true;
            }

            // 現在フレームと前フレームの比較でアナログ軸の処理を行うため、取得する。
            _s_rel_old_x = _s_rel_now_x;
            _s_rel_now_x = jState.X;
            _s_rel_old_y = _s_rel_now_y;
            _s_rel_now_y = jState.Y;

            // アナログ皿入力は基本的にDAOや虹、プロコンのみ
            if (AnalogSCTips.Checked == true)
            {
                AnalogSCMode(jState);
            }
            // BMSプレイ時の皿の処理
            else if (DigitalSCTips.Checked == true)
            {
                DigitalSCMode(jState);
            }

            var a = jState.PointOfViewControllers;

            #region Key判定
            if (DaoTips.Checked)
            {
                // Key1 判定 (1鍵が押されたとき且つ、押しっぱなしになってない判定の場合)
                if (jState.Buttons[0] && _joy1b == false)
                {
                    Push1Key(true);
                }
                else
                {
                    if (jState.Buttons[0] == false)
                    {
                        Push1Key(false);
                    }
                }

                // Key2 判定
                if (jState.Buttons[1] && _joy2b == false)
                {
                    Push2Key(true);
                }
                else
                {
                    if (jState.Buttons[1] == false)
                    {
                        Push2Key(false);
                    }
                }

                // Key3 判定
                if (jState.Buttons[2] && _joy3b == false)
                {
                    Push3Key(true);
                }
                else
                {
                    if (jState.Buttons[2] == false)
                    {
                        Push3Key(false);
                    }
                }

                // Key4 判定
                if (jState.Buttons[3] && _joy4b == false)
                {
                    Push4Key(true);
                }
                else
                {
                    if (jState.Buttons[3] == false)
                    {
                        Push4Key(false);
                    }
                }

                // Key5 判定
                if (jState.Buttons[4] && _joy5b == false)
                {
                    Push5Key(true);
                }
                else
                {
                    if (jState.Buttons[4] == false)
                    {
                        Push5Key(false);
                    }
                }

                // Key6 判定
                if (jState.Buttons[5] && _joy6b == false)
                {
                    Push6Key(true);
                }
                else
                {
                    if (jState.Buttons[5] == false)
                    {
                        Push6Key(false);
                    }
                }

                // Key7 判定
                if (jState.Buttons[6] && _joy7b == false)
                {
                    Push7Key(true);
                }
                else
                {
                    if (jState.Buttons[6] == false)
                    {
                        Push7Key(false);
                    }
                }
            }
            else if (PS2ConTips.Checked)
            {
                // Key1 判定 (1鍵が押されたとき且つ、押しっぱなしになってない判定の場合)
                if (jState.Buttons[3] && _joy1b == false)
                {
                    Push1Key(true);
                }
                else
                {
                    if (jState.Buttons[3] == false)
                    {
                        Push1Key(false);
                    }
                }

                // Key2 判定
                if (jState.Buttons[6] && _joy2b == false)
                {
                    Push2Key(true);
                }
                else
                {
                    if (jState.Buttons[6] == false)
                    {
                        Push2Key(false);
                    }
                }

                // Key3 判定
                if (jState.Buttons[2] && _joy3b == false)
                {
                    Push3Key(true);
                }
                else
                {
                    if (jState.Buttons[2] == false)
                    {
                        Push3Key(false);
                    }
                }

                // Key4 判定
                if (jState.Buttons[7] && _joy4b == false)
                {
                    Push4Key(true);
                }
                else
                {
                    if (jState.Buttons[7] == false)
                    {
                        Push4Key(false);
                    }
                }

                // Key5 判定
                if (jState.Buttons[1] && _joy5b == false)
                {
                    Push5Key(true);
                }
                else
                {
                    if (jState.Buttons[1] == false)
                    {
                        Push5Key(false);
                    }
                }

                // Key6 判定
                if (jState.Buttons[4] && _joy6b == false)
                {
                    Push6Key(true);
                }
                else
                {
                    if (jState.Buttons[4] == false)
                    {
                        Push6Key(false);
                    }
                }

                // Key7 判定
                if (_s_rel_now_x == 0 && _joy7b == false)
                {
                    Push7Key(true);
                }
                else
                {
                    if (_s_rel_now_x != 0)
                    {
                        Push7Key(false);
                    }
                }
            }
            else if (BeatmaniaProConTips.Checked)
            {

            }
            else if (CustomTips.Checked)
            {
                // 十字キー入力とボタン入力で処理を分ける必要がある？要修正？
                // Key1 判定 (1鍵が押されたとき且つ、押しっぱなしになってない判定の場合)
                if (_cls.C_Key1 < 20)
                {
                    // 保存された設定を元に押されたキーが該当するキーであることを判別する。
                    if (jState.Buttons[_cls.C_Key1] && _joy1b == false)
                    {
                        Push1Key(true);
                    }
                    else
                    {
                        if (jState.Buttons[_cls.C_Key1] == false)
                        {
                            Push1Key(false);
                        }
                    }
                }
                else if (_cls.C_Key1 >= 50)
                {
                    // 軸入力の場合の処理
                    if (_cls.C_Key1 == 50)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 0 && _joy1b == false)
                        {
                            Push1Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 0)
                            {
                                Push1Key(false);
                            }
                        }
                    }
                    // 十字キー入力「右」の場合。
                    else if (_cls.C_Key1 == 51)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 1000 && _joy1b == false)
                        {
                            Push1Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 1000)
                            {
                                Push1Key(false);
                            }
                        }
                    }
                    // 十字キー入力「上」の場合。
                    else if (_cls.C_Key1 == 52)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 1000 && _joy1b == false)
                        {
                            Push1Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 1000)
                            {
                                Push1Key(false);
                            }
                        }
                    }
                    // 十字キー入力「下」の場合。
                    else if (_cls.C_Key1 == 53)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 0 && _joy1b == false)
                        {
                            Push1Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 0)
                            {
                                Push1Key(false);
                            }
                        }
                    }
                }

                // Key2 判定
                if (_cls.C_Key2 < 20)
                {
                    if (jState.Buttons[_cls.C_Key2] && _joy2b == false)
                    {
                        Push2Key(true);
                    }
                    else
                    {
                        if (jState.Buttons[_cls.C_Key2] == false)
                        {
                            Push2Key(false);
                        }
                    }
                }
                else if (_cls.C_Key2 >= 50)
                {
                    // 軸入力の場合の処理
                    if (_cls.C_Key2 == 50)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 0 && _joy2b == false)
                        {
                            Push2Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 0)
                            {
                                Push2Key(false);
                            }
                        }
                    }
                    // 十字キー入力「右」の場合。
                    else if (_cls.C_Key2 == 51)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 1000 && _joy2b == false)
                        {
                            Push2Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 1000)
                            {
                                Push2Key(false);
                            }
                        }
                    }
                    // 十字キー入力「上」の場合。
                    else if (_cls.C_Key2 == 52)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 1000 && _joy2b == false)
                        {
                            Push2Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 1000)
                            {
                                Push2Key(false);
                            }
                        }
                    }
                    // 十字キー入力「下」の場合。
                    else if (_cls.C_Key2 == 53)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 0 && _joy2b == false)
                        {
                            Push2Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 0)
                            {
                                Push2Key(false);
                            }
                        }
                    }
                }

                // Key3 判定
                if (_cls.C_Key3 < 20)
                {
                    if (jState.Buttons[_cls.C_Key3] && _joy3b == false)
                    {
                        Push3Key(true);
                    }
                    else
                    {
                        if (jState.Buttons[_cls.C_Key3] == false)
                        {
                            Push3Key(false);
                        }
                    }
                }
                else if (_cls.C_Key3 >= 50)
                {
                    // 軸入力の場合の処理
                    if (_cls.C_Key3 == 50)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 0 && _joy3b == false)
                        {
                            Push3Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 0)
                            {
                                Push3Key(false);
                            }
                        }
                    }
                    // 十字キー入力「右」の場合。
                    else if (_cls.C_Key3 == 51)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 1000 && _joy3b == false)
                        {
                            Push3Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 1000)
                            {
                                Push3Key(false);
                            }
                        }
                    }
                    // 十字キー入力「上」の場合。
                    else if (_cls.C_Key3 == 52)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 1000 && _joy3b == false)
                        {
                            Push3Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 1000)
                            {
                                Push3Key(false);
                            }
                        }
                    }
                    // 十字キー入力「下」の場合。
                    else if (_cls.C_Key3 == 53)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 0 && _joy3b == false)
                        {
                            Push3Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 0)
                            {
                                Push3Key(false);
                            }
                        }
                    }
                }

                // Key4 判定
                if (_cls.C_Key4 < 20)
                {
                    if (jState.Buttons[_cls.C_Key4] && _joy4b == false)
                    {
                        Push4Key(true);
                    }
                    else
                    {
                        if (jState.Buttons[_cls.C_Key4] == false)
                        {
                            Push4Key(false);
                        }
                    }
                }
                else if (_cls.C_Key4 >= 50)
                {
                    // 軸入力の場合の処理
                    if (_cls.C_Key4 == 50)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 0 && _joy4b == false)
                        {
                            Push4Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 0)
                            {
                                Push4Key(false);
                            }
                        }
                    }
                    // 十字キー入力「右」の場合。
                    else if (_cls.C_Key4 == 51)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 1000 && _joy4b == false)
                        {
                            Push4Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 1000)
                            {
                                Push4Key(false);
                            }
                        }
                    }
                    // 十字キー入力「上」の場合。
                    else if (_cls.C_Key4 == 52)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 1000 && _joy4b == false)
                        {
                            Push4Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 1000)
                            {
                                Push4Key(false);
                            }
                        }
                    }
                    // 十字キー入力「下」の場合。
                    else if (_cls.C_Key4 == 53)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 0 && _joy4b == false)
                        {
                            Push4Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 0)
                            {
                                Push4Key(false);
                            }
                        }
                    }
                }

                // Key5 判定
                if (_cls.C_Key5 < 20)
                {
                    if (jState.Buttons[_cls.C_Key5] && _joy5b == false)
                    {
                        Push5Key(true);
                    }
                    else
                    {
                        if (jState.Buttons[_cls.C_Key5] == false)
                        {
                            Push5Key(false);
                        }
                    }
                }
                else if (_cls.C_Key5 >= 50)
                {
                    // 軸入力の場合の処理
                    if (_cls.C_Key5 == 50)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 0 && _joy5b == false)
                        {
                            Push5Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 0)
                            {
                                Push5Key(false);
                            }
                        }
                    }
                    // 十字キー入力「右」の場合。
                    else if (_cls.C_Key5 == 51)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 1000 && _joy5b == false)
                        {
                            Push5Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 1000)
                            {
                                Push5Key(false);
                            }
                        }
                    }
                    // 十字キー入力「上」の場合。
                    else if (_cls.C_Key5 == 52)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 1000 && _joy5b == false)
                        {
                            Push5Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 1000)
                            {
                                Push5Key(false);
                            }
                        }
                    }
                    // 十字キー入力「下」の場合。
                    else if (_cls.C_Key5 == 53)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 0 && _joy5b == false)
                        {
                            Push5Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 0)
                            {
                                Push5Key(false);
                            }
                        }
                    }
                }

                // Key6 判定
                if (_cls.C_Key6 < 20)
                {
                    if (jState.Buttons[_cls.C_Key6] && _joy6b == false)
                    {
                        Push6Key(true);
                    }
                    else
                    {
                        if (jState.Buttons[_cls.C_Key6] == false)
                        {
                            Push6Key(false);
                        }
                    }
                }
                else if (_cls.C_Key6 >= 50)
                {
                    // 軸入力の場合の処理
                    if (_cls.C_Key6 == 50)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 0 && _joy6b == false)
                        {
                            Push6Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 0)
                            {
                                Push6Key(false);
                            }
                        }
                    }
                    // 十字キー入力「右」の場合。
                    else if (_cls.C_Key6 == 51)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 1000 && _joy6b == false)
                        {
                            Push6Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 1000)
                            {
                                Push6Key(false);
                            }
                        }
                    }
                    // 十字キー入力「上」の場合。
                    else if (_cls.C_Key6 == 52)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 1000 && _joy6b == false)
                        {
                            Push6Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 1000)
                            {
                                Push6Key(false);
                            }
                        }
                    }
                    // 十字キー入力「下」の場合。
                    else if (_cls.C_Key6 == 53)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 0 && _joy6b == false)
                        {
                            Push6Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 0)
                            {
                                Push6Key(false);
                            }
                        }
                    }
                }

                // Key7 判定
                if (_cls.C_Key7 < 20)
                {
                    if (jState.Buttons[_cls.C_Key7] && _joy7b == false)
                    {
                        Push7Key(true);
                    }
                    else
                    {
                        if (jState.Buttons[_cls.C_Key7] == false)
                        {
                            Push7Key(false);
                        }
                    }
                }
                else if (_cls.C_Key7 >= 50)
                {
                    // 軸入力の場合の処理
                    if (_cls.C_Key7 == 50)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 0 && _joy7b == false)
                        {
                            Push7Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 0)
                            {
                                Push7Key(false);
                            }
                        }
                    }
                    // 十字キー入力「右」の場合。
                    else if (_cls.C_Key7 == 51)
                    {
                        // 左の場合
                        if (_s_rel_now_x == 1000 && _joy7b == false)
                        {
                            Push7Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != 1000)
                            {
                                Push7Key(false);
                            }
                        }
                    }
                    // 十字キー入力「上」の場合。
                    else if (_cls.C_Key7 == 52)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 1000 && _joy7b == false)
                        {
                            Push7Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 1000)
                            {
                                Push7Key(false);
                            }
                        }
                    }
                    // 十字キー入力「下」の場合。
                    else if (_cls.C_Key7 == 53)
                    {
                        // 左の場合
                        if (_s_rel_now_y == 0 && _joy7b == false)
                        {
                            Push7Key(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != 0)
                            {
                                Push7Key(false);
                            }
                        }
                    }
                }
            }
            #endregion

            // 今回の合計と総合計を押された分だけ加算。
            TodayKeys.Text = _todaynum.ToString();
            AllDayKeys.Text = _alldaynum.ToString();

            // 初期化処理 起動時に+1など加算されないようにするため。
            if (_onceAction2 == false)
            {
                S_Up.Text = "0";
                _s_upnum = 0;
                S_Down.Text = "0";
                _s_downnum = 0;
                _onceAction2 = true;
            }
        }

        /// <summary>
        /// キ－ボード入力処理
        /// </summary>
        public void UpdateForKeyBoard()
        {
            // 数値変更モードの時は処理しない。
            if (_keyChangeMode) { return; }

            // Key判定自体はKeyDownの処理内で行う。

            // バックグラウンドでもキーボードが押下されているかチェックする処理。
            foreach (string str in _list)
            {
                var item = (Keys)Enum.Parse(typeof(Keys), str);
                GetStateOfKeyLocked(item);
            }

            // 今回の合計と総合計を押された分だけ加算。
            TodayKeys.Text = _todaynum.ToString();
            AllDayKeys.Text = _alldaynum.ToString();
        }
        #endregion

        #region 初期化処理群
        /// <summary>
        /// DirectXデバイスの初期化
        /// </summary>
        public void DirectXInit()
        {
            // 入力周りの初期化
            DirectInput dinput = new DirectInput();
            if (dinput != null)
            {
                // 使用するゲームパッドのID
                var joystickGuid = Guid.Empty;

                if (PS2ConTips.Checked)
                {
                    // ゲームパッドからコントローラを取得する。(専コンや虹コンはこっち？)
                    if (joystickGuid == Guid.Empty)
                    {
                        foreach (DeviceInstance device in dinput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                        {
                            joystickGuid = device.InstanceGuid;
                            break;
                        }
                    }

                    // ジョイスティックからコントローラを取得する。
                    //if (joystickGuid == Guid.Empty)
                    //{
                    //    foreach (DeviceInstance device in dinput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    //    {
                    //        joystickGuid = device.InstanceGuid;
                    //        break;
                    //    }
                    //}
                }
                else if (DaoTips.Checked)
                {
                    // ジョイスティックからコントローラを取得する。(DAOコン)
                    if (joystickGuid == Guid.Empty)
                    {
                        foreach (DeviceInstance device in dinput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                        {
                            joystickGuid = device.InstanceGuid;
                            break;
                        }
                    }
                }
                else if (CustomTips.Checked)
                {
                    // ゲームパッドからコントローラを取得する。(専コンや虹コンはこっち？)
                    if (joystickGuid == Guid.Empty)
                    {
                        foreach (DeviceInstance device in dinput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                        {
                            joystickGuid = device.InstanceGuid;
                            break;
                        }
                    }

                    // ジョイスティックからコントローラを取得する。
                    if (joystickGuid == Guid.Empty)
                    {
                        foreach (DeviceInstance device in dinput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                        {
                            joystickGuid = device.InstanceGuid;
                            break;
                        }
                    }
                }

                // コントローラーが見つかった場合
                if (joystickGuid != Guid.Empty)
                {
                    // パッド入力周りの初期化
                    _joy = new Joystick(dinput, joystickGuid);
                    if (_joy != null)
                    {
                        // バッファサイズを指定
                        _joy.Properties.BufferSize = 128;

                        // 相対軸・絶対軸の最小値と最大値を
                        // 指定した値の範囲に設定する
                        foreach (DeviceObjectInstance deviceObject in _joy.GetObjects())
                        {
                            switch (deviceObject.ObjectId.Flags)
                            {
                                case DeviceObjectTypeFlags.Axis:
                                // 絶対軸or相対軸
                                case DeviceObjectTypeFlags.AbsoluteAxis:
                                // 絶対軸
                                case DeviceObjectTypeFlags.RelativeAxis:
                                    // 相対軸
                                    var ir = _joy.GetObjectPropertiesById(deviceObject.ObjectId);
                                    if (ir != null)
                                    {
                                        try
                                        {
                                            ir.Range = new InputRange(_rangeMin, _rangeMax);
                                        }
                                        catch (Exception) { }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 今回分の初期化処理 (起動時の変数を初期化する処理でもある)
        /// </summary>
        public void TodayInit(bool swt)
        {
            int InitInt = 0;
            T_S_Up.Text = InitInt.ToString();
            T_S_Down.Text = InitInt.ToString();
            T_Key1.Text = InitInt.ToString();
            T_Key2.Text = InitInt.ToString();
            T_Key3.Text = InitInt.ToString();
            T_Key4.Text = InitInt.ToString();
            T_Key5.Text = InitInt.ToString();
            T_Key6.Text = InitInt.ToString();
            T_Key7.Text = InitInt.ToString();
            T_TodayKeys.Text = InitInt.ToString();

            S_Up.Text = InitInt.ToString();
            S_Down.Text = InitInt.ToString();
            Key1.Text = InitInt.ToString();
            Key2.Text = InitInt.ToString();
            Key3.Text = InitInt.ToString();
            Key4.Text = InitInt.ToString();
            Key5.Text = InitInt.ToString();
            Key6.Text = InitInt.ToString();
            Key7.Text = InitInt.ToString();
            TodayKeys.Text = InitInt.ToString();

            _s_upnum = InitInt;
            _s_downnum = InitInt;
            _key1num = InitInt;
            _key2num = InitInt;
            _key3num = InitInt;
            _key4num = InitInt;
            _key5num = InitInt;
            _key6num = InitInt;
            _key7num = InitInt;
            _todaynum = InitInt;

            if (swt)
            {
                // 使用されているカウンタに応じてTotalを初期化。
                if (Total1_Load.Checked)
                {
                    var num = _xml.XPathSelectElement("//Counter1").Value;
                    T_AllDayKeys.Text = num;
                    AllDayKeys.Text = num;
                    _alldaynum = long.Parse(num);
                }
                else if (Total2_Load.Checked)
                {
                    var num = _xml.XPathSelectElement("//Counter2").Value;
                    T_AllDayKeys.Text = num;
                    AllDayKeys.Text = num;
                    _alldaynum = long.Parse(num);
                }
                else if (Total3_Load.Checked)
                {
                    var num = _xml.XPathSelectElement("//Counter3").Value;
                    T_AllDayKeys.Text = num;
                    AllDayKeys.Text = num;
                    _alldaynum = long.Parse(num);
                }
            }
        }


        /// <summary>
        /// 総合計の初期化処理
        /// </summary>
        public void AlldayInit()
        {
            int InitInt = 0;

            TodayInit(false);

            T_AllDayKeys.Text = InitInt.ToString();
            AllDayKeys.Text = InitInt.ToString();
            _alldaynum = InitInt;
        }

        /// <summary>
        /// 設定の読み込み
        /// </summary>
        public void SettingInit()
        {
            _cls.BackColor = int.Parse(_xml.XPathSelectElement("//BackColor").Value);
            _cls.Controller = int.Parse(_xml.XPathSelectElement("//Controller").Value);
            _cls.Counter1 = int.Parse(_xml.XPathSelectElement("//Counter1").Value);
            _cls.Counter2 = int.Parse(_xml.XPathSelectElement("//Counter2").Value);
            _cls.Counter3 = int.Parse(_xml.XPathSelectElement("//Counter3").Value);
            _cls.Kando = ulong.Parse(_xml.XPathSelectElement("//Kando").Value);
            _cls.PlayMode = int.Parse(_xml.XPathSelectElement("//PlayMode").Value);
            _cls.PlaySide = int.Parse(_xml.XPathSelectElement("//PlaySide").Value);
            _cls.SaveCounter = int.Parse(_xml.XPathSelectElement("//SaveCounter").Value);

            // KeyBoard用の変数
            _cls.K_Key1 = _xml.XPathSelectElement("//K_Key1").Value;
            _cls.K_Key2 = _xml.XPathSelectElement("//K_Key2").Value;
            _cls.K_Key3 = _xml.XPathSelectElement("//K_Key3").Value;
            _cls.K_Key4 = _xml.XPathSelectElement("//K_Key4").Value;
            _cls.K_Key5 = _xml.XPathSelectElement("//K_Key5").Value;
            _cls.K_Key6 = _xml.XPathSelectElement("//K_Key6").Value;
            _cls.K_Key7 = _xml.XPathSelectElement("//K_Key7").Value;
            _cls.K_S_Up = _xml.XPathSelectElement("//K_S_Up").Value;
            _cls.K_S_Down = _xml.XPathSelectElement("//K_S_Down").Value;

            // GamePad用の変数
            _cls.C_Key1 = int.Parse(_xml.XPathSelectElement("//C_Key1").Value);
            _cls.C_Key2 = int.Parse(_xml.XPathSelectElement("//C_Key2").Value);
            _cls.C_Key3 = int.Parse(_xml.XPathSelectElement("//C_Key3").Value);
            _cls.C_Key4 = int.Parse(_xml.XPathSelectElement("//C_Key4").Value);
            _cls.C_Key5 = int.Parse(_xml.XPathSelectElement("//C_Key5").Value);
            _cls.C_Key6 = int.Parse(_xml.XPathSelectElement("//C_Key6").Value);
            _cls.C_Key7 = int.Parse(_xml.XPathSelectElement("//C_Key7").Value);
            _cls.C_S_Up = int.Parse(_xml.XPathSelectElement("//C_S_Up").Value);
            _cls.C_S_Down = int.Parse(_xml.XPathSelectElement("//C_S_Down").Value);

            _list = new List<string>()
            {
                _cls.K_Key1,
                _cls.K_Key2,
                _cls.K_Key3,
                _cls.K_Key4,
                _cls.K_Key5,
                _cls.K_Key6,
                _cls.K_Key7,
                _cls.K_S_Up,
                _cls.K_S_Down
            };

            // 初回読み込み用の処理。
            if (_onceAction3 == false)
            {
                _onceAction3 = true;

                // 前回終了時のモードを初期選択にする。
                if (_cls.PlayMode == 0)
                {
                    InfinitasPlayTips_Click(new object(), new EventArgs());
                }
                else if (_cls.PlayMode == 1)
                {
                    BmsPlayTips_Click(new object(), new EventArgs());
                }

                // 前回終了時のコントローラを初期選択にする。
                if (_cls.Controller == 0)
                {
                    DaoTips_Click(new object(), new EventArgs());
                }
                else if (_cls.Controller == 1)
                {
                    PS2ConTips_Click(new object(), new EventArgs());
                }
                else if (_cls.Controller == 2)
                {
                    BeatmaniaProConTips_Click(new object(), new EventArgs());
                }
                else if (_cls.Controller == 3)
                {
                    CustomTips_Click(new object(), new EventArgs());
                }
                else if (_cls.Controller == 4)
                {
                    KeyBoardTips_Click(new object(), new EventArgs());
                }

                // 全期間の合計値を保存する。
                if (_cls.BackColor == 0)
                {
                    WhiteTips_Click(new object(), new EventArgs());
                }
                else if (_cls.BackColor == 1)
                {
                    BlackTips_Click(new object(), new EventArgs());
                }
                else
                {
                    ClearWTips_Click(new object(), new EventArgs());
                }

                // カウンタの切り替え
                if (_cls.SaveCounter == 1)
                {
                    Total1_Load_Click(new object(), new EventArgs());
                }
                else if (_cls.SaveCounter == 2)
                {
                    Total2_Load_Click(new object(), new EventArgs());
                }
                else if (_cls.SaveCounter == 3)
                {
                    Total3_Load_Click(new object(), new EventArgs());
                }

                // 前回終了時のプレイサイドを初期選択にする。
                if (_cls.PlaySide == 1)
                {
                    LeftSideTips_Click(new object(), new EventArgs());
                }
                else if (_cls.PlaySide == 2)
                {
                    RightSideTips_Click(new object(), new EventArgs());
                }
            }
        }

        /// <summary>
        /// 背景色の変更時の共通処理
        /// </summary>
        public void ChangeColorInit(Color keyBackColor)
        {
            var backcolor = int.Parse(_xml.XPathSelectElement("//BackColor").Value);


            if (backcolor == 0)
            {
                this.TransparencyKey = new Color();
                this.BackColor = SystemColors.Control;
                menuStrip1.BackColor = SystemColors.Control;

                L_TodayKeys.ForeColor = SystemColors.ControlText;
                L_AlldayKeys.ForeColor = SystemColors.ControlText;
                L_S_Up.ForeColor = SystemColors.ControlText;
                L_S_Down.ForeColor = SystemColors.ControlText;
            }
            else if (backcolor == 1)
            {
                this.TransparencyKey = new Color();
                this.BackColor = Color.FromArgb(64, 64, 64);
                menuStrip1.BackColor = SystemColors.ControlDark;

                L_TodayKeys.ForeColor = SystemColors.Control;
                L_AlldayKeys.ForeColor = SystemColors.Control;
                L_S_Up.ForeColor = SystemColors.Control;
                L_S_Down.ForeColor = SystemColors.Control;

            }
            else
            {
                this.TransparencyKey = SystemColors.ControlDark;
                this.BackColor = SystemColors.ControlDark;
                menuStrip1.BackColor = SystemColors.Control;

                L_TodayKeys.ForeColor = SystemColors.ControlDark;
                L_AlldayKeys.ForeColor = SystemColors.ControlDark;
                L_S_Up.ForeColor = SystemColors.ControlDark;
                L_S_Down.ForeColor = SystemColors.ControlDark;
            }

            Key1.BackColor = keyBackColor;
            Key2.BackColor = keyBackColor;
            Key3.BackColor = keyBackColor;
            Key4.BackColor = keyBackColor;
            Key5.BackColor = keyBackColor;
            Key6.BackColor = keyBackColor;
            Key7.BackColor = keyBackColor;
            S_Up.BackColor = keyBackColor;
            S_Down.BackColor = keyBackColor;
            TodayKeys.BackColor = keyBackColor;
            AllDayKeys.BackColor = keyBackColor;
            _keyBackColor = keyBackColor;
        }
        #endregion

        #region 皿の判定処理
        /// <summary>
        /// 皿の処理がアナログ時の処理(皿入力が回した分だけ加減算される、ON/OFFではないとき)
        /// </summary>
        public void AnalogSCMode(JoystickState jState)
        {
            if (DaoTips.Checked)
            {
                // 現在のコントローラがDAOの場合の処理
                if (_s_rel_old_x != _s_rel_now_x)
                {
                    bool nowRight = false;
                    if (_s_rel_old_x < _s_rel_now_x)
                    {
                        nowRight = true;
                        if ((_s_rel_now_x - _s_rel_old_x) > (1000 - _s_rel_now_x + _s_rel_old_x))
                        {
                            nowRight = false;
                        }
                    }
                    else if (_s_rel_old_x > _s_rel_now_x)
                    {

                        nowRight = false;
                        if ((_s_rel_old_x - _s_rel_now_x) > ((_s_rel_now_x + 1000) - _s_rel_old_x))
                        {
                            nowRight = true;
                        }
                    }

                    if (_isActive && !(_isRight == nowRight))
                    {
                        // 皿を逆回転させた時の処理。
                        if (_isRight)
                        {
                            Console.WriteLine("1");
                            TurnTable_PlaySideCheck(1);
                        }
                        else
                        {
                            Console.WriteLine("2");
                            TurnTable_PlaySideCheck(2);
                        }

                        _isRight = nowRight;
                        Console.WriteLine("Change");

                    }
                    else if (!_isActive)
                    {
                        // 皿を回していない状態から回し始めた時の処理。
                        if (nowRight)
                        {
                            Console.WriteLine("3");
                            TurnTable_PlaySideCheck(2);
                        }
                        else
                        {
                            Console.WriteLine("4");
                            TurnTable_PlaySideCheck(1);
                        }

                        _isActive = true;

                        _isRight = nowRight;
                    }

                    // カウンタ, 位置の初期化
                    _counter = 0;
                    _s_rel_old_x = _s_rel_now_x;
                }

                // スクラッチを回した時にもう回していない扱いになるかの判定。デフォルト：5000。
                if (_counter > _cls.Kando && _isActive)
                {
                    _isActive = false;
                    _counter = 0;
                    S_Down.BackColor = _keyBackColor;
                    S_Up.BackColor = _keyBackColor;
                }

                if (_counter == ulong.MaxValue)
                {
                    _counter = 0;
                }

                _counter++;
            }
            else if (PS2ConTips.Checked)
            {
                // 現在のコントローラが専コンの場合の処理
                PS2ConSCCheck();
            }
            else if (BeatmaniaProConTips.Checked)
            {
                // 現在のコントローラがプロコンの場合の処理
                ProConSCCheck();
            }
            else if (CustomTips.Checked)
            {
                CustomSCCheck(jState);
            }
        }

        /// <summary>
        /// 皿の処理がデジタル時の処理(皿入力がON/OFF入力の時)
        /// </summary>
        public void DigitalSCMode(JoystickState jState)
        {
            if (DaoTips.Checked)
            {
                // 皿が動作しているか。皿が同じ方向に回り続けている場合はカウントしない。
                if (_s_rel_now_x != _s_rel_old_x)
                {
                    // 現在軸の位置がMAX値の場合。
                    if (_s_rel_now_x == _rangeMax)
                    {
                        Debug.Print("入力キー：↑");
                        TurnTable_PlaySideCheck(2);
                    }
                    // 現在軸の位置がMIN値の場合。
                    else if (_s_rel_now_x == _rangeMin)
                    {
                        Debug.Print("入力キー：↓");
                        TurnTable_PlaySideCheck(1);
                    }
                }
                // 中央に軸が存在する場合(動いていない場合)
                else if (_s_rel_now_x == _rangeMax / 2)
                {
                    S_Up.BackColor = _keyBackColor;
                    S_Down.BackColor = _keyBackColor;
                }
            }
            else if (PS2ConTips.Checked)
            {
                PS2ConSCCheck();
            }
            else if (BeatmaniaProConTips.Checked)
            {
                // 現在のコントローラがプロコンの場合の処理
                ProConSCCheck();
            }
            else if (CustomTips.Checked)
            {
                CustomSCCheck(jState);
            }
        }

        /// <summary>
        /// PS2専用コントローラ時の皿入力処理
        /// </summary>
        public void PS2ConSCCheck()
        {
            // 皿が動作しているか。皿が同じ方向に回り続けている場合はカウントしない。
            if (_s_rel_now_y != _s_rel_old_y)
            {
                // 現在軸の位置がMIN値の場合。
                if (_s_rel_now_y == _rangeMax)
                {
                    Debug.Print("入力キー：↑");
                    TurnTable_PlaySideCheck(2);
                }
                // 現在軸の位置がMAX値の場合。
                else if (_s_rel_now_y == _rangeMin)
                {
                    Debug.Print("入力キー：↓");
                    TurnTable_PlaySideCheck(1);
                }
            }
            // 中央に軸が存在する場合(動いていない場合)
            else if (_s_rel_now_y != _rangeMax && _s_rel_now_y != _rangeMin)
            {
                S_Up.BackColor = _keyBackColor;
                S_Down.BackColor = _keyBackColor;
            }
        }

        /// <summary>
        /// INFINITAS用プロフェッショナルコントローラ時の皿入力処理(未作成)
        /// </summary>
        public void ProConSCCheck()
        {

        }

        /// <summary>
        /// カスタム設定時の皿入力処理
        /// </summary>
        public void CustomSCCheck(JoystickState jState)
        {
            // S_UP 判定
            if (_cls.C_S_Up < 20)
            {
                if (jState.Buttons[_cls.C_S_Up] && _joyUp == false)
                {
                    PushUpKey(true);
                }
                else
                {
                    if (jState.Buttons[_cls.C_S_Up] == false)
                    {
                        PushUpKey(false);
                    }
                }
            }
            else if (_cls.C_S_Up >= 50 && _cls.C_S_Up < 80)
            {
                // 軸入力の場合の処理
                if (_cls.C_S_Up == 50)
                {
                    if (_s_rel_old_x != _s_rel_now_x)
                    {
                        // 左の場合
                        if (_s_rel_now_x == _rangeMin && _joyUp == false)
                        {
                            PushUpKey(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != _rangeMin)
                            {
                                PushUpKey(false);
                            }
                        }
                    }
                }
                // 十字キー入力「右」の場合。
                else if (_cls.C_S_Up == 51)
                {
                    if (_s_rel_old_x != _s_rel_now_x)
                    {
                        // 右の場合
                        if (_s_rel_now_x == _rangeMax && _joyUp == false)
                        {
                            PushUpKey(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != _rangeMax)
                            {
                                PushUpKey(false);
                            }
                        }
                    }
                }
                // 十字キー入力「上」の場合。
                else if (_cls.C_S_Up == 52)
                {
                    if (_s_rel_old_y != _s_rel_now_y)
                    {
                        // 上の場合
                        if (_s_rel_now_y == _rangeMin && _joyUp == false)
                        {
                            PushUpKey(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != _rangeMin)
                            {
                                PushUpKey(false);
                            }
                        }
                    }
                }
                // 十字キー入力「下」の場合。
                else if (_cls.C_S_Up == 53)
                {
                    if (_s_rel_old_y != _s_rel_now_y)
                    {
                        // 左の場合
                        if (_s_rel_now_y == _rangeMax && _joyUp == false)
                        {
                            PushUpKey(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != _rangeMax)
                            {
                                PushUpKey(false);
                            }
                        }
                    }
                }
            }

            // S_DOWN 判定
            if (_cls.C_S_Down < 20)
            {
                if (jState.Buttons[_cls.C_S_Down] && _joyDown == false)
                {
                    PushDownKey(true);
                }
                else
                {
                    if (jState.Buttons[_cls.C_S_Down] == false)
                    {
                        PushDownKey(false);
                    }
                }
            }
            else if (_cls.C_S_Down >= 50 && _cls.C_S_Down < 80)
            //else if (_cls.C_S_Down >= 50 && _cls.C_S_Up < 80)
            {
                // 軸入力の場合の処理
                if (_cls.C_S_Down == 50)
                {
                    if (_s_rel_old_x != _s_rel_now_x)
                    {
                        // 左の場合
                        if (_s_rel_now_x == _rangeMin && _joyDown == false)
                        {
                            PushDownKey(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != _rangeMin)
                            {
                                PushDownKey(false);
                            }
                        }
                    }
                }
                // 十字キー入力「右」の場合。
                else if (_cls.C_S_Down == 51)
                {
                    if (_s_rel_old_x != _s_rel_now_x)
                    {
                        // 右の場合
                        if (_s_rel_now_x == _rangeMax && _joyDown == false)
                        {
                            PushDownKey(true);
                        }
                        else
                        {
                            if (_s_rel_now_x != _rangeMax)
                            {
                                PushDownKey(false);
                            }
                        }
                    }
                }
                // 十字キー入力「上」の場合。
                else if (_cls.C_S_Down == 52)
                {
                    if (_s_rel_old_y != _s_rel_now_y)
                    {
                        // 上の場合
                        if (_s_rel_now_y == _rangeMin && _joyDown == false)
                        {
                            PushDownKey(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != _rangeMin)
                            {
                                PushDownKey(false);
                            }
                        }
                    }
                }
                // 十字キー入力「下」の場合。
                else if (_cls.C_S_Down == 53)
                {
                    if (_s_rel_old_y != _s_rel_now_y)
                    {
                        // 下の場合
                        if (_s_rel_now_y == _rangeMax && _joyDown == false)
                        {
                            PushDownKey(true);
                        }
                        else
                        {
                            if (_s_rel_now_y != _rangeMax)
                            {
                                PushDownKey(false);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Actions
        /// <summary>
        /// アプリケーションロード時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeatCounter_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// INFINITASモードを選択した場合。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfinitasPlayTips_Click(object sender, EventArgs e)
        {
            AnalogSCTips.Checked = true;
            DigitalSCTips.Checked = false;

            // 変数に格納したXML情報にある要素の取得処理
            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "PlayMode");
            // 取得した要素に新しい値をセットする。
            element.SetValue(0);

            DirectXInit();
        }

        /// <summary>
        /// BMSモードを選択した場合。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BmsPlayTips_Click(object sender, EventArgs e)
        {
            AnalogSCTips.Checked = false;
            DigitalSCTips.Checked = true;

            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "PlayMode");
            element.SetValue(1);

            DirectXInit();
        }

        /// <summary>
        /// コントローラがDAOの時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoTips_Click(object sender, EventArgs e)
        {
            // 指定されたコントローラ以外のチェックを外す。
            DaoTips.Checked = true;
            PS2ConTips.Checked = false;
            BeatmaniaProConTips.Checked = false;
            CustomTips.Checked = false;
            KeyBoardTips.Checked = false;

            // BMSのみチェックを入れる。
            DigitalSCTips.Enabled = true;
            AnalogSCTips.Enabled = true;

            var playmode = int.Parse(_xml.XPathSelectElement("//PlayMode").Value);

            if (playmode == 0)
            {
                AnalogSCTips.Checked = true;
                DigitalSCTips.Checked = false;
            }
            else
            {
                AnalogSCTips.Checked = false;
                DigitalSCTips.Checked = true;
            }

            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Controller");
            element.SetValue(0);

            DirectXInit();
        }

        /// <summary>
        /// コントローラがPS2専コンの時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PS2ConTips_Click(object sender, EventArgs e)
        {
            // 指定されたコントローラ以外のチェックを外す。
            DaoTips.Checked = false;
            PS2ConTips.Checked = true;
            BeatmaniaProConTips.Checked = false;
            CustomTips.Checked = false;
            KeyBoardTips.Checked = false;

            // BMSのみチェックを入れる。
            DigitalSCTips.Enabled = false;
            AnalogSCTips.Enabled = false;
            DigitalSCTips.Checked = true;
            AnalogSCTips.Checked = false;

            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Controller");
            element.SetValue(1);

            DirectXInit();
        }

        /// <summary>
        /// コントローラがプロコンの時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeatmaniaProConTips_Click(object sender, EventArgs e)
        {
            // 指定されたコントローラ以外のチェックを外す。
            DaoTips.Checked = false;
            PS2ConTips.Checked = false;
            BeatmaniaProConTips.Checked = true;
            CustomTips.Checked = false;
            KeyBoardTips.Checked = false;

            // INFINITASモードのみチェックを付ける。
            DigitalSCTips.Enabled = false;
            DigitalSCTips.Checked = false;
            AnalogSCTips.Enabled = false;
            AnalogSCTips.Checked = true;

            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Controller");
            element.SetValue(2);

            DirectXInit();
        }

        /// <summary>
        /// コントローラがカスタムの時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomTips_Click(object sender, EventArgs e)
        {
            // 指定されたコントローラ以外のチェックを外す。
            DaoTips.Checked = false;
            PS2ConTips.Checked = false;
            BeatmaniaProConTips.Checked = false;
            CustomTips.Checked = true;
            KeyBoardTips.Checked = false;

            // BMSモードのみチェックとEnable属性を付ける。
            DigitalSCTips.Enabled = false;
            DigitalSCTips.Checked = true;
            AnalogSCTips.Enabled = false;
            AnalogSCTips.Checked = false;

            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Controller");
            element.SetValue(3);

            KeyConfig keyconf = new KeyConfig(_xml, _cls);
            DialogResult dr = keyconf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                _xml = keyconf._keyConfxml;
            }
            SettingInit();

            DirectXInit();
        }

        /// <summary>
        /// コントローラではなくキーボードの時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyBoardTips_Click(object sender, EventArgs e)
        {
            // 指定されたコントローラ以外のチェックを外す。
            DaoTips.Checked = false;
            PS2ConTips.Checked = false;
            BeatmaniaProConTips.Checked = false;
            CustomTips.Checked = false;
            KeyBoardTips.Checked = true;

            // BMSモードのみチェックとEnable属性を付ける。
            DigitalSCTips.Enabled = false;
            DigitalSCTips.Checked = true;
            AnalogSCTips.Enabled = false;
            AnalogSCTips.Checked = false;

            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Controller");
            element.SetValue(4);

            KeyBoardConfig keyconf = new KeyBoardConfig(_xml, _cls);
            DialogResult dr = keyconf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                _xml = keyconf._keyboardConfxml;
            }
            SettingInit();

            this.KeyPreview = true;
        }

        /// <summary>
        /// 皿感度の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SChangeTips_Click(object sender, EventArgs e)
        {
            Config conf = new Config(_xml, _cls);
            DialogResult dr = conf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                _xml = conf._confxml;
            }
            SettingInit();
        }

        /// <summary>
        /// カウンタの変更を押下した時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountChangeTips_Click(object sender, EventArgs e)
        {
            // 変数の初期化
            List<TextBox> T_Keys = new List<TextBox>() {
                T_Key1, T_Key2, T_Key3,
                T_Key4, T_Key5, T_Key6,
                T_Key7, T_S_Up, T_S_Down,
                T_TodayKeys, T_AllDayKeys
            };
            List<Label> Keys = new List<Label>() {
                Key1, Key2, Key3,
                Key4, Key5, Key6,
                Key7, S_Up, S_Down,
                TodayKeys, AllDayKeys
            };
            int count = 0;

            // チェック状態の切り替え
            CountChangeTips.Checked = !CountChangeTips.Checked;

            // カウンタの変更にチェックが入った場合。
            if (CountChangeTips.Checked)
            {
                // 数値変更モードの切り替え(ボタンの入力チェック処理を行わないようにする。)
                _keyChangeMode = !_keyChangeMode;

                // カウンタの変更以外の処理をUnenableにする。
                ControllerTips.Enabled = false;
                SChangeTips.Enabled = false;
                BackColorTips.Enabled = false;
                ResetTips.Enabled = false;
                TotalTips.Enabled = false;

                // Labelを画面から非表示にする。
                foreach (Label Lab in Keys)
                {
                    Lab.Visible = false;
                }

                foreach (TextBox Key in T_Keys)
                {
                    // 現在値を渡す。
                    Key.Text = Keys[count].Text;

                    // TextBoxを画面に表示する。
                    Key.Visible = true;
                    // TextBoxを入力可能状態にする。
                    Key.ReadOnly = false;
                    count++;
                }
            }
            else
            {
                // チェックが外された場合。
                foreach (TextBox Key in T_Keys)
                {
                    // TextBoxに入力された値が数値であるかチェックする。
                    var check = long.TryParse(Key.Text, out long i);

                    // 数値以外が入力されている場合エラーを返す。
                    if (!check)
                    {
                        DialogResult dialog = MessageBox.Show(
                            "テキストボックスには数値を" +
                            "入力してください。",
                            "エラー",
                            MessageBoxButtons.OK);

                        // 編集モードのチェック状態を維持する。
                        CountChangeTips.Checked = true;
                        _keyChangeMode = true;

                        // 途中の場合でも全てのテキストボックスのプロパティを戻す為の処理。
                        foreach (TextBox key_2 in T_Keys)
                        {
                            Key.ReadOnly = false;
                            Key.Visible = true;
                        }

                        return;
                    }

                    // 同じ鍵盤, 皿, 合計の対応するLabelと変数にTextBoxの値を挿入する。
                    Key.Text = i.ToString();
                    switch (count)
                    {
                        case 0:
                            Key1.Text = i.ToString();
                            _key1num = i;
                            break;
                        case 1:
                            Key2.Text = i.ToString();
                            _key2num = i;
                            break;
                        case 2:
                            Key3.Text = i.ToString();
                            _key3num = i;
                            break;
                        case 3:
                            Key4.Text = i.ToString();
                            _key4num = i;
                            break;
                        case 4:
                            Key5.Text = i.ToString();
                            _key5num = i;
                            break;
                        case 5:
                            Key6.Text = i.ToString();
                            _key6num = i;
                            break;
                        case 6:
                            Key7.Text = i.ToString();
                            _key7num = i;
                            break;
                        case 7:
                            S_Up.Text = i.ToString();
                            _s_upnum = i;
                            break;
                        case 8:
                            S_Down.Text = i.ToString();
                            _s_downnum = i;
                            break;
                        case 9:
                            TodayKeys.Text = i.ToString();
                            _todaynum = i;
                            break;
                        case 10:
                            AllDayKeys.Text = i.ToString();
                            _alldaynum = i;
                            break;
                        default:
                            break;
                    }
                    count++;
                }

                // カウンタの変更以外の処理をUnenableにする。
                ControllerTips.Enabled = true;
                SChangeTips.Enabled = true;
                BackColorTips.Enabled = true;
                ResetTips.Enabled = true;
                TotalTips.Enabled = true;

                // 各LabelのVisibleをTrueにする。
                foreach (Label Lab in Keys)
                {
                    Lab.Visible = true;
                }

                // 各TextBoxのVisibleをFalseにする。(＋ReadOnly状態にする)
                foreach (TextBox Key in T_Keys)
                {
                    Key.Visible = false;
                    Key.ReadOnly = true;
                }

                // 数値変更モードの切り替え(ボタンの入力チェック処理を行うようにする。)
                _keyChangeMode = false;
            }
        }

        /// <summary>
        /// 背景色が白の場合。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WhiteTips_Click(object sender, EventArgs e)
        {
            if (WhiteTips.Checked)
            {
                return;
            }

            WhiteTips.Checked = !WhiteTips.Checked;
            BlackTips.Checked = false;
            ClearWTips.Checked = false;

            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "BackColor");
            element.SetValue(0);


            ChangeColorInit(SystemColors.Info);
        }

        /// <summary>
        /// 背景色が黒の場合。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlackTips_Click(object sender, EventArgs e)
        {
            if (BlackTips.Checked)
            {
                return;
            }

            WhiteTips.Checked = false;
            BlackTips.Checked = !BlackTips.Checked;
            ClearWTips.Checked = false;

            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id").Value == "BackColor");
            element.SetValue(1);

            ChangeColorInit(SystemColors.Info);
        }

        /// <summary>
        /// 背景色が透明の場合。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearWTips_Click(object sender, EventArgs e)
        {
            if (ClearWTips.Checked)
            {
                return;
            }

            WhiteTips.Checked = false;
            BlackTips.Checked = false;
            ClearWTips.Checked = !ClearWTips.Checked;

            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id").Value == "BackColor");
            element.SetValue(2);

            ChangeColorInit(Color.LightSteelBlue);
        }

        /// <summary>
        /// TODAYの削除を押下した時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TodayClearTips_Click(object sender, EventArgs e)
        {
            var cls = new BeatCounterCommon();
            var res = cls.DialogYesNo("今回の回数を消去します。", "よろしいですか？", "今回の回数の消去");

            if (res == DialogResult.Yes)
            {
                TodayInit(true);
            }
        }

        /// <summary>
        /// TOTALを削除を押下した時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlldayClearTips_Click(object sender, EventArgs e)
        {
            var cls = new BeatCounterCommon();
            var res = cls.DialogYesNo("全ての回数を消去します。", "よろしいですか？", "全期間合計の消去");
            if (res == DialogResult.Yes)
            {
                AlldayInit();
            }
        }

        /// <summary>
        /// Shift＋Deleteキーを押下した時、警告無しに今日のカウントを初期化する処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TodayShortTips_Click(object sender, EventArgs e)
        {
            TodayInit(true);
        }

        /// <summary>
        /// アプリケーションを閉じる時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeatCounter_Close(object sender, FormClosingEventArgs e)
        {
            if (Total1_Load.Checked)
            {

                // 全期間の合計値を保存する。
                var element1 = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Counter1");
                element1.SetValue(_alldaynum);

                // カウンタの番号を保存して、次回起動時に初期設定されるようにする。
                var element2 = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "SaveCounter");
                element2.SetValue(1);
            }
            else if (Total2_Load.Checked)
            {
                var element1 = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Counter2");
                element1.SetValue(_alldaynum);

                var element2 = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "SaveCounter");
                element2.SetValue(2);
            }
            else if (Total3_Load.Checked)
            {
                var element1 = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Counter3");
                element1.SetValue(_alldaynum);

                var element2 = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "SaveCounter");
                element2.SetValue(3);
            }

            _xml.Save("Config.xml");

            Application.Exit();
        }

        public void GetStateOfKeyLocked(System.Windows.Forms.Keys Key_Value)
        {
            // WindowsAPIで押下判定
            bool Key_State = (GetAsyncKeyState((int)Key_Value) & 0x8000) != 0;

            if (Key_State == true && Key_Value.ToString() == _list[0] && _joy1b == false)
            {
                Push1Key(true);
            }
            else if (Key_State == false && Key_Value.ToString() == _list[0])
            {
                Push1Key(false);
            }

            if (Key_State == true && Key_Value.ToString() == _list[1] && _joy2b == false)
            {
                Push2Key(true);
            }
            else if (Key_State == false && Key_Value.ToString() == _list[1])
            {
                Push2Key(false);
            }

            if (Key_State == true && Key_Value.ToString() == _list[2] && _joy3b == false)
            {
                Push3Key(true);
            }
            else if (Key_State == false && Key_Value.ToString() == _list[2])
            {
                Push3Key(false);
            }

            if (Key_State == true && Key_Value.ToString() == _list[3] && _joy4b == false)
            {
                Push4Key(true);
            }
            else if (Key_State == false && Key_Value.ToString() == _list[3])
            {
                Push4Key(false);
            }

            if (Key_State == true && Key_Value.ToString() == _list[4] && _joy5b == false)
            {
                Push5Key(true);
            }
            else if (Key_State == false && Key_Value.ToString() == _list[4])
            {
                Push5Key(false);
            }

            if (Key_State == true && Key_Value.ToString() == _list[5] && _joy6b == false)
            {
                Push6Key(true);
            }
            else if (Key_State == false && Key_Value.ToString() == _list[5])
            {
                Push6Key(false);
            }

            if (Key_State == true && Key_Value.ToString() == _list[6] && _joy7b == false)
            {
                Push7Key(true);
            }
            else if (Key_State == false && Key_Value.ToString() == _list[6])
            {
                Push7Key(false);
            }

            if (Key_State == true && Key_Value.ToString() == _list[7] && _joyUp == false)
            {
                PushUpKey(true);
            }
            else if (Key_State == false && Key_Value.ToString() == _list[7])
            {
                PushUpKey(false);
            }

            if (Key_State == true && Key_Value.ToString() == _list[8] && _joyDown == false)
            {
                PushDownKey(true);
            }
            else if (Key_State == false && Key_Value.ToString() == _list[8])
            {
                PushDownKey(false);
            }
        }

        /// <summary>
        /// TOTALの保存1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Total1_Load_Click(object sender, EventArgs e)
        {
            if (Total1_Load.Checked)
            {
                return;
            }
            else if (Total2_Load.Checked)
            {
                var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Counter2");
                element.SetValue(_alldaynum);

                Total2_Load.Checked = false;
            }
            else if (Total3_Load.Checked)
            {
                var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Counter3");
                element.SetValue(_alldaynum);

                Total3_Load.Checked = false;
            }

            Total1_Load.Checked = true;
            _alldaynum = long.Parse(_xml.XPathSelectElement("//Counter1").Value);
            SettingInit();
        }

        /// <summary>
        /// TOTALの保存2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Total2_Load_Click(object sender, EventArgs e)
        {
            if (Total2_Load.Checked)
            {
                return;
            }
            else if (Total1_Load.Checked)
            {
                var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Counter1");
                element.SetValue(_alldaynum);

                Total1_Load.Checked = false;
            }
            else if (Total3_Load.Checked)
            {
                var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Counter3");
                element.SetValue(_alldaynum);

                Total3_Load.Checked = false;
            }

            Total2_Load.Checked = true;
            _alldaynum = long.Parse(_xml.XPathSelectElement("//Counter2").Value);

            SettingInit();
        }

        /// <summary>
        /// TOTALの保存3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Total3_Load_Click(object sender, EventArgs e)
        {
            if (Total3_Load.Checked)
            {
                return;
            }
            else if (Total1_Load.Checked)
            {
                var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Counter1");
                element.SetValue(_alldaynum);

                Total1_Load.Checked = false;
            }
            else if (Total2_Load.Checked)
            {
                var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Counter2");
                element.SetValue(_alldaynum);

                Total2_Load.Checked = false;
            }

            Total3_Load.Checked = true;
            _alldaynum = long.Parse(_xml.XPathSelectElement("//Counter3").Value);
            SettingInit();
        }

        #endregion

        #region Key情報
        public void Push1Key(bool chk)
        {
            if (chk)
            {
                Debug.Print("入力キー：1");
                _todaynum++;
                _alldaynum++;
                _key1num++;
                Key1.Text = _key1num.ToString();
                Key1.BackColor = Color.LightPink;
                _joy1b = true;
            }
            else
            {
                // 押しっぱなし判定を解除
                _joy1b = false;

                // 背景色を戻す。
                Key1.BackColor = _keyBackColor;
            }
        }
        public void Push2Key(bool chk)
        {
            if (chk)
            {
                Debug.Print("入力キー：2");
                _todaynum++;
                _alldaynum++;
                _key2num++;
                Key2.Text = _key2num.ToString();
                Key2.BackColor = Color.LightPink;
                _joy2b = true;
            }
            else
            {
                _joy2b = false;
                Key2.BackColor = _keyBackColor;
            }
        }
        public void Push3Key(bool chk)
        {
            if (chk)
            {
                Debug.Print("入力キー：3");
                _todaynum++;
                _alldaynum++;
                _key3num++;
                Key3.Text = _key3num.ToString();
                Key3.BackColor = Color.LightPink;
                _joy3b = true;
            }
            else
            {
                _joy3b = false;
                Key3.BackColor = _keyBackColor;
            }
        }
        public void Push4Key(bool chk)
        {
            if (chk)
            {
                Debug.Print("入力キー：4");
                _todaynum++;
                _alldaynum++;
                _key4num++;
                Key4.Text = _key4num.ToString();
                Key4.BackColor = Color.LightPink;
                _joy4b = true;
            }
            else
            {
                _joy4b = false;
                Key4.BackColor = _keyBackColor;
            }
        }
        public void Push5Key(bool chk)
        {
            if (chk)
            {
                Debug.Print("入力キー：5");
                _todaynum++;
                _alldaynum++;
                _key5num++;
                Key5.Text = _key5num.ToString();
                Key5.BackColor = Color.LightPink;
                _joy5b = true;
            }
            else
            {
                _joy5b = false;
                Key5.BackColor = _keyBackColor;
            }
        }
        public void Push6Key(bool chk)
        {
            if (chk)
            {
                Debug.Print("入力キー：6");
                _todaynum++;
                _alldaynum++;
                _key6num++;
                Key6.Text = _key6num.ToString();
                Key6.BackColor = Color.LightPink;
                _joy6b = true;
            }
            else
            {
                _joy6b = false;
                Key6.BackColor = _keyBackColor;
            }
        }
        public void Push7Key(bool chk)
        {
            if (chk)
            {
                Debug.Print("入力キー：7");
                _todaynum++;
                _alldaynum++;
                _key7num++;
                Key7.Text = _key7num.ToString();
                Key7.BackColor = Color.LightPink;
                _joy7b = true;
            }
            else
            {
                _joy7b = false;
                Key7.BackColor = _keyBackColor;
            }
        }
        public void PushUpKey(bool chk)
        {
            if (chk)
            {
                Debug.Print("入力キー：↑");
                _todaynum++;
                _alldaynum++;
                _s_upnum++;
                S_Up.Text = _s_upnum.ToString();
                S_Up.BackColor = Color.LightPink;
                _joyUp = true;
            }
            else
            {
                _joyUp = false;
                S_Up.BackColor = _keyBackColor;
            }
        }
        public void PushDownKey(bool chk)
        {
            if (chk)
            {
                Debug.Print("入力キー：↓");
                _todaynum++;
                _alldaynum++;
                _s_downnum++;
                S_Down.Text = _s_downnum.ToString();
                S_Down.BackColor = Color.LightPink;
                _joyDown = true;
            }
            else
            {
                _joyDown = false;
                S_Down.BackColor = _keyBackColor;
            }
        }
        #endregion

        /// <summary>
        /// コンフィグ「プレイサイド」で1Pを選択したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftSideTips_Click(object sender, EventArgs e)
        {
            if (LeftSideTips.Checked)
            {
                return;
            }
            if (RightSideTips.Checked)
            {
                RightSideTips.Checked = false;
                LeftSideTips.Checked = true;
            }

            // 表示板の位置を1Pサイドに適したものにする。
            L_S_Up.Left = 9;
            L_S_Down.Left = 9;
            S_Up.Left = 32;
            S_Down.Left = 32;
            Key1.Left = 133;
            Key2.Left = 182;
            Key3.Left = 229;
            Key4.Left = 278;
            Key5.Left = 325;
            Key6.Left = 374;
            Key7.Left = 421;

            // 編集モードも上記と同様。
            T_S_Up.Left = 32;
            T_S_Down.Left = 32;
            T_Key1.Left = 133;
            T_Key2.Left = 182;
            T_Key3.Left = 229;
            T_Key4.Left = 278;
            T_Key5.Left = 325;
            T_Key6.Left = 374;
            T_Key7.Left = 421;

            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id").Value == "PlaySide");
            element.SetValue(1);
        }

        /// <summary>
        /// コンフィグ「プレイサイド」で2Pを選択したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightSideTips_Click(object sender, EventArgs e)
        {
            if (RightSideTips.Checked)
            {
                return;
            }
            if (LeftSideTips.Checked)
            {
                LeftSideTips.Checked = false;
                RightSideTips.Checked = true;
            }

            // 表示板の位置を2Pサイドに適したものにする。
            L_S_Up.Left = 498;
            L_S_Down.Left = 498;
            S_Up.Left = 401;
            S_Down.Left = 401;
            Key1.Left = 12;
            Key2.Left = 61;
            Key3.Left = 108;
            Key4.Left = 157;
            Key5.Left = 204;
            Key6.Left = 257;
            Key7.Left = 300;

            // 編集モードも上記と同様。
            T_S_Up.Left = 401;
            T_S_Down.Left = 401;
            T_Key1.Left = 12;
            T_Key2.Left = 61;
            T_Key3.Left = 108;
            T_Key4.Left = 157;
            T_Key5.Left = 204;
            T_Key6.Left = 257;
            T_Key7.Left = 300;

            var element = _xml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "PlaySide");
            element.SetValue(2);
        }

        /// <summary>
        /// 皿を回した時にカウンタを計測するための処理
        /// </summary>
        /// <param name="slide">1:下方向, 2:上方向</param>
        public void TurnTable_PlaySideCheck(int slide)
        {
            // 下方向に回した。
            if (slide == 1)
            {
                // 1P側で下に回した。
                if (LeftSideTips.Checked == true)
                {
                    _todaynum++;
                    _alldaynum++;
                    _s_downnum++;
                    S_Down.Text = _s_downnum.ToString();
                    S_Down.BackColor = Color.LightPink;
                    S_Up.BackColor = _keyBackColor;
                }
                // 2P側で下に回した。 = 1P側で上に回した。
                else if (RightSideTips.Checked == true)
                {
                    _todaynum++;
                    _alldaynum++;
                    _s_upnum++;
                    S_Up.Text = _s_upnum.ToString();
                    S_Up.BackColor = Color.LightPink;
                    S_Down.BackColor = _keyBackColor;
                }
            }
            // 上方向に回した。
            else if (slide == 2)
            {
                // 1P側で上に回した。
                if (LeftSideTips.Checked == true)
                {
                    _todaynum++;
                    _alldaynum++;
                    _s_upnum++;
                    S_Up.Text = _s_upnum.ToString();
                    S_Up.BackColor = Color.LightPink;
                    S_Down.BackColor = _keyBackColor;

                }
                // 2P側で上に回した。 = 1P側で下に回した。
                else if (RightSideTips.Checked == true)
                {
                    _todaynum++;
                    _alldaynum++;
                    _s_downnum++;
                    S_Down.Text = _s_downnum.ToString();
                    S_Down.BackColor = Color.LightPink;
                    S_Up.BackColor = _keyBackColor;
                }
            }
        }
    }
}
