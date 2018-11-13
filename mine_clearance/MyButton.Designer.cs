namespace mine_clearance
{
    partial class MyButton
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnText
            // 
            this.BtnText.AutoSize = true;
            this.BtnText.BackColor = System.Drawing.Color.Transparent;
            this.BtnText.Font = new System.Drawing.Font("楷体", 13F, System.Drawing.FontStyle.Bold);
            this.BtnText.Location = new System.Drawing.Point(2, 3);
            this.BtnText.Name = "BtnText";
            this.BtnText.Size = new System.Drawing.Size(18, 18);
            this.BtnText.TabIndex = 0;
            this.BtnText.Text = "*";
            this.BtnText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnText_MouseDown);
            this.BtnText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnText_MouseUp);
            // 
            // MyButton
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.BtnText);
            this.Name = "MyButton";
            this.Size = new System.Drawing.Size(20, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BtnText;
    }
}
