using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace BeatCounter
{
    public partial class Config : Form
    {
        // 親ウィンドウから渡されたXML情報を格納/リターンするための変数。
        public XDocument _confxml;

        public Config(XDocument xml, XMLClass cls)
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
                _confxml = xml;
                if (_confxml == null)
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

            Text_Kando.Text = _confxml.XPathSelectElement("//Kando").Value;
            var backcolor = int.Parse(_confxml.XPathSelectElement("//BackColor").Value);

            if (backcolor == 1)
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
            try
            {

                // 入力された値を数値に変換する。
                var chk = int.TryParse(Text_Kando.Text, out int i);

                // 格納する変数の型に変換する。
                ulong j = (ulong)i;

                // 数値以外の場合、エラーメッセージを表示する。
                if (chk == false)
                {
                    DialogResult dialog = MessageBox.Show(
                        "テキストボックスには数値を" +
                        "入力してください。",
                        "Error",
                        MessageBoxButtons.OK);
                }
                else
                {
                    var element = _confxml.Root.Elements().FirstOrDefault(x => x.Attribute("id")?.Value == "Kando");
                    element.SetValue(j);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
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

        /// <summary>
        /// defaultボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Def_Button_Click(object sender, EventArgs e)
        {
            // デフォルト値として5000をセット。
            Text_Kando.Text = "5000";
        }
    }
}
