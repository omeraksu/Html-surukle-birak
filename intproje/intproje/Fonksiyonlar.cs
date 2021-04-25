using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using System.Drawing;
using HtmlAgilityPack;

namespace intproje
{
    public class Fonksiyonlar
    {
        public string[] div = new string[] {"Genişlik","Yükseklik","Class Adı"};
        public string yol;
        string secilen;
        public bool dosyaç()
        {
            bool ok = false;
            FolderBrowserDialog file = new FolderBrowserDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                StreamWriter yaz = new StreamWriter(Application.StartupPath.ToString() + @"/acilanlar.txt", true);
                yaz.WriteLine(file.SelectedPath);
                yaz.Close();
                yol = file.SelectedPath;
                ok = true;
            }
            return ok;
        }
        public FileInfo[] htfilter()
        {
            DirectoryInfo di = new DirectoryInfo(yol);
            return di.GetFiles("*.html");
        }

        public void projebaslangıc(string yol)
        {
            StringBuilder st = new StringBuilder();
            st.Append("<!DOCTYPE html>\n");
            st.Append("<html lang = 'tr'>\n");
            st.Append("<head>\n");
            st.Append("<meta charset = 'UTF - 8'>\n");
            st.Append("<meta http - equiv = 'X - UA - Compatible' content = 'IE = edge' >\n");
            st.Append("<meta name = 'viewport' content = 'width = device - width, initial - scale = 1.0'>\n");
            st.Append("<title > Document </title >\n");
            st.Append("<style>\n");
            st.Append("</style>\n");
            st.Append("</head>\n");
            st.Append("<body>\n");
            st.Append("</body>\n");
            st.Append("</html>\n");
            File.WriteAllText(yol, st.ToString());
        }

        public void ikili(int x, int y, string ad,int syc)
        {
            Form1 anaform = (Form1)Application.OpenForms["Form1"];
            Label label = new Label();
            label.Text = ad;
            label.Name = ad+"l"+syc.ToString();
            label.ForeColor = Color.FromArgb(241,241,241);
            label.Size = new Size(60,20);
            label.Location = new Point(x,y);
            TextBox textb = new TextBox();
            textb.Name = ad + "t" + syc.ToString();
            textb.Location = new Point(74,y);
            textb.Size = new Size(100,20);
            textb.KeyDown += new KeyEventHandler(keyt);
            anaform.panel3.Controls.Add(label);
            anaform.panel3.Controls.Add(textb);
        }

        public void combosecilen(TextBox tex,string att)
        {
            Form1 anaform = (Form1)Application.OpenForms["Form1"];
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(Form1.url);
            var clas = doc.DocumentNode.SelectSingleNode("//*[contains(@class,'" + secilen + "')]");
            string[] s = clas.Attributes["style"].Value.Split(';');
            int i;
            for (i = 0; i < s.Length; i++)
            {
                if (s[i].Split(':')[0] == att)
                {
                    break;
                }
            }
            string birlestir = "";
            for (int k = 0; k < s.Length; k++)
            {
                if (s[k]!="")
                {
                    string[] nv = s[k].Split(':');
                    if (k == i)
                    {
                        nv[1] = tex.Text + "px";
                    }
                    birlestir += nv[0] + ":" + nv[1] + ";";
                }
            }
            clas.SetAttributeValue("style", birlestir);
            doc.Save(Form1.url);
            anaform.webBrowser1.Navigate(Form1.url);
        }

        public void keyt(object sender, KeyEventArgs e)
        {
            TextBox tex = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                if (tex.Name.Substring(0,8)=="Genişlik")
                {
                    combosecilen(tex,"width");
                    for (int i = 0; i < Form1.nesneler.Count; i++)
                    {
                        if (Form1.nesneler[i].classname == secilen)
                        {
                            Form1.nesneler[i].width = Convert.ToInt16(tex.Text);
                            break;
                        }
                    }
                }
                if (tex.Name.Substring(0,9) == "Yükseklik")
                {
                    combosecilen(tex, "height");
                    for (int i = 0; i < Form1.nesneler.Count; i++)
                    {
                        if (Form1.nesneler[i].classname == secilen)
                        {
                            Form1.nesneler[i].height = Convert.ToInt16(tex.Text);
                            break;
                        }
                    }
                }
                if (tex.Name.Substring(0, 9) == "Class Adı")
                {
                    for (int i = 0; i < Form1.nesneler.Count; i++)
                    {
                        if (Form1.nesneler[i].classname == secilen)
                        {
                            Form1.nesneler[i].classname = tex.Text;
                            break;
                        }
                    }
                    Form1.enter = true;
                    Form1 anaform = (Form1)Application.OpenForms["Form1"];
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.Load(Form1.url);
                    var clas = doc.DocumentNode.SelectSingleNode("//*[contains(@class,'" + secilen + "')]");
                    clas.SetAttributeValue("class", tex.Text);
                    doc.Save(Form1.url);
                    anaform.comboBox1.Items.Remove(secilen);
                    anaform.comboBox1.Items.Add(tex.Text);
                    anaform.comboBox1.Text=tex.Text;
                    secilen = tex.Text;
                }
            }
        }

        public void btncombo(string sec)
        {
            Form1 anaform = (Form1)Application.OpenForms["Form1"];
            if (Form1.enter == false&&Form1.özellika==true&&anaform.panel3.Controls.Count==1)
            {
                secilen = sec;
                int yy = 36;
                for (int i = 0; i < div.Length; i++)
                {
                    ikili(14, yy, div[i],i);
                    yy += 24;
                }
            }
            if (Form1.enter == true)
            {
                Form1.enter = false;
            }
            if (Form1.enter == false && Form1.özellika == true && anaform.panel3.Controls.Count != 1)
            {
                secilen = sec;
                foreach (Control c in anaform.panel3.Controls)
                {
                    if (c.Name == "Class Adıt2")
                    {
                        c.Text = anaform.comboBox1.SelectedItem.ToString();
                    }
                    if (c.Name == "Genişlikt0")
                    {
                        for (int i = 0; i < Form1.nesneler.Count; i++)
                        {
                            if (Form1.nesneler[i].classname==secilen)
                            {
                                c.Text = Form1.nesneler[i].width.ToString();
                                break;
                            }
                        }
                    }
                    if (c.Name == "Yükseklikt1")
                    {
                        for (int i = 0; i < Form1.nesneler.Count; i++)
                        {
                            if (Form1.nesneler[i].classname == secilen)
                            {
                                c.Text = Form1.nesneler[i].height.ToString();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
