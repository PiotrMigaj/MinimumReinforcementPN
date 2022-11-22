using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Diagnostics;



namespace ZbrMinPN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 4;
            this.comboBox2.SelectedIndex = 1;
            this.comboBox3.SelectedIndex = 5;

            textBox3_TextChanged(null, null);
        }

        string klasaBetonu;
        double fctm;
        double ky;
        double fcteff;
        double ecm;
        double es;
        double h;
        double fi;
        double cnom;
        double wlim;
        double kt;
        double wynik;
        double eps;
        double srmax;
        double wk;

        private void button1_Click(object sender, EventArgs e)
        {

            //double Dzialanie(double x)
            //{
            //    return Math.Pow((x - 2.55), 2) - 4.24;
            //}

            //double wynik;
            //wynik = MigiMath.MetodaSiecznych(Dzialanie, 4, 5, 0.0001, 10);

            //MessageBox.Show(wynik.ToString());

            //string klasaBetonu;
            klasaBetonu = comboBox1.SelectedItem.ToString();

            double fck;
            fck = Convert.ToDouble(klasaBetonu.Substring(1, 2));

            //double fctm;
            fctm = 0.3 * Math.Pow(fck, 2.0 / 3)*1000;

            int ecmIndex;
            ecmIndex = comboBox1.SelectedIndex;

            double[] ecmTablica = new double[9];
            ecmTablica[0] = 27;
            ecmTablica[1] = 29;
            ecmTablica[2] = 30;
            ecmTablica[3] = 31;
            ecmTablica[4] = 33;
            ecmTablica[5] = 34;
            ecmTablica[6] = 35;
            ecmTablica[7] = 36;
            ecmTablica[8] = 37;

            //double ecm;
            ecm = ecmTablica[ecmIndex]*1000000;

            //MessageBox.Show(ecm.ToString());

            //double ky;
            if (!double.TryParse(textBox1.Text,out ky))
            {
                MessageBox.Show("Wartość ky musi być wartością liczbową!");
            }
            ky = 0.01 * ky;

            //double fcteff;
            fcteff = ky * fctm;
            
            //double es;
            if (!double.TryParse(textBox2.Text, out es))
            {
                MessageBox.Show("Wartość Es musi być wartością liczbową!");
            }
            es = es * 1000000;

            //double h;
            if (!double.TryParse(textBox3.Text, out h))
            {
                MessageBox.Show("Wartość h musi być wartością liczbową!");
            }
            h = h * 0.001;

            //double cnom;
            if (!double.TryParse(textBox4.Text, out cnom))
            {
                MessageBox.Show("Wartość cnom musi być wartością liczbową!");
            }
            cnom = cnom * 0.001;

            //double fi;
            fi = Convert.ToDouble(comboBox2.SelectedItem.ToString())*0.001;

            //double wlim;
            wlim = Convert.ToDouble(comboBox3.SelectedItem.ToString()) * 0.001;

            //double kt;
            if (!double.TryParse(textBox5.Text, out kt))
            {
                MessageBox.Show("Wartość kt musi być wartością liczbową!");
            }


            ZbrMin zbrojenie = new ZbrMin(fctm,ecm,ky,es,h,cnom,fi,wlim,kt);


            double Asx0;
            Asx0 = Convert.ToDouble(textBox6.Text) * 0.0001;
            double Asx1;
            Asx1 = Convert.ToDouble(textBox7.Text) * 0.0001;

            //double wynik;
            wynik = zbrojenie.zbrMinValue(Asx0, Asx1, 0.0001, 100)*10000;
            label10.Text = Math.Round(wynik,2).ToString();
            label14.Text = Math.Round(0.5 * wynik, 2).ToString();

            //MessageBox.Show(ZbrMin.ka.ToString());

            //double eps;
            eps = Math.Round(ZbrMin.eps(kt, fcteff, (wynik*0.0001), ZbrMin.Aceff, ZbrMin.ae, es),6);
            //MessageBox.Show(eps.ToString());

            srmax = Math.Round(ZbrMin.srmax(fi,(wynik*0.0001),ZbrMin.Aceff)*1000,1);
            //MessageBox.Show(srmax.ToString());

            wk = Math.Round(ZbrMin.eps(kt, fcteff, (wynik * 0.0001), ZbrMin.Aceff, ZbrMin.ae, es) * ZbrMin.srmax(fi, (wynik * 0.0001), ZbrMin.Aceff) * 1000,3);
            //MessageBox.Show(wk.ToString());

            //string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //var directory = System.IO.Path.GetDirectoryName(path);
            //string directory2;
            //directory2 = directory + @"\dupa";
            //MessageBox.Show(directory2);

            //Random r = new Random();
            //var x = r.Next(0, 1000000);
            //string s = x.ToString("000000");
            //MessageBox.Show(s);
            //string nazwaRaportu = "\\" + s + "_ZbrMinPNEN.docx";
            //MessageBox.Show(nazwaRaportu);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            //string strfilename;
            //strfilename = "";
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    strfilename = openFileDialog1.FileName;
            //    //MessageBox.Show(strfilename);
            //}

            //Nazwa raportu
            Random r = new Random();
            var x = r.Next(0, 1000000);
            string s = x.ToString("000000");
            string nazwaRaportu = "\\" + s + "_ZbrMinPNEN.docx";

            //Aktualna ścieżka dostępu
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);
            string directory2;
            directory2 = directory + nazwaRaportu;

            byte[] myfile = Properties.Resources.raport;
            File.WriteAllBytes(@directory2, myfile);

            
            Word.Application objWord = new Word.Application();
            //objWord.Visible = true;
            objWord.WindowState = Word.WdWindowState.wdWindowStateNormal;

            Word.Document objDoc = objWord.Documents.Open(@directory2);
            objDoc.Bookmarks["klasabetonu"].Select();
            objWord.Selection.TypeText(klasaBetonu);
            objDoc.Bookmarks["fctm"].Select();
            objWord.Selection.TypeText((Math.Round(fctm*0.001,2)).ToString());
            objDoc.Bookmarks["ky"].Select();
            objWord.Selection.TypeText((Math.Round(ky*100, 0)).ToString());
            objDoc.Bookmarks["fcteff"].Select();
            objWord.Selection.TypeText((Math.Round(fcteff * 0.001, 2)).ToString());
            objDoc.Bookmarks["ecm"].Select();
            objWord.Selection.TypeText((Math.Round(ecm * 0.000001, 0)).ToString());
            objDoc.Bookmarks["es"].Select();
            objWord.Selection.TypeText((Math.Round(es * 0.000001, 0)).ToString());
            objDoc.Bookmarks["h"].Select();
            objWord.Selection.TypeText((Math.Round(h*1000, 0)).ToString());
            objDoc.Bookmarks["fis"].Select();
            objWord.Selection.TypeText((Math.Round(fi * 1000, 0)).ToString());
            objDoc.Bookmarks["cnom"].Select();
            objWord.Selection.TypeText((Math.Round(cnom * 1000, 0)).ToString());
            objDoc.Bookmarks["wlim"].Select();
            objWord.Selection.TypeText((Math.Round(wlim * 1000, 1)).ToString());
            objDoc.Bookmarks["kt"].Select();
            objWord.Selection.TypeText((Math.Round(kt * 1, 1)).ToString());
            objDoc.Bookmarks["a"].Select();
            objWord.Selection.TypeText((Math.Round((ZbrMin.a*1000), 0)).ToString());
            objDoc.Bookmarks["ka"].Select();
            objWord.Selection.TypeText((Math.Round((ZbrMin.ka), 3)).ToString());
            objDoc.Bookmarks["hceff"].Select();
            objWord.Selection.TypeText((Math.Round((ZbrMin.hceff*1000), 0)).ToString());
            objDoc.Bookmarks["aceff"].Select();
            objWord.Selection.TypeText((Math.Round((ZbrMin.Aceff * 10000), 0)).ToString());
            objDoc.Bookmarks["asmin"].Select();
            objWord.Selection.TypeText((Math.Round((wynik * 1), 2)).ToString());
            objDoc.Bookmarks["asminpol"].Select();
            objWord.Selection.TypeText((Math.Round((wynik * 0.5), 2)).ToString());
            objDoc.Bookmarks["ropeff"].Select();
            objWord.Selection.TypeText((Math.Round((wynik/(ZbrMin.Aceff * 10000)), 4)).ToString());
            objDoc.Bookmarks["ncr"].Select();
            objWord.Selection.TypeText((Math.Round((fcteff * (ZbrMin.Aceff * 1)), 1)).ToString());
            objDoc.Bookmarks["ss"].Select();
            objWord.Selection.TypeText((Math.Round((fcteff * (ZbrMin.Aceff * 1)/(wynik*0.0001*1000)), 1)).ToString());
            objDoc.Bookmarks["eps"].Select();
            objWord.Selection.TypeText(eps.ToString());
            objDoc.Bookmarks["srmax"].Select();
            objWord.Selection.TypeText(srmax.ToString());
            objDoc.Bookmarks["wk"].Select();
            objWord.Selection.TypeText(wk.ToString());

            objWord.ActiveDocument.Save();
            
            objWord.Visible = true;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            double x0;
            x0 = Math.Round(Convert.ToDouble(textBox3.Text)*0.003*10,2);

            double x1;
            x1 = Math.Round(Convert.ToDouble(textBox3.Text) * 0.0125*10,2);


            textBox6.Text = x0.ToString();
            textBox7.Text = x1.ToString();
        }
    }
}
