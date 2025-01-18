using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Drawing.Drawing2D;
// Ali HIMEYDA B231200561
namespace oyunum
{
    // Ali HIMEYDA B231200561
    class Oyuncubilgipaneli : Panel
    {
        readonly Color acikKirmizi = ColorTranslator.FromHtml("#ff6b6b");
        readonly Color gri = ColorTranslator.FromHtml("#e8eceb");
        readonly Color sari = ColorTranslator.FromHtml("#e09e50");
        readonly Color acikSari = ColorTranslator.FromHtml("#e5bb88");
        readonly Color mavi = ColorTranslator.FromHtml("#8cbdb9");
        readonly Color acikMavi = ColorTranslator.FromHtml("#b2d8d5");
        readonly Color yeşil = ColorTranslator.FromHtml("#5cb85c"); // Yeşil
        readonly Color açıkYeşil = ColorTranslator.FromHtml("#8fd98f"); // Açık Yeşil
        readonly Color kırmızı = ColorTranslator.FromHtml("#d9534f"); // Kırmızı
        readonly Color açıkKırmızı = ColorTranslator.FromHtml("#e58c8a"); // Açık Kırmızı
        readonly Color mor = ColorTranslator.FromHtml("#9463a7"); // Mor
        readonly Color açıkMor = ColorTranslator.FromHtml("#b68fc7"); // Açık Mor
        public static Panel oyuncubilgisipanelimiz;
        public Oyuncubilgipaneli()
        {
            this.Width = Oyuntasi.kenarUzunlugu * (SecimPenceresi.secilenbilgiyiintedonusturme());
            this.Height = 99;
            this.BackColor = sari;
            this.Location = new Point(300, 1);


            for (int i = 0; i < Oyuncu.oyuncular.Length; i++)
            {

                if (Oyuncu.oyuncular[i] == null)
                {
                    Oyuncu.sonoyuncu = Oyuncu.oyuncular[i - 1];
                    oyuncubilgisipanelimiz = new Panel();
                    oyuncubilgisipanelimiz.Width = 200;
                    oyuncubilgisipanelimiz.Height = 200;
                    oyuncubilgisipanelimiz.BackColor = sari;
                    Label oyuncubilgisi = new Label();
                    oyuncubilgisi.Text = Oyuncu.oyuncular[i - 1].oyuncuismi;
                    oyuncubilgisi.ForeColor = Color.White;
                    oyuncubilgisi.Width = 100;
                    oyuncubilgisi.Height = 50;
                    oyuncubilgisi.Font = new Font(oyuncubilgisi.Font.FontFamily, 16);
                    oyuncubilgisipanelimiz.Controls.Add(oyuncubilgisi);
                    this.Controls.Add(oyuncubilgisipanelimiz);
                    break;
                }

            }

        }




    }


    // Ali HIMEYDA B231200561
    internal class Oyuncu
    {
        public string oyuncuismi;
        public int puan;
        public static Oyuncu[] oyuncular = new Oyuncu[15];
        public static Oyuncu sonoyuncu;
        public Oyuncu(string isim)
        {
            this.oyuncuismi = isim;
            this.puan = 0;

        }
        public static void oyuncularidosyadanoku()
        {
            using (StreamReader sr = new StreamReader("C:/C#_projeleri/C#kareler_oyunu/oyunum/bilgi_dosyalari/oyuncubilgileri.txt"))
            {
                int i = 0;
                string satir;
                while ((satir = sr.ReadLine()) != null)
                {
                    Oyuncu yenioyuncu = new Oyuncu(satir);
                    satir = sr.ReadLine();
                    yenioyuncu.puan = int.Parse(satir);
                    oyuncular[i] = yenioyuncu;
                    i++;
                }
            }
        }
        public static void yenioyuncuyudosyayaekle(Oyuncu[] oyuncular)
        {
            using (StreamWriter sr = new StreamWriter("C:/C#_projeleri/C#kareler_oyunu/oyunum/bilgi_dosyalari/oyuncubilgileri.txt"))
            {
                int i = 0;
                while (oyuncular[i] != null)
                {
                    sr.WriteLine(oyuncular[i].oyuncuismi);
                    sr.WriteLine(oyuncular[i].puan);
                    i++;
                }
            }
        }

      
       
    }
}
