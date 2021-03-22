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
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
            Text_Kando.Text = Properties.Settings.Default.Kando.ToString();
        }

        private void S_Kando_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            // 入力された値を数値に変換する。
            var chk = int.TryParse(Text_Kando.Text, out int i);

            // 格納する変数の型に変換する。
            ulong j = (ulong)i;

            // 数値以外の場合、エラーメッセージを表示する。
            if(chk == false)
            {
                DialogResult dialog = MessageBox.Show(
                    "テキストボックスには数値を" +
                    "入力してください。",
                    "Error",
                    MessageBoxButtons.OK);
            }
            else
            {
                // 変数を格納。
                Properties.Settings.Default.Kando = j;
                Properties.Settings.Default.Save();

                this.Close();
            }
        }

        private void Def_Button_Click(object sender, EventArgs e)
        {
            Text_Kando.Text = Properties.Settings.Default.Kando.ToString();
        }
    }
}
