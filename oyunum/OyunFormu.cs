using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace oyunum
{
    
    // Ali HIMEYDA B231200561


    // ozel puanpanel sinifi yaptim :)
    public class RoundedPanel : Panel
    {
        public int BorderRadius { get; set; } = 10; // Varsayılan yuvarlaklık

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Yuvarlak köşe için GraphicsPath oluştur
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, BorderRadius, BorderRadius), 180, 90); // Sol üst
            path.AddArc(new Rectangle(this.Width - BorderRadius, 0, BorderRadius, BorderRadius), 270, 90); // Sağ üst
            path.AddArc(new Rectangle(this.Width - BorderRadius, this.Height - BorderRadius, BorderRadius, BorderRadius), 0, 90); // Sağ alt
            path.AddArc(new Rectangle(0, this.Height - BorderRadius, BorderRadius, BorderRadius), 90, 90); // Sol alt
            path.CloseFigure();

            // Panelin bölgesini (Region) ayarla
            this.Region = new Region(path);

            // Panelin kenar çizgisi (isteğe bağlı)
            using (Pen pen = new Pen(Color.Black, 1)) // Kenar çizgi rengi ve kalınlığı
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }
        }
    }

    // Ali HIMEYDA B231200561
    class SecimPenceresi : Form
    {
        readonly Color gri = ColorTranslator.FromHtml("#e8eceb");
        public Color[] renkler = {
            ColorTranslator.FromHtml("#e09e50"), // Sarı
            ColorTranslator.FromHtml("#8cbdb9"), // Mavi
            ColorTranslator.FromHtml("#5cb85c"), // Yeşil
            ColorTranslator.FromHtml("#d9534f"), // Kırmızı
            ColorTranslator.FromHtml("#9463a7")  // Mor
        };
        public int secimSayisi;
        public string[] secimYazisi;
        public int[] secimButonuKenarligi = { 150, 40 };
        public static string secilenBilgi;
        public TextBox bilgialani;
        public SecimPenceresi(string mesaj, int secimSayisi, params string[] secimYazisi)
        {
            System.Windows.Forms.Label mesajLabel = new System.Windows.Forms.Label();
            mesajLabel.Text = mesaj;
            mesajLabel.Location = new Point( 100 , 2);
            Controls.Add(mesajLabel);
            Panel secenekler = new Panel();
            secenekler.Text = "Botutlar";
            secenekler.Location = new Point(55, 120);
            secenekler.BackColor = gri;
            secenekler.Width = 31;
            Controls.Add((secenekler));
            this.secimSayisi = secimSayisi;
            this.secimYazisi = secimYazisi;
            Button[] butonlar = new Button[secimSayisi];
            for (int i = 0; i < butonlar.Length; i++)
            {
                butonlar[i] = new Button();
                butonlar[i].Text = secimYazisi[i];
                butonlar[i].Width = secimButonuKenarligi[0];
                butonlar[i].Height = secimButonuKenarligi[1];
                butonlar[i].Location = new Point(2, (butonlar[i].Height * i));
                secenekler.Controls.Add(butonlar[i]);
                // butonlarin olaylarini tetikleme
                butonlar[i].Click += secenekTanima;
                butonlar[i].BackColor = renkler[i + 2];

            }
            secenekler.Width = secimButonuKenarligi[0] + 1;
            secenekler.Height = secimButonuKenarligi[1] * butonlar.Length;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = renkler[0];

        }
        //secilen bilgiyi aktarma
        public void secenekTanima(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Text == "Ekle")
            {
                Oyuncu.oyuncularidosyadanoku();
                secilenBilgi = bilgialani.Text;
                Oyuncu yenioyuncu = new Oyuncu(secilenBilgi);
                for (int i = 0; i < Oyuncu.oyuncular.Length; i++)
                {
                    if (Oyuncu.oyuncular[i]==null)
                    {
                        Oyuncu.oyuncular[i] = yenioyuncu;
                        break;
                    }
                }
                Oyuncu.yenioyuncuyudosyayaekle(Oyuncu.oyuncular);
                Close();
            }
            else
            {
                secilenBilgi = btn.Text;
                Close();
            }
            
        }
        public static int secilenbilgiyiintedonusturme()
        {
            int secilenDeger;
            secilenDeger = int.Parse(secilenBilgi);
            return secilenDeger;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SecimPenceresi
            // 
            this.ClientSize = new System.Drawing.Size(754, 442);
            this.Name = "SecimPenceresi";
            this.Load += new System.EventHandler(this.SecimPenceresi_Load);
            this.ResumeLayout(false);

        }

        private void SecimPenceresi_Load(object sender, EventArgs e)
        {

        }
    }
    // Ali HIMEYDA B231200561
    public partial class OyunFormu : Form
    {

        readonly Color acikKirmizi = ColorTranslator.FromHtml("#ff6b6b");
        readonly Color gri = ColorTranslator.FromHtml("#e8eceb");
        readonly Color sari = ColorTranslator.FromHtml("#e09e50");
        readonly Color acikSari = ColorTranslator.FromHtml("#e5bb88");
        readonly Color mavi = ColorTranslator.FromHtml("#8cbdb9");
        readonly Color acikMavi = ColorTranslator.FromHtml("#b2d8d5");
        readonly Color yeşil = ColorTranslator.FromHtml("#5cb85c"); // Yeşil
        readonly Color acikYesil = ColorTranslator.FromHtml("#8fd98f"); // Açık Yeşil
        readonly Color kırmızı = ColorTranslator.FromHtml("#d9534f"); // Kırmızı
        readonly Color açıkKırmızı = ColorTranslator.FromHtml("#e58c8a"); // Açık Kırmızı
        readonly Color mor = ColorTranslator.FromHtml("#9463a7"); // Mor
        readonly Color açıkMor = ColorTranslator.FromHtml("#b68fc7"); // Açık Mor
        private Button BaslaButonu;
        private Button SiralamaButonu;
        private Button KapatmaButonu;
        private Button durdurButonu;
        private Button yenidenoynaButonu;
        private Button oynanmasekliButonu;

        public int toplampuan;
        public int puan;
        private OyunGridi genel;
        private Oyuncubilgipaneli oyuncubilgisipaneli;
        private Timer oyunZamanlayici;
        public static int kalanSure; // Kalan süre (saniye cinsinden)
        private System.Windows.Forms.Label sureLabel; // Kalan süreyi göstermek için bir Label
        private RoundedPanel zamanPanel; // Süreye göre uzunluğu azalan panel
        private int panelBaslangicUzunlugu; // Panelin başlangıç uzunluğu
        private bool durdurulduMu; // Zamanlayıcı durduruldu mu?
        private IdamPaneli idampaneli;
        private SoundPlayer baslasesi;
        private SoundPlayer oyunbittisesi;
        public OyunFormu()
        {
            InitializeComponent();
            Text = "hos geldiniz";
            BackColor = gri;
            Height = 750;

            this.StartPosition = FormStartPosition.CenterScreen;
            //butonlari olusturdum:

            BaslaButonu = new Button();
            SiralamaButonu = new Button();
            KapatmaButonu = new Button();
            durdurButonu = new Button();
            yenidenoynaButonu = new Button();
            oynanmasekliButonu = new Button();
            Controls.Add(BaslaButonu);
            BaslaButonu.SetBounds(10, 200, 100, 30);
            Controls.Add(SiralamaButonu);
            SiralamaButonu.SetBounds(10, 250, 100, 30);
            Controls.Add(KapatmaButonu);
            KapatmaButonu.SetBounds(10, 300, 100, 30);
            Controls.Add(durdurButonu);
            durdurButonu.SetBounds(10, 350, 100, 30);
            Controls.Add(yenidenoynaButonu);
            yenidenoynaButonu.SetBounds(10, 400, 100, 30);
            Controls.Add(oynanmasekliButonu);
            oynanmasekliButonu.SetBounds(10, 450, 100, 30);
            BaslaButonu.Text = "Basla";
            SiralamaButonu.Text = "Siralama";
            KapatmaButonu.Text = "Kapat";
            durdurButonu.Text = "Oyunu Durdur";
            oynanmasekliButonu.Text = "Oyun Rehberi";
            yenidenoynaButonu.Text = "Yeniden Oyna";
            yenidenoynaButonu.Enabled = false;
            BaslaButonu.BackColor = sari;
            SiralamaButonu.BackColor = mavi;
            KapatmaButonu.BackColor = acikKirmizi;
            durdurButonu.BackColor = yeşil;
            yenidenoynaButonu.BackColor = mor;
            oynanmasekliButonu.BackColor = acikYesil;

            BaslaButonu.FlatStyle = FlatStyle.Flat;
            SiralamaButonu.FlatStyle = FlatStyle.Flat;
            KapatmaButonu.FlatStyle = FlatStyle.Flat;
            durdurButonu.FlatStyle = FlatStyle.Flat;
            yenidenoynaButonu.FlatStyle = FlatStyle.Flat;
            oynanmasekliButonu.FlatStyle = FlatStyle.Flat;

            BaslaButonu.FlatAppearance.BorderSize = 1;
            SiralamaButonu.FlatAppearance.BorderSize = 1;
            KapatmaButonu.FlatAppearance.BorderSize = 1;
            durdurButonu.FlatAppearance.BorderSize = 1;
            yenidenoynaButonu.FlatAppearance.BorderSize = 1;
            oynanmasekliButonu.FlatAppearance.BorderSize = 1;

            //buton olaylari:

            BaslaButonu.Click += OyunuBaslat;
            KapatmaButonu.Click += OyunuKapat;
            SiralamaButonu.Click += Oyuncularisirala;
            durdurButonu.Click += DurdurDevamButonu_Click;
            yenidenoynaButonu.Click += YenidenoynaButonu_Click;
            oynanmasekliButonu.Click += OyunrehberiButonu_Click;

            //form kapanmadan once aktarma islemleri yapilsin
            this.FormClosing += OyunFormu_FormClosing;

            // Tam ekran modu
            this.WindowState = FormWindowState.Maximized; // Pencereyi maksimize eder
            this.FormBorderStyle = FormBorderStyle.None;  // Kenarlık ve kontrol düğmelerini kaldırır

        }

        private void OyunrehberiButonu_Click(object sender, EventArgs e)
        {

            Form oyunbilgiFormu = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = acikSari,
                Size = new Size(600, 700)
            };
            // Yuvarlak kenarlar oluştur
            oyunbilgiFormu.Load += (s, ev) =>
            {
                int radius = 30; // Yuvarlatma yarıçapı
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(oyunbilgiFormu.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(oyunbilgiFormu.Width - radius, oyunbilgiFormu.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, oyunbilgiFormu.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                oyunbilgiFormu.Region = new Region(path);
            };


           

            Button kapatButon = new Button
            {
                Text = "X",
                Size = new Size(20, 20),
                Location = new Point(oyunbilgiFormu.Width - 30, 10),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            kapatButon.FlatAppearance.BorderSize = 0;
            oyunbilgiFormu.Controls.Add(kapatButon);
            kapatButon.Click += (s, _) => { oyunbilgiFormu.Close(); };

            Panel butonpaneli = new Panel();
            butonpaneli.Width = oyunbilgiFormu.Width - 20;
            butonpaneli.Height = 50;
            butonpaneli.BackColor = acikSari;



            PictureBox kurallar = new PictureBox
            {
                Image = Image.FromFile("C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/kurallar.jpg"),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = oyunbilgiFormu.Width - 70,
                Height = 500,
                Location = new Point(35, (butonpaneli.Top + 130)),
                Visible = false

            };
            oyunbilgiFormu.Controls.Add(kurallar);

            PictureBox oynamasekli = new PictureBox
            {
                Image = Image.FromFile("C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/oynamasekli.jpg"),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = oyunbilgiFormu.Width - 70,
                Height = 500,
                Location = new Point(35, (butonpaneli.Top + 130)),
                Visible = false

            };
            oyunbilgiFormu.Controls.Add(oynamasekli);
            PictureBox oyunbutonlaritarifi = new PictureBox
            {
                Image = Image.FromFile("C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/kontroller.jpg"),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = oyunbilgiFormu.Width - 70,
                Height = 500,
                Location = new Point(35, (butonpaneli.Top + 130)),
                Visible = false

            };
            oyunbilgiFormu.Controls.Add(oyunbutonlaritarifi);
            PictureBox jokerler = new PictureBox
            {
                Image = Image.FromFile("C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/jokerler.jpg"),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = oyunbilgiFormu.Width - 70,
                Height = 500,
                Location = new Point(35, (butonpaneli.Top + 130)),
                Visible = false

            };
            oyunbilgiFormu.Controls.Add(jokerler);

            Button kurallarButon = new Button
            {
                Text = "Kurallar",
                Width = butonpaneli.Width / 4,
                Height = 50,
                Location = new Point((butonpaneli.Width/4)*0, 0),
                BackColor = sari,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            kurallarButon.FlatAppearance.BorderSize = 0;
            butonpaneli.Controls.Add(kurallarButon);
            kurallarButon.Click += (s, _) => { kurallar.Visible = true;oynamasekli.Visible = false;oyunbutonlaritarifi.Visible = false;jokerler.Visible = false; };

            Button oynamasekliButon = new Button
            {
                Text = "Oynama Sekli",
                Width = butonpaneli.Width / 4,
                Height = 50,
                Location = new Point((butonpaneli.Width / 4) * 1, 0),
                BackColor = sari,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            oynamasekliButon.FlatAppearance.BorderSize = 0;
            butonpaneli.Controls.Add(oynamasekliButon);
            oynamasekliButon.Click += (s, _) => { kurallar.Visible = false; oynamasekli.Visible = true; oyunbutonlaritarifi.Visible = false; jokerler.Visible = false; };

            Button oyunButonlari = new Button
            {
                Text = "Butonlar",
                Width = butonpaneli.Width / 4,
                Height = 50,
                Location = new Point((butonpaneli.Width / 4) * 2, 0),
                BackColor = sari,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            oyunButonlari.FlatAppearance.BorderSize = 0;
            butonpaneli.Controls.Add(oyunButonlari);
            oyunButonlari.Click += (s, _) => { kurallar.Visible =false; oynamasekli.Visible = false; oyunbutonlaritarifi.Visible = true; jokerler.Visible = false; };

            Button jokerlerButon = new Button
            {
                Text = "Butonlar",
                Width = butonpaneli.Width / 4,
                Height = 50,
                Location = new Point((butonpaneli.Width / 4) * 3, 0),
                BackColor = sari,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            jokerlerButon.FlatAppearance.BorderSize = 0;
            butonpaneli.Controls.Add(jokerlerButon);
            jokerlerButon.Click += (s, _) => { kurallar.Visible = false; oynamasekli.Visible = false; oyunbutonlaritarifi.Visible = false; jokerler.Visible = true; };



            oyunbilgiFormu.Controls.Add(butonpaneli);
            butonpaneli.Location = new Point((oyunbilgiFormu.Width-butonpaneli.Width)/2, 50);
            butonpaneli.Paint += (s_, e_) =>
            {
                // Kenarlık rengi ve kalınlığı
                Color borderColor = sari;
                int borderWidth = 2;

                // Kenarlık çizimi
                Control panel = s_ as Control;
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    e_.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
                }
            };

           

            oyunbilgiFormu.ShowDialog();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            string imagePath = "C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/arkaplanresmi11.jpg";

            using (Image img = Image.FromFile(imagePath))
            {
                // Resmi formun tamamına yayarak çizin
                Rectangle destRect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
                e.Graphics.DrawImage(img, destRect);
            }
        }
        private void YenidenoynaButonu_Click(object sender, EventArgs e)
        {
            baslasesi = new SoundPlayer(oyunum.Properties.Resources.oyunbaslangici);
            baslasesi.Play();
            oyunculistesiguncelle();
            OyunGridi.oynamapuani = 0;
            genel.Enabled = true;
            durdurButonu.Enabled = true;
            BaslaButonu.Enabled = false;
            IdamPaneli.basici.Height = 50;
            IdamPaneli.basicibase.Top = IdamPaneli.basici.Height + IdamPaneli.taban1.Height;
            IdamPaneli.adam.VucutUzunlugu = 200;
            IdamPaneli.adam.Top = idampaneli.Height - IdamPaneli.adam.Height - 20;
            IdamPaneli.oyunkontrolu.Enabled = true  ;
            kalanSure = 60; // 1 dakika = 60 saniye
            zamanPanel.Height = panelBaslangicUzunlugu; // Panelin uzunluğunu sıfırla
            sureLabel.Text = $"Kalan Süre: {kalanSure}";
            oyunZamanlayici.Start(); // Zamanlayıcıyı başlat
        }
        private void DurdurDevamButonu_Click(object sender, EventArgs e)
        {
            if (!durdurulduMu)
            {
                // Zamanlayıcıyı durdur
                oyunZamanlayici.Stop();
                durdurButonu.Text = "Devam";
                durdurulduMu = true;
                IdamPaneli.oyunkontrolu.Enabled = false;

                // Tüm formdaki kontrolleri devre dışı bırak
                foreach (Control kontrol in Controls)
                {
                    if (kontrol != durdurButonu)
                    {
                        kontrol.Enabled = false;
                    }
                }
            }
            else
            {
                // Zamanlayıcıyı devam ettir
                oyunZamanlayici.Start();
                durdurButonu.Text = "Durdur";
                durdurulduMu = false;
                IdamPaneli.oyunkontrolu.Enabled = true;

                // Tüm formdaki kontrolleri etkinleştir
                foreach (Control kontrol in Controls)
                {
                    kontrol.Enabled = true;
                }
            }
        }
        // form kapatilmadan oyuncu bilgiileri kaydedilecek
        private void OyunFormu_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (Oyuncu.oyuncular[0]==null)
            {

                Close();
            }
            using (StreamReader sr = new StreamReader("C:/C#_projeleri/C#kareler_oyunu/oyunum/bilgi_dosyalari/oyuncubilgileri.txt"))
            {
                int i = 0;
                string satir;
                while ((satir = sr.ReadLine()) != null)
                {
                    if (satir == Oyuncu.sonoyuncu.oyuncuismi)
                    {
                            Oyuncu.oyuncular[i] = Oyuncu.sonoyuncu;
                            satir = sr.ReadLine();
                            puan = int.Parse(satir);
                            toplampuan = puan + OyunGridi.oynamapuani;
                            Oyuncu.oyuncular[i].puan = toplampuan;
                            break;
                    }
                    satir = sr.ReadLine();
                    i++;

                }
            }
            using (StreamWriter sr = new StreamWriter("C:/C#_projeleri/C#kareler_oyunu/oyunum/bilgi_dosyalari/oyuncubilgileri.txt"))
            {
                int i = 0;
                while (Oyuncu.oyuncular[i] != null)
                {
                    sr.WriteLine(Oyuncu.oyuncular[i].oyuncuismi);
                    sr.WriteLine(Oyuncu.oyuncular[i].puan);
                    i++;
                }

            }
            // Örnek: Kullanıcıya kapanma onayı sorma
            DialogResult result = MessageBox.Show("Formu kapatmak istediğinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Form kapanmasını iptal et
            }

        }
        public void OyunuBaslat(object sender, EventArgs e)
        {
            SecimPenceresi oyuncubilgilerPenceresi = new SecimPenceresi("isim ve soyisminizi giriniz :", 1, "Ekle");
            TextBox bilgialani = new TextBox();
            oyuncubilgilerPenceresi.bilgialani = bilgialani;
            oyuncubilgilerPenceresi.Controls.Add(bilgialani);
            bilgialani.Location = new Point(85, 60);
            oyuncubilgilerPenceresi.ShowDialog();
            string[] secimler = { "10", "15", "20" };
            SecimPenceresi sec = new SecimPenceresi("boyut seciniz", 3, secimler);
            sec.ShowDialog();
            genel = new OyunGridi();
            Controls.Add(genel);
            oyuncubilgisipaneli = new Oyuncubilgipaneli();
            Controls.Add(oyuncubilgisipaneli);

            idampaneli = new IdamPaneli();
            Controls.Add(idampaneli);
            baslasesi = new SoundPlayer(oyunum.Properties.Resources.oyunbaslangici);

            baslasesi.Play();




            oyunZamanlayici = new Timer
            {
                Interval = 1000 // 1 saniyelik aralık
            };
            oyunZamanlayici.Tick += OyunZamanlayici_Tick; // Tick olayına metodu bağla
                                                          // Süreyi başlat

            zamanPanel = new RoundedPanel
            {
                BackColor = sari,
                Location = new Point(270, 100), // Panelin konumu
                Size = new Size(20, Oyuntasi.kenarUzunlugu*SecimPenceresi.secilenbilgiyiintedonusturme()) // Başlangıç boyutu
            };
            Controls.Add(zamanPanel);

            // Panelin başlangıç uzunluğunu kaydet
            panelBaslangicUzunlugu = zamanPanel.Height;
            // Süreyi göstermek için Label
            sureLabel = new System.Windows.Forms.Label
            {
                Text = "Kalan Süre: 60",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 150),
                AutoSize = true
            };
            kalanSure = 60; // 1 dakika = 60 saniye
            zamanPanel.Height = panelBaslangicUzunlugu; // Panelin uzunluğunu sıfırla
            sureLabel.Text = $"Kalan Süre: {kalanSure}";
            oyunZamanlayici.Start(); // Zamanlayıcıyı başlat
            Controls.Add(sureLabel);
        }
        private void OyunZamanlayici_Tick(object sender, EventArgs e)
        {
           
            // Her saniye kalan süreyi azalt
            kalanSure--;

            // Süreyi güncelle
            sureLabel.Text = $"Kalan Süre: {kalanSure}";
            // Panelin yüksekliğini güncelle
            if (kalanSure > 0)
            {
                zamanPanel.Height = (int)((double)kalanSure / 60 * panelBaslangicUzunlugu);
                if (zamanPanel.Height<(Oyuntasi.kenarUzunlugu*SecimPenceresi.secilenbilgiyiintedonusturme())/2)
                {
                    zamanPanel.BackColor = acikKirmizi;
                }
            }
            // Süre bittiğinde oyun sona erer
            if (kalanSure <= 0)
            {
                oyunbittisesi = new SoundPlayer(oyunum.Properties.Resources.oyunbitti);
                oyunbittisesi.Play();
                oyunZamanlayici.Stop(); // Zamanlayıcıyı durdur
                durdurButonu.Enabled = false; // Durdur/Devam butonunu devre dışı bırak
                BaslaButonu.Enabled=false;
                yenidenoynaButonu.Enabled = true;
                genel.Enabled = false;
                IdamPaneli.oyunkontrolu.Enabled = false;
                MessageBox.Show("oyun bitti !!");
                
            }
            else if (IdamPaneli.adam.VucutUzunlugu==20)
            {
                oyunZamanlayici.Stop(); // Zamanlayıcıyı durdur
                durdurButonu.Enabled = false; // Durdur/Devam butonunu devre dışı bırak
                BaslaButonu.Enabled = false;
                yenidenoynaButonu.Enabled = true;
                genel.Enabled = false;
                IdamPaneli.oyunkontrolu.Enabled = false;
                MessageBox.Show("oyun bitti !!");
            }
        }
        public void OyunuKapat(object sender, EventArgs e)
        {
            Close();
        }
        private void OyunFormu_Load(object sender, EventArgs e)
        {

        }

        private void Oyuncularisirala(object sender , EventArgs e)
        {
            Oyuncu.oyuncularidosyadanoku();
            oyunculistesiguncelle();
            //Siralama siralama = new Siralama();
            Form oyunFormu = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = acikSari,
                Size = new Size(600, 700)
            };
            // Yuvarlak kenarlar oluştur
            oyunFormu.Load += (s, ev) =>
            {
                int radius = 30; // Yuvarlatma yarıçapı
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(oyunFormu.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(oyunFormu.Width - radius, oyunFormu.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, oyunFormu.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                oyunFormu.Region = new Region(path);
            };
            Button kapatButon = new Button
            {
                Text = "X",
                Size = new Size(20, 20),
                Location = new Point(oyunFormu.Width-30, 10),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            kapatButon.FlatAppearance.BorderSize = 0;
            oyunFormu.Controls.Add(kapatButon);
            // oyunculari puana gore siralamak 
            Oyuncu[] siralanmisOyuncular = Oyuncu.oyuncular
            .Where(o => o != null) // Null değerleri filtrele
            .OrderByDescending(o => o.puan) // Puanlara göre büyükten küçüğe sırala
            .ToArray(); // Diziye çevir
            System.Windows.Forms.Label basliklavel = new System.Windows.Forms.Label
            {
                Text = "OYUNUN YILDIZLARI :",
                Location = new Point((oyunFormu.Width/4), 30), // Label'in konumu
                AutoSize = true // Yazıya göre boyutlanır

            };
            basliklavel.Font = new Font("Arial", 20, FontStyle.Bold);
            oyunFormu.Controls.Add(basliklavel);
            if (siralanmisOyuncular != null && siralanmisOyuncular.Length > 0)
            {
                // Form üzerindeki panellerin başlangıç noktası
                int baslangicY = 90;

                // Maksimum genişlik
                int maksimumGenişlik = 300;

                // En yüksek puanı bul
                int enYuksekPuan = siralanmisOyuncular.Max(o => o.puan);

                foreach (var oyuncu in siralanmisOyuncular)
                {
                    // Her oyuncu için bir panel oluştur
                    Panel oyuncuPanel = new Panel
                    {
                        Size = new Size(550, 40), // Panel boyutu (genişlik, yükseklik)
                        Location = new Point(10, baslangicY), // Panelin konumu
                        BorderStyle = BorderStyle.None // İsterseniz sınır çizebilirsiniz
                    };

                    // Oyuncu bilgilerini gösterecek label
                    System.Windows.Forms.Label oyuncuLabel = new  System.Windows.Forms.Label
                    {
                        Text = $"{oyuncu.oyuncuismi}, puan: {oyuncu.puan}",
                        Location = new Point(10, 10), // Label'in konumu
                        AutoSize = true // Yazıya göre boyutlanır
                        
                    };
                    oyuncuLabel.Font = new Font("Arial", 10, FontStyle.Bold);
                    oyuncuPanel.Controls.Add(oyuncuLabel);

                    // Puanı temsil eden yuvarlak panel
                    int hedefGenislik = (int)((double)oyuncu.puan / enYuksekPuan * maksimumGenişlik); // Dinamik genişlik
                    RoundedPanel puanPanel = new RoundedPanel
                    {
                        BorderRadius = 10, // Yuvarlaklık miktarı
                        BackColor = yeşil, // Renk
                        Size = new Size(0, 20), // Başlangıçta genişlik 0
                        Location = new Point(230, 10) // Panelin konumu
                    };
                    oyuncuPanel.Controls.Add(puanPanel);

                    // Oyuncu panelini forma ekle
                    oyunFormu.Controls.Add(oyuncuPanel);

                    // Bir Timer ile genişleme animasyonu
                    Timer animasyonTimer = new Timer
                    {
                        Interval = 10 // Her 10 ms'de bir çalışır
                    };

                    animasyonTimer.Tick += (s, _) =>
                    {
                        if (puanPanel.Width < hedefGenislik)
                        {
                            puanPanel.Width += 5; // Genişliği adım adım artır
                            if (puanPanel.Width > hedefGenislik) // Hedefi aşarsa düzelt
                                puanPanel.Width = hedefGenislik;
                            puanPanel.Invalidate(); // Yeniden çizimi zorla
                        }
                        else
                        {
                            animasyonTimer.Stop(); // Animasyonu durdur
                            animasyonTimer.Dispose(); // Timer'ı bellekten temizle
                        }
                    };

                    animasyonTimer.Start(); // Animasyonu başlat

                    // Bir sonraki panel için başlangıç konumunu ayarla
                    baslangicY += 50; // Y ekseninde aralığı ayarla
                }
            }
            kapatButon.Click += (s , _) => { oyunFormu.Close(); };
            oyunFormu.ShowDialog();
        }
       
        public void oyunculistesiguncelle()
        {
            Oyuncu.oyuncularidosyadanoku();
            using (StreamReader sr = new StreamReader("C:/C#_projeleri/C#kareler_oyunu/oyunum/bilgi_dosyalari/oyuncubilgileri.txt"))
            {
                int i = 0;
                string satir;
                while ((satir = sr.ReadLine()) != null)
                {
                    if (satir == Oyuncu.sonoyuncu.oyuncuismi)
                    {
                        Oyuncu.oyuncular[i] = Oyuncu.sonoyuncu;
                        satir = sr.ReadLine();
                        puan = int.Parse(satir);
                        toplampuan = puan + OyunGridi.oynamapuani;
                        Oyuncu.oyuncular[i].puan = toplampuan;
                        break;
                    }
                    satir = sr.ReadLine();
                    i++;

                }
            }
            using (StreamWriter sr = new StreamWriter("C:/C#_projeleri/C#kareler_oyunu/oyunum/bilgi_dosyalari/oyuncubilgileri.txt"))
            {
                int i = 0;
                while (Oyuncu.oyuncular[i] != null)
                {
                    sr.WriteLine(Oyuncu.oyuncular[i].oyuncuismi);
                    sr.WriteLine(Oyuncu.oyuncular[i].puan);
                    i++;
                }

            }
        }

    }
}
