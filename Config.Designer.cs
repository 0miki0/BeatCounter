
namespace BeatCounter
{
    partial class Config
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.S_Kando = new System.Windows.Forms.Label();
            this.Text_Kando = new System.Windows.Forms.TextBox();
            this.OK_Button = new System.Windows.Forms.Button();
            this.Def_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // S_Kando
            // 
            this.S_Kando.AutoSize = true;
            this.S_Kando.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.S_Kando.Location = new System.Drawing.Point(46, 40);
            this.S_Kando.Name = "S_Kando";
            this.S_Kando.Size = new System.Drawing.Size(75, 22);
            this.S_Kando.TabIndex = 0;
            this.S_Kando.Text = "皿の感度:";
            // 
            // Text_Kando
            // 
            this.Text_Kando.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Text_Kando.Location = new System.Drawing.Point(140, 40);
            this.Text_Kando.Name = "Text_Kando";
            this.Text_Kando.Size = new System.Drawing.Size(100, 25);
            this.Text_Kando.TabIndex = 1;
            this.Text_Kando.Text = "900";
            // 
            // OK_Button
            // 
            this.OK_Button.Location = new System.Drawing.Point(197, 76);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(75, 23);
            this.OK_Button.TabIndex = 2;
            this.OK_Button.Text = "OK";
            this.OK_Button.UseVisualStyleBackColor = true;
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // Def_Button
            // 
            this.Def_Button.Location = new System.Drawing.Point(116, 76);
            this.Def_Button.Name = "Def_Button";
            this.Def_Button.Size = new System.Drawing.Size(75, 23);
            this.Def_Button.TabIndex = 3;
            this.Def_Button.Text = "Default";
            this.Def_Button.UseVisualStyleBackColor = true;
            this.Def_Button.Click += new System.EventHandler(this.Def_Button_Click);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 111);
            this.Controls.Add(this.Def_Button);
            this.Controls.Add(this.OK_Button);
            this.Controls.Add(this.Text_Kando);
            this.Controls.Add(this.S_Kando);
            this.Name = "Config";
            this.Text = "Config";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label S_Kando;
        private System.Windows.Forms.TextBox Text_Kando;
        private System.Windows.Forms.Button OK_Button;
        private System.Windows.Forms.Button Def_Button;
    }
}