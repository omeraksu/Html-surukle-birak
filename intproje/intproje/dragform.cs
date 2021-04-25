using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace intproje
{
    public partial class dragform : Form
    {
        Button btn;
        int divs = 0;

        public dragform()
        {
            InitializeComponent();
        }

        public void divolustur(int x, int y, string ad)
        {
            btn = new Button();
            btn.Size = new Size(100, 100);
            btn.Location = new Point(x, y);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Name = ad;
            btn.BackColor = Color.FromArgb(0, 0, 0);
            this.Controls.Add(btn);
        }
        //ethics true canvas grant shrimp hope gap arrange filter climb cotton explain suffer fitness absent weekend rebuild tobacco viable leaf diagram hair hour pattern
        private void dragform_DragOver(object sender, DragEventArgs e)
        {
            if (e.KeyState==1)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        void div(object sender, DragEventArgs e)
        {

            Form1 anaform = (Form1)Application.OpenForms["Form1"];
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(Form1.url);

            HtmlNode body = doc.DocumentNode.SelectSingleNode("//body");
            //HtmlNode head = doc.DocumentNode.SelectSingleNode("//style");
            //var divcss = HtmlNode.CreateNode(".div"+divs.ToString()+ "{height:100px;width:100px;background-color:red;}\n");
            //head.AppendChild(divcss);

            var div = HtmlNode.CreateNode("<div></div>");
            div.Attributes.Add("class", "div" + divs.ToString());
            div.Attributes.Add("style", "height:100px;width:100px;background-color:red");
            body.AppendChild(div);
            body.InnerHtml += "\n";
            doc.Save(Form1.url);
            anaform.webBrowser1.Navigate(Form1.url);
            anaform.comboBox1.Items.Add("div"+divs.ToString());
            //divolustur(e.X-anaform.panel7.Width-10, e.Y-32, "div" + divs.ToString());
            Form1.nesnem nes = new Form1.nesnem();
            nes.classname = "div" + divs.ToString();
            Form1.nesneler.Add(nes);
            divs += 1;
        }

        private void dragform_DragDrop(object sender, DragEventArgs e)
        {
            div(sender,e);
        }
    }
}
