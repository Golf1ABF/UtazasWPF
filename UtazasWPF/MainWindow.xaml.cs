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

            cbx.SelectedIndex = 0;
            berletTipusa.SelectedIndex = 0;

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
            if (card.Text.Length < 7)
            {
                counter.Content = $"{card.Text.Length} db";
            }
            else
            {
                counter.Content = "7 a maximális karakterszám";
                card.IsEnabled = false;
            }
        }

        private void jegy_Checked(object sender, RoutedEventArgs e)
        {
            jegyBox.Visibility = Visibility.Visible;
            berletBox.Visibility = Visibility.Hidden;
        }

        private void berlet_Checked(object sender, RoutedEventArgs e)
        {
            berletBox.Visibility = Visibility.Visible;
            jegyBox.Visibility = Visibility.Hidden;
        }

        private void sliderJegy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sliderCounter.Content = $"{((int)sliderJegy.Value)} db";
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            if (cbx.SelectedIndex == 0)
            {
                MessageBox.Show("Nem választott megállót!");
            } else if (!TimeSpan.TryParseExact(timeTbx.Text, @"hh\:mm", null, out TimeSpan time))
            {
                MessageBox.Show("Nem óó:pp formátumban adtad meg az időt!");
            } else if (card.Text.Length != 7)
            {
                MessageBox.Show("A kártya azonosítója nem 7 karakter hosszú!");
            } else if (card.Text.Length < 1)
            {
                MessageBox.Show("Pozitív egész számot adj meg!");
            } else if (berletTipusa.SelectedIndex == 0 && berlet.IsChecked == true)
            {
                MessageBox.Show("Nem adta meg a bérlet tipusát!");
            } else if (berletErvenyesseg == null && berlet.IsChecked == true)
            {
                MessageBox.Show("Nem adta meg a \r\nbérlet érvényességi idejét!");
            } else
            {
                using (StreamWriter sw = new StreamWriter("../../../source/utasadat.txt", true))
                {
                    if (berlet.IsChecked == true)
                    {
                        sw.WriteLine($"{cbx.SelectedItem} {datePicker.SelectedDate?.ToString("yyyyMMdd")}-{timeTbx.Text.Replace(":", "")} {card.Text} {berletTipusa.Text} {ervenyessegDate.SelectedDate?.ToString("yyyyMMdd")}");
                    }
                    else
                    {
                        sw.WriteLine($"{cbx.SelectedItem} {datePicker.SelectedDate?.ToString("yyyyMMdd")}-{timeTbx.Text.Replace(":", "")} {card.Text} JGY {sliderCounter.Content}");
                    }
                }
                MessageBox.Show("Az adatok tárolása sikeres volt!");
                cbx.SelectedIndex = 0;
                berletTipusa.SelectedIndex = 0;
                timeTbx.Text = string.Empty;
                card.Text = string.Empty;
                card.IsEnabled = true;
                berlet.IsChecked = false;
                berletBox.Visibility = Visibility.Hidden;
                jegy.IsChecked = false;
                jegyBox.Visibility = Visibility.Hidden;
            }
        }
    }
}