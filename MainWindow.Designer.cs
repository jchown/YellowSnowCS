namespace YellowSnow
{
    partial class MainWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowSnowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkBruisesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textView = new System.Windows.Forms.WebBrowser();
            this.mapView = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1714, 49);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(75, 45);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(396, 46);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(396, 46);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(396, 46);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(94, 45);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yellowSnowToolStripMenuItem,
            this.darkBruisesToolStripMenuItem});
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(223, 46);
            this.themeToolStripMenuItem.Text = "Theme";
            // 
            // yellowSnowToolStripMenuItem
            // 
            this.yellowSnowToolStripMenuItem.Name = "yellowSnowToolStripMenuItem";
            this.yellowSnowToolStripMenuItem.Size = new System.Drawing.Size(297, 46);
            this.yellowSnowToolStripMenuItem.Text = "Yellow Snow";
            // 
            // darkBruisesToolStripMenuItem
            // 
            this.darkBruisesToolStripMenuItem.Name = "darkBruisesToolStripMenuItem";
            this.darkBruisesToolStripMenuItem.Size = new System.Drawing.Size(297, 46);
            this.darkBruisesToolStripMenuItem.Text = "Dark Bruises";
            // 
            // textView
            // 
            this.textView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textView.Location = new System.Drawing.Point(0, 49);
            this.textView.MinimumSize = new System.Drawing.Size(20, 20);
            this.textView.Name = "textView";
            this.textView.Size = new System.Drawing.Size(1503, 903);
            this.textView.TabIndex = 1;
            this.textView.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.OnTextViewRegionChanged);
            this.textView.RegionChanged += new System.EventHandler(this.OnTextViewRegionChanged);
            this.textView.SizeChanged += new System.EventHandler(this.OnTextViewRegionChanged);
            this.textView.Resize += new System.EventHandler(this.OnTextViewRegionChanged);
            // 
            // mapView
            // 
            this.mapView.Dock = System.Windows.Forms.DockStyle.Right;
            this.mapView.Location = new System.Drawing.Point(1503, 49);
            this.mapView.Name = "mapView";
            this.mapView.Size = new System.Drawing.Size(211, 903);
            this.mapView.TabIndex = 2;
            this.mapView.TabStop = false;
            this.mapView.ClientSizeChanged += new System.EventHandler(this.OnMapViewResized);
            this.mapView.Click += new System.EventHandler(this.OnMapViewClicked);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1714, 952);
            this.Controls.Add(this.textView);
            this.Controls.Add(this.mapView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "YellowSnow";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yellowSnowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkBruisesToolStripMenuItem;
        private System.Windows.Forms.WebBrowser textView;
        private System.Windows.Forms.PictureBox mapView;
    }
}

