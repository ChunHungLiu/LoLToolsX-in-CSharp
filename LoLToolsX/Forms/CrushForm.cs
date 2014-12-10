﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using LoLToolsX.Core;

namespace LoLToolsX
{
    public partial class CrushForm : Form
    {
        string message = "";
        string stack = "";

        public CrushForm(string _message,string _stack)
        {
            this.message = _message;
            this.stack = _stack;

            InitializeComponent();
        }

        private void CrushForm_Load(object sender, EventArgs e)
        {
            textBox1.Text += "程式發生未處理的錯誤!\r\n\r\n";
            textBox1.Text += message + "\r\n\r\n";
            textBox1.Text += stack;
            textBox1.Select(0, 0);
            Logger.log(textBox1.Text, Logger.LogType.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool sucess;
            Wait wait = new Wait();
            if (!Variable.haveUpdate)
            {
                    Logger.log("傳送錯誤報告");
                       sucess = Utility.UploadLogs();
                       if (sucess)
                       {
                           MessageBox.Show("傳送錯誤報告成功!");
                           Environment.Exit(0);
                       }
                       else
                       {
                           MessageBox.Show("傳送錯誤報告失敗!");
                       }
            }
            else
            {
                MessageBox.Show("正在檢查更新... 請稍後再傳送錯誤報告");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("http://nitroxenon.com/guestbook/");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("http://nitroxenon.com/");
        }
    }
}
