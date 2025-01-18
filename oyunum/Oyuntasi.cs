using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Ali HIMEYDA B231200561
namespace oyunum
{
    // Ali HIMEYDA B231200561
    internal class Oyuntasi : Button
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
        public static int kenarUzunlugu = 40;
        public string[] renkler = { "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim1.jpg", "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim2.jpg", "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim3.jpg", "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim4.jpg", "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim5.jpg", "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim6.jpg"  };
        public string[] jokerler = { "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim7.jpg" , "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim8.jpg" , "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim9.jpg", "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim10.jpg" };
        private static int[] sayilar = new int[8];
        public int satir;
        public int sutun;
        public bool silinecekmi;
        public string resimyolu;
        //kurucu fonksyon
        public Oyuntasi(Random rnd,Random oran) 
        {
            Timer timer = new Timer();

            this.Width = this.Height = kenarUzunlugu;
            int index = rnd.Next()%renkler.Length;
            double jokerorani = 0.08;
            if (oran.NextDouble()<jokerorani)
            {
                this.resimyolu = jokerler[index%jokerler.Length];
            }
            else
            {
                this.resimyolu = renkler[index];
            }
            this.BackgroundImage = Image.FromFile(resimyolu); 
            this.BackgroundImageLayout = ImageLayout.Stretch; // Resmi butona sığdırmak için
            this.silinecekmi = false;
            

        }
        //public static void temizleyici()
        //{
        //    for (int i = 0; i < SecimPenceresi.secilenbilgiyiintedonusturme(); i++)
        //    {
        //        for (int j = 0; j < SecimPenceresi.secilenbilgiyiintedonusturme(); j++)
        //        {
        //            if (taslar[j, i].BackColor == taslar[j + 1, i].BackColor && taslar[j + 1, i].BackColor == taslar[j+2,i].BackColor)
        //            {
        //                taslar[j, i] = null;
        //                taslar[j + 1, i] = null;
        //                taslar[j+ 2, i] = null; 
        //            }
        //        }
        //    }
        //}
        //public void ilktiklama(object sender, EventArgs e)
        //{
        //    int diziboyutu = SecimPenceresi.secilenbilgiyiintedonusturme();
        //   Oyuntasi yakalanantas = sender as Oyuntasi;
        //    for (int i = 0;i<diziboyutu; i++)
        //    {
        //        for (int j = 0; j < diziboyutu; j++)
        //        {
        //            if (taslar[i, j].satir == yakalanantas.satir && taslar[i, j].sutun == yakalanantas.sutun)
        //            {
        //                sender = taslar[i, j];
        //                secilenilktas = taslar[i, j];
        //                MessageBox.Show("ilk tas secildi");
        //                break;
        //            }
        //        }
               
        //    }

        //}
        //public void ikincitiklama(object sender, EventArgs e) 
        //{
        //    Oyuntasi yakalanantas = sender as Oyuntasi;
        //    for (int i = 0; i < SecimPenceresi.secilenbilgiyiintedonusturme(); i++)
        //    {
        //        for (int j = 0; j < SecimPenceresi.secilenbilgiyiintedonusturme(); j++)
        //        {
        //            if (taslar[i, j].satir == yakalanantas.satir && taslar[i, j].sutun == yakalanantas.sutun)
        //            {
        //                if (secilenilktas != null)
        //                {
        //                    secilenikincitas = taslar[i, j];
        //                    MessageBox.Show(secilenilktas.BackColor + " ve " + secilenikincitas.BackColor + " taslar yer degistirilecektir");
        //                    break;
        //                }
        //                else
        //                {
        //                    return;
        //                }
                       
        //            }
        //        }

        //    }

        //}
    }
}
