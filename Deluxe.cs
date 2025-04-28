using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public class Deluxe : Room
    {
        private double discount;
        public double Discount
        {
            get { return discount; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("invalid");
                else
                    discount = value;
            }
        }
        public Deluxe() {
            discount = 0;
        }
        public Deluxe(int roomID, EBedType bedType, bool isAvailable, int capacity,double discount) :base(roomID, bedType, isAvailable, capacity) 
        {
            Discount = discount;
        }


        public override double Calculatetotalcost(int nonight)
        {
            return nonight * PricePerNight*Capacity;
        }

        public override void DisplayRoomServices()
        {
            base.DisplayRoomInfo();
            Console.WriteLine("Internet and private pool");
        }
    }
}
