namespace LoLToolsX
{
    partial class GameLangEdit
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbENUS = new System.Windows.Forms.TextBox();
            this.tbZHTW = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbFinal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLeave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbENUS
            // 
            this.tbENUS.Location = new System.Drawing.Point(12, 102);
            this.tbENUS.Multiline = true;
            this.tbENUS.Name = "tbENUS";
            this.tbENUS.Size = new System.Drawing.Size(469, 181);
            this.tbENUS.TabIndex = 0;
            // 
            // tbZHTW
            // 
            this.tbZHTW.BackColor = System.Drawing.SystemColors.Window;
            this.tbZHTW.Location = new System.Drawing.Point(12, 302);
            this.tbZHTW.Multiline = true;
            this.tbZHTW.Name = "tbZHTW";
            this.tbZHTW.ReadOnly = true;
            this.tbZHTW.Size = new System.Drawing.Size(469, 151);
            this.tbZHTW.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(433, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "請確認下方文字框的文字 跟 上方文字框選取了的文字相同，並按儲存鍵確認修改";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 472);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(384, 33);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "儲存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbFinal
            // 
            this.tbFinal.BackColor = System.Drawing.SystemColors.Window;
            this.tbFinal.Location = new System.Drawing.Point(511, 102);
            this.tbFinal.Multiline = true;
            this.tbFinal.Name = "tbFinal";
            this.tbFinal.ReadOnly = true;
            this.tbFinal.Size = new System.Drawing.Size(331, 351);
            this.tbFinal.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(517, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "修改後預覽 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(361, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "請確認上方文字方框 跟 右邊預覽的文字相同，並按儲存鍵確認修改\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(343, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "如有其中一項不相同，即表示你已進行修改，請按離開按鈕關閉";
            // 
            // btnLeave
            // 
            this.btnLeave.Location = new System.Drawing.Point(458, 472);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(384, 33);
            this.btnLeave.TabIndex = 8;
            this.btnLeave.Text = "離開";
            this.btnLeave.UseVisualStyleBackColor = true;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // GameLangEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 517);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbFinal);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbZHTW);
            this.Controls.Add(this.tbENUS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameLangEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "遊戲語言切換";
            this.Load += new System.EventHandler(this.GameLangEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbENUS;
        private System.Windows.Forms.TextBox tbZHTW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLeave;
    }
}