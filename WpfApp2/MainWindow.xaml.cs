using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Salesperson> salespersons;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSalespersons();
            lvSalespersons.ItemsSource = salespersons;
        }

        private void InitializeSalespersons()
        {
            salespersons = new ObservableCollection<Salesperson>
            {
                new Salesperson { ShopID = 10153, FullName = "Чупашева А.И.", HireDate = new DateTime(2015, 10, 1), DailyRevenue = 50000 },
                new Salesperson { ShopID = 20174, FullName = "Иванов А.В.", HireDate = new DateTime(2017, 1, 10), DailyRevenue = 55320 },
                new Salesperson { ShopID = 30191, FullName = "Кривцов О.П.", HireDate = new DateTime(2019, 2, 5), DailyRevenue = 45500 },
                new Salesperson { ShopID = 40140, FullName = "Янаева Э.С.", HireDate = new DateTime(2014, 12, 15), DailyRevenue = 60000 }
            };
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            CalculateCommissions();
        }

        private void CalculateCommissions()
        {
            foreach (var person in salespersons)
            {
                person.Commission = person.CalculateCommission();
                person.YearsOfService = person.CalculateYearsOfService();
            }
        }


        private void LvSalespersons_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedPerson = lvSalespersons.SelectedItem as Salesperson;
            if (selectedPerson != null)
            {
                tbFullName.Text = $"ФИО продавца: {selectedPerson.FullName}";
                tbHireDate.Text = $"Дата приема на работу: {selectedPerson.HireDate.ToShortDateString()}";
                tbDailyRevenue.Text = $"Дневная выручка: {selectedPerson.DailyRevenue:F2} руб.";
                tbCommission.Text = $"Комиссионное вознаграждение: {selectedPerson.Commission:F2} руб.";
                tbYearsOfService.Text = $"Стаж работы: {selectedPerson.YearsOfService} лет.";
            }
            else
            {
                tbFullName.Text = "ФИО продавца: ";
                tbHireDate.Text = "Дата приема на работу: ";
                tbDailyRevenue.Text = "Дневная выручка: ";
                tbCommission.Text = "Комиссионное вознаграждение: ";
                tbYearsOfService.Text = "Стаж работы: ";
            }
        }
        
    }
}

    public class Salesperson
    {
        public int ShopID { get; set; }
        public string FullName { get; set; }
        public DateTime HireDate { get; set; }
        public decimal DailyRevenue { get; set; }
        public decimal Commission { get; set; }
        public int YearsOfService { get; set; }

        public decimal CalculateCommission()
        {
            decimal commissionRate = DailyRevenue < 50000 ? 0.03m : 0.06m;
            decimal commission = DailyRevenue * commissionRate;
            if ((DateTime.Now - HireDate).TotalDays > 3 * 365)
            {
                commission *= 1.05m;
            }
            return commission;
        }

        public int CalculateYearsOfService()
        {
            return (DateTime.Now - HireDate).Days / 365;
        }
    }