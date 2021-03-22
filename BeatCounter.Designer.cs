
namespace BeatCounter
{
    partial class BeatCounter
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PlayGameTips = new System.Windows.Forms.ToolStripMenuItem();
            this.InfinitasPlayTips = new System.Windows.Forms.ToolStripMenuItem();
            this.BmsPlayTips = new System.Windows.Forms.ToolStripMenuItem();
            this.ControllerTips = new System.Windows.Forms.ToolStripMenuItem();
            this.DaoTips = new System.Windows.Forms.ToolStripMenuItem();
            this.PhoenixTips = new System.Windows.Forms.ToolStripMenuItem();
            this.コンフィグToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SChangeTips = new System.Windows.Forms.ToolStripMenuItem();
            this.CountChangeTips = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetTips = new System.Windows.Forms.ToolStripMenuItem();
            this.TodayClearTips = new System.Windows.Forms.ToolStripMenuItem();
            this.AlldayClearTips = new System.Windows.Forms.ToolStripMenuItem();
            this.TodayShortTips = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.T_S_Up = new System.Windows.Forms.TextBox();
            this.T_S_Down = new System.Windows.Forms.TextBox();
            this.T_Key1 = new System.Windows.Forms.TextBox();
            this.T_Key2 = new System.Windows.Forms.TextBox();
            this.T_Key3 = new System.Windows.Forms.TextBox();
            this.T_Key6 = new System.Windows.Forms.TextBox();
            this.T_Key4 = new System.Windows.Forms.TextBox();
            this.T_Key5 = new System.Windows.Forms.TextBox();
            this.T_Key7 = new System.Windows.Forms.TextBox();
            this.T_TodayKeys = new System.Windows.Forms.TextBox();
            this.T_AllDayKeys = new System.Windows.Forms.TextBox();
            this.TodayKeys = new System.Windows.Forms.Label();
            this.S_Up = new System.Windows.Forms.Label();
            this.S_Down = new System.Windows.Forms.Label();
            this.Key1 = new System.Windows.Forms.Label();
            this.Key2 = new System.Windows.Forms.Label();
            this.Key3 = new System.Windows.Forms.Label();
            this.Key5 = new System.Windows.Forms.Label();
            this.Key4 = new System.Windows.Forms.Label();
            this.Key6 = new System.Windows.Forms.Label();
            this.AllDayKeys = new System.Windows.Forms.Label();
            this.Key7 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("游ゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(299, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 17);
            this.label9.TabIndex = 7;
            this.label9.Text = "全期間合計:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("游ゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(107, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 17);
            this.label8.TabIndex = 7;
            this.label8.Text = "今回の合計:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("源ノ角ゴシック Heavy", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(9, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 18);
            this.label1.TabIndex = 33;
            this.label1.Text = "↑";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("源ノ角ゴシック Heavy", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(9, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 18);
            this.label2.TabIndex = 34;
            this.label2.Text = "↓";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PlayGameTips
            // 
            this.PlayGameTips.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InfinitasPlayTips,
            this.BmsPlayTips});
            this.PlayGameTips.Name = "PlayGameTips";
            this.PlayGameTips.Size = new System.Drawing.Size(70, 20);
            this.PlayGameTips.Text = "プレイ対象";
            // 
            // InfinitasPlayTips
            // 
            this.InfinitasPlayTips.Checked = true;
            this.InfinitasPlayTips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.InfinitasPlayTips.Name = "InfinitasPlayTips";
            this.InfinitasPlayTips.Size = new System.Drawing.Size(180, 22);
            this.InfinitasPlayTips.Text = "INFINITAS";
            this.InfinitasPlayTips.Click += new System.EventHandler(this.InfinitasPlayTips_Click);
            // 
            // BmsPlayTips
            // 
            this.BmsPlayTips.Name = "BmsPlayTips";
            this.BmsPlayTips.Size = new System.Drawing.Size(180, 22);
            this.BmsPlayTips.Text = "BMS";
            this.BmsPlayTips.Click += new System.EventHandler(this.BmsPlayTips_Click);
            // 
            // ControllerTips
            // 
            this.ControllerTips.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DaoTips,
            this.PhoenixTips});
            this.ControllerTips.Enabled = false;
            this.ControllerTips.Name = "ControllerTips";
            this.ControllerTips.Size = new System.Drawing.Size(77, 20);
            this.ControllerTips.Text = "コントローラー";
            // 
            // DaoTips
            // 
            this.DaoTips.Checked = true;
            this.DaoTips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DaoTips.Font = new System.Drawing.Font("源ノ角ゴシック Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DaoTips.Name = "DaoTips";
            this.DaoTips.Size = new System.Drawing.Size(180, 22);
            this.DaoTips.Text = "DAO";
            // 
            // PhoenixTips
            // 
            this.PhoenixTips.Font = new System.Drawing.Font("源ノ角ゴシック Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PhoenixTips.Name = "PhoenixTips";
            this.PhoenixTips.Size = new System.Drawing.Size(180, 22);
            this.PhoenixTips.Text = "Phoenix";
            // 
            // コンフィグToolStripMenuItem
            // 
            this.コンフィグToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SChangeTips,
            this.CountChangeTips});
            this.コンフィグToolStripMenuItem.Name = "コンフィグToolStripMenuItem";
            this.コンフィグToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.コンフィグToolStripMenuItem.Text = "コンフィグ";
            this.コンフィグToolStripMenuItem.Click += new System.EventHandler(this.コンフィグToolStripMenuItem_Click);
            // 
            // SChangeTips
            // 
            this.SChangeTips.Name = "SChangeTips";
            this.SChangeTips.Size = new System.Drawing.Size(180, 22);
            this.SChangeTips.Text = "皿の感度";
            this.SChangeTips.Click += new System.EventHandler(this.感度ToolStripMenuItem_Click);
            // 
            // CountChangeTips
            // 
            this.CountChangeTips.Name = "CountChangeTips";
            this.CountChangeTips.Size = new System.Drawing.Size(180, 22);
            this.CountChangeTips.Text = "カウントの変更";
            this.CountChangeTips.Click += new System.EventHandler(this.カウントの変更ToolStripMenuItem_Click);
            // 
            // ResetTips
            // 
            this.ResetTips.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TodayClearTips,
            this.AlldayClearTips,
            this.TodayShortTips});
            this.ResetTips.Name = "ResetTips";
            this.ResetTips.ShowShortcutKeys = false;
            this.ResetTips.Size = new System.Drawing.Size(53, 20);
            this.ResetTips.Text = "リセット";
            this.ResetTips.Click += new System.EventHandler(this.リセットToolStripMenuItem_Click);
            // 
            // TodayClearTips
            // 
            this.TodayClearTips.Font = new System.Drawing.Font("源ノ角ゴシック Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TodayClearTips.Name = "TodayClearTips";
            this.TodayClearTips.Size = new System.Drawing.Size(180, 22);
            this.TodayClearTips.Text = "今回分";
            this.TodayClearTips.Click += new System.EventHandler(this.今日の回数ToolStripMenuItem_Click);
            // 
            // AlldayClearTips
            // 
            this.AlldayClearTips.Font = new System.Drawing.Font("源ノ角ゴシック Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AlldayClearTips.Name = "AlldayClearTips";
            this.AlldayClearTips.Size = new System.Drawing.Size(180, 22);
            this.AlldayClearTips.Text = "全期間";
            this.AlldayClearTips.Click += new System.EventHandler(this.全期間回数ToolStripMenuItem_Click);
            // 
            // TodayShortTips
            // 
            this.TodayShortTips.Name = "TodayShortTips";
            this.TodayShortTips.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.TodayShortTips.Size = new System.Drawing.Size(180, 22);
            this.TodayShortTips.Text = "今S";
            this.TodayShortTips.Visible = false;
            this.TodayShortTips.Click += new System.EventHandler(this.TodayShortTips_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlayGameTips,
            this.ControllerTips,
            this.コンフィグToolStripMenuItem,
            this.ResetTips});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(524, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // T_S_Up
            // 
            this.T_S_Up.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.T_S_Up.BackColor = System.Drawing.Color.White;
            this.T_S_Up.CausesValidation = false;
            this.T_S_Up.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.T_S_Up.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T_S_Up.HideSelection = false;
            this.T_S_Up.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.T_S_Up.Location = new System.Drawing.Point(32, 79);
            this.T_S_Up.Margin = new System.Windows.Forms.Padding(0);
            this.T_S_Up.MaxLength = 8;
            this.T_S_Up.Multiline = true;
            this.T_S_Up.Name = "T_S_Up";
            this.T_S_Up.ReadOnly = true;
            this.T_S_Up.ShortcutsEnabled = false;
            this.T_S_Up.Size = new System.Drawing.Size(90, 25);
            this.T_S_Up.TabIndex = 10;
            this.T_S_Up.Text = "99999999";
            this.T_S_Up.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.T_S_Up.Visible = false;
            this.T_S_Up.WordWrap = false;
            // 
            // T_S_Down
            // 
            this.T_S_Down.AllowDrop = true;
            this.T_S_Down.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.T_S_Down.BackColor = System.Drawing.Color.White;
            this.T_S_Down.CausesValidation = false;
            this.T_S_Down.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.T_S_Down.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T_S_Down.HideSelection = false;
            this.T_S_Down.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.T_S_Down.Location = new System.Drawing.Point(32, 110);
            this.T_S_Down.Margin = new System.Windows.Forms.Padding(0);
            this.T_S_Down.MaxLength = 8;
            this.T_S_Down.Multiline = true;
            this.T_S_Down.Name = "T_S_Down";
            this.T_S_Down.ReadOnly = true;
            this.T_S_Down.ShortcutsEnabled = false;
            this.T_S_Down.Size = new System.Drawing.Size(90, 25);
            this.T_S_Down.TabIndex = 11;
            this.T_S_Down.Text = "99999999";
            this.T_S_Down.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.T_S_Down.Visible = false;
            this.T_S_Down.WordWrap = false;
            // 
            // T_Key1
            // 
            this.T_Key1.AllowDrop = true;
            this.T_Key1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.T_Key1.BackColor = System.Drawing.Color.White;
            this.T_Key1.CausesValidation = false;
            this.T_Key1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.T_Key1.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T_Key1.HideSelection = false;
            this.T_Key1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.T_Key1.Location = new System.Drawing.Point(133, 110);
            this.T_Key1.Margin = new System.Windows.Forms.Padding(0);
            this.T_Key1.MaxLength = 8;
            this.T_Key1.Multiline = true;
            this.T_Key1.Name = "T_Key1";
            this.T_Key1.ReadOnly = true;
            this.T_Key1.ShortcutsEnabled = false;
            this.T_Key1.Size = new System.Drawing.Size(90, 25);
            this.T_Key1.TabIndex = 12;
            this.T_Key1.Text = "99999999";
            this.T_Key1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.T_Key1.Visible = false;
            this.T_Key1.WordWrap = false;
            // 
            // T_Key2
            // 
            this.T_Key2.AllowDrop = true;
            this.T_Key2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.T_Key2.BackColor = System.Drawing.Color.White;
            this.T_Key2.CausesValidation = false;
            this.T_Key2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.T_Key2.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T_Key2.HideSelection = false;
            this.T_Key2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.T_Key2.Location = new System.Drawing.Point(182, 79);
            this.T_Key2.Margin = new System.Windows.Forms.Padding(0);
            this.T_Key2.MaxLength = 8;
            this.T_Key2.Multiline = true;
            this.T_Key2.Name = "T_Key2";
            this.T_Key2.ReadOnly = true;
            this.T_Key2.ShortcutsEnabled = false;
            this.T_Key2.Size = new System.Drawing.Size(90, 25);
            this.T_Key2.TabIndex = 13;
            this.T_Key2.Text = "99999999";
            this.T_Key2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.T_Key2.Visible = false;
            this.T_Key2.WordWrap = false;
            // 
            // T_Key3
            // 
            this.T_Key3.AllowDrop = true;
            this.T_Key3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.T_Key3.BackColor = System.Drawing.Color.White;
            this.T_Key3.CausesValidation = false;
            this.T_Key3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.T_Key3.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T_Key3.HideSelection = false;
            this.T_Key3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.T_Key3.Location = new System.Drawing.Point(229, 110);
            this.T_Key3.Margin = new System.Windows.Forms.Padding(0);
            this.T_Key3.MaxLength = 8;
            this.T_Key3.Multiline = true;
            this.T_Key3.Name = "T_Key3";
            this.T_Key3.ReadOnly = true;
            this.T_Key3.ShortcutsEnabled = false;
            this.T_Key3.Size = new System.Drawing.Size(90, 25);
            this.T_Key3.TabIndex = 14;
            this.T_Key3.Text = "99999999";
            this.T_Key3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.T_Key3.Visible = false;
            this.T_Key3.WordWrap = false;
            // 
            // T_Key6
            // 
            this.T_Key6.AllowDrop = true;
            this.T_Key6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.T_Key6.BackColor = System.Drawing.Color.White;
            this.T_Key6.CausesValidation = false;
            this.T_Key6.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.T_Key6.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T_Key6.HideSelection = false;
            this.T_Key6.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.T_Key6.Location = new System.Drawing.Point(374, 79);
            this.T_Key6.Margin = new System.Windows.Forms.Padding(0);
            this.T_Key6.MaxLength = 8;
            this.T_Key6.Multiline = true;
            this.T_Key6.Name = "T_Key6";
            this.T_Key6.ReadOnly = true;
            this.T_Key6.ShortcutsEnabled = false;
            this.T_Key6.Size = new System.Drawing.Size(90, 25);
            this.T_Key6.TabIndex = 17;
            this.T_Key6.Text = "99999999";
            this.T_Key6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.T_Key6.Visible = false;
            this.T_Key6.WordWrap = false;
            // 
            // T_Key4
            // 
            this.T_Key4.AllowDrop = true;
            this.T_Key4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.T_Key4.BackColor = System.Drawing.Color.White;
            this.T_Key4.CausesValidation = false;
            this.T_Key4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.T_Key4.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T_Key4.HideSelection = false;
            this.T_Key4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.T_Key4.Location = new System.Drawing.Point(278, 79);
            this.T_Key4.Margin = new System.Windows.Forms.Padding(0);
            this.T_Key4.MaxLength = 8;
            this.T_Key4.Multiline = true;
            this.T_Key4.Name = "T_Key4";
            this.T_Key4.ReadOnly = true;
            this.T_Key4.ShortcutsEnabled = false;
            this.T_Key4.Size = new System.Drawing.Size(90, 25);
            this.T_Key4.TabIndex = 15;
            this.T_Key4.Text = "99999999";
            this.T_Key4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.T_Key4.Visible = false;
            this.T_Key4.WordWrap = false;
            // 
            // T_Key5
            // 
            this.T_Key5.AllowDrop = true;
            this.T_Key5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.T_Key5.BackColor = System.Drawing.Color.White;
            this.T_Key5.CausesValidation = false;
            this.T_Key5.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.T_Key5.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T_Key5.HideSelection = false;
            this.T_Key5.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.T_Key5.Location = new System.Drawing.Point(325, 110);
            this.T_Key5.Margin = new System.Windows.Forms.Padding(0);
            this.T_Key5.MaxLength = 8;
            this.T_Key5.Multiline = true;
            this.T_Key5.Name = "T_Key5";
            this.T_Key5.ReadOnly = true;
            this.T_Key5.ShortcutsEnabled = false;
            this.T_Key5.Size = new System.Drawing.Size(90, 25);
            this.T_Key5.TabIndex = 16;
            this.T_Key5.Text = "99999999";
            this.T_Key5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.T_Key5.Visible = false;
            this.T_Key5.WordWrap = false;
            // 
            // T_Key7
            // 
            this.T_Key7.AllowDrop = true;
            this.T_Key7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.T_Key7.BackColor = System.Drawing.Color.White;
            this.T_Key7.CausesValidation = false;
            this.T_Key7.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.T_Key7.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T_Key7.HideSelection = false;
            this.T_Key7.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.T_Key7.Location = new System.Drawing.Point(421, 110);
            this.T_Key7.Margin = new System.Windows.Forms.Padding(0);
            this.T_Key7.MaxLength = 8;
            this.T_Key7.Multiline = true;
            this.T_Key7.Name = "T_Key7";
            this.T_Key7.ReadOnly = true;
            this.T_Key7.ShortcutsEnabled = false;
            this.T_Key7.Size = new System.Drawing.Size(90, 25);
            this.T_Key7.TabIndex = 18;
            this.T_Key7.Text = "99999999";
            this.T_Key7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.T_Key7.Visible = false;
            this.T_Key7.WordWrap = false;
            // 
            // T_TodayKeys
            // 
            this.T_TodayKeys.AllowDrop = true;
            this.T_TodayKeys.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.T_TodayKeys.BackColor = System.Drawing.Color.White;
            this.T_TodayKeys.CausesValidation = false;
            this.T_TodayKeys.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.T_TodayKeys.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T_TodayKeys.HideSelection = false;
            this.T_TodayKeys.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.T_TodayKeys.Location = new System.Drawing.Point(182, 40);
            this.T_TodayKeys.Margin = new System.Windows.Forms.Padding(0);
            this.T_TodayKeys.MaxLength = 8;
            this.T_TodayKeys.Multiline = true;
            this.T_TodayKeys.Name = "T_TodayKeys";
            this.T_TodayKeys.ReadOnly = true;
            this.T_TodayKeys.ShortcutsEnabled = false;
            this.T_TodayKeys.Size = new System.Drawing.Size(90, 25);
            this.T_TodayKeys.TabIndex = 19;
            this.T_TodayKeys.Text = "99999999";
            this.T_TodayKeys.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.T_TodayKeys.Visible = false;
            this.T_TodayKeys.WordWrap = false;
            // 
            // T_AllDayKeys
            // 
            this.T_AllDayKeys.AllowDrop = true;
            this.T_AllDayKeys.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.T_AllDayKeys.BackColor = System.Drawing.Color.White;
            this.T_AllDayKeys.CausesValidation = false;
            this.T_AllDayKeys.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.T_AllDayKeys.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T_AllDayKeys.HideSelection = false;
            this.T_AllDayKeys.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.T_AllDayKeys.Location = new System.Drawing.Point(374, 40);
            this.T_AllDayKeys.Margin = new System.Windows.Forms.Padding(0);
            this.T_AllDayKeys.MaxLength = 8;
            this.T_AllDayKeys.Multiline = true;
            this.T_AllDayKeys.Name = "T_AllDayKeys";
            this.T_AllDayKeys.ReadOnly = true;
            this.T_AllDayKeys.ShortcutsEnabled = false;
            this.T_AllDayKeys.Size = new System.Drawing.Size(90, 25);
            this.T_AllDayKeys.TabIndex = 20;
            this.T_AllDayKeys.Text = "99999999";
            this.T_AllDayKeys.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.T_AllDayKeys.Visible = false;
            this.T_AllDayKeys.WordWrap = false;
            // 
            // TodayKeys
            // 
            this.TodayKeys.BackColor = System.Drawing.SystemColors.Info;
            this.TodayKeys.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TodayKeys.CausesValidation = false;
            this.TodayKeys.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TodayKeys.Location = new System.Drawing.Point(182, 40);
            this.TodayKeys.Name = "TodayKeys";
            this.TodayKeys.Size = new System.Drawing.Size(90, 25);
            this.TodayKeys.TabIndex = 40;
            this.TodayKeys.Text = "label3";
            this.TodayKeys.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // S_Up
            // 
            this.S_Up.BackColor = System.Drawing.SystemColors.Info;
            this.S_Up.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.S_Up.CausesValidation = false;
            this.S_Up.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.S_Up.Location = new System.Drawing.Point(32, 79);
            this.S_Up.Name = "S_Up";
            this.S_Up.Size = new System.Drawing.Size(90, 25);
            this.S_Up.TabIndex = 31;
            this.S_Up.Text = "label4";
            this.S_Up.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // S_Down
            // 
            this.S_Down.BackColor = System.Drawing.SystemColors.Info;
            this.S_Down.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.S_Down.CausesValidation = false;
            this.S_Down.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.S_Down.Location = new System.Drawing.Point(32, 110);
            this.S_Down.Name = "S_Down";
            this.S_Down.Size = new System.Drawing.Size(90, 25);
            this.S_Down.TabIndex = 32;
            this.S_Down.Text = "label5";
            this.S_Down.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Key1
            // 
            this.Key1.BackColor = System.Drawing.SystemColors.Info;
            this.Key1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Key1.CausesValidation = false;
            this.Key1.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Key1.Location = new System.Drawing.Point(133, 110);
            this.Key1.Name = "Key1";
            this.Key1.Size = new System.Drawing.Size(90, 25);
            this.Key1.TabIndex = 33;
            this.Key1.Text = "label6";
            this.Key1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Key2
            // 
            this.Key2.BackColor = System.Drawing.SystemColors.Info;
            this.Key2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Key2.CausesValidation = false;
            this.Key2.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Key2.Location = new System.Drawing.Point(182, 79);
            this.Key2.Name = "Key2";
            this.Key2.Size = new System.Drawing.Size(90, 25);
            this.Key2.TabIndex = 34;
            this.Key2.Text = "label7";
            this.Key2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Key3
            // 
            this.Key3.BackColor = System.Drawing.SystemColors.Info;
            this.Key3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Key3.CausesValidation = false;
            this.Key3.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Key3.Location = new System.Drawing.Point(229, 110);
            this.Key3.Name = "Key3";
            this.Key3.Size = new System.Drawing.Size(90, 25);
            this.Key3.TabIndex = 35;
            this.Key3.Text = "label10";
            this.Key3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Key5
            // 
            this.Key5.BackColor = System.Drawing.SystemColors.Info;
            this.Key5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Key5.CausesValidation = false;
            this.Key5.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Key5.Location = new System.Drawing.Point(325, 110);
            this.Key5.Name = "Key5";
            this.Key5.Size = new System.Drawing.Size(90, 25);
            this.Key5.TabIndex = 37;
            this.Key5.Text = "label11";
            this.Key5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Key4
            // 
            this.Key4.BackColor = System.Drawing.SystemColors.Info;
            this.Key4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Key4.CausesValidation = false;
            this.Key4.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Key4.Location = new System.Drawing.Point(278, 79);
            this.Key4.Name = "Key4";
            this.Key4.Size = new System.Drawing.Size(90, 25);
            this.Key4.TabIndex = 36;
            this.Key4.Text = "label12";
            this.Key4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Key6
            // 
            this.Key6.BackColor = System.Drawing.SystemColors.Info;
            this.Key6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Key6.CausesValidation = false;
            this.Key6.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Key6.Location = new System.Drawing.Point(374, 79);
            this.Key6.Name = "Key6";
            this.Key6.Size = new System.Drawing.Size(90, 25);
            this.Key6.TabIndex = 38;
            this.Key6.Text = "label13";
            this.Key6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AllDayKeys
            // 
            this.AllDayKeys.BackColor = System.Drawing.SystemColors.Info;
            this.AllDayKeys.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AllDayKeys.CausesValidation = false;
            this.AllDayKeys.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AllDayKeys.Location = new System.Drawing.Point(374, 40);
            this.AllDayKeys.Name = "AllDayKeys";
            this.AllDayKeys.Size = new System.Drawing.Size(90, 25);
            this.AllDayKeys.TabIndex = 41;
            this.AllDayKeys.Text = "label14";
            this.AllDayKeys.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Key7
            // 
            this.Key7.BackColor = System.Drawing.SystemColors.Info;
            this.Key7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Key7.CausesValidation = false;
            this.Key7.Font = new System.Drawing.Font("源ノ角ゴシック Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Key7.Location = new System.Drawing.Point(421, 110);
            this.Key7.Name = "Key7";
            this.Key7.Size = new System.Drawing.Size(90, 25);
            this.Key7.TabIndex = 39;
            this.Key7.Text = "label15";
            this.Key7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BeatCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 161);
            this.Controls.Add(this.Key7);
            this.Controls.Add(this.AllDayKeys);
            this.Controls.Add(this.Key6);
            this.Controls.Add(this.Key4);
            this.Controls.Add(this.Key5);
            this.Controls.Add(this.Key3);
            this.Controls.Add(this.Key2);
            this.Controls.Add(this.Key1);
            this.Controls.Add(this.S_Down);
            this.Controls.Add(this.S_Up);
            this.Controls.Add(this.TodayKeys);
            this.Controls.Add(this.T_AllDayKeys);
            this.Controls.Add(this.T_TodayKeys);
            this.Controls.Add(this.T_Key7);
            this.Controls.Add(this.T_Key5);
            this.Controls.Add(this.T_Key4);
            this.Controls.Add(this.T_Key6);
            this.Controls.Add(this.T_Key3);
            this.Controls.Add(this.T_Key2);
            this.Controls.Add(this.T_Key1);
            this.Controls.Add(this.T_S_Down);
            this.Controls.Add(this.T_S_Up);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BeatCounter";
            this.Text = "Beat Counter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BeatCounter_Close);
            this.Load += new System.EventHandler(this.BeatCounter_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem PlayGameTips;
        private System.Windows.Forms.ToolStripMenuItem InfinitasPlayTips;
        private System.Windows.Forms.ToolStripMenuItem BmsPlayTips;
        private System.Windows.Forms.ToolStripMenuItem ControllerTips;
        private System.Windows.Forms.ToolStripMenuItem DaoTips;
        private System.Windows.Forms.ToolStripMenuItem PhoenixTips;
        private System.Windows.Forms.ToolStripMenuItem コンフィグToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SChangeTips;
        private System.Windows.Forms.ToolStripMenuItem ResetTips;
        private System.Windows.Forms.ToolStripMenuItem TodayClearTips;
        private System.Windows.Forms.ToolStripMenuItem AlldayClearTips;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TodayShortTips;
        private System.Windows.Forms.ToolStripMenuItem CountChangeTips;
        private System.Windows.Forms.TextBox T_S_Up;
        private System.Windows.Forms.TextBox T_S_Down;
        private System.Windows.Forms.TextBox T_Key1;
        private System.Windows.Forms.TextBox T_Key2;
        private System.Windows.Forms.TextBox T_Key3;
        private System.Windows.Forms.TextBox T_Key6;
        private System.Windows.Forms.TextBox T_Key4;
        private System.Windows.Forms.TextBox T_Key5;
        private System.Windows.Forms.TextBox T_Key7;
        private System.Windows.Forms.TextBox T_TodayKeys;
        private System.Windows.Forms.TextBox T_AllDayKeys;
        private System.Windows.Forms.Label TodayKeys;
        private System.Windows.Forms.Label S_Up;
        private System.Windows.Forms.Label S_Down;
        private System.Windows.Forms.Label Key1;
        private System.Windows.Forms.Label Key2;
        private System.Windows.Forms.Label Key3;
        private System.Windows.Forms.Label Key5;
        private System.Windows.Forms.Label Key4;
        private System.Windows.Forms.Label Key6;
        private System.Windows.Forms.Label AllDayKeys;
        private System.Windows.Forms.Label Key7;
    }
}

