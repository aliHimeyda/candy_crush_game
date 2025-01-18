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
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;


// Ali HIMEYDA B231200561
namespace oyunum
{
    // Ali HIMEYDA B231200561
    internal class IdamPaneli:Panel
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
        public static PictureBox basicibase;
        public static PictureBox basici;
        public static PictureBox taban1;
        public static PictureBox taban2;
        public static int width = 400;
        public static int height = 700;
        public static Adam adam;
        public static Basici basici2 = new Basici();
        private SoundPlayer adamayaklasti;
        public static bool basicinin_istedigi_altnokta = false;
        private int basicinin_enkucuk_noktasi = 50;
        public static bool basici_adama_temasettimi = false;
        public int ilkpuan = OyunGridi.oynamapuani;
        public int ikincipuan;
        public static Timer oyunkontrolu;
        public IdamPaneli() 
        {
            this.Width = width;
            this.Height = height;
            this.BackColor = gri;
            this.Location = new Point(950, 10);
            adam = new Adam();
            this.Controls.Add(adam);
            adam.Location = new Point(
                (this.Width - adam.Width) / 2,
                this.Height-adam.Height-20
            );
            basicibase = new PictureBox();
            taban1 = new PictureBox();
            taban2 = new PictureBox();
            basici = new PictureBox();
            taban1.Image = Image.FromFile("C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/basicibase.png");
            taban1.BackColor = Color.Transparent;
            taban1.SizeMode = PictureBoxSizeMode.StretchImage;
            taban1.Width = 400;
            taban1.Height = 20;
            this.Controls.Add(taban1);
            taban1.Location = new Point(0, 0);

            taban2.Image = Image.FromFile("C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/basicibase.png");
            taban2.BackColor = Color.Transparent;
            taban2.SizeMode = PictureBoxSizeMode.StretchImage;
            taban2.Width = 400;
            taban2.Height = 20;
            this.Controls.Add(taban2);
            taban2.Location = new Point(0,this.Height-taban2.Height );

            basicibase.Image = Image.FromFile("C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/basici.png");
            basicibase.BackColor = Color.Transparent;
            basicibase.SizeMode = PictureBoxSizeMode.StretchImage;
            basicibase.Width = 250;
            basicibase.Height = 50;
            this.Controls.Add(basicibase);
            basicibase.Location = new Point(
                (this.Width - basicibase.Width) / 2,
                basici.Height+taban1.Height
            );

            basici.Image = Image.FromFile("C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/basicibase.png");
            basici.BackColor = Color.Transparent;
            basici.SizeMode = PictureBoxSizeMode.StretchImage;
            basici.Width = 50;
            basici.Height = 50;
            this.Controls.Add(basici);
            basici.Location = new Point(
                (this.Width - basici.Width) / 2,
                taban1.Height-1
            );


            oyunkontrolu = new Timer();
            oyunkontrolu.Interval = 20;
            oyunkontrolu.Tick += Oyunkontrolu_Tick;
            ikincipuan = ilkpuan;
            int fark;
            oyunkontrolu.Start();

            void Oyunkontrolu_Tick(object sender_, EventArgs e_)
            {
                ilkpuan = ikincipuan;
                ikincipuan = OyunGridi.oynamapuani;
                fark = (ikincipuan - ilkpuan);
                if (ilkpuan != ikincipuan)
                {
                    if (basicinin_enkucuk_noktasi<basici.Height)
                    {
                        basici.Height -= 50;
                        basicibase.Top = basici.Height;
                    }
                    
                }
                else
                {
                    if (basici.Height == (IdamPaneli.height - taban1.Height - taban2.Height - adam.Height) / 2)
                    {
                        adamayaklasti = new SoundPlayer(oyunum.Properties.Resources.yaklasmasesi);
                        adamayaklasti.Play();
                    }
                    if (basici_adama_temasettimi)
                    {
                        
                        adam.Boyutlandirma();
                    }
                    else
                    {
                        basici2.Boyutlandirma();

                    }
                }
            }


        }
       
    }




    // Ali HIMEYDA B231200561
    class Adam : PictureBox
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
        public Adam adam;

        private int vucutuzunlugu;

        public int VucutUzunlugu
        {
            get { return vucutuzunlugu; }
            set 
            {
                if (value<20)
                {
                    IdamPaneli.oyunkontrolu.Enabled = false;
                }
                else
                {
                    vucutuzunlugu = value;
                    this.Height = vucutuzunlugu;
                }
            }
        }

        public Adam()
        {
            this.Image = Image.FromFile("C:/C#_projeleri/C#kareler_oyunu/oyunum/resimler/tamadam.png");
            this.BackColor = Color.Transparent;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Width = 150;
            vucutuzunlugu = 200;
            this.Height = vucutuzunlugu;
        }

        public virtual void Boyutlandirma()
        {
            IdamPaneli.basici_adama_temasettimi = false;
            IdamPaneli.basici.Height += 1;
            IdamPaneli.basicibase.Top = IdamPaneli.basici.Height;
            IdamPaneli.adam.VucutUzunlugu -= 1;
            IdamPaneli.adam.Top += 1;
            if ((IdamPaneli.taban2.Top) == (IdamPaneli.basicibase.Top + IdamPaneli.basicibase.Height))
            {
                IdamPaneli.basicinin_istedigi_altnokta = true;
            }
            else if ((IdamPaneli.basicibase.Top + IdamPaneli.basicibase.Height) == IdamPaneli.adam.Top)
            {
                IdamPaneli.basici_adama_temasettimi = true;
            }
        }

    }
    // Ali HIMEYDA B231200561
    internal class Basici:Adam
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
       
        public Basici() 
        {
        }
        public override void Boyutlandirma()
        {
            IdamPaneli.basici.Height += 1;
            IdamPaneli.basicibase.Top = IdamPaneli.basici.Height;
            if ((IdamPaneli.height-20)==IdamPaneli.basici.Height)
            {
                IdamPaneli.basicinin_istedigi_altnokta = true;
            }
            else if ((IdamPaneli.basicibase.Top+IdamPaneli.basicibase.Height)==IdamPaneli.adam.Top)
            {
                IdamPaneli.basici_adama_temasettimi = true;
            }

        }
    }

}
