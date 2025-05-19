using Google.Protobuf.Compiler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{

    internal class viewmodel : INotifyPropertyChanged
    {
        private ObservableCollection<Room> room ;
        public ObservableCollection<Room> Rooms
        {
            get { return room; }
            set
            {
                room = value;
                OnPropertyChanged("Room");
            }
        }

        private Room selectedrooom ;
        public Room SelectedRoom
        {
            get { return selectedrooom; }
            set
            {
                selectedrooom = value;
                OnPropertyChanged("SelectedRoom");
            }
        }
        public viewmodel()
        {
            Rooms = new ObservableCollection<Room>();
        }

        public void AddStandared(int roomID, EBedType bedType, bool isAvailable, int capacity, double pricePerNight, EMealPlan MealPlan, ERoomType roomtype)
        {
            StandardRoom std = new StandardRoom(roomID,bedType,isAvailable,capacity,pricePerNight, MealPlan, roomtype);
           
            Rooms.Add(std);
        }
        public void AddDeluxe(int roomID, EBedType bedType, bool isAvailable, int capacity, double pricePerNight, EMealPlan MealPlan, ERoomType roomtype,double discount)
        {
            DeluxeRoom dlx = new DeluxeRoom(roomID, bedType, isAvailable, capacity, pricePerNight, MealPlan, roomtype,discount);
           
            Rooms.Add(dlx);
        }
        public void RemoveSelected()
        {
            if (SelectedRoom != null && Rooms.Contains(SelectedRoom))
            {
                Rooms.Remove(SelectedRoom);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

}
