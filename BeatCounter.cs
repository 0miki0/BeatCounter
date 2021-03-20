using SharpDX.DirectInput;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BeatCounter
{
    public partial class BeatCounter : Form, IDisposable
    {
        private Joystick _joy;
        private int _s_upnum;
        private int _s_downnum;
        private int _s_rel_old;
        private int _s_rel_now;
        private int _key1num;
        private int _key2num;
        private int _key3num;
        private int _key4num;
        private int _key5num;
        private int _key6num;
        private int _key7num;
        private int _todaynum;
        //private int _alldaynum;

        private ulong counter = 1;

        // Is Scratching flag.
        private bool isActive = false;

        // is right Scratching.
        private bool isRight = false;



        private int _rangeMax = 1000;
        private int _rangeMin = 0;

        private bool joy1b = false;
        private bool joy2b = false;
        private bool joy3b = false;
        private bool joy4b = false;
        private bool joy5b = false;
        private bool joy6b = false;
        private bool joy7b = false;
        private bool onceAction1 = false;
        private bool onceAction2 = false;

        private Color KeyBackColor;

        public BeatCounter()
        {
            InitializeComponent();

            // 今日分の変数を初期化
            TodayInit();

            // 最大化を無効に
            MaximizeBox = false;
        }

        /// <summary>
        /// 毎フレーム処理
        /// </summary>
        public void Exec()
        {
            Initialize();

            // フォームの生成
            Show();
            // フォームが作成されている間は、ループし続ける
            while (Created)
            {
                MainLoop();

                // イベントがある場合は処理する
                Application.DoEvents();

                // CPUがフル稼働しないようにFPSの制限をかける
                // ※簡易的に、おおよそ秒間60フレーム程度に制限
                //Thread.Sleep(16);
            }
        }

        /// <summary>
        /// DirectXデバイスの初期化
        /// </summary>
        public void Initialize()
        {
            // 入力周りの初期化
            DirectInput dinput = new DirectInput();
            if (dinput != null)
            {
                // 使用するゲームパッドのID
                var joystickGuid = Guid.Empty;
                // ゲームパッドからゲームパッドを取得する
                //if (joystickGuid == Guid.Empty)
                //{
                //    foreach (DeviceInstance device in dinput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                //    {
                //        joystickGuid = device.InstanceGuid;
                //        break;
                //    }
                //}
                // ジョイスティックからゲームパッドを取得する
                if (joystickGuid == Guid.Empty)
                {
                    foreach (DeviceInstance device in dinput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    {
                        joystickGuid = device.InstanceGuid;
                        break;
                    }
                }
                // 見つかった場合
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
        /// メインループ処理
        /// </summary>
        public void MainLoop()
        {
            UpdateForPad();
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
            if (onceAction1 == false)
            {
                _s_rel_old = jState.X;
                _s_rel_now = jState.X;
                onceAction1 = true;
            }

            // 現在フレームと前フレームの比較でアナログ軸の処理を行うため、取得する。
            _s_rel_old = _s_rel_now;
            _s_rel_now = jState.X;

            // INFINITASプレイ時の処理
            if (InfinitasPlayTips.Checked == true)
            {
                if (_s_rel_old != _s_rel_now)
                {
                    bool nowRight = false;
                    if (_s_rel_old < _s_rel_now)
                    {
                        nowRight = true;
                        if ((_s_rel_now - _s_rel_old) > (1000 - _s_rel_now + _s_rel_old))
                        {
                            nowRight = false;
                        }
                    }
                    else if (_s_rel_old > _s_rel_now)
                    {

                        nowRight = false;
                        if ((_s_rel_old - _s_rel_now) > ((_s_rel_now + 1000) - _s_rel_old))
                        {
                            nowRight = true;
                        }
                    }

                    if (isActive && !(isRight == nowRight))
                    {
                        // 皿を逆回転させた時の処理。
                        if (isRight)
                        {
                            keybd_event(LEFT_SHIFT, 0, 2u, (UIntPtr)0uL);
                            keybd_event(LEFT_CTRL, 0, 0u, (UIntPtr)0uL);

                            Console.WriteLine("1");
                            _todaynum++;
                            _s_downnum++;
                            S_Down.Text = _s_downnum.ToString();
                            S_Down.BackColor = Color.LightPink;
                            S_Up.BackColor = KeyBackColor;
                        }
                        else
                        {
                            keybd_event(LEFT_CTRL, 0, 2u, (UIntPtr)0uL);
                            keybd_event(LEFT_SHIFT, 0, 0u, (UIntPtr)0uL);

                            Console.WriteLine("2");
                            _todaynum++;
                            _s_upnum++;
                            S_Up.Text = _s_upnum.ToString();
                            S_Up.BackColor = Color.LightPink;
                            S_Down.BackColor = KeyBackColor;
                        }

                        isRight = nowRight;
                        Console.WriteLine("Change");

                    }
                    else if (!isActive)
                    {
                        // 皿を回していない状態から回し始めた時の処理。
                        if (nowRight)
                        {
                            keybd_event(LEFT_SHIFT, 0, 0u, (UIntPtr)0uL);
                            Console.WriteLine("3");
                            _todaynum++;
                            _s_upnum++;
                            S_Up.Text = _s_upnum.ToString();
                            S_Up.BackColor = Color.LightPink;
                            S_Down.BackColor = KeyBackColor;
                        }
                        else
                        {
                            keybd_event(LEFT_CTRL, 0, 0u, (UIntPtr)0uL);
                            Console.WriteLine("4");
                            _todaynum++;
                            _s_downnum++;
                            S_Down.Text = _s_downnum.ToString();
                            S_Down.BackColor = Color.LightPink;
                            S_Up.BackColor = KeyBackColor;
                        }

                        isActive = true;

                        isRight = nowRight;
                    }

                    // カウンタ, 位置の初期化
                    counter = 0;
                    _s_rel_old = _s_rel_now;
                }

                // スクラッチを回した時にどれだけカウントされるかの判定。デフォルト：5000。
                if (counter > kando.Set_Kando && isActive)
                {
                    if (isRight)
                    {
                        keybd_event(LEFT_SHIFT, 0, 2u, (UIntPtr)0uL);
                    }
                    else
                    {
                        keybd_event(LEFT_CTRL, 0, 2u, (UIntPtr)0uL);
                    }

                    isActive = false;
                    counter = 0;
                    S_Down.BackColor = KeyBackColor;
                    S_Up.BackColor = KeyBackColor;
                }

                if (counter == ulong.MaxValue)
                {
                    counter = 0;
                }

                counter++;
            }
            // BMSプレイ時の処理
            else if (BmsPlayTips.Checked == true)
            {
                // 皿が動作しているか。皿が同じ方向に回り続けている場合はカウントしない。
                if (_s_rel_now != _s_rel_old)
                {
                    Debug.Print(jState.X.ToString());

                    // 現在軸の位置がMAX値の場合。
                    if (_s_rel_now == _rangeMax)
                    {
                        Debug.Print("入力キー：↑");
                        _todaynum++;
                        _s_upnum++;
                        S_Up.Text = _s_upnum.ToString();
                        S_Up.BackColor = Color.LightPink;
                        S_Down.BackColor = KeyBackColor;
                    }
                    // 現在軸の位置がMIN値の場合。
                    else if (_s_rel_now == _rangeMin)
                    {
                        Debug.Print("入力キー：↓");
                        _todaynum++;
                        _s_downnum++;
                        S_Down.Text = _s_downnum.ToString();
                        S_Down.BackColor = Color.LightPink;
                        S_Up.BackColor = KeyBackColor;
                    }
                }
                // 中央に軸が存在する場合(動いていない場合)
                else if (_s_rel_now == _rangeMax / 2)
                {
                    S_Up.BackColor = KeyBackColor;
                    S_Down.BackColor = KeyBackColor;
                }
            }

            #region Key判定
            // Key1 判定 (1鍵が押されたとき且つ、押しっぱなしになってない判定の場合)
            if (jState.Buttons[0] && joy1b == false)
            {
                Debug.Print("入力キー：1");
                // 今日分の叩いた数を加算する。
                _todaynum++;

                // 1鍵を叩いた数を加算する。
                _key1num++;

                // Formに表示する。
                Key1.Text = _key1num.ToString();

                // 押下された時、光らせる。
                Key1.BackColor = Color.LightPink;

                // 押下されっぱなしの時にカウントされないように判定を追加。
                joy1b = true;
            }
            else
            {
                if (jState.Buttons[0] == false)
                {
                    // 押しっぱなし判定を解除
                    joy1b = false;

                    // 背景色を戻す。
                    Key1.BackColor = KeyBackColor;
                }
            }

            // Key2 判定
            if (jState.Buttons[1] && joy2b == false)
            {
                Debug.Print("入力キー：2");
                _todaynum++;
                _key2num++;
                Key2.Text = _key2num.ToString();
                Key2.BackColor = Color.LightPink;
                joy2b = true;

            }
            else
            {
                if (jState.Buttons[1] == false)
                {
                    joy2b = false;
                    Key2.BackColor = KeyBackColor;
                }
            }

            // Key3 判定
            if (jState.Buttons[2] && joy3b == false)
            {
                Debug.Print("入力キー：3");
                _todaynum++;
                _key3num++;
                Key3.Text = _key3num.ToString();
                Key3.BackColor = Color.LightPink;
                joy3b = true;
            }
            else
            {
                if (jState.Buttons[2] == false)
                {
                    joy3b = false;
                    Key3.BackColor = KeyBackColor;
                }
            }

            // Key4 判定
            if (jState.Buttons[3] && joy4b == false)
            {
                Debug.Print("入力キー：4");
                _todaynum++;
                _key4num++;
                Key4.Text = _key4num.ToString();
                Key4.BackColor = Color.LightPink;
                joy4b = true;
            }
            else
            {
                if (jState.Buttons[3] == false)
                {
                    joy4b = false;
                    Key4.BackColor = KeyBackColor;
                }
            }

            // Key5 判定
            if (jState.Buttons[4] && joy5b == false)
            {
                Debug.Print("入力キー：5");
                _todaynum++;
                _key5num++;
                Key5.Text = _key5num.ToString();
                Key5.BackColor = Color.LightPink;
                joy5b = true;
            }
            else
            {
                if (jState.Buttons[4] == false)
                {
                    joy5b = false;
                    Key5.BackColor = KeyBackColor;
                }
            }

            // Key6 判定
            if (jState.Buttons[5] && joy6b == false)
            {
                Debug.Print("入力キー：6");
                _todaynum++;
                _key6num++;
                Key6.Text = _key6num.ToString();
                Key6.BackColor = Color.LightPink;
                joy6b = true;
            }
            else
            {
                if (jState.Buttons[5] == false)
                {
                    joy6b = false;
                    Key6.BackColor = KeyBackColor;
                }
            }

            // Key7 判定
            if (jState.Buttons[6] && joy7b == false)
            {
                Debug.Print("入力キー：7");
                _todaynum++;
                _key7num++;
                Key7.Text = _key7num.ToString();
                Key7.BackColor = Color.LightPink;
                joy7b = true;
            }
            else
            {
                if (jState.Buttons[6] == false)
                {
                    joy7b = false;
                    Key7.BackColor = KeyBackColor;
                }
            }
            #endregion

            TodayKeys.Text = _todaynum.ToString();

            if (onceAction2 == false)
            {
                S_Up.Text = "0";
                S_Down.Text = "0";
                onceAction2 = true;
            }
        }

        private void phoenixToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 閉じるToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void リセットToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void BeatCounter_Load(object sender, EventArgs e)
        {

        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }

        private void 今日の回数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(
                "今回の叩数を消去します。" +
                "よろしいですか？",
                "今回の叩数の消去",
                MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                TodayInit();
            }
        }

        private void 全期間回数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(
                "全期間の叩数を消去します。" +
                "よろしいですか？",
                "全期間の叩数の消去",
                MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                AlldayInit();
            }
        }

        private void InfinitasPlayTips_Click(object sender, EventArgs e)
        {
            InfinitasPlayTips.Checked = true;
            BmsPlayTips.Checked = false;
        }

        private void BmsPlayTips_Click(object sender, EventArgs e)
        {
            InfinitasPlayTips.Checked = false;
            BmsPlayTips.Checked = true;
        }

        private void 感度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config conf = new Config();
            conf.Show();
        }


        public void TodayInit()
        {
            int InitInt = 0;
            S_Up.Text = InitInt.ToString();
            S_Down.Text = InitInt.ToString();
            Key1.Text = InitInt.ToString();
            Key2.Text = InitInt.ToString();
            Key3.Text = InitInt.ToString();
            Key4.Text = InitInt.ToString();
            Key5.Text = InitInt.ToString();
            Key6.Text = InitInt.ToString();
            Key7.Text = InitInt.ToString();
            Key7.Text = InitInt.ToString();
            TodayKeys.Text = InitInt.ToString();
            KeyBackColor = Key1.BackColor;

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
        }

        public void AlldayInit()
        {
            int InitInt = 0;
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
        }

        [DllImport("user32.dll")]
        public static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private static byte LEFT_SHIFT = 0xA0;
        private static byte LEFT_CTRL = 0xA2;
    }
}
