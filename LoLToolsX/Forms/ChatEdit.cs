using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ChatColour;
using LoLToolsX.Core;

namespace LoLToolsX
{
    public partial class ChatEdit : Form
    {
        string installPath;
        CFGFile ini = new CFGFile(Application.StartupPath + @"\config.ini");
        ChatColour.ChatColour cc = new ChatColour.ChatColour();


        public ChatEdit(string installPath)
        {
            InitializeComponent();
            this.installPath = installPath;
        }

        private void ChatEdit_Load(object sender, EventArgs e)
        {
            TextBox[] rArr =    {R,
                                    RS,
                                    R2,
                                    R3,
                                    R4,
                                    R5
                                    };

            TextBox[] gArr =    {G,
                                    GS,
                                    G2,
                                    G3,
                                    G4,
                                    G5
                                    };

            TextBox[] bArr =    {B,
                                    BS,
                                    B2,
                                    B3,
                                    B4,
                                    B5
                                    };
            
            string[] rTmp = {
                                "R",
                                "RS",
                                "R2",
                                "R3",
                                "R4",
                                "R5"
                            };
            string[] gTmp = {
                                "G",
                                "GS",
                                "G2",
                                "G3",
                                "G4",
                                "G5"
                            };
            string[] bTmp = {
                                "B",
                                "BS",
                                "B2",
                                "B3",
                                "B4",
                                "B5"
                            };

            for (int i=0;i<=5;i++)
            {
                rArr[i].Text = ini.GetValue("ChatColour", rTmp[i]);
            }
            for (int i = 0; i <= 5; i++)
            {
                gArr[i].Text = ini.GetValue("ChatColour", gTmp[i]);
            }
            for (int i = 0; i <= 5; i++)
            {
                bArr[i].Text = ini.GetValue("ChatColour", bTmp[i]);
            }

            FontSize1.Text = ini.GetValue("ChatFontSize", "FontSize");
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                R.Text = colorDialog1.Color.R.ToString();
                B.Text = colorDialog1.Color.B.ToString();
                G.Text = colorDialog1.Color.G.ToString();
            }

        }

