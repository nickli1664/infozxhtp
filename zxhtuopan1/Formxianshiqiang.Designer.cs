namespace zxhtuopan1
{
    partial class Formxianshiqiang
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("大屏控制器");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("矩阵");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("硬盘录像机");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("网络摄像机");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("流媒体");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Transmitter");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("投影设备");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formxianshiqiang));
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("视频", 4, 4);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("图片", 2, 2);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("文档", 3, 3);
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("网页", 5, 5);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageListdevice = new System.Windows.Forms.ImageList(this.components);
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.controlimg1 = new System.Windows.Forms.ImageList(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageListdevice;
            this.treeView1.Location = new System.Drawing.Point(559, 50);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "1";
            treeNode1.NodeFont = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode1.Text = "大屏控制器";
            treeNode2.Name = "2";
            treeNode2.NodeFont = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode2.Text = "矩阵";
            treeNode3.Name = "3";
            treeNode3.NodeFont = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode3.Text = "硬盘录像机";
            treeNode4.Name = "4";
            treeNode4.NodeFont = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode4.Text = "网络摄像机";
            treeNode5.Name = "5";
            treeNode5.NodeFont = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode5.Text = "流媒体";
            treeNode6.Name = "6";
            treeNode6.NodeFont = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode6.Text = "Transmitter";
            treeNode7.Name = "7";
            treeNode7.NodeFont = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode7.Text = "投影设备";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(183, 432);
            this.treeView1.TabIndex = 1;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            // 
            // imageListdevice
            // 
            this.imageListdevice.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListdevice.ImageStream")));
            this.imageListdevice.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListdevice.Images.SetKeyName(0, "testnull.png");
            this.imageListdevice.Images.SetKeyName(1, "airplay_default_thumbnail.png");
            this.imageListdevice.Images.SetKeyName(2, "mediaFolderCommon.png");
            this.imageListdevice.Images.SetKeyName(3, "application_default_thumbnail.png");
            this.imageListdevice.Images.SetKeyName(4, "capture_default_thumbnail.png");
            this.imageListdevice.Images.SetKeyName(5, "controller_default_thumbnail.png");
            this.imageListdevice.Images.SetKeyName(6, "ipc_channel_default_thumbnail.png");
            this.imageListdevice.Images.SetKeyName(7, "ipc_default_thumbnail.png");
            this.imageListdevice.Images.SetKeyName(8, "streaming_default_thumbnail.png");
            this.imageListdevice.Images.SetKeyName(9, "matrix_default_thumbnail.png");
            this.imageListdevice.Images.SetKeyName(10, "matrix_source_default_thumbnail.png");
            this.imageListdevice.Images.SetKeyName(11, "DVRNVR_default_thumbnail.png");
            this.imageListdevice.Images.SetKeyName(12, "trans.png");
            // 
            // treeView2
            // 
            this.treeView2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView2.ImageIndex = 1;
            this.treeView2.ImageList = this.imageList1;
            this.treeView2.Location = new System.Drawing.Point(370, 50);
            this.treeView2.Name = "treeView2";
            treeNode8.ImageIndex = 4;
            treeNode8.Name = "11";
            treeNode8.SelectedImageIndex = 4;
            treeNode8.Text = "视频";
            treeNode9.ImageIndex = 2;
            treeNode9.Name = "12";
            treeNode9.SelectedImageIndex = 2;
            treeNode9.Text = "图片";
            treeNode10.ImageIndex = 3;
            treeNode10.Name = "13";
            treeNode10.SelectedImageIndex = 3;
            treeNode10.Text = "文档";
            treeNode11.ImageIndex = 5;
            treeNode11.Name = "14";
            treeNode11.SelectedImageIndex = 5;
            treeNode11.Text = "网页";
            this.treeView2.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            this.treeView2.SelectedImageIndex = 1;
            this.treeView2.Size = new System.Drawing.Size(183, 432);
            this.treeView2.TabIndex = 1;
            this.treeView2.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView2_BeforeExpand);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "test1_fuben1.png");
            this.imageList1.Images.SetKeyName(1, "mediaFolderCommon.png");
            this.imageList1.Images.SetKeyName(2, "mediaFolderImage.png");
            this.imageList1.Images.SetKeyName(3, "mediaFolderOffice.png");
            this.imageList1.Images.SetKeyName(4, "mediaFolderVideo.png");
            this.imageList1.Images.SetKeyName(5, "mediaFolderWeb.png");
            this.imageList1.Images.SetKeyName(6, "testnull.png");
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SteelBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(24, 362);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 120);
            this.button1.TabIndex = 2;
            this.button1.Text = "返回";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SteelBlue;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ImageIndex = 0;
            this.button2.ImageList = this.controlimg1;
            this.button2.Location = new System.Drawing.Point(150, 403);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(47, 47);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // controlimg1
            // 
            this.controlimg1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("controlimg1.ImageStream")));
            this.controlimg1.TransparentColor = System.Drawing.Color.Transparent;
            this.controlimg1.Images.SetKeyName(0, "realtimeOff.PNG");
            this.controlimg1.Images.SetKeyName(1, "realtimeOn.PNG");
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Black;
            this.button3.Enabled = false;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SteelBlue;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(232, 362);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 120);
            this.button3.TabIndex = 4;
            this.button3.Text = "test";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Black;
            this.button4.Enabled = false;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SteelBlue;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(333, 362);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(120, 120);
            this.button4.TabIndex = 5;
            this.button4.Text = "清屏";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Formxianshiqiang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::zxhtuopan1.Resource1.blackfull100;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(763, 526);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.treeView2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Formxianshiqiang";
            this.Opacity = 0.7D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formxianshiqiang";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Formxianshiqiang_FormClosing);
            this.Load += new System.EventHandler(this.Formxianshiqiang_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ImageList controlimg1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ImageList imageListdevice;
        private System.Windows.Forms.Button button4;
    }
}