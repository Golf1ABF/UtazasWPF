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
        public string Felszallas { get; set; }
        public int EgyediAzonosito { get; set; }
        public string BerletTipus { get; set; }
        public DateTime BerletErvenyesseg { get; set; }

        public Utasok(string sor)
        {
            var s = sor.Split(" ");
            this.Allomas = int.Parse(s[0]);
            this.Felszallas = s[1];
            this.EgyediAzonosito = int.Parse(s[2]);
            this.BerletTipus = s[3];
            this.BerletErvenyesseg = DateTime.ParseExact(s[4], "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        public static implicit operator Utasok(List<Utasok> v)
        {
            throw new NotImplementedException();
        }
    }
}
