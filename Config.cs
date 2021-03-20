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
            Text_Kando.Text = kando.Set_Kando.ToString();
        }

        private void S_Kando_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            var chk = int.TryParse(Text_Kando.Text, out int i);
            ulong j = (ulong)i;
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
                kando.Set_Kando = j;
                this.Close();
            }
        }

        private void Def_Button_Click(object sender, EventArgs e)
        {
            Text_Kando.Text = kando.Set_Kando.ToString();
        }
    }
}
