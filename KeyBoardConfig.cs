using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace BeatCounter
{
    public partial class KeyBoardConfig : Form
    {
        private int checkNow = 0;

        // 親ウィンドウから渡されたXML情報を格納/リターンするための変数。
        public XDocument _keyboardConfxml;


        public KeyBoardConfig(XDocument xml, XMLClass cls)
        {
            //フォームの最大化ボタンの表示、非表示を切り替える
            this.MaximizeBox = !this.MaximizeBox;
            //フォームの最小化ボタンの表示、非表示を切り替える
            this.MinimizeBox = !this.MinimizeBox;
            //フォームのコントロールボックスの表示、非表示を切り替える
            //コントロールボックスを非表示にすると最大化、最小化、閉じるボタンも消える
            this.ControlBox = !this.ControlBox;

            try
            {
                //xmlファイルを指定する
                _keyboardConfxml = xml;
                if (_keyboardConfxml == null)
                {
                    throw new System.IO.DirectoryNotFoundException();
                }
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

            KeyInit();
        }

        public void KeyInit()
        {
            Label_Set.Text = "";
            KeyPreview = false;
            this.KeyDown += KeyBoardConfig_KeyDown;

            var up = _keyboardConfxml.XPathSelectElement("//K_S_Up").Value;
            var down = _keyboardConfxml.XPathSelectElement("//K_S_Down").Value;
            var k1 = _keyboardConfxml.XPathSelectElement("//K_Key1").Value;
            var k2 = _keyboardConfxml.XPathSelectElement("//K_Key2").Value;
            var k3 = _keyboardConfxml.XPathSelectElement("//K_Key3").Value;
            var k4 = _keyboardConfxml.XPathSelectElement("//K_Key4").Value;
            var k5 = _keyboardConfxml.XPathSelectElement("//K_Key5").Value;
            var k6 = _keyboardConfxml.XPathSelectElement("//K_Key6").Value;
            var k7 = _keyboardConfxml.XPathSelectElement("//K_Key7").Value;

            SC_UP_B.Text = up;
            SC_UP_B.Tag = up;
            SC_Down_B.Text = down;
            SC_Down_B.Tag = down;
            Key1_B.Text = k1;
            Key1_B.Tag = k1;
            Key2_B.Text = k2;
            Key2_B.Tag = k2;
            Key3_B.Text = k3;
            Key3_B.Tag = k3;
            Key4_B.Text = k4;
            Key4_B.Tag = k4;
            Key5_B.Text = k5;
            Key5_B.Tag = k5;
            Key6_B.Text = k6;
            Key6_B.Tag = k6;
            Key7_B.Text = k7;
            Key7_B.Tag = k7;
        }

        private void SC_UP_B_CheckedChanged(object sender, EventArgs e)
        {
            if (SC_UP_B.Checked)
            {
                SC_UP_B.Checked = true;
                SC_Down_B.Checked = false;
                Key1_B.Checked = false;
                Key2_B.Checked = false;
                Key3_B.Checked = false;
                Key4_B.Checked = false;
                Key5_B.Checked = false;
                Key6_B.Checked = false;
                Key7_B.Checked = false;

                this.KeyPreview = true;
                Label_Set.Text = "皿↑に設定するKeyを入力してください。";
                checkNow = 1;
            }
            else
            {
                this.KeyPreview = false;
                SC_UP_B.Checked = false;
                Label_Set.Text = "";
            }
        }

        private void SC_Down_B_CheckedChanged(object sender, EventArgs e)
        {
            if (SC_Down_B.Checked)
            {
                SC_UP_B.Checked = false;
                SC_Down_B.Checked = true;
                Key1_B.Checked = false;
                Key2_B.Checked = false;
                Key3_B.Checked = false;
                Key4_B.Checked = false;
                Key5_B.Checked = false;
                Key6_B.Checked = false;
                Key7_B.Checked = false;

                this.KeyPreview = true;
                Label_Set.Text = "皿↓に設定するKeyを入力してください。";
                checkNow = 2;
            }
            else
            {
                this.KeyPreview = false;
                SC_Down_B.Checked = false;
                Label_Set.Text = "";
            }
        }

        private void Key1_B_CheckedChanged(object sender, EventArgs e)
        {
            if (Key1_B.Checked)
            {
                SC_UP_B.Checked = false;
                SC_Down_B.Checked = false;
                Key1_B.Checked = true;
                Key2_B.Checked = false;
                Key3_B.Checked = false;
                Key4_B.Checked = false;
                Key5_B.Checked = false;
                Key6_B.Checked = false;
                Key7_B.Checked = false;

                this.KeyPreview = true;
                Label_Set.Text = "Key1に設定するKeyを入力してください。";
                checkNow = 3;
            }
            else
            {
                this.KeyPreview = false;
                Key1_B.Checked = false;
                Label_Set.Text = "";
            }
        }

        private void Key2_B_CheckedChanged(object sender, EventArgs e)
        {
            if (Key2_B.Checked)
            {
                SC_UP_B.Checked = false;
                SC_Down_B.Checked = false;
                Key1_B.Checked = false;
                Key2_B.Checked = true;
                Key3_B.Checked = false;
                Key4_B.Checked = false;
                Key5_B.Checked = false;
                Key6_B.Checked = false;
                Key7_B.Checked = false;

                this.KeyPreview = true;
                Label_Set.Text = "Key2に設定するKeyを入力してください。";
                checkNow = 4;
            }
            else
            {
                this.KeyPreview = false;
                Key2_B.Checked = false;
                Label_Set.Text = "";
            }
        }

        private void Key3_B_CheckedChanged(object sender, EventArgs e)
        {
            if (Key3_B.Checked)
            {
                SC_UP_B.Checked = false;
                SC_Down_B.Checked = false;
                Key1_B.Checked = false;
                Key2_B.Checked = false;
                Key3_B.Checked = true;
                Key4_B.Checked = false;
                Key5_B.Checked = false;
                Key6_B.Checked = false;
                Key7_B.Checked = false;

                this.KeyPreview = true;
                Label_Set.Text = "Key3に設定するKeyを入力してください。";
                checkNow = 5;
            }
            else
            {
                this.KeyPreview = false;
                Key3_B.Checked = false;
                Label_Set.Text = "";
            }
        }

        private void Key4_B_CheckedChanged(object sender, EventArgs e)
        {
            if (Key4_B.Checked)
            {
                SC_UP_B.Checked = false;
                SC_Down_B.Checked = false;
                Key1_B.Checked = false;
                Key2_B.Checked = false;
                Key3_B.Checked = false;
                Key4_B.Checked = true;
                Key5_B.Checked = false;
                Key6_B.Checked = false;
                Key7_B.Checked = false;

                this.KeyPreview = true;
                Label_Set.Text = "Key4に設定するKeyを入力してください。";
                checkNow = 6;
            }
            else
            {
                this.KeyPreview = false;
                Key4_B.Checked = false;
                Label_Set.Text = "";
            }
        }

        private void Key5_B_CheckedChanged(object sender, EventArgs e)
        {
            if (Key5_B.Checked)
            {
                SC_UP_B.Checked = false;
                SC_Down_B.Checked = false;
                Key1_B.Checked = false;
                Key2_B.Checked = false;
                Key3_B.Checked = false;
                Key4_B.Checked = false;
                Key5_B.Checked = true;
                Key6_B.Checked = false;
                Key7_B.Checked = false;

                this.KeyPreview = true;
                Label_Set.Text = "Key5に設定するKeyを入力してください。";
                checkNow = 7;
            }
            else
            {
                this.KeyPreview = false;
                Key5_B.Checked = false;
                Label_Set.Text = "";
            }
        }

        private void Key6_B_CheckedChanged(object sender, EventArgs e)
        {
            if (Key6_B.Checked)
            {
                SC_UP_B.Checked = false;
                SC_Down_B.Checked = false;
                Key1_B.Checked = false;
                Key2_B.Checked = false;
                Key3_B.Checked = false;
                Key4_B.Checked = false;
                Key5_B.Checked = false;
                Key6_B.Checked = true;
                Key7_B.Checked = false;

                this.KeyPreview = true;
                Label_Set.Text = "Key6に設定するKeyを入力してください。";
                checkNow = 8;
            }
            else
            {
                this.KeyPreview = false;
                Key6_B.Checked = false;
                Label_Set.Text = "";
            }
        }

        private void Key7_B_CheckedChanged(object sender, EventArgs e)
        {
            if (Key7_B.Checked)
            {
                SC_UP_B.Checked = false;
                SC_Down_B.Checked = false;
                Key1_B.Checked = false;
                Key2_B.Checked = false;
                Key3_B.Checked = false;
                Key4_B.Checked = false;
                Key5_B.Checked = false;
                Key6_B.Checked = false;
                Key7_B.Checked = true;

                this.KeyPreview = true;
                Label_Set.Text = "Key7に設定するKeyを入力してください。";
                checkNow = 9;
            }
            else
            {
                this.KeyPreview = false;
                Key7_B.Checked = false;
                Label_Set.Text = "";
            }
        }

        private void OK_B_Click(object sender, EventArgs e)
        {
            try
            {
                var res_up = SC_UP_B.Tag.ToString();
                var res_down = SC_Down_B.Tag.ToString();
                var res_1 = Key1_B.Tag.ToString();
                var res_2 = Key2_B.Tag.ToString();
                var res_3 = Key3_B.Tag.ToString();
                var res_4 = Key4_B.Tag.ToString();
                var res_5 = Key5_B.Tag.ToString();
                var res_6 = Key6_B.Tag.ToString();
                var res_7 = Key7_B.Tag.ToString();

                var element_u = _keyboardConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "K_S_Up");
                var element_d = _keyboardConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "K_S_Down");
                var element1 = _keyboardConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "K_Key1");
                var element2 = _keyboardConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "K_Key2");
                var element3 = _keyboardConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "K_Key3");
                var element4 = _keyboardConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "K_Key4");
                var element5 = _keyboardConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "K_Key5");
                var element6 = _keyboardConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "K_Key6");
                var element7 = _keyboardConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "K_Key7");

                element_u.SetValue(res_up);
                element_d.SetValue(res_down);
                element1.SetValue(res_1);
                element2.SetValue(res_2);
                element3.SetValue(res_3);
                element4.SetValue(res_4);
                element5.SetValue(res_5);
                element6.SetValue(res_6);
                element7.SetValue(res_7);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception)
            {
                DialogResult dialog = MessageBox.Show(
                    "Errorが発生しました。" +
                    "アプリケーションを終了します。",
                    "エラー",
                    MessageBoxButtons.OK);
                Close();
                Application.Exit();
                return;
            }
        }

        private void KeyBoardConfig_KeyDown(object sender, KeyEventArgs e)
        {
            //受け取ったキーをセットする
            switch (checkNow)
            {
                case 1:
                    SC_UP_B.Text = e.KeyCode.ToString();
                    SC_UP_B.Tag = e.KeyCode.ToString();
                    SC_UP_B.Checked = false;
                    Label_Set.Text = "";
                    break;
                case 2:
                    SC_Down_B.Text = e.KeyCode.ToString();
                    SC_Down_B.Tag = e.KeyCode.ToString();
                    SC_Down_B.Checked = false;
                    Label_Set.Text = "";
                    break;
                case 3:
                    Key1_B.Text = e.KeyCode.ToString();
                    Key1_B.Tag = e.KeyCode.ToString();
                    Key1_B.Checked = false;
                    Label_Set.Text = "";
                    break;
                case 4:
                    Key2_B.Text = e.KeyCode.ToString();
                    Key2_B.Tag = e.KeyCode.ToString();
                    Key2_B.Checked = false;
                    Label_Set.Text = "";
                    break;
                case 5:
                    Key3_B.Text = e.KeyCode.ToString();
                    Key3_B.Tag = e.KeyCode.ToString();
                    Key3_B.Checked = false;
                    Label_Set.Text = "";
                    break;
                case 6:
                    Key4_B.Text = e.KeyCode.ToString();
                    Key4_B.Tag = e.KeyCode.ToString();
                    Key4_B.Checked = false;
                    Label_Set.Text = "";
                    break;
                case 7:
                    Key5_B.Text = e.KeyCode.ToString();
                    Key5_B.Tag = e.KeyCode.ToString();
                    Key5_B.Checked = false;
                    Label_Set.Text = "";
                    break;
                case 8:
                    Key6_B.Text = e.KeyCode.ToString();
                    Key6_B.Tag = e.KeyCode.ToString();
                    Key6_B.Checked = false;
                    Label_Set.Text = "";
                    break;
                case 9:
                    Key7_B.Text = e.KeyCode.ToString();
                    Key7_B.Tag = e.KeyCode.ToString();
                    Key7_B.Checked = false;
                    Label_Set.Text = "";
                    break;

                default:
                    break;
            }
            this.KeyPreview = !this.KeyPreview;
        }
    }
}
