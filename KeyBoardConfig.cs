using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatCounter
{
    public partial class KeyBoardConfig : Form
    {
        private int checkNow = 0;


        public KeyBoardConfig()
        {
            //フォームの最大化ボタンの表示、非表示を切り替える
            this.MaximizeBox = !this.MaximizeBox;
            //フォームの最小化ボタンの表示、非表示を切り替える
            this.MinimizeBox = !this.MinimizeBox;
            //フォームのコントロールボックスの表示、非表示を切り替える
            //コントロールボックスを非表示にすると最大化、最小化、閉じるボタンも消える
            this.ControlBox = !this.ControlBox;

            InitializeComponent();

            KeyInit();
        }

        public void KeyInit()
        {
            Label_Set.Text = "";
            KeyPreview = false;
            this.KeyDown += KeyBoardConfig_KeyDown;

            var up = Properties.Settings.Default.K_S_Up;
            var down = Properties.Settings.Default.K_S_Down;
            var k1 = Properties.Settings.Default.K_Key1;
            var k2 = Properties.Settings.Default.K_Key2;
            var k3 = Properties.Settings.Default.K_Key3;
            var k4 = Properties.Settings.Default.K_Key4;
            var k5 = Properties.Settings.Default.K_Key5;
            var k6 = Properties.Settings.Default.K_Key6;
            var k7 = Properties.Settings.Default.K_Key7;

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
            var res_up = SC_UP_B.Tag.ToString();
            var res_down = SC_Down_B.Tag.ToString();
            var res_1 = Key1_B.Tag.ToString();
            var res_2 = Key2_B.Tag.ToString();
            var res_3 = Key3_B.Tag.ToString();
            var res_4 = Key4_B.Tag.ToString();
            var res_5 = Key5_B.Tag.ToString();
            var res_6 = Key6_B.Tag.ToString();
            var res_7 = Key7_B.Tag.ToString();


            Properties.Settings.Default.K_S_Up = res_up;
            Properties.Settings.Default.K_S_Down = res_down;
            Properties.Settings.Default.K_Key1 = res_1;
            Properties.Settings.Default.K_Key2 = res_2;
            Properties.Settings.Default.K_Key3 = res_3;
            Properties.Settings.Default.K_Key4 = res_4;
            Properties.Settings.Default.K_Key5 = res_5;
            Properties.Settings.Default.K_Key6 = res_6;
            Properties.Settings.Default.K_Key7 = res_7;

            Properties.Settings.Default.Save();
            this.Close();
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
