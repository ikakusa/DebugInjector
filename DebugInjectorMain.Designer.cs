namespace DebugInjector
{
    partial class DebugInjectorMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugInjectorMain));
            this.inject = new System.Windows.Forms.Button();
            this.ps = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.check = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.addlist = new System.Windows.Forms.Button();
            this.openmc = new System.Windows.Forms.Button();
            this.closemc = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.upload = new System.Windows.Forms.Button();
            this.loaddllfromurl = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.current = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.mcstat = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.urlbox = new System.Windows.Forms.TextBox();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // inject
            // 
            this.inject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.inject.FlatAppearance.BorderSize = 0;
            this.inject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.inject.ForeColor = System.Drawing.Color.White;
            this.inject.Location = new System.Drawing.Point(337, 189);
            this.inject.Margin = new System.Windows.Forms.Padding(2);
            this.inject.Name = "inject";
            this.inject.Size = new System.Drawing.Size(95, 40);
            this.inject.TabIndex = 0;
            this.inject.Text = "Inject Dll";
            this.toolTip1.SetToolTip(this.inject, "Right click to load dll from explorer");
            this.inject.UseVisualStyleBackColor = false;
            this.inject.Click += new System.EventHandler(this.inject_Click);
            this.inject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.inject_MouseDown);
            // 
            // ps
            // 
            this.ps.AllowDrop = true;
            this.ps.AutoScroll = true;
            this.ps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ps.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ps.Location = new System.Drawing.Point(9, 40);
            this.ps.Margin = new System.Windows.Forms.Padding(2);
            this.ps.Name = "ps";
            this.ps.Size = new System.Drawing.Size(201, 254);
            this.ps.TabIndex = 1;
            this.ps.DragDrop += new System.Windows.Forms.DragEventHandler(this.ps_DragDrop);
            this.ps.DragEnter += new System.Windows.Forms.DragEventHandler(this.ps_DragEnter);
            this.ps.Paint += new System.Windows.Forms.PaintEventHandler(this.ps_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Dll paths";
            // 
            // check
            // 
            this.check.AutoSize = true;
            this.check.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.check.ForeColor = System.Drawing.Color.White;
            this.check.Location = new System.Drawing.Point(236, 242);
            this.check.Margin = new System.Windows.Forms.Padding(2);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(103, 21);
            this.check.TabIndex = 3;
            this.check.Text = "Inject Check";
            this.check.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "SulfurDev";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(448, 189);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 40);
            this.button1.TabIndex = 6;
            this.button1.Text = "Save Data";
            this.toolTip1.SetToolTip(this.button1, "Save current dll path lists");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addlist
            // 
            this.addlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.addlist.FlatAppearance.BorderSize = 0;
            this.addlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.addlist.ForeColor = System.Drawing.Color.White;
            this.addlist.Location = new System.Drawing.Point(448, 141);
            this.addlist.Margin = new System.Windows.Forms.Padding(2);
            this.addlist.Name = "addlist";
            this.addlist.Size = new System.Drawing.Size(79, 40);
            this.addlist.TabIndex = 7;
            this.addlist.Text = "Add to List";
            this.toolTip1.SetToolTip(this.addlist, "Right click to add current dll");
            this.addlist.UseVisualStyleBackColor = false;
            this.addlist.Click += new System.EventHandler(this.addlist_Click);
            this.addlist.MouseUp += new System.Windows.Forms.MouseEventHandler(this.addlist_MouseUp);
            // 
            // openmc
            // 
            this.openmc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.openmc.FlatAppearance.BorderSize = 0;
            this.openmc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openmc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.openmc.ForeColor = System.Drawing.Color.White;
            this.openmc.Location = new System.Drawing.Point(447, 93);
            this.openmc.Margin = new System.Windows.Forms.Padding(2);
            this.openmc.Name = "openmc";
            this.openmc.Size = new System.Drawing.Size(79, 40);
            this.openmc.TabIndex = 9;
            this.openmc.Text = "Open MC";
            this.toolTip1.SetToolTip(this.openmc, "Open Minecraft");
            this.openmc.UseVisualStyleBackColor = false;
            this.openmc.Click += new System.EventHandler(this.openmc_Click);
            // 
            // closemc
            // 
            this.closemc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.closemc.FlatAppearance.BorderSize = 0;
            this.closemc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closemc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.closemc.ForeColor = System.Drawing.Color.White;
            this.closemc.Location = new System.Drawing.Point(448, 46);
            this.closemc.Margin = new System.Windows.Forms.Padding(2);
            this.closemc.Name = "closemc";
            this.closemc.Size = new System.Drawing.Size(79, 40);
            this.closemc.TabIndex = 10;
            this.closemc.Text = "Kill MC";
            this.toolTip1.SetToolTip(this.closemc, "Close Minecraft");
            this.closemc.UseVisualStyleBackColor = false;
            this.closemc.Click += new System.EventHandler(this.closemc_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(236, 189);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 40);
            this.button2.TabIndex = 11;
            this.button2.Text = "Uninject Dll";
            this.toolTip1.SetToolTip(this.button2, "Not working :rage:");
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(236, 93);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(196, 40);
            this.button3.TabIndex = 12;
            this.button3.Text = "Open Roaming Folder";
            this.toolTip1.SetToolTip(this.button3, "Open RoamingState folder");
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // upload
            // 
            this.upload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.upload.FlatAppearance.BorderSize = 0;
            this.upload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.upload.ForeColor = System.Drawing.Color.White;
            this.upload.Location = new System.Drawing.Point(236, 141);
            this.upload.Margin = new System.Windows.Forms.Padding(2);
            this.upload.Name = "upload";
            this.upload.Size = new System.Drawing.Size(196, 40);
            this.upload.TabIndex = 13;
            this.upload.Text = "Upload dll in GoFile.io";
            this.toolTip1.SetToolTip(this.upload, "Upload current dll file in GoFile");
            this.upload.UseVisualStyleBackColor = false;
            this.upload.Click += new System.EventHandler(this.upload_Click);
            // 
            // loaddllfromurl
            // 
            this.loaddllfromurl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.loaddllfromurl.FlatAppearance.BorderSize = 0;
            this.loaddllfromurl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loaddllfromurl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.loaddllfromurl.ForeColor = System.Drawing.Color.White;
            this.loaddllfromurl.Location = new System.Drawing.Point(439, 298);
            this.loaddllfromurl.Margin = new System.Windows.Forms.Padding(2);
            this.loaddllfromurl.Name = "loaddllfromurl";
            this.loaddllfromurl.Size = new System.Drawing.Size(95, 40);
            this.loaddllfromurl.TabIndex = 15;
            this.loaddllfromurl.Text = "Load";
            this.toolTip1.SetToolTip(this.loaddllfromurl, "Right click to save in local");
            this.loaddllfromurl.UseVisualStyleBackColor = false;
            this.loaddllfromurl.Click += new System.EventHandler(this.loaddllfromurl_Click);
            this.loaddllfromurl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.loaddllfromurl_MouseUp);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(14, 306);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(196, 29);
            this.button4.TabIndex = 17;
            this.button4.Text = "Clean Temp Folder";
            this.toolTip1.SetToolTip(this.button4, "Clean Temp folder!");
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // current
            // 
            this.current.AutoSize = true;
            this.current.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.current.ForeColor = System.Drawing.Color.White;
            this.current.Location = new System.Drawing.Point(232, 26);
            this.current.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.current.Name = "current";
            this.current.Size = new System.Drawing.Size(88, 20);
            this.current.TabIndex = 4;
            this.current.Text = "Current Dll:";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.status.ForeColor = System.Drawing.Color.White;
            this.status.Location = new System.Drawing.Point(443, 246);
            this.status.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(83, 17);
            this.status.TabIndex = 5;
            this.status.Text = "Not Injected";
            // 
            // mcstat
            // 
            this.mcstat.AutoSize = true;
            this.mcstat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mcstat.ForeColor = System.Drawing.Color.White;
            this.mcstat.Location = new System.Drawing.Point(232, 55);
            this.mcstat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mcstat.Name = "mcstat";
            this.mcstat.Size = new System.Drawing.Size(169, 20);
            this.mcstat.TabIndex = 8;
            this.mcstat.Text = "Minecraft: Not Opened";
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(232, 274);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Load dll from url";
            // 
            // urlbox
            // 
            this.urlbox.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.urlbox.Location = new System.Drawing.Point(236, 306);
            this.urlbox.Multiline = true;
            this.urlbox.Name = "urlbox";
            this.urlbox.Size = new System.Drawing.Size(190, 29);
            this.urlbox.TabIndex = 16;
            // 
            // DebugInjectorMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(547, 349);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.urlbox);
            this.Controls.Add(this.loaddllfromurl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.upload);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.closemc);
            this.Controls.Add(this.openmc);
            this.Controls.Add(this.mcstat);
            this.Controls.Add(this.addlist);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.status);
            this.Controls.Add(this.current);
            this.Controls.Add(this.check);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ps);
            this.Controls.Add(this.inject);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(563, 388);
            this.MinimumSize = new System.Drawing.Size(563, 388);
            this.Name = "DebugInjectorMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Debug Injector | Made by ikakusa_";
            this.Load += new System.EventHandler(this.DebugInjectorMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ps_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ps_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button inject;
        private System.Windows.Forms.Panel ps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox check;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label current;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button addlist;
        private System.Windows.Forms.Label mcstat;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button openmc;
        private System.Windows.Forms.Button closemc;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button upload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loaddllfromurl;
        private System.Windows.Forms.TextBox urlbox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ToolTip toolTip2;
    }
}

