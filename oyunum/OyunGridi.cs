using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oyunum
{// Ali HIMEYDA B231200561
    interface jokerPatlamalari
    {
        void RoketPatlat(int satir, int sutun, bool yatay, Oyuntasi[,] grid);
        void KopterPatlat(Oyuntasi[,] grid, Random random, Oyuntasi kopter);
        void BombaPatlat(int satir, int sutun, Oyuntasi[,] grid, Oyuntasi bomba);
        void GokkusagiPatlat(Oyuntasi[,] grid, string hedefResimYolu, Oyuntasi gokkusagi);
    }
    // Ali HIMEYDA B231200561
    internal class OyunGridi : Panel, jokerPatlamalari
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
        public OyunGridi genelkapsayici;
        public Oyuntasi[,] taslar;
        public static int oynamapuani;
        public Oyuntasi secilenilktas;
        public Oyuntasi secilenikincitas;
        public Label puan = new Label();

        private SoundPlayer bombasesi = new SoundPlayer(oyunum.Properties.Resources.bomba);
        private SoundPlayer roketsesi = new SoundPlayer(oyunum.Properties.Resources.roket);
        private SoundPlayer koptersesi = new SoundPlayer(oyunum.Properties.Resources.kopter);
        private SoundPlayer gokkusagisesi = new SoundPlayer(oyunum.Properties.Resources.gokkusagisesi);
        public OyunGridi()
        {
            this.Width = Oyuntasi.kenarUzunlugu * (SecimPenceresi.secilenbilgiyiintedonusturme());
            this.Height = Oyuntasi.kenarUzunlugu * (SecimPenceresi.secilenbilgiyiintedonusturme());
            this.BackColor = gri;
            this.Location = new Point(300, 100);
            Random rnd = new Random();
            Random oran = new Random();
            taslar = new Oyuntasi[SecimPenceresi.secilenbilgiyiintedonusturme(), SecimPenceresi.secilenbilgiyiintedonusturme()];
            for (int i = 0; i < SecimPenceresi.secilenbilgiyiintedonusturme(); i++)
            {
                for (int j = 0; j < SecimPenceresi.secilenbilgiyiintedonusturme(); j++)
                {
                    taslar[j, i] = new Oyuntasi(rnd,oran);
                    Controls.Add(taslar[j, i]);
                    taslar[j, i].satir = j;
                    taslar[j, i].sutun = i;
                    taslar[j, i].Location = new Point(Oyuntasi.kenarUzunlugu * i, Oyuntasi.kenarUzunlugu * j);

                    taslar[j, i].Click += tassec;

                }
            }
            this.Paint += OyunGridi_Paint;
            oynamapuani = 0;
        }

        private void OyunGridi_Paint(object sender, PaintEventArgs e)
        {
            EslesmeleriKontrolEt();
        }


        //yerleri degismesi istenilen taslari secmek icin
        public void tassec(object sender, EventArgs e)
        {

            Oyuntasi tas = sender as Oyuntasi;
            if (secilenilktas == null)
            {
                secilenilktas = tas;
                if (tas.resimyolu== "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim7.jpg")
                {
                    Random random = new Random();
                    bool rastgeleDeger = random.Next(0, 2) == 1; // 0 => false, 1 => true
                    RoketPatlat(tas.satir,tas.sutun,rastgeleDeger,taslar);
                    tassec(sender, e);
                }
                else if (tas.resimyolu == "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim8.jpg")
                {
                    Random random = new Random();
                    KopterPatlat(taslar,random,tas);
                    tassec(sender, e);
                }
                else if (tas.resimyolu == "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim9.jpg")
                {
                    BombaPatlat(tas.satir, tas.sutun, taslar, tas);
                    tassec(sender, e);

                }
            }
            else if (secilenikincitas == null)
            {
                secilenikincitas = tas;
                if (secilenilktas.resimyolu == "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/resim10.jpg")
                {
                    GokkusagiPatlat(taslar,secilenikincitas.resimyolu, secilenilktas);
                    tassec(sender, e);
                    secilenilktas = null;
                    secilenikincitas = null;
                }
                else 
                {
                    if (komsulukdurumu(secilenilktas, secilenikincitas))
                    {
                        tasrenkdegistirme(secilenilktas, secilenikincitas);
                        secilenilktas = null;
                        secilenikincitas = null;
                    }
                    //komsu degillerse
                    else
                    {
                        secilenilktas = null;
                        secilenikincitas = null;
                        return;
                    }
                }

               
            }
        }
        public void GokkusagiPatlat(Oyuntasi[,] grid, string hedefResimYolu , Oyuntasi gokkusagi)
        {
            gokkusagisesi.Play();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != null && grid[i, j].resimyolu == hedefResimYolu)
                    {
                        KaroSilmeAnimasyonu(grid[i, j]);
                        grid[i, j] = null; // Taşı sil
                        oynamapuani += 10;
                        puan.Text = oynamapuani.ToString();
                        puan.ForeColor = Color.White;
                        puan.Width = 100;
                        puan.Height = 50;
                        puan.Top = 50;
                        puan.Font = new Font(puan.Font.FontFamily, 16);
                        if (Oyuncubilgipaneli.oyuncubilgisipanelimiz != null)
                        {
                            Oyuncubilgipaneli.oyuncubilgisipanelimiz.Controls.Add(puan);
                        }

                    }
                }
            }
            KaroSilmeAnimasyonu(grid[gokkusagi.satir, gokkusagi.sutun]);
            grid[gokkusagi.satir, gokkusagi.sutun] = null;
            BosAlanlariDoldur();
        }


        public void BombaPatlat(int satir, int sutun, Oyuntasi[,] grid,Oyuntasi bomba)
        {
            bombasesi.Play();
            for (int i = satir - 1; i <= satir + 1; i++)
            {
                for (int j = sutun - 1; j <= sutun + 1; j++)
                { 
                    // Oyun alanının sınırları içinde kal
                    if (i >= 0 && i < grid.GetLength(0) && j >= 0 && j < grid.GetLength(1))
                    {
                        KaroSilmeAnimasyonu(grid[i,j]);
                        grid[i, j] = null; // Taşı sil
                        oynamapuani += 10;
                        puan.Text = oynamapuani.ToString();
                        puan.ForeColor = Color.White;
                        puan.Width = 100;
                        puan.Height = 50;
                        puan.Top = 50;
                        puan.Font = new Font(puan.Font.FontFamily, 16);
                        if (Oyuncubilgipaneli.oyuncubilgisipanelimiz != null)
                        {
                            Oyuncubilgipaneli.oyuncubilgisipanelimiz.Controls.Add(puan);
                        }
                    }
                }
            }
            KaroSilmeAnimasyonu(grid[bomba.satir, bomba.sutun]);
            grid[bomba.satir,bomba.sutun] = null;
            BosAlanlariDoldur();

        }
        // komsuysa yerlerini degistirir
        public void tasrenkdegistirme(Oyuntasi tas1, Oyuntasi tas2)
        {
            string gecici = tas2.resimyolu;
            tas2.resimyolu = tas1.resimyolu;
            tas1.resimyolu = gecici;
            tas1.BackgroundImage = Image.FromFile(tas1.resimyolu);
            tas2.BackgroundImage = Image.FromFile(tas2.resimyolu);
            bool eslesmevarmi = aynirenkliVARMI();
            if (!eslesmevarmi)
            {
                Timer timer = new Timer();
                timer.Interval = 300;
                timer.Tick += Timer_Tick;
                timer.Start();
                void Timer_Tick(object sender, EventArgs e)
                {
                    // Renkleri eski hallerine döndür
                    gecici = tas1.resimyolu;
                    tas1.resimyolu = tas2.resimyolu;
                    tas2.resimyolu = gecici;
                    tas1.BackgroundImage = Image.FromFile(tas1.resimyolu);
                    tas2.BackgroundImage = Image.FromFile(tas2.resimyolu);
                    timer.Stop();
                }

                
            }
           

            EslesmeleriKontrolEt();

        }

        //taslarin komsu olup olmadigini kontrol edebilmek icin
        public bool komsulukdurumu(Oyuntasi tas1, Oyuntasi tas2)
        {
            int satirkomsulugu = tas1.satir - tas2.satir;
            int sutunkomsulugu = tas1.sutun - tas2.sutun;
            if ((satirkomsulugu == 0 && Math.Pow(sutunkomsulugu, 2) == 1) || (sutunkomsulugu == 0 && Math.Pow(satirkomsulugu, 2) == 1))
            { return true; }
            else
            { return false; }
        }

        public bool aynirenkliVARMI()
        {

            int satirSayisi = taslar.GetLength(0);
            int sutunSayisi = taslar.GetLength(1);

            // Yatay eşleşmeleri kontrol et
            for (int i = 0; i < satirSayisi; i++)
            {
                for (int j = 0; j < sutunSayisi - 2; j++)
                {
                    if (taslar[i, j] != null &&
                        taslar[i, j + 1] != null &&
                        taslar[i, j + 2] != null &&
                        taslar[i, j].resimyolu == taslar[i, j + 1].resimyolu &&
                        taslar[i, j].resimyolu == taslar[i, j + 2].resimyolu)
                    {
                        return true; // Eşleşme bulundu
                    }
                }
            }

            // Dikey eşleşmeleri kontrol et
            for (int i = 0; i < satirSayisi - 2; i++)
            {
                for (int j = 0; j < sutunSayisi; j++)
                {
                    if (taslar[i, j] != null &&
                        taslar[i + 1, j] != null &&
                        taslar[i + 2, j] != null &&
                        taslar[i, j].resimyolu == taslar[i + 1, j].resimyolu &&
                        taslar[i, j].resimyolu == taslar[i + 2, j].resimyolu)
                    {
                        return true; // Eşleşme bulundu
                    }
                }
            }

            return false; // Hiç eşleşme bulunamadı
        }

        public  void EslesmeleriKontrolEt()
        {

            // sadece 3 taneyi siler


            //while (aynirenkliVARMI()) 
            //{
            //    // Yatay ve dikey eşleşmeleri kontrol etmek için
            //    for (int i = 0; i < taslar.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < taslar.GetLength(1); j++)
            //        {
            //            // Geçerli karoyu al
            //            Oyuntasi mevcutKaro = taslar[i, j];
            //            if (mevcutKaro == null) continue;

            //            // Yatay eşleşmeyi kontrol et
            //            if (j + 2 < taslar.GetLength(1) &&
            //                taslar[i, j + 1]?.BackColor == mevcutKaro.BackColor &&
            //                taslar[i, j + 2]?.BackColor == mevcutKaro.BackColor)
            //            {

            //                // Eşleşen karoları null yap
            //                Controls.Remove(taslar[i, j]);
            //                Controls.Remove(taslar[i, j + 1]);
            //                Controls.Remove(taslar[i, j + 2]);
            //                taslar[i, j] = null;
            //                taslar[i, j + 1] = null;
            //                taslar[i, j + 2] = null;


            //            }

            //            // Dikey eşleşmeyi kontrol et
            //            if (i + 2 < taslar.GetLength(0) &&
            //                taslar[i + 1, j]?.BackColor == mevcutKaro.BackColor &&
            //                taslar[i + 2, j]?.BackColor == mevcutKaro.BackColor)
            //            {

            //                // Eşleşen karoları null yap
            //                Controls.Remove(taslar[i, j]);
            //                Controls.Remove(taslar[i + 1, j]);
            //                Controls.Remove(taslar[i + 2, j]);
            //                taslar[i, j] = null;
            //                taslar[i + 1, j] = null;
            //                taslar[i + 2, j] = null;

            //            }
            //        }
            //    }
            //    BosAlanlariDoldur();

            //}


            //3ten fazla eslesen  tum taslari siler


            while (aynirenkliVARMI())
            {
                // Yatay ve dikey eşleşmeleri kontrol etmek için
                for (int i = 0; i < taslar.GetLength(0); i++)
                {
                    for (int j = 0; j < taslar.GetLength(1); j++)
                    {
                        // Geçerli karoyu al
                        Oyuntasi mevcutKaro = taslar[i, j];
                        if (mevcutKaro == null) continue;

                        // *** Yatay eşleşmeyi kontrol et ***
                        int yatayEslesmeSayisi = 1; // Mevcut karo da dahil
                        for (int k = j + 1; k < taslar.GetLength(1); k++)
                        {
                            if (taslar[i, k]?.resimyolu == mevcutKaro.resimyolu)
                            {
                                yatayEslesmeSayisi++;
                            }
                            else
                            {
                                break; // Eşleşme sona erdi
                            }
                        }

                        // Yatay eşleşme varsa, tüm eşleşen karoları sil
                        if (yatayEslesmeSayisi >= 3)
                        {
                            for (int k = 0; k < yatayEslesmeSayisi; k++)
                            {
                               
                                KaroSilmeAnimasyonu(taslar[i, j + k]);
                                taslar[i , j+k] = null;

                                oynamapuani += 10;
                                puan.Text = oynamapuani.ToString();
                                puan.ForeColor = Color.White;
                                puan.Width = 100;
                                puan.Height = 50;
                                puan.Top = 50;
                                puan.Font = new Font(puan.Font.FontFamily, 16);
                                if (Oyuncubilgipaneli.oyuncubilgisipanelimiz!=null)
                                {
                                    Oyuncubilgipaneli.oyuncubilgisipanelimiz.Controls.Add(puan);
                                }
                               

                            }
                        }

                        // *** Dikey eşleşmeyi kontrol et ***
                        int dikeyEslesmeSayisi = 1; // Mevcut karo da dahil
                        for (int k = i + 1; k < taslar.GetLength(0); k++)
                        {
                            if (taslar[k, j]?.resimyolu == mevcutKaro.resimyolu)
                            {
                                dikeyEslesmeSayisi++;
                            }
                            else
                            {
                                break; // Eşleşme sona erdi
                            }
                        }

                        // Dikey eşleşme varsa, tüm eşleşen karoları sil
                        if (dikeyEslesmeSayisi >= 3)
                        {
                            for (int k = 0; k < dikeyEslesmeSayisi; k++)
                            {
                                KaroSilmeAnimasyonu(taslar[i + k, j]);
                                taslar[i + k, j] = null;


                                oynamapuani += 10;
                                puan.Text = oynamapuani.ToString();
                                puan.ForeColor = Color.White;
                                puan.Width = 100;
                                puan.Height = 50;
                                puan.Top = 50;
                                puan.Font = new Font(puan.Font.FontFamily, 16);
                                if (Oyuncubilgipaneli.oyuncubilgisipanelimiz != null)
                                {
                                    Oyuncubilgipaneli.oyuncubilgisipanelimiz.Controls.Add(puan);
                                }
                            }
                        }
                    }
                }
                BosAlanlariDoldur();
            }




        }

        public void KopterPatlat(Oyuntasi[,] grid, Random random , Oyuntasi kopter)
        {
            int rastgeleSatir = random.Next(grid.GetLength(0));
            int rastgeleSutun = random.Next(grid.GetLength(1));
           
            koptersesi.Play();
            // Rastgele taşı sil
            KaroSilmeAnimasyonu(grid[kopter.satir, kopter.sutun]);
            KaroSilmeAnimasyonu(grid[rastgeleSatir, rastgeleSutun]);
            grid[kopter.satir,kopter.sutun] = null;
            grid[rastgeleSatir, rastgeleSutun] = null;
            oynamapuani += 10;
            puan.Text = oynamapuani.ToString();
            puan.ForeColor = Color.White;
            puan.Width = 100;
            puan.Height = 50;
            puan.Top = 50;
            puan.Font = new Font(puan.Font.FontFamily, 16);
            if (Oyuncubilgipaneli.oyuncubilgisipanelimiz != null)
            {
                Oyuncubilgipaneli.oyuncubilgisipanelimiz.Controls.Add(puan);
            }
            BosAlanlariDoldur();
        }
        public void RoketPatlat(int satir, int sutun, bool yatay, Oyuntasi[,] grid)
        {
            if (yatay)
            {
                
                roketsesi.Play();
                // Yatay tüm sütunları sil
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    KaroSilmeAnimasyonu(grid[satir, j]);
                    grid[satir, j] = null; // Taşları kaldır
                    oynamapuani += 10;
                    puan.Text = oynamapuani.ToString();
                    puan.ForeColor = Color.White;
                    puan.Width = 100;
                    puan.Height = 50;
                    puan.Top = 50;
                    puan.Font = new Font(puan.Font.FontFamily, 16);
                    if (Oyuncubilgipaneli.oyuncubilgisipanelimiz != null)
                    {
                        Oyuncubilgipaneli.oyuncubilgisipanelimiz.Controls.Add(puan);
                    }
                }
                BosAlanlariDoldur();
            }
            else
            {
                roketsesi = new SoundPlayer(oyunum.Properties.Resources.roket);
                roketsesi.Play();
                // Dikey tüm satırları sil
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    KaroSilmeAnimasyonu(grid[i, sutun]);
                    grid[i, sutun] = null; // Taşları kaldır
                    oynamapuani += 10;
                    puan.Text = oynamapuani.ToString();
                    puan.ForeColor = Color.White;
                    puan.Width = 100;
                    puan.Height = 50;
                    puan.Top = 50;
                    puan.Font = new Font(puan.Font.FontFamily, 16);
                    if (Oyuncubilgipaneli.oyuncubilgisipanelimiz != null)
                    {
                        Oyuncubilgipaneli.oyuncubilgisipanelimiz.Controls.Add(puan);
                    }
                }
                BosAlanlariDoldur();

            }
        }

        private void KaroSilmeAnimasyonu(Oyuntasi tas)
        {
            Timer animasyonTimer = new Timer();
            animasyonTimer.Interval = 50; // Her 50ms'de bir çalışır
            int azaltmaAdimi = 10;

            animasyonTimer.Tick += (s, e) =>
            {
                if (tas==null)
                {
                    return;
                }
                else if (tas.Width > 0 && tas.Height > 0)
                {
                    tas.Width -= azaltmaAdimi;
                    tas.Height -= azaltmaAdimi;
                    tas.Left += azaltmaAdimi / 2;
                    tas.Top += azaltmaAdimi / 2;
                }
                else
                {
                    animasyonTimer.Stop();
                    animasyonTimer.Dispose();
                    Controls.Remove(tas);
                }
            };

            animasyonTimer.Start();
            return;
        }

        public void BosAlanlariDoldur()
        {
            Random rnd = new Random();
            Random oran = new Random();
            // Karoların sütun bazında düşmesi için
            for (int j = 0; j < taslar.GetLength(1); j++)
            {
                // Boşlukları yukarıdaki karolarla doldur
                for (int i = taslar.GetLength(0) - 1; i >= 0; i--)
                {
                    if (taslar[i, j] == null)
                    {

                        // Üstteki karoları aşağıya doğru kaydır
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (taslar[k, j] != null)
                            {
                                taslar[i, j] = taslar[k, j];
                                taslar[i,j].Top+=40*(i-k);
                                taslar[i, j].satir = i;
                                taslar[i,j].sutun = j;

                                taslar[k, j] = null;
                                break;
                            }
                        }
                    }
                }
                //bos alanlari dolduracak
                for (int i = taslar.GetLength(0) - 1; i >= 0; i--)
                {
                    if (taslar[i, j] == null)
                    {
                        // Rastgele bir renk oluştur ve karo ekle
                        taslar[i, j] = new Oyuntasi(rnd, oran);
                        Controls.Add(taslar[i, j]);
                        taslar[i, j].satir = i;
                        taslar[i, j].sutun = j;
                        taslar[i, j].Location = new Point(Oyuntasi.kenarUzunlugu * j, Oyuntasi.kenarUzunlugu * i);
                        taslar[i, j].Click += tassec;

                    }
                }
            }

        }

       


    }
}
