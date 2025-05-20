using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ResManagementWindow.xaml
    /// </summary>
    public partial class ResManagementWindow : Window
    {
        public ObservableCollection<Reservation> reservationbinding = new ObservableCollection<Reservation>();

        public ResManagementWindow()
        {
            InitializeComponent();
            Data.GetData();
            foreach (var rs in Data.Reservations)
            {
                if (rs.PCustomer.UserID != ActiveUser.UserID)
                    continue;
                if (rs != null)
                {
                    reservationbinding.Add(rs);
                }
            }
            Reservationname.ItemsSource = reservationbinding;

        }

    }

    }
    


