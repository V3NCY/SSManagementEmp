using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeDocumentManagementApp
{
    public partial class ArchiveListWindow : Window
    {
        private ObservableCollection<Employee> archivedEmployees;

        public ArchiveListWindow()
        {
            InitializeComponent();
            archivedEmployees = new ObservableCollection<Employee>();
            lvArchivedEmployees.ItemsSource = archivedEmployees;
            LoadArchivedEmployees();
        }

        private void LoadArchivedEmployees()
        {
            try
            {
                var employees = ArchiveEmployeeRepository.GetArchivedEmployees();
                archivedEmployees.Clear();
                foreach (var employee in employees)
                {
                    archivedEmployees.Add(employee);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading archived employees: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            LoadArchivedEmployees();
        }

        private void OnDeleteEmployeeMenuItemClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && lvArchivedEmployees.SelectedItem is Employee employee)
            {
                var confirmResult = MessageBox.Show($"Are you sure you want to permanently delete {employee.FullName}? This action cannot be undone.",
                                                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirmResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        ArchiveEmployeeRepository.DeleteArchivedEmployee(employee);
                        archivedEmployees.Remove(employee);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void OnDeleteAllItemsClick(object sender, RoutedEventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to delete all items permanently? This action cannot be undone!",
                                                "Confirm Delete All", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmResult == MessageBoxResult.Yes)
            {
                try
                {
                    ArchiveEmployeeRepository.DeleteAllArchivedEmployees();
                    archivedEmployees.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting all archived employees: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
