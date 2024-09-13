using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtazasWPF
{
    internal class Utasok
    {
        public int Allomas { get; set; }
        public DateTime Felszallas { get; set; }
        public int EgyediAzonosito { get; set; }
        public string BerletTipus { get; set; }
        public DateTime? BerletErvenyesseg { get; set; }

        public Utasok(string sor)
        {
            var s = sor.Split(" ");
            this.Allomas = int.Parse(s[0]);
            this.Felszallas = DateTime.ParseExact(s[1], "yyyyMMdd-HHmm", null);
            this.EgyediAzonosito = int.Parse(s[2]);
            this.BerletTipus = s[3];
            DateTime ervenyessegDatum;
            if (DateTime.TryParseExact(s[4], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out ervenyessegDatum))
            {
                this.BerletErvenyesseg = ervenyessegDatum;
            }
            else
            {
                this.BerletErvenyesseg = null;
            }
        }

        public static implicit operator Utasok(List<Utasok> v)
        {
            throw new NotImplementedException();
        }
    }
}
