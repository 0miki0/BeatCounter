using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeatCounter
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
            Text_Kando.Text = Properties.Settings.Default.Kando.ToString();

            if(Properties.Settings.Default.BackColor == 1)
            {
                this.BackColor = Color.FromArgb(64, 64, 64);
                S_Kando.ForeColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// OKボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// defaultボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Def_Button_Click(object sender, EventArgs e)
        {
            Text_Kando.Text = Properties.Settings.Default.Kando.ToString();
        }
    }
}
