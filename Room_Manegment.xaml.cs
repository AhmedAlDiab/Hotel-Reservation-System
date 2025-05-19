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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Interaction logic for Room_Manegment.xaml
    /// </summary>
    public partial class Room_Manegment : UserControl
    {
        private viewmodel ViewModel
        {
            get { return (viewmodel)this.DataContext; }
        }
        public Room_Manegment()
        {
            InitializeComponent();
            this.DataContext = new viewmodel();

        }

        private void roomGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



    }
}