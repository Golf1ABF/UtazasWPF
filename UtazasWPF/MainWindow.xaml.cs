using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UtazasWPF
{
    public partial class MainWindow : Window
    {
        List<Utasok> utasok; 
        public MainWindow()
        {
            InitializeComponent();

            utasok = new List<Utasok>();
            var sr = new StreamReader("../../../source/utasadat.txt", System.Text.Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                utasok.Add(new Utasok(sr.ReadLine()));
            }

            cbx.SelectedIndex = 1;
            berletTipusa.SelectedIndex = 1;

            datePicker.SelectedDate = DateTime.Now;

            foreach (var item in utasok)
            {
                if (!berletTipusa.Items.Contains(item.BerletTipus))
                {
                    berletTipusa.Items.Add(item.BerletTipus);
                }
                
            }

            for (int i = 0; i <= 29; i++)
            {
                cbx.Items.Add(i);
            }
        }

        private void card_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (card.Text.Length <7)
            {
                counter.Content = $"{card.Text.Length} db";
            }
            else
            {
                counter.Content = "7 a maximális karakterszám";
                card.IsEnabled = false;
            }
        }
    }
}