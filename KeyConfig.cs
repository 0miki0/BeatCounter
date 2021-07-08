using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace BeatCounter
{
    public partial class KeyConfig : Form
    {
        // 親ウィンドウから渡されたXML情報を格納/リターンするための変数。
        public XDocument _keyConfxml;

        public KeyConfig(XDocument xml, XMLClass cls)
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
                _keyConfxml = xml;
                if (_keyConfxml == null)
                {
                    throw new System.IO.DirectoryNotFoundException();
                }
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                DialogResult dialog = MessageBox.Show(
                    "Config.xmlファイルが見つかりませんでした。" +
                    "アプリケーションを終了します。",
                    "エラー",
                    MessageBoxButtons.OK);
                Close();
                Application.Exit();
                return;
            }

            InitializeComponent();

            KeyInit();
        }

        public void KeyInit()
        {
            var up = int.Parse(_keyConfxml.XPathSelectElement("//C_S_Up").Value);
            var down = int.Parse(_keyConfxml.XPathSelectElement("//C_S_Down").Value);
            var k1 = int.Parse(_keyConfxml.XPathSelectElement("//C_Key1").Value);
            var k2 = int.Parse(_keyConfxml.XPathSelectElement("//C_Key2").Value);
            var k3 = int.Parse(_keyConfxml.XPathSelectElement("//C_Key3").Value);
            var k4 = int.Parse(_keyConfxml.XPathSelectElement("//C_Key4").Value);
            var k5 = int.Parse(_keyConfxml.XPathSelectElement("//C_Key5").Value);
            var k6 = int.Parse(_keyConfxml.XPathSelectElement("//C_Key6").Value);
            var k7 = int.Parse(_keyConfxml.XPathSelectElement("//C_Key7").Value);


            if (up < 50)
            {
                SC_UP_B.Text = (up + 1).ToString();
                SC_UP_B.Tag = up;
            }
            else
            {
                SC_UP_B.Text = Key_Binding(up);
                SC_UP_B.Tag = up;
            }

            if (down < 50)
            {
                SC_Down_B.Text = (down + 1).ToString();
                SC_Down_B.Tag = down;
            }
            else
            {
                SC_Down_B.Text = Key_Binding(down);
                SC_Down_B.Tag = down;
            }

            if (k1 < 50)
            {
                Key1_B.Text = (k1 + 1).ToString();
                Key1_B.Tag = k1;
            }
            else
            {
                Key1_B.Text = Key_Binding(k1);
                Key1_B.Tag = k1;
            }

            if (k2 < 50)
            {
                Key2_B.Text = (k2 + 1).ToString();
                Key2_B.Tag = k2;
            }
            else
            {
                Key2_B.Text = Key_Binding(k2);
                Key2_B.Tag = k2;
            }

            if (k3 < 50)
            {
                Key3_B.Text = (k3 + 1).ToString();
                Key3_B.Tag = k3;
            }
            else
            {
                Key3_B.Text = Key_Binding(k3);
                Key3_B.Tag = k3;
            }

            if (k4 < 50)
            {
                Key4_B.Text = (k4 + 1).ToString();
                Key4_B.Tag = k4;
            }
            else
            {
                Key4_B.Text = Key_Binding(k4);
                Key4_B.Tag = k4;
            }

            if (k5 < 50)
            {
                Key5_B.Text = (k5 + 1).ToString();
                Key5_B.Tag = k5;
            }
            else
            {
                Key5_B.Text = Key_Binding(k5);
                Key5_B.Tag = k5;
            }

            if (k6 < 50)
            {
                Key6_B.Text = (k6 + 1).ToString();
                Key6_B.Tag = k6;
            }
            else
            {
                Key6_B.Text = Key_Binding(k6);
                Key6_B.Tag = k6;
            }

            if (k7 < 50)
            {
                Key7_B.Text = (k7 + 1).ToString();
                Key7_B.Tag = k7;
            }
            else
            {
                Key7_B.Text = Key_Binding(k7);
                Key7_B.Tag = k7;
            }
        }

        public string Key_Binding(int num)
        {
            string result = String.Empty;

            if (num == 50)
            {
                result = "LEFT";
            }
            else if (num == 51)
            {
                result = "RIGHT";
            }
            else if (num == 52)
            {
                result = "UP";
            }
            else if (num == 53)
            {
                result = "DOWN";
            }
            else if (num == 80)
            {
                result = "P_LeftUp";
            }
            else if (num == 81)
            {
                result = "P_Up";
            }
            else if (num == 82)
            {
                result = "P_RightUp";
            }
            else if (num == 83)
            {
                result = "P_Left";
            }
            else if (num == 84)
            {
                result = "P_Right";
            }
            else if (num == 85)
            {
                result = "P_LeftDown";
            }
            else if (num == 86)
            {
                result = "P_Down";
            }
            else if (num == 87)
            {
                result = "P_RightDown";
            }

            return result;
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
            }
            else
            {
                SC_UP_B.Checked = false;
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
            }
            else
            {
                SC_Down_B.Checked = false;
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
            }
            else
            {
                Key1_B.Checked = false;
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
            }
            else
            {
                Key2_B.Checked = false;
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
            }
            else
            {
                Key3_B.Checked = false;
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
            }
            else
            {
                Key4_B.Checked = false;
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
            }
            else
            {
                Key5_B.Checked = false;
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
            }
            else
            {
                Key6_B.Checked = false;
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
            }
            else
            {
                Key7_B.Checked = false;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "1") return;
                    }
                    chk.Text = "1";
                    chk.Tag = 0;
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "2") return;
                    }
                    chk.Text = "2";
                    chk.Tag = 1;
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "3") return;
                    }
                    chk.Text = "3";
                    chk.Tag = 2;
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "4") return;
                    }
                    chk.Text = "4";
                    chk.Tag = 3;
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "5") return;
                    }
                    chk.Text = "5";
                    chk.Tag = 4;
                }
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "6") return;
                    }
                    chk.Text = "6";
                    chk.Tag = 5;
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "7") return;
                    }
                    chk.Text = "7";
                    chk.Tag = 6;
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "8") return;
                    }
                    chk.Text = "8";
                    chk.Tag = 7;
                }
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "9") return;
                    }
                    chk.Text = "9";
                    chk.Tag = 8;
                }
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "10") return;
                    }
                    chk.Text = "10";
                    chk.Tag = 9;
                }
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "11") return;
                    }
                    chk.Text = "11";
                    chk.Tag = 10;
                }
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "12") return;
                    }
                    chk.Text = "12";
                    chk.Tag = 11;
                }
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "13") return;
                    }
                    chk.Text = "13";
                    chk.Tag = 12;
                }
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "14") return;
                    }
                    chk.Text = "14";
                    chk.Tag = 13;
                }
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "15") return;
                    }
                    chk.Text = "15";
                    chk.Tag = 14;
                }
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "16") return;
                    }
                    chk.Text = "16";
                    chk.Tag = 15;
                }
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "17") return;
                    }
                    chk.Text = "17";
                    chk.Tag = 16;
                }
            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "18") return;
                    }
                    chk.Text = "18";
                    chk.Tag = 17;
                }
            }
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "19") return;
                    }
                    chk.Text = "19";
                    chk.Tag = 18;
                }
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "20") return;
                    }
                    chk.Text = "20";
                    chk.Tag = 19;
                }
            }
        }

        private void Up_B_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "UP") return;
                    }
                    chk.Text = "UP";
                    chk.Tag = 52;
                }
            }
        }

        private void Left_B_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "LEFT") return;
                    }
                    chk.Text = "LEFT";
                    chk.Tag = 50;
                }
            }
        }

        private void Down_B_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "DOWN") return;
                    }
                    chk.Text = "DOWN";
                    chk.Tag = 53;
                }
            }
        }

        private void Right_B_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "RIGHT") return;
                    }
                    chk.Text = "RIGHT";
                    chk.Tag = 51;
                }
            }
        }

        private void POV_LU_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "P_LeftUp") return;
                    }
                    chk.Text = "P_LeftUp";
                    chk.Tag = 80;
                }
            }
        }

        private void POV_Up_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "P_Up") return;
                    }
                    chk.Text = "P_Up";
                    chk.Tag = 81;
                }
            }
        }

        private void POV_RU_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "P_RightUp") return;
                    }
                    chk.Text = "P_RightUp";
                    chk.Tag = 82;
                }
            }
        }

        private void POV_Left_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "P_Left") return;
                    }
                    chk.Text = "P_Left";
                    chk.Tag = 83;
                }
            }
        }

        private void POV_Right_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "P_Right") return;
                    }
                    chk.Text = "P_Right";
                    chk.Tag = 84;
                }
            }
        }

        private void POV_LD_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "P_LeftDown") return;
                    }
                    chk.Text = "P_LeftDown";
                    chk.Tag = 85;
                }
            }
        }

        private void POV_Down_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "P_Down") return;
                    }
                    chk.Text = "P_Down";
                    chk.Tag = 86;
                }
            }
        }

        private void POV_RD_Click(object sender, EventArgs e)
        {
            List<CheckBox> checks = new List<CheckBox>() { SC_UP_B, SC_Down_B, Key1_B, Key2_B, Key3_B, Key4_B, Key5_B, Key6_B, Key7_B };

            foreach (CheckBox chk in checks)
            {
                if (chk.Checked)
                {
                    foreach (var chk2 in checks)
                    {

                        if (chk2.Text == "P_RightDown") return;
                    }
                    chk.Text = "P_RightDown";
                    chk.Tag = 87;
                }
            }
        }

        private void OK_B_Click(object sender, EventArgs e)
        {
            var try_up = int.TryParse(SC_UP_B.Tag.ToString(), out int res_up);
            var try_down = int.TryParse(SC_Down_B.Tag.ToString(), out int res_down);
            var try_1 = int.TryParse(Key1_B.Tag.ToString(), out int res_1);
            var try_2 = int.TryParse(Key2_B.Tag.ToString(), out int res_2);
            var try_3 = int.TryParse(Key3_B.Tag.ToString(), out int res_3);
            var try_4 = int.TryParse(Key4_B.Tag.ToString(), out int res_4);
            var try_5 = int.TryParse(Key5_B.Tag.ToString(), out int res_5);
            var try_6 = int.TryParse(Key6_B.Tag.ToString(), out int res_6);
            var try_7 = int.TryParse(Key7_B.Tag.ToString(), out int res_7);

            try
            {
                if (try_up)
                {
                    var element = _keyConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "C_S_Up");
                    element.SetValue(res_up);
                }

                if (try_down)
                {
                    var element = _keyConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "C_S_Down");
                    element.SetValue(res_down);
                }

                if (try_1)
                {
                    var element = _keyConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "C_Key1");
                    element.SetValue(res_1);
                }

                if (try_2)
                {
                    var element = _keyConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "C_Key2");
                    element.SetValue(res_2);
                }

                if (try_3)
                {
                    var element = _keyConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "C_Key3");
                    element.SetValue(res_3);
                }

                if (try_4)
                {
                    var element = _keyConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "C_Key4");
                    element.SetValue(res_4);
                }

                if (try_5)
                {
                    var element = _keyConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "C_Key5");
                    element.SetValue(res_5);
                }

                if (try_6)
                {
                    var element = _keyConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "C_Key6");
                    element.SetValue(res_6);
                }

                if (try_7)
                {
                    var element = _keyConfxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "C_Key7");
                    element.SetValue(res_7);
                }

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
    }
}
