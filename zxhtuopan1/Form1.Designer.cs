namespace zxhtuopan1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.设置登录用户名密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置WebServerIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置城市信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置控件大小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "123";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置登录用户名密码ToolStripMenuItem,
            this.设置WebServerIPToolStripMenuItem,
            this.设置城市信息ToolStripMenuItem,
            this.设置控件大小ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(246, 114);
            // 
            // 设置登录用户名密码ToolStripMenuItem
            // 
            this.设置登录用户名密码ToolStripMenuItem.Name = "设置登录用户名密码ToolStripMenuItem";
            this.设置登录用户名密码ToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.设置登录用户名密码ToolStripMenuItem.Text = "设置MaxWall用户名密码IP端口";
            this.设置登录用户名密码ToolStripMenuItem.Click += new System.EventHandler(this.设置登录用户名密码ToolStripMenuItem_Click);
            // 
            // 设置WebServerIPToolStripMenuItem
            // 
            this.设置WebServerIPToolStripMenuItem.Name = "设置WebServerIPToolStripMenuItem";
            this.设置WebServerIPToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.设置WebServerIPToolStripMenuItem.Text = "设置WebServerIP";
            this.设置WebServerIPToolStripMenuItem.Click += new System.EventHandler(this.设置WebServerIPToolStripMenuItem_Click);
            // 
            // 设置城市信息ToolStripMenuItem
            // 
            this.设置城市信息ToolStripMenuItem.Name = "设置城市信息ToolStripMenuItem";
            this.设置城市信息ToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.设置城市信息ToolStripMenuItem.Text = "设置城市信息";
            this.设置城市信息ToolStripMenuItem.Click += new System.EventHandler(this.设置城市信息ToolStripMenuItem_Click);
            // 
            // 设置控件大小ToolStripMenuItem
            // 
            this.设置控件大小ToolStripMenuItem.Name = "设置控件大小ToolStripMenuItem";
            this.设置控件大小ToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.设置控件大小ToolStripMenuItem.Text = "设置控件大小";
            this.设置控件大小ToolStripMenuItem.Click += new System.EventHandler(this.设置控件大小ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(880, 538);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 14);
            this.label1.TabIndex = 2;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_mousedown);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1_mouseup);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(300, 137);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "纯后台";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.form1_Click);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置登录用户名密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置控件大小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem 设置城市信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置WebServerIPToolStripMenuItem;
    }
}

