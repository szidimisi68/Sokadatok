using ConsoleAppOrgBig;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
            List<Organization> organizations = new List<Organization>();
        public MainWindow()
        {
            File.ReadAllLines("C:\\teszt\\organizations-100000.csv").Skip(1).ToList().ForEach(x => organizations.Add(new Organization(x.Split(";"))));
            
            InitializeComponent();

            List<string> orszagok = new List<string>();
            orszagok.Add("none");
            organizations.Select(x => x.Country).Distinct().OrderBy(x => x).ToList().ForEach(x => orszagok.Add(x));
            cbOrszag.ItemsSource = orszagok;

            List<string> evek = new List<string>();
            evek.Add("none");
            organizations.Select(x => x.Founded).Distinct().OrderBy(x => x).ToList().ForEach(x => evek.Add(Convert.ToString(x)));
            cbEv.ItemsSource = evek;

            dgAdatok.ItemsSource = organizations;
            lbOsszes.Content = $"Összes: {organizations.Sum(x => x.EmployeesNumber)}";
        }

        private void cbOrszag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Organization> szurt = organizations;

            string orszag = cbOrszag.SelectedItem.ToString();
            if (cbOrszag.SelectedIndex != 0 && cbOrszag.SelectedIndex != -1)
            {
                szurt = orszagSzures(szurt, orszag);
            }
            int ev = Convert.ToInt32(cbEv.SelectedItem);

            if (cbEv.SelectedIndex != 0 && cbEv.SelectedIndex != -1)
            {
                szurt = evSzures(szurt, ev);
            }
            dgAdatok.ItemsSource = szurt;
            lbOsszes.Content = $"Összes: {szurt.Sum(x => x.EmployeesNumber)}";

        }
        private void cbEv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Organization> szurt = organizations;

            string orszag = cbOrszag.SelectedItem.ToString();
            if (cbOrszag.SelectedIndex != 0 && cbOrszag.SelectedIndex != -1)
            {
                szurt = orszagSzures(szurt, orszag);
            }
            int ev = Convert.ToInt32(cbEv.SelectedItem);

            if (cbEv.SelectedIndex != 0 && cbEv.SelectedIndex != -1)
            {
                szurt = evSzures(szurt, ev);
            }
            dgAdatok.ItemsSource = szurt;
            lbOsszes.Content = $"Összes: {szurt.Sum(x => x.EmployeesNumber)}";
        }

        static List<Organization> orszagSzures(List<Organization> szurt, string orszag)
        {
            szurt = szurt.Where(x => x.Country == orszag).ToList();
            return szurt;
        }

        static List<Organization> evSzures(List<Organization> szurt, int ev)
        {
            szurt = szurt.Where(x => x.Founded == ev).ToList();
            return szurt;
        }

    }
}
