using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace BilgiYarismasi
{
    public partial class FormSorular : Form
    {
        String name;
        String oyunLevel;
        int oyunBitisNo;
        int oyunBitisNo2;

        public FormSorular(String oyunLevel,String name)
        {
            this.name = name;
            this.oyunLevel = oyunLevel;
            if(oyunLevel == "basicLevel")
            {
                oyunBitisNo2 = 5;
                oyunBitisNo = 6;
            }else if(oyunLevel == "mediumLevel")            //önceki formda seçilen oyun paketine göre oyun levelinin hangisi olucağını ve oyunun                                                            
            {                                               //bitiş sorusunuayarlar
                oyunBitisNo2 = 8;
                oyunBitisNo = 9;
            }
            else
            {
                oyunBitisNo2 = 11;
                oyunBitisNo = 12;
            }
            InitializeComponent();           
            music.setUrl("gamemusic.mp3");
        }


        BasitSeviyeSorular[] soru = new BasitSeviyeSorular[6];
        OrtaSeviyeSorular[] soruOrtaSeviye = new OrtaSeviyeSorular[9];        //Sınıflardan soru dizileri oluşturma
        ZorSeviyeSorular[] soruZorSeviye = new ZorSeviyeSorular[12];
        PictureBox[] pictureBox = new PictureBox[12];
        int soruNo = 1;
        int sayacButton = 1;  //soruda A B C D şıklarından birine tıklandığında doğru cevabın gelmesi için beklenen süre

        private void FormSorular_Load(object sender, EventArgs e)
        {
            basitSeviyeSoruOlustur();
            ortaSeviyeSoruOlustur();
            zorSeviyeSoruOlustur();               //form ilk açıldığında soruları oluştur
            yeniSoruGetir();
            resimAyarla();

        }
        public void yeniSoruGetir()      //sonraki soruya geçmek istenildiğinde yeni soruyu getirme
        {
            timer1.Start();
            basitSeviyeSoruOlustur();
            ortaSeviyeSoruOlustur();
            zorSeviyeSoruOlustur();

            if (oyunLevel == "basicLevel")
            {
                for(int i = 0; i <= 5; i++)
                {
                    if (LblSoruNo.Text.Equals(i.ToString()))
                    {
                        labelSoru.Text = soru[i].Soru;
                        rjButton1.Text = soru[i].CevapA;
                        rjButton2.Text = soru[i].CevapB;
                        rjButton3.Text = soru[i].CevapC;
                        rjButton4.Text = soru[i].CevapD;
                    }
                }
            }else if (oyunLevel == "mediumLevel")
            {
                for (int i = 0; i <= 8; i++)
                {
                    if (LblSoruNo.Text.Equals(i.ToString()))
                    {
                        labelSoru.Text = soruOrtaSeviye[i].Soru;
                        rjButton1.Text = soruOrtaSeviye[i].CevapA;
                        rjButton2.Text = soruOrtaSeviye[i].CevapB;
                        rjButton3.Text = soruOrtaSeviye[i].CevapC;
                        rjButton4.Text = soruOrtaSeviye[i].CevapD;
                    }
                }
            }else
            {
                for (int i = 0; i <= 11; i++)
                {
                    if (LblSoruNo.Text.Equals(i.ToString()))
                    {
                        labelSoru.Text = soruZorSeviye[i].Soru;
                        rjButton1.Text = soruZorSeviye[i].CevapA;
                        rjButton2.Text = soruZorSeviye[i].CevapB;
                        rjButton3.Text = soruZorSeviye[i].CevapC;
                        rjButton4.Text = soruZorSeviye[i].CevapD;
                    }
                }
            }

            resetButtonColor();

        }

        public void resetButtonColor()  //sonraki soruya geçildiğinde button renklerini sıfırlama
        {
            rjButton1.BackColor = Color.GhostWhite;
            rjButton2.BackColor = Color.GhostWhite;
            rjButton3.BackColor = Color.GhostWhite;
            rjButton4.BackColor = Color.GhostWhite;
        }

        int sayac = 60;

        private void timer1_Tick(object sender, EventArgs e)        //her soru için 60 sn süre ayarlama
        {
            label3.Text = "Kalan Süre: " + sayac.ToString();
            timer1.Interval = 1000;

            sayac--;

            if (sayac < 0)
            {
                timer1.Stop();
            }
            else
            {
                progressBar1.Value = sayac;
            }
        }


        private void timer2_Tick(object sender, EventArgs e) // Şıklardan birine tıklandığında belirlenen süre kadar bekle ve doğru cevabı göster
        {                                                    // Doğruysa butonun rengini yeşil ve diğer soruya geçme butonunu görünür yap
            timer2.Interval = 1000;                          // Yanlışsa butonun rengini gri yap ve aldığı para ödülünü göster
            sayacButton--;
            if (sayacButton < 0)
            {
                timer2.Stop();
                if(oyunLevel == "basicLevel")
                {
                    if (tiklananButton.Text == soru[soruNo].DogruCevap)
                    {
                        tiklananButton.BackColor = Color.Green;
                        rjButtonSonrakiSoru.Visible = true;
                        pictureBox[soruNo].Visible = true;
                    }
                    else
                    {
                        tiklananButton.BackColor = Color.Gray;
                        OyunBitirPuanGoster();
                    }
                }else if(oyunLevel == "mediumLevel")
                {
                    if (tiklananButton.Text == soruOrtaSeviye[soruNo].DogruCevap)
                    {
                        tiklananButton.BackColor = Color.Green;
                        rjButtonSonrakiSoru.Visible = true;
                        pictureBox[soruNo].Visible = true;
                    }
                    else
                    {
                        tiklananButton.BackColor = Color.Gray;
                        OyunBitirPuanGoster();
                    }
                }
                else
                {
                    if (tiklananButton.Text == soruZorSeviye[soruNo].DogruCevap)
                    {
                        tiklananButton.BackColor = Color.Green;
                        rjButtonSonrakiSoru.Visible = true;
                        pictureBox[soruNo].Visible = true;
                    }
                    else
                    {
                        tiklananButton.BackColor = Color.Gray;
                        OyunBitirPuanGoster();
                    }
                }
            }
            else
            {
                tiklananButton.BackColor = Color.Orange;
            }
        }

        public void yarismaSonuParaGoster() // Leveli başarılı bir şekilde geçerse para ödüllerini göster
        {
            rjButtonSonrakiSoru.Visible = true;
            rjButtonSonrakiSoru.Text = "Oyun Bitti";
            switch (soruNo.ToString())
            {
                case "5": MessageBox.Show("Tebrikler Yarışmamızdan 30.000 TL Ödül Kazandınız\nGüle Güle Harcayınız.."); break;
                case "8": MessageBox.Show("Tebrikler Yarışmamızdan 250.000 TL Ödül Kazandınız\nGüle Güle Harcayınız.."); break;
                case "11": MessageBox.Show("Tebrikler Yarışmamızdan 1.000.000 TL Ödül Kazandınız\nGüle Güle Harcayınız.."); break;

            }
        }
        public void OyunBitirPuanGoster() // herhangi bir soruda yanlış olursa oyunu bitir ve puanını göster
        {

            rjButtonSonrakiSoru.Visible = true;
            rjButtonSonrakiSoru.Text = "Oyun Bitti";
            switch (soruNo.ToString())
            {
                case "1": MessageBox.Show("Malesef Yarışmamızdan Ödül Kazanamadınız :(("); break;
                case "2": MessageBox.Show("Tebrikler Yarışmamızdan 1.+000 TL Ödül Kazandınız"); break;
                case "3": MessageBox.Show("Tebrikler Yarışmamızdan 3.000 TL Ödül Kazandınız"); break;
                case "4": MessageBox.Show("Tebrikler Yarışmamızdan 7.000 TL Ödül Kazandınız"); break;
                case "5": MessageBox.Show("Tebrikler Yarışmamızdan 15.000 TL Ödül Kazandınız"); break;
                case "6": MessageBox.Show("Tebrikler Yarışmamızdan 30.000 TL Ödül Kazandınız"); break;
                case "7": MessageBox.Show("Tebrikler Yarışmamızdan 60.000 TL Ödül Kazandınız"); break;
                case "8": MessageBox.Show("Tebrikler Yarışmamızdan 120.000 TL Ödül Kazandınız"); break;
                case "9": MessageBox.Show("Tebrikler Yarışmamızdan 250.000 TL Ödül Kazandınız"); break;
                case "10": MessageBox.Show("Tebrikler Yarışmamızdan 500.000 TL Ödül Kazandınız"); break;
                case "11": MessageBox.Show("Tebrikler Yarışmamızdan 1.000.000 TL Ödül Kazandınız"); break;

            }
        }

        public void resimAyarla() // her soru için yeşil tik resimlerini ayarla
        {
            pictureBox[1] = PcrBox1;
            pictureBox[2] = PcrBox2;
            pictureBox[3] = PcrBox3;
            pictureBox[4] = PcrBox4;
            pictureBox[5] = PcrBox5;
            pictureBox[6] = PcrBox6;
            pictureBox[7] = PcrBox7;
            pictureBox[8] = PcrBox8;
            pictureBox[9] = PcrBox9;
            pictureBox[10] = PcrBox10;
            pictureBox[11] = PcrBox11;
        }
        public void basitSeviyeSoruOlustur() // Basit seviye soruları oluşturma
        {
            soru[1] = new BasitSeviyeSorular("Hangi İl Ege Bölgemizde değildir?", "A- İzmir", "B- Kütahya", "C- Antalya", "D- Muğla", "C- Antalya");
            soru[2] = new BasitSeviyeSorular("Cumhuriyet kaç yilinda ilan edilmiştir?", "A- 1920", "B- 1921", "C- 1922", "D- 1923", "D- 1923");
            soru[3] = new BasitSeviyeSorular("Bir Gün Kaç Saniyedir ? ", "A - 86000", "B - 88600", "C - 86400", "D - 84800", "C - 86400");
            soru[4] = new BasitSeviyeSorular("Tsunami Felaketinde En Fazla Zarar Gören Güney Asya Ülkesi Aşağıdakilerden Hangisidir?", "A- Endonezya", "B- Srilanka", "C- Tayland", "D- Hindistan", "A- Endonezya");
            soru[5] = new BasitSeviyeSorular("Ana taşıyıcı kuleleri arasındaki mesafe en uzun olan, 'en uzun asma köprü' hangisidir?", "A- Japonya'daki Akaşi Kaikyo Köprüsü", "B- Türkiye'deki 1915 Çanakkale Köprüsü", "C- Çin'deki Yangtze Köprüsü", "D- ABD'deki Golden Gate Köprüsü", "B- Türkiye'deki 1915 Çanakkale Köprüsü");
           
        }
        public void ortaSeviyeSoruOlustur() // Orta seviye soruları oluşturma
        {
            soruOrtaSeviye[1] = new OrtaSeviyeSorular("Cevdet Bey ve Oğulları Eseri Kime Aittir?", "A- Orhan Pamuk", "B- Yahya Kemal Bayatlı", "C- Atilla İlhan", "D- Samipaşazade Sezai", "A- Orhan Pamuk");
            soruOrtaSeviye[2] = new OrtaSeviyeSorular("Hangi Ülkenin İki Tane Başkenti Vardır?", "A- Güney Afrika", "B- Senegal", "C- El Salvador", "D- Venezuela", "A- Güney Afrika");
            soruOrtaSeviye[3] = new OrtaSeviyeSorular("Üç Büyük Dince Kutsal Sayılan Şehir Hangisidir ? ", "A - Mekke", "B - Kudüs", "C - Roma", "D - Istanbul", "B - Kudüs");
            soruOrtaSeviye[4] = new OrtaSeviyeSorular("Hangi İlimizde Demiryolu Yoktur?", "A- Batman", "B- Kütahya", "C- Aydın", "D- Muğla", "D- Muğla");
            soruOrtaSeviye[5] = new OrtaSeviyeSorular("Son Kuşlar hangi yazarimiza aittir?", "A- Sait Faik", "B- Atilla İlhan", "C- Reşat Nuri", "D- Orhan Pamuk", "A- Sait Faik");
            soruOrtaSeviye[6] = new OrtaSeviyeSorular("Bir Sebepten Dolayı Tek Kulağına Küpe Takan Osmanlı Padişahı Kimdir?", "A- Kanuni Sultan Süleyman", "B- Yavuz Sultan Selim", "C- Orhan Bey", "D- Fatih Sultan Selim", "B- Yavuz Sultan Selim");
            soruOrtaSeviye[7] = new OrtaSeviyeSorular("Romen Rakamında Hangi Sayı Yoktur?", "A- 0", "B- 50", "C- 100", "D- 1000", "A- 0");
            soruOrtaSeviye[8] = new OrtaSeviyeSorular("Sherlock Holmes'un birçok macerasında yanında olan yakın dostu ve yardımcısı kimdir?", "A- Hercule Poirot", "B- Dr. Watson", "C- Müfettiş Clouseau", "D- Miss Marple", "B- Dr. Watson");
        }

        public void zorSeviyeSoruOlustur() // Zor seviye soruları oluşturma
        {
            soruZorSeviye[1] = new ZorSeviyeSorular("Hangi adda bir sos yoktur?", "A- Pesto", "B- Ranch", "C- Avogadro", "D- Paprika", "C- Avogadro");
            soruZorSeviye[2] = new ZorSeviyeSorular("Bugüne kadar nerede ölen hiçbir insan olmamıştır?", "A- Antarktika kıtasında", "B- Mariana Çukuru'nun dibinde", "C- Everest Dağı'nda", "D- Dünya atmosferinin dışında", "B- Mariana Çukuru'nun dibinde");
            soruZorSeviye[3] = new ZorSeviyeSorular("Türkçeye Arapçadan geçen 'muhteşem' kelimesinin kökeninin anlamı hangisidir?", "A- Askerler", "B- Efendiler", "C- Köylüler", "D- Hizmetçiler", "D- Hizmetçiler");
            soruZorSeviye[4] = new ZorSeviyeSorular("Asfaltın üzerine beyaz çizgiler çekilerek oluşturulan yaya geçitleri hangi ülkede 1951 yılında resmi olarak\nilk defa kullanılmaya başlanmıştır?", "A- Fransa", "B- Almanya", "C- Birleşik Krallık", "D- ABD", "C- Birleşik Krallık");
            soruZorSeviye[5] = new ZorSeviyeSorular("İki kez uçak kazası geçiren, köpekbalığı avlarken bacaklarını silahla vuran\n1. Dünya Savaşı'nda bacaklarına 200'den fazla şarapnel parçası isabet edip ölmeyen yazar kimdir?", "A- Mark Twain", "B- Ernest Hemingway", "C- George Orwell", "D- F. Scott Fitzgerald", "B- Ernest Hemingway");
            soruZorSeviye[6] = new ZorSeviyeSorular("Amerika'da bulunan ve dünyanın en ücra yerlerinden biri olmasından ötürü \n'erişilemez nokta' adı verilen Güney Kutup Noktası'na kimin büstü konulmuştur?", "A- Vladimir Lenin", "B- Winston Churchill", "C- Mao Zedong", "D- George Washington", "A- Vladimir Lenin");
            soruZorSeviye[7] = new ZorSeviyeSorular("Türkçeye Farsçadan geçen 'zengin' kelimesinin köken anlamı hangisidir?", "A- Taş", "B- Kum", "C- Deniz", "D- Altın", "A- Taş");
            soruZorSeviye[8] = new ZorSeviyeSorular("Osmanlı Devleti'nde, saraydaki 'kehhalbaşı' adlı hekim hangisinin tedavisi için görevliydi?", "A- Diş", "B- Bel", "C- Göz", "D- Kulak", "C- Göz");
            soruZorSeviye[9] = new ZorSeviyeSorular("Tosun Paşa, Züğürt Ağa, Çiçek Abbas, Şekerpare, Muhsin Bey ve Eşkıya filmlerinin senaristi kimdir?", "A- Ertem Eğilmez", "B- Atıf Yılmaz", "C- Yavuz Turgul", "D- Kartal Tibet", "C- Yavuz Turgul");
            soruZorSeviye[10] = new ZorSeviyeSorular("Cevabı 'sam yeli' olan bir bulmaca sorusunda sorulan hangisi olur?", "A- Çölden esen sıcak rüzgar", "B- Denizden esen soğuk rüzgar", "C- Nehirden esen sıcak rüzgar", "D- Dağdan esen soğuk rüzgar", "A- Çölden esen sıcak rüzgar");
            soruZorSeviye[11] = new ZorSeviyeSorular("Hangisi Ay'ın yörüngesinde tur atmış ve canlı olarak Dünya'ya dönmüş ilk canlılardandır?", "A- Köpek", "B- İnsan", "C- Salyangoz", "D- Kaplumbağa", "D- Kaplumbağa");
        }
        public void disableButton() // Herhangi bir şıkka tıklandığında bir daha tıklanmaması için butonları tıklanamaz yapma
        {
            rjButton1.Enabled = false;
            rjButton2.Enabled = false;
            rjButton3.Enabled = false;
            rjButton4.Enabled = false;
        }
        public void enableButton() // Diğer soruya geçildiğinde butonları tekrar aktif etme
        {
            rjButton1.Enabled = true;
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton4.Enabled = true;
        }




        Button tiklananButton;
        private void rjButton1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            disableButton();
            tiklananButton = rjButton1;
            timer2.Start();
            if (soruNo.ToString() == oyunBitisNo2.ToString()) // oyun bitiş no ne ise o soruya geldiğinde çalışacak kod 
            {
                rjButtonSonrakiSoru.Text = "Oyun Bitti Tebrikler";
                yarismaSonuParaGoster();
            }

        }
       

       

        private void rjButtonSonrakiSoru_Click(object sender, EventArgs e)  
        {


            sayac = 60;
            sayacButton = 1;
            enableButton();
            soruNo++;
            LblSoruNo.Text = soruNo.ToString();
            yeniSoruGetir();
            rjButtonSonrakiSoru.Visible = false;
            if (soruNo.ToString() == oyunBitisNo.ToString() || rjButtonSonrakiSoru.Text == "Oyun Bitti")
            {

                FormAnaMenu frm = new FormAnaMenu(name);
                frm.Show();
                this.Hide();

            }

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            tiklananButton = rjButton2;
            timer2.Start();
            disableButton();
            if (soruNo.ToString() == oyunBitisNo2.ToString())
            {
                rjButtonSonrakiSoru.Text = "Oyun Bitti Tebrikler";
                yarismaSonuParaGoster();
            }

        }
       

        private void rjButton3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            disableButton();
            tiklananButton = rjButton3;
            timer2.Start();
            if (soruNo.ToString() == oyunBitisNo2.ToString())
            {
                rjButtonSonrakiSoru.Text = "Oyun Bitti Tebrikler";
                yarismaSonuParaGoster();
            }
        }

        private void rjButton4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            disableButton();
            tiklananButton = rjButton4;
            timer2.Start();
            if (soruNo.ToString() == oyunBitisNo2.ToString())
            {
                rjButtonSonrakiSoru.Text = "Oyun Bitti Tebrikler";
                yarismaSonuParaGoster();
            }
        }

        
        GameMusic music = new GameMusic();
        private void toggleSwitch1_Toggled(object sender, EventArgs e)  // oyun müziğin ayarlama
        {
            if (toggleSwitch1.IsOn)
            {
                music.muzikAcKapat(true);
            }
            else
            {
                music.muzikAcKapat(false);
            }
        }
    }

}
