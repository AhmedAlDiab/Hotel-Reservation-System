using System;
using System.Collections.Generic;
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

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        public PaymentWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int startYear = 2025;
            int endYear = 2035;

            for (int year = startYear; year <= endYear; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    if (year == 2025 && month < 6)
                        continue; // Skip months before June 2025

                    string formattedMonth = month.ToString("D2"); // e.g., 01, 02, ..., 12
                    string formattedYear = year.ToString().Substring(2); // e.g., 25, 26, ..., 50
                    ExpireDate.Items.Add($"{formattedMonth}/{formattedYear}");
                }
            }
        }
    }
}
