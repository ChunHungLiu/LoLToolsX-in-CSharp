﻿namespace LoLToolsX
{
    partial class ServerSelect
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
            this.selectKR = new System.Windows.Forms.Button();
            this.selectNA = new System.Windows.Forms.Button();
            this.selectTW = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectKR
            // 
            this.selectKR.Enabled = false;
            this.selectKR.Location = new System.Drawing.Point(62, 238);
            this.selectKR.Name = "selectKR";
            this.selectKR.Size = new System.Drawing.Size(118, 49);
            this.selectKR.TabIndex = 5;
            this.selectKR.Text = "韓服";
            this.selectKR.UseVisualStyleBackColor = true;
            // 
            // selectNA
            // 
            this.selectNA.Location = new System.Drawing.Point(62, 133);
            this.selectNA.Name = "selectNA";
            this.selectNA.Size = new System.Drawing.Size(118, 49);
            this.selectNA.TabIndex = 4;
            this.selectNA.Text = "美服";
            this.selectNA.UseVisualStyleBackColor = true;
            // 
            // selectTW
            // 
            this.selectTW.Location = new System.Drawing.Point(62, 36);
            this.selectTW.Name = "selectTW";
            this.selectTW.Size = new System.Drawing.Size(118, 49);
            this.selectTW.TabIndex = 3;
            this.selectTW.Text = "台服";
            this.selectTW.UseVisualStyleBackColor = true;
            this.selectTW.Click += new System.EventHandler(this.selectTW_Click);
            // 
            // ServerSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 330);
            this.Controls.Add(this.selectKR);
            this.Controls.Add(this.selectNA);
            this.Controls.Add(this.selectTW);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "請選擇客戶端";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ServerSelect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button selectKR;
        internal System.Windows.Forms.Button selectNA;
        internal System.Windows.Forms.Button selectTW;

    }
}