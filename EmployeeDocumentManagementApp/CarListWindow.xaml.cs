using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

namespace EmployeeDocumentManagementApp
{
    public partial class CarListWindow : Window
    {
        public CarListWindow()
        {
            InitializeComponent();
        }

        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
        }

        private void lvCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvCars.SelectedItem != null)
            {
                MessageBox.Show($"Selected car: {(lvCars.SelectedItem as Car).Model}");
            }
        }

        public class Car
        {
            public int CarID { get; set; }

            public string Brand { get; set; }
            public string Model { get; set; }
            public string YearManufacture { get; set; }
            public string CarNo { get; set; }
            public double Expense { get; set; }
            public double Acceleration { get; set; }
            public string EngineType { get; set; }
            public string Transmission { get; set; }
            public string Suspension { get; set; }
        }
    }
}