        private void Button14_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                RS.Text = colorDialog1.Color.R.ToString();
                BS.Text = colorDialog1.Color.B.ToString();
                GS.Text = colorDialog1.Color.G.ToString();
            }
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                R2.Text = colorDialog1.Color.R.ToString();
                B2.Text = colorDialog1.Color.B.ToString();
                G2.Text = colorDialog1.Color.G.ToString();
            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                R3.Text = colorDialog1.Color.R.ToString();
                B3.Text = colorDialog1.Color.B.ToString();
                G3.Text = colorDialog1.Color.G.ToString();
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                R4.Text = colorDialog1.Color.R.ToString();
                B4.Text = colorDialog1.Color.B.ToString();
                G4.Text = colorDialog1.Color.G.ToString();
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                R5.Text = colorDialog1.Color.R.ToString();
                B5.Text = colorDialog1.Color.B.ToString();
                G5.Text = colorDialog1.Color.G.ToString();
            }
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "B", B.Text);
            ini.SetValue("ChatColour", "G", G.Text);
            ini.SetValue("ChatColour", "B", B.Text);
            cc.WriteFriendlyColour(installPath, R.Text, G.Text, B.Text);
            Logger.log("對話框文字顏色修改成功 : " + R.Text + " " + G.Text + " " + B.Text,Logger.LogType.Info);
            MessageBox.Show("修改成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "BS", BS.Text);
            ini.SetValue("ChatColour", "GS", GS.Text);
            ini.SetValue("ChatColour", "BS", BS.Text);
            cc.WriteTeamChatColour(installPath, RS.Text, GS.Text, BS.Text);
            Logger.log("對話框文字顏色修改成功 : " + RS.Text + " " + GS.Text + " " + BS.Text, Logger.LogType.Info);
            MessageBox.Show("修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "B2", B2.Text);
            ini.SetValue("ChatColour", "G2", G2.Text);
            ini.SetValue("ChatColour", "B2", B2.Text);
            cc.WriteAllChatColor(installPath, R2.Text, G2.Text, B2.Text);
            Logger.log("對話框文字顏色修改成功 : " + R2.Text + " " + G2.Text + " " + B2.Text, Logger.LogType.Info);
            MessageBox.Show("修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "B3", B3.Text);
            ini.SetValue("ChatColour", "G3", G3.Text);
            ini.SetValue("ChatColour", "B3", B3.Text);
            cc.WriteEnemyColor(installPath, R3.Text, G3.Text, B3.Text);
            Logger.log("對話框文字顏色修改成功 : " + R3.Text + " " + G3.Text + " " + B3.Text, Logger.LogType.Info);
            MessageBox.Show("修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "B4", B4.Text);
            ini.SetValue("ChatColour", "G4", G4.Text);
            ini.SetValue("ChatColour", "B4", B4.Text);
            cc.WriteWhisperColor(installPath, R4.Text, G4.Text, B4.Text);
            Logger.log("對話框文字顏色修改成功 : " + R4.Text + " " + G4.Text + " " + B4.Text, Logger.LogType.Info);
            MessageBox.Show("修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "B5", B5.Text);
            ini.SetValue("ChatColour", "G5", G5.Text);
            ini.SetValue("ChatColour", "B5", B5.Text);
            cc.WriteTimestampColor(installPath, R5.Text, G5.Text, B5.Text);
            Logger.log("對話框文字顏色修改成功 : " + R5.Text + " " + G5.Text + " " + B5.Text, Logger.LogType.Info);
            MessageBox.Show("修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "B", "0");
            ini.SetValue("ChatColour", "G", "255");
            ini.SetValue("ChatColour", "B", "0");
            cc.WriteFriendlyColour(installPath, "0","255","0");
            Logger.log("對話框文字顏色 恢復預設值成功!", Logger.LogType.Info);
            MessageBox.Show("恢復成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "BS", "255");
            ini.SetValue("ChatColour", "GS", "255");
            ini.SetValue("ChatColour", "BS", "255");
            cc.WriteTeamChatColour(installPath, "255", "255", "255");
            Logger.log("對話框文字顏色 恢復預設值成功!", Logger.LogType.Info);
            MessageBox.Show("恢復成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "B2", "255");
            ini.SetValue("ChatColour", "G2", "255");
            ini.SetValue("ChatColour", "B2", "255");
            cc.WriteAllChatColor(installPath, "255", "255", "255");
            Logger.log("對話框文字顏色 恢復預設值成功!", Logger.LogType.Info);
            MessageBox.Show("恢復成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "B3", "255");
            ini.SetValue("ChatColour", "G3", "51");
            ini.SetValue("ChatColour", "B3", "51");
            cc.WriteEnemyColor(installPath, "255", "51", "51");
            Logger.log("對話框文字顏色 恢復預設值成功!", Logger.LogType.Info);
            MessageBox.Show("恢復成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "B4", "241");
            ini.SetValue("ChatColour", "G4", "231");
            ini.SetValue("ChatColour", "B4", "22");
            cc.WriteWhisperColor(installPath, "241", "231", "22");
            Logger.log("對話框文字顏色 恢復預設值成功!", Logger.LogType.Info);
            MessageBox.Show("恢復成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "B5", "220");
            ini.SetValue("ChatColour", "G5", "220");
            ini.SetValue("ChatColour", "B5", "220");
            cc.WriteTimestampColor(installPath, "220", "220", "220");
            Logger.log("對話框文字顏色 恢復預設值成功!", Logger.LogType.Info);
            MessageBox.Show("恢復成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatColour", "R", "0");
            ini.SetValue("ChatColour", "G", "255");
            ini.SetValue("ChatColour", "B", "0");
            cc.WriteFriendlyColour(installPath, "0", "255", "0");
            R.Text = "0";
            G.Text = "255";
            B.Text = "0";

            ini.SetValue("ChatColour", "R2", "255");
            ini.SetValue("ChatColour", "G2", "255");
            ini.SetValue("ChatColour", "B2", "255");
            cc.WriteAllChatColor(installPath, "255", "255", "255");
            R2.Text = "255";
            G2.Text = "255";
            B2.Text = "255";

            ini.SetValue("ChatColour", "R3", "255");
            ini.SetValue("ChatColour", "G3", "51");
            ini.SetValue("ChatColour", "B3", "51");
            cc.WriteEnemyColor(installPath, "255", "51", "51");
            R3.Text = "255";
            G3.Text = "51";
            B3.Text = "51";

            ini.SetValue("ChatColour", "R4", "241");
            ini.SetValue("ChatColour", "G4", "231");
            ini.SetValue("ChatColour", "B4", "22");
            cc.WriteWhisperColor(installPath, "241", "231", "22");
            R4.Text = "241";
            G4.Text = "231";
            B4.Text = "22";

            ini.SetValue("ChatColour", "R5", "220");
            ini.SetValue("ChatColour", "G5", "220");
            ini.SetValue("ChatColour", "B5", "220");
            cc.WriteTimestampColor(installPath, "220", "220", "220");
            R5.Text = "220";
            G5.Text = "220";
            B5.Text = "220";

            ini.SetValue("ChatColour", "RS", "255");
            ini.SetValue("ChatColour", "GS", "255");
            ini.SetValue("ChatColour", "BS", "255");
            cc.WriteTeamChatColour(installPath, "255", "255", "255");
            RS.Text = "255";
            GS.Text = "255";
            BS.Text = "255";

            ini.SetValue("ChatFontSize", "FontSize", "20");
            cc.WriteFontSize(installPath, 20);
            FontSize1.Text = "20";

            Logger.log("對話框文字 : 一鍵恢復預設值成功!", Logger.LogType.Info);
            MessageBox.Show("一鍵恢復預設值成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatFontSize", "FontSize", FontSize1.Text);
            cc.WriteFontSize(installPath, Convert.ToInt32(FontSize1.Text));
            Logger.log("對話框文字 字體大小修改成功 : " + FontSize1.Text, Logger.LogType.Info);
            MessageBox.Show("修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            ini.SetValue("ChatFontSize", "FontSize", "20");
            cc.WriteFontSize(installPath, 20);
            Logger.log("對話框文字 恢復預設值成功!", Logger.LogType.Info);
            MessageBox.Show("恢復成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Chat(1);
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Chat(2);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Chat(3);
        }

    }
}
