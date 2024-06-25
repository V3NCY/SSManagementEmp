using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using iAnywhere.Data.SQLAnywhere;

namespace EmployeeDocumentManagementApp
{
    public static class ArchiveEmployeeRepository
    {
        private static ObservableCollection<Employee> archivedEmployees;

        static ArchiveEmployeeRepository()
        {
            LoadArchivedEmployees();
        }

        public static void ArchiveEmployee(Employee employee)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBEmployees"].ConnectionString;
                using (SAConnection connection = new SAConnection(connectionString))
                {
                    connection.Open();

                    string updateSql = "UPDATE Employees SET IsArchived = 1 WHERE EmployeeId = :EmployeeId";
                    using (SACommand updateCommand = new SACommand(updateSql, connection))
                    {
                        updateCommand.Parameters.Add(new SAParameter(":EmployeeId", employee.EmployeeId));
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error archiving employee");
            }
        }

        private static void HandleError(Exception ex, string message)
        {
            Console.WriteLine($"{message}: {ex.Message}");
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static ObservableCollection<Employee> GetArchivedEmployees()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBEmployees"].ConnectionString;
                using (SAConnection connection = new SAConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Employees WHERE IsArchived = 1";
                    using (SACommand command = new SACommand(query, connection))
                    {
                        using (SADataReader reader = command.ExecuteReader())
                        {
                            var archivedEmployees = new ObservableCollection<Employee>();
                            while (reader.Read())
                            {
                                var employee = new Employee
                                {
                                    EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeId")) ? 0 : reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                    FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("LastName")),
                                    EGN = reader.IsDBNull(reader.GetOrdinal("EGN")) ? string.Empty : reader.GetString(reader.GetOrdinal("EGN")),
                                    RemainingLeaveDays = 20,
                                    JobTitle = reader.IsDBNull(reader.GetOrdinal("JobTitle")) ? string.Empty : reader.GetString(reader.GetOrdinal("JobTitle")),
                                    Department = reader.IsDBNull(reader.GetOrdinal("Department")) ? string.Empty : reader.GetString(reader.GetOrdinal("Department"))
                                };
                                archivedEmployees.Add(employee);
                            }
                            return archivedEmployees;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error loading archived employees");
                return new ObservableCollection<Employee>();
            }
        }

        public static void LoadArchivedEmployees()
        {
            try
            {
                string filePath = "archivedEmployees.dat";

                if (File.Exists(filePath))
                {
                    using (var fileStream = File.OpenRead(filePath))
                    {
                        if (fileStream.Length > 0)
                        {
                            var formatter = new BinaryFormatter();
                            archivedEmployees = (ObservableCollection<Employee>)formatter.Deserialize(fileStream);
                        }
                        else
                        {
                            archivedEmployees = new ObservableCollection<Employee>();
                        }
                    }
                }
                else
                {
                    archivedEmployees = new ObservableCollection<Employee>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading archived employees: {ex.Message}");
                archivedEmployees = new ObservableCollection<Employee>();
            }
        }

        public static void SaveArchivedEmployees()
        {
            try
            {
                using (var fileStream = File.Create("archivedEmployees.dat"))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, archivedEmployees);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving archived employees: {ex.Message}");
                throw;
            }
        }

        public static void AddArchivedEmployee(Employee employee)
        {
            try
            {
                archivedEmployees.Add(employee);
                SaveArchivedEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding archived employee: {ex.Message}");
            }
        }

        public static void DeleteArchivedEmployee(Employee employee)
        {
            try
            {
                archivedEmployees.Remove(employee);
                SaveArchivedEmployees();
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error deleting archived employee");
                throw;
            }
        }

        public static void DeleteAllArchivedEmployees()
        {
            try
            {
                archivedEmployees.Clear();
                SaveArchivedEmployees();
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error deleting archived employees");
                throw;
            }
        }
    }
}
