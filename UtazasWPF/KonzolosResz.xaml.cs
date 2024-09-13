using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace UtazasWPF
{
    public partial class KonzolosResz : Window
    {
        public KonzolosResz()
        {
            InitializeComponent();
            List<Utasok> lista = new List<Utasok>();
            var sr = new StreamReader("../../../source/utasadat.txt", System.Text.Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                lista.Add(new Utasok(sr.ReadLine()));
            }

            harmadikF.Content = $"{lista.Count()} utas utazott";


            var elutasitottUtasok = lista
               .Where(utas => utas.BerletErvenyesseg.HasValue && utas.Felszallas > utas.BerletErvenyesseg.Value)
               .Count();

            negyedikF.Content = elutasitottUtasok;

            otodikF.Content = lista
               .GroupBy(utas => utas.Allomas)
               .Select(group => new { Allomas = group.Key, Count = group.Count() })
               .OrderByDescending(group => group.Count)
               .ThenBy(group => group.Allomas)
               .FirstOrDefault();

            var kedvezmenyesUtasok = 0;
            var ingyenesUtasok = 0;

            foreach (var item in lista)
            {

                if (item.BerletTipus == "TAB" || item.BerletTipus == "NYB")
                {
                    kedvezmenyesUtasok++;
                }
                else if (item.BerletTipus == "GYK" || item.BerletTipus == "NYP" || item.BerletTipus == "RVS" )
                {
                    ingyenesUtasok++;
                }
            }

            hatodikF.Content = $"Kedvezményes utasok: {kedvezmenyesUtasok}, Ingyenes utasok: {ingyenesUtasok}";

            DateTime today = DateTime.Now;

            var figyelmeztetesreJogosultak = lista
                .Where(utas => utas.BerletErvenyesseg.HasValue &&
                               utas.BerletErvenyesseg.Value >= today &&
                               utas.BerletErvenyesseg.Value <= today.AddDays(3))
                .Select(utas => $"{utas.EgyediAzonosito} {utas.BerletErvenyesseg.Value.ToString("yyyy-MM-dd")}")
                .ToList();

            using (StreamWriter sw = new StreamWriter("../../../source/figyelmeztetes.txt", true))
            {
                foreach (var item in figyelmeztetesreJogosultak)
                {
                    sw.WriteLine(item);
                }
            }
        }
    }
}
