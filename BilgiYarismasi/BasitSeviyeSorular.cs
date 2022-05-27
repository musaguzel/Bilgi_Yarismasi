using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgiYarismasi
{
    class BasitSeviyeSorular : Sorular
    {
        public BasitSeviyeSorular(string soru, string cevapA, string cevapB, string cevapC, string cevapD, string dogruCevap) : base(soru, cevapA, cevapB, cevapC, cevapD, dogruCevap)
        {
        }
    }
}
