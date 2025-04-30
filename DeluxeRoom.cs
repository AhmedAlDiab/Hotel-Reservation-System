using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public class DeluxeRoom : Room
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
        public DeluxeRoom() 
        {
            discount = 0;
        }
        public DeluxeRoom(int roomID, EBedType bedType, bool isAvailable, int capacity, double pricePerNight, EMealPlan MealPlan, ERoomType roomtype, double discount) :base(roomID, bedType, isAvailable, capacity,pricePerNight,MealPlan,roomtype) 
        {
            Discount = discount;
        }


        public override double Calculatetotalcost(int nonight)
        {
            return nonight * PricePerNight-Discount* nonight * PricePerNight;
        }

        public override void DisplayRoomServices()
        {            
            // not complete yet
            base.DisplayRoomInfo();
            Console.WriteLine("Internet and private pool");
        }
    }
}
