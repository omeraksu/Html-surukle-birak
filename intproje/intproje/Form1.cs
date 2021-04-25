using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using HtmlAgilityPack;

namespace intproje
{
    public partial class Form1 : Form
    {
        public static string url;
        string[] sağpanelnesne = new string[1] {"Div"};
        dragform dr = new dragform();
        Fonksiyonlar f = new Fonksiyonlar();
        Button btn;
        string btnad;
        bool kucuk,nesnea = true;
        public static bool enter, özellika = false;
        bool pclick, aclick,solt = false;
        int pxfark, pyfark, btns;
        public Form1()
        {
            InitializeComponent();
        }

        public static List<nesnem> nesneler = new List<nesnem>();
        public class nesnem
        {
            public int width=100;
            public int height = 100;
            public string classname;
        }

        public void sağtıknesne(object sender, MouseEventArgs e)
        {
            dr.Focus();
            ((Button)sender).DoDragDrop(((Button)sender).Name, DragDropEffects.All);
        }
        public void tıkla(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Right)
            {
                btnad = ((Button)sender).Name;
                panel10.Location = new Point(MousePosition.X - this.Location.X, MousePosition.Y - this.Location.Y-32);
                panel10.Visible = true;
            }
            if (e.Button==MouseButtons.Left)
            {
                panel10.Visible = false;
                Button sec = (Button)sender;
                webBrowser1.Navigate(f.yol + @"\" + sec.Text);
                url = f.yol + @"\" + sec.Text;
                //string[] lines = webBrowser1.DocumentText.Split(new[] { "\r\n", "\r", "\n" },StringSplitOptions.None);
            }
        }

        public void btnolustur(int x,int y,string ad,string text,Panel panel)
        {
            btn = new Button();
            btn.Size = new Size(146,23);
            btn.Location = new Point(x,y);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Name = ad;
            btn.Text = text;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.ForeColor = Color.FromArgb(241,241,241);
            panel.Controls.Add(btn);
        }

        public void listle()
        {
            if (f.dosyaç()==true)
            {
                FileInfo[] file = f.htfilter();
                btns = file.Length;
                string btnad = "btn";
                int yy = 36;
                for (int i = 0; i < btns; i++)
                {
                    btnolustur(14, yy, btnad + i.ToString(), file[i].ToString(),panel7);
                    yy += 24;
                    btn.MouseDown += new MouseEventHandler(tıkla);
                }
                yy = 36;
                for (int i = 0; i < sağpanelnesne.Length; i++)
                {
                    btnolustur(14, yy, sağpanelnesne[i], sağpanelnesne[i], panel3);
                    yy += 24;
                    btn.MouseDown += new MouseEventHandler(sağtıknesne);
                }
                visib();
            }
        }

