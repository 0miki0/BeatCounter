using System.Windows.Forms;

namespace BeatCounter
{
    class BeatCounterCommon : BeatCounter
    {
        public DialogResult DialogYesNo(string str1, string str2, string str3)
        {
            DialogResult dialog = MessageBox.Show(
                str1 +
                str2,
                str3,
                MessageBoxButtons.YesNo);

            return dialog;
        }
    }
}
