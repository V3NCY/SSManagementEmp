using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EmployeeDocumentManagementApp
{
    public partial class EmployeeProfile : Window
    {
        private static List<EmployeeProfile> openWindows = new List<EmployeeProfile>();
        private Employee employee;
        private static Dictionary<int, BitmapImage> employeeImages = new Dictionary<int, BitmapImage>();

        public EmployeeProfile(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            DataContext = this.employee;

            Loaded += EmployeeProfile_Loaded;

            Closed += EmployeeProfile_Closed;
        }
        private void EnableSaveButton()
        {
            SaveButton.IsEnabled = true;
        }

        private void DisableSaveButton()
        {
            SaveButton.IsEnabled = false;
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            string initialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmployeeImages");
            openFileDialog.InitialDirectory = Directory.Exists(initialDirectory) ? initialDirectory : AppDomain.CurrentDomain.BaseDirectory;

            bool? fileSelected = openFileDialog.ShowDialog();

            if (fileSelected == true)
            {
                string imagePath = openFileDialog.FileName;
                string imageFileName = $"{employee.EmployeeId}_photo.jpg";
                string destinationDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmployeeImages");
                string destinationPath = Path.Combine(destinationDirectory, imageFileName);

                try
                {
                    if (!Directory.Exists(destinationDirectory))
                    {
                        Directory.CreateDirectory(destinationDirectory);
                    }

                    File.Copy(imagePath, destinationPath, true);
                    employee.ImagePath = destinationPath;

                    LoadImage();

                    Console.WriteLine($"Image uploaded successfully to: {destinationPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while uploading the image: {ex.Message}");
                    MessageBox.Show($"Грешка при качването на изображението: {ex.Message}", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoadImage()
        {
            if (!string.IsNullOrEmpty(employee.ImagePath) && File.Exists(employee.ImagePath))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(employee.ImagePath, UriKind.Absolute);
                bitmap.EndInit();
                imgEmployee.Source = bitmap;
            }
        }
        private void LoadEmployeeImage()
        {
            try
            {
                string employeeImagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmployeeImages");
                if (!Directory.Exists(employeeImagesDirectory))
                {
                    Console.WriteLine("EmployeeImages directory does not exist.");
                    return;
                }

                string searchPattern = $"{employee.EmployeeId}_photo.*";
                string[] imageFiles = Directory.GetFiles(employeeImagesDirectory, searchPattern);
                if (imageFiles.Length == 0)
                {
                    Console.WriteLine($"Image file for Employee ID {employee.EmployeeId} not found.");
                    return;
                }

                employee.ImagePath = imageFiles[0];

                LoadImage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the employee image: {ex.Message}");
            }
        }
        private void EmployeeProfile_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEmployeeImage();
        }
        private void EmployeeProfile_Closed(object sender, EventArgs e)
        {

        }

        public static bool IsWindowOpen()
        {
            return openWindows.Any();
        }

        public static void BringToFront()
        {
            if (openWindows.Any())
            {
                openWindows.Last().Activate();
            }
        }

        private void OpenInformation_Click(object sender, RoutedEventArgs e)
        {
        }
        private void RemoveImageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Сигурни ли сте, че искате да изтриете снимката?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                employeeImages.Remove(employee.EmployeeId);

                if (File.Exists(employee.ImagePath))
                {
                    File.Delete(employee.ImagePath);
                }

                employee.ImagePath = null;
                imgEmployee.Source = null;
            }
        }
        public class BoolToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is bool boolValue)
                {
                    return boolValue ? Visibility.Visible : Visibility.Collapsed;
                }
                return Visibility.Collapsed;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveEmployeeProfile(employee);
        }

        private void SaveEmployeeProfile(Employee employee)
        {
            try
            {
                if (!string.IsNullOrEmpty(employee.ImagePath) && File.Exists(employee.ImagePath))
                {
                    Console.WriteLine($"Запазване на профила на служителя с ИД {employee.EmployeeId}: ImagePath = {employee.ImagePath}");
                    MessageBox.Show("Профилът е запазен успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Моля, първо добавете снимка на служителя!", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Възникна грешка при запазване на профила: {ex.Message}", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}