        public void visib()
        {
            panel6.Visible = false;
            webBrowser1.Visible = true;
            panel8.Visible = true;
            panel7.Visible = true;
            panel4.Visible = true;
            panel3.Visible = true;
            pictureBox3.Visible = true;
            pictureBox2.Visible = true;
            panel9.Visible = true;
            dr.Show();
            dr.Visible = false;
            ozellik.Visible = true;
            nesne.Visible = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            panel4.Size = new Size(10,this.Height-panel1.Height);
            pictureBox1.Image = Image.FromFile(@"D:\KODLAMA\Form\intproje\images\carp.png");
            pictureBox2.Image = Image.FromFile(@"D:\KODLAMA\Form\intproje\images\kucuk.png");
            pictureBox3.Image = Image.FromFile(@"D:\KODLAMA\Form\intproje\images\cızık.png");
            pictureBox5.Image = Image.FromFile(@"D:\KODLAMA\Form\intproje\images\folder.png");
            pictureBox6.Image = Image.FromFile(@"D:\KODLAMA\Form\intproje\images\plusfolder.png");
            this.TransparencyKey = Color.Empty;
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            aclick = true;
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            aclick = false;
            dr.Size = new Size(this.Width - panel7.Width - 15 - panel3.Width, 736);
            dr.Location = new Point(panel7.Width + 10, 32);
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (aclick == true&& MousePosition.X < this.Location.X+this.Width)
            {
                panel4.Location = new Point(MousePosition.X-this.Location.X-10, 0);
                panel3.Size = new Size(this.Width-panel4.Location.X-5 , panel3.Height);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(63, 63, 65);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(45, 45, 45);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            float sol, sağ;
            sağ = (float)panel3.Width / (float)this.Width;
            sol = (float)panel7.Width / (float)this.Width;
            if (kucuk==true)
            {
                this.WindowState = FormWindowState.Normal;
                kucuk= false;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                kucuk = true;
            }
            panel3.Size = new Size((int)Math.Round(this.Width * sağ), panel3.Height);
            panel4.Location = new Point(panel3.Location.X, 0);
            panel7.Size = new Size((int)Math.Round(this.Width * sol), panel7.Height);
            panel8.Location = new Point(panel7.Width-2, 0);
            if (panel4.Location.X > this.Width - 10)
            {
                panel4.Location = new Point(this.Width - 10, 0);
            }
            if (dr.Visible == false)
            {
                dr.Visible = true;
                dr.Size = new Size(this.Width - panel7.Width - 15 - panel3.Width, 736);
                dr.Location = new Point(panel7.Width + 10, 32);
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(63, 63, 65);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(45, 45, 45);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (kucuk==false&&pclick==true)
            {
                this.Location = new Point(MousePosition.X-pxfark, MousePosition.Y - pyfark);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            pclick = true;
            pxfark = MousePosition.X - this.Location.X;
            pyfark = MousePosition.Y - this.Location.Y;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(63, 63, 65);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(45, 45, 45);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            pclick = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            f.dosyaç();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            f.dosyaç();
        }


        private void panel6_MouseEnter(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(63, 63, 64);
        }

        private void panel6_MouseLeave(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(28, 28, 28);
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(63, 63, 64);
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(63, 63, 64);
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            listle();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            listle();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            listle();
        }

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            aclick = true;
        }

        private void panel8_MouseMove(object sender, MouseEventArgs e)
        {
            if (aclick == true&&MousePosition.X-5> this.Location.X)
            {
                panel8.Location = new Point(MousePosition.X - this.Location.X-6, 0);
                panel7.Size = new Size(MousePosition.X-this.Location.X-5, panel7.Height);
            }
        }

        private void panel8_MouseUp(object sender, MouseEventArgs e)
        {
            aclick = false;
            dr.Size = new Size(this.Width - panel7.Width - 15 - panel3.Width, 736);
            dr.Location = new Point(panel7.Width + 10, 32);
        }

        private void panel9_MouseEnter(object sender, EventArgs e)
        {
            panel9.BackColor = Color.FromArgb(63, 63, 64);
        }

        private void panel9_MouseLeave(object sender, EventArgs e)
        {
            
            panel9.BackColor = Color.FromArgb(45, 45, 45);
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            panel9.BackColor = Color.FromArgb(63, 63, 64);
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            panel9.BackColor = Color.FromArgb(45, 45, 45);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                if (solt == false)
                { 
                    for (int i = 2; panel7.Controls.Count>2; i++)
                    {
                        panel7.Controls.Remove(panel7.Controls[2]);
                    }
                    FileStream fs = File.Create(f.yol + @"\" + textBox1.Text+".html");
                    fs.Close();
                    FileInfo[] file = f.htfilter();
                    btns = file.Length;
                    int yy = 36;
                    for (int i = 0; i < btns; i++)
                    {
                        btnolustur(14, yy, "btn" + i.ToString(), file[i].ToString(),panel7);
                        yy += 24;
                        btn.MouseDown += new MouseEventHandler(tıkla);
                    }
                    f.projebaslangıc(f.yol + @"\" + textBox1.Text + ".html");
                    webBrowser1.Navigate(f.yol + @"\" + textBox1.Text + ".html");
                    textBox1.Visible = false;
                    url = f.yol + @"\" + textBox1.Text + ".html";
                    textBox1.Clear();
                }
                else
                {
                    for (int i = 0; i < btns; i++)
                    {
                        if (panel7.Controls[btnad].Text == f.htfilter()[i].ToString())
                        {
                            f.htfilter()[i].MoveTo(Path.Combine(f.htfilter()[i].Directory.FullName, textBox1.Text+".html"));
                            break;
                        }
                    }
                    panel7.Controls[btnad].Text = textBox1.Text+".html";
                    textBox1.Visible = false;
                    textBox1.Clear();
                }
            }
            if (e.KeyCode==Keys.Escape)
            {
                for (int i = 0; i < btns; i++)
                {
                    int y = panel7.Controls["btn" + i.ToString()].Location.Y;
                    panel7.Controls["btn" + i.ToString()].Location = new Point(14, y - 24);
                    textBox1.Visible = false;
                    textBox1.Clear();
                }
            }
        }

        private void sil_Click(object sender, EventArgs e)
        {
            File.Delete(f.yol + @"\" + panel7.Controls[btnad].Text);
            for (int i = 2; panel7.Controls.Count > 2; i++)
            {
                panel7.Controls.Remove(panel7.Controls[2]);
            }
            FileInfo[] file = f.htfilter();
            btns = file.Length;
            int yy = 36;
            for (int i = 0; i < btns; i++)
            {
                btnolustur(14, yy, "btn" + i.ToString(), file[i].ToString(), panel7);
                yy += 24;
                btn.MouseDown += new MouseEventHandler(tıkla);
            }
            panel10.Visible = false;
        }

        private void ozellik_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font("Arial",10);
            Brush brush = new SolidBrush(Color.FromArgb(241,241,241));
            e.Graphics.TranslateTransform(20,30);
            e.Graphics.RotateTransform(90);
            e.Graphics.DrawString("Özellik",font,brush,0,0);
        }

        private void ozellik_MouseDown(object sender, MouseEventArgs e)
        {
            if (özellika==false)
            {
                özellika = true;
                nesnea = false;
                comboBox1.Visible = true;
                for (int i = 0; i < panel3.Controls.Count; i++)
                {
                    panel3.Controls.Remove(panel3.Controls[sağpanelnesne[i]]);
                }
            }
        }

        private void nesne_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font("Arial", 10);
            Brush brush = new SolidBrush(Color.FromArgb(241, 241, 241));
            e.Graphics.TranslateTransform(20, 30);
            e.Graphics.RotateTransform(90);
            e.Graphics.DrawString("Nesneler", font, brush, 0, 0);
        }

        private void nesne_MouseDown(object sender, MouseEventArgs e)
        {
            if (nesnea==false)
            {
                nesnea = true;
                özellika = false;
                comboBox1.Visible = false;
                int yy = 36;
                int syc = (panel3.Controls.Count - 1) / 2;
                for (int i = 0; i < syc; i++)
                {
                    panel3.Controls.Remove(panel3.Controls[f.div[i]+"l"+i.ToString()]);
                    panel3.Controls.Remove(panel3.Controls[f.div[i] + "t" + i.ToString()]);
                }
                for (int i = 0; i < sağpanelnesne.Length; i++)
                {
                    btnolustur(14, yy, sağpanelnesne[i], sağpanelnesne[i], panel3);
                    yy += 24;
                    btn.MouseDown += new MouseEventHandler(sağtıknesne);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            f.btncombo(comboBox1.SelectedItem.ToString());
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            panel10.Visible = false;
        }

        private void yenidenadlandır_Click(object sender, EventArgs e)
        {
            int r = Convert.ToByte(btnad.Substring(3, btnad.Length - 3));
            textBox1.Location = new Point(21,41+(r*24));
            textBox1.Visible = true;
            textBox1.Focus();
            solt = true;
            panel10.Visible = false;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible==false)
            {
                for (int i = 0; i < btns; i++)
                {
                    int y = panel7.Controls["btn" + i.ToString()].Location.Y;
                    panel7.Controls["btn" + i.ToString()].Location = new Point(14, y + 24);
                }
                textBox1.Visible = true;
                textBox1.Focus();
                textBox1.Clear();
                solt = false;
                panel10.Visible = false;
            }
            else
            {
                SendKeys.Send("{ENTER}");
            }
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            f.dosyaç();
        }
    }
}
