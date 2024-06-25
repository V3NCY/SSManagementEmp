using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using iAnywhere.Data.SQLAnywhere;
using System.Windows.Controls;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;


namespace EmployeeDocumentManagementApp
{
    public partial class EmployeeListWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Employee> employeesList;
        public ObservableCollection<Employee> EmployeesList
        {
            get { return employeesList; }
            set
            {
                employeesList = value;
                OnPropertyChanged("EmployeesList");
            }
        }

        public ICommand RefreshCommand => new RelayCommand(() => LoadEmployeeList());

        public EmployeeListWindow()
        {
            InitializeComponent();
            DataContext = this;
            SubscribeToEmployeeChanges();
            LoadEmployeeList();
        }

        private async void EmployeeListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadEmployeeListAsync();
        }

        public async Task LoadEmployeeListAsync()
        {
            try
            {
                var newEmployeeList = await Task.Run(() => LoadEmployeesFromDatabase());
                var activeEmployees = newEmployeeList.Where(emp => !emp.IsArchived);
                EmployeesList = new ObservableCollection<Employee>(activeEmployees);
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error loading employee list");
            }
        }

        public void LoadEmployeeList()
        {
            try
            {
                EmployeesList = LoadEmployeesFromDatabase();
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error loading employee list");
            }
        }

        public void RefreshEmployeeList()
        {
            try
            {
                var newEmployeeList = LoadEmployeesFromDatabase();
                var activeEmployees = newEmployeeList.Where(emp => !emp.IsArchived);
                EmployeesList = new ObservableCollection<Employee>(activeEmployees);
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error loading employee list");
            }
        }

        public ObservableCollection<Employee> LoadEmployeesFromDatabase()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBEmployees"].ConnectionString;
                using (SAConnection connection = new SAConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT EmployeeId, FirstName, LastName, EGN, RemainingLeaveDays, JobTitle, Department, IsArchived FROM Employees";
                    using (SACommand command = new SACommand(query, connection))
                    {
                        using (SADataReader reader = command.ExecuteReader())
                        {
                            var employees = new ObservableCollection<Employee>();
                            while (reader.Read())
                            {
                                var employee = new Employee
                                {
                                    EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeId")) ? 0 : reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                    FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("LastName")),
                                    EGN = reader.IsDBNull(reader.GetOrdinal("EGN")) ? string.Empty : reader.GetString(reader.GetOrdinal("EGN")),
                                    RemainingLeaveDays = reader.IsDBNull(reader.GetOrdinal("RemainingLeaveDays")) ? 0 : reader.GetInt32(reader.GetOrdinal("RemainingLeaveDays")),
                                    JobTitle = reader.IsDBNull(reader.GetOrdinal("JobTitle")) ? string.Empty : reader.GetString(reader.GetOrdinal("JobTitle")),
                                    Department = reader.IsDBNull(reader.GetOrdinal("Department")) ? string.Empty : reader.GetString(reader.GetOrdinal("Department")),
                                    IsArchived = reader.IsDBNull(reader.GetOrdinal("IsArchived")) ? false : reader.GetBoolean(reader.GetOrdinal("IsArchived"))
                                };
                                employees.Add(employee);
                            }
                            return employees;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error loading employees");
                return new ObservableCollection<Employee>();
            }
        }

        private void HandleError(Exception ex, string message)
        {
            Console.WriteLine($"{message}: {ex.Message}");
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SubscribeToEmployeeChanges()
        {
            if (employeesList != null)
            {
                employeesList.CollectionChanged += EmployeesList_CollectionChanged;
            }
        }

        private void EmployeesList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        private class RelayCommand : ICommand
        {
            private readonly Action _execute;

            public RelayCommand(Action execute)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter)
            {
                _execute();
            }
        }
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle the event
        }
        private void lvEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = (Employee)lvEmployees.SelectedItem;
            if (selectedEmployee != null)
            {
                EditEmployeeWindow editWindow = new EditEmployeeWindow(this, selectedEmployee);
                editWindow.ShowDialog();
            }
        }

        private void OnArchiveMenuItemClick(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.SelectedItem is Employee selectedEmployee)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to archive this employee?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        ArchiveEmployeeRepository.ArchiveEmployee(selectedEmployee);
                        EmployeesList.Remove(selectedEmployee);
                    }
                    catch (Exception ex)
                    {
                        HandleError(ex, "Error archiving employee");
                    }
                }
            }
        }

        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            RefreshEmployeeList();
        }
    }
}
