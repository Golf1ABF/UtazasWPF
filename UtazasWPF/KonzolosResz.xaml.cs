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

namespace UtazasWPF
{
    public partial class KonzolosResz : Window
    {
        public KonzolosResz()
        {
            InitializeComponent();
            Utasok lista = new List<Utasok>();
            var sr = new StreamReader("../../../source/utasadat.txt", System.Text.Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                lista.
            }
        }
    }
}
