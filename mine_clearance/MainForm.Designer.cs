namespace mine_clearance
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gamepanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Width = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Height = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Mines = new System.Windows.Forms.TextBox();
            this.flaglabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gamepanel
            // 
            this.gamepanel.Location = new System.Drawing.Point(14, 33);
            this.gamepanel.Name = "gamepanel";
            this.gamepanel.Size = new System.Drawing.Size(200, 100);
            this.gamepanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Width:";
            // 
            // txt_Width
            // 
            this.txt_Width.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txt_Width.Location = new System.Drawing.Point(59, 6);
            this.txt_Width.Name = "txt_Width";
            this.txt_Width.Size = new System.Drawing.Size(46, 21);
            this.txt_Width.TabIndex = 2;
            this.txt_Width.Text = "30";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Height:";
            // 
            // txt_Height
            // 
            this.txt_Height.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txt_Height.Location = new System.Drawing.Point(164, 7);
            this.txt_Height.Name = "txt_Height";
            this.txt_Height.Size = new System.Drawing.Size(46, 21);
            this.txt_Height.TabIndex = 2;
            this.txt_Height.Text = "16";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "Mines:";
            // 
            // txt_Mines
            // 
            this.txt_Mines.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txt_Mines.Location = new System.Drawing.Point(263, 7);
            this.txt_Mines.Name = "txt_Mines";
            this.txt_Mines.Size = new System.Drawing.Size(46, 21);
            this.txt_Mines.TabIndex = 2;
            this.txt_Mines.Text = "99";
            // 
            // flaglabel
            // 
            this.flaglabel.AutoSize = true;
            this.flaglabel.Location = new System.Drawing.Point(332, 9);
            this.flaglabel.Name = "flaglabel";
            this.flaglabel.Size = new System.Drawing.Size(47, 12);
            this.flaglabel.TabIndex = 3;
            this.flaglabel.Text = "flags:0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(399, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "ReStart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 362);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flaglabel);
            this.Controls.Add(this.txt_Mines);
            this.Controls.Add(this.txt_Height);
            this.Controls.Add(this.txt_Width);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gamepanel);
            this.Name = "MainForm";
            this.Text = "扫雷";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel gamepanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Width;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Height;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Mines;
        private System.Windows.Forms.Label flaglabel;
        private System.Windows.Forms.Button button1;

    }
}

