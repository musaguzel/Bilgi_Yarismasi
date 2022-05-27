using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgiYarismasi
{
    class Sorular
    {
        private String soru;
        private String cevapA;
        private String cevapB;
        private String cevapC;
        private String cevapD;
        private String dogruCevap;

        public Sorular(string soru, string cevapA, string cevapB, string cevapC, string cevapD,string dogruCevap)
        {
            this.Soru = soru;
            this.CevapA = cevapA;
            this.CevapB = cevapB;
            this.CevapC = cevapC;
            this.CevapD = cevapD;
            this.DogruCevap = dogruCevap;
        }

        public string Soru { get => soru; set => soru = value; }
        public string CevapA { get => cevapA; set => cevapA = value; }
        public string CevapB { get => cevapB; set => cevapB = value; }
        public string CevapC { get => cevapC; set => cevapC = value; }
        public string CevapD { get => cevapD; set => cevapD = value; }
        public string DogruCevap { get => dogruCevap; set => dogruCevap = value; }
    }
}
