using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public class StandardRoom : Room
    {
        public StandardRoom() {}
        public StandardRoom(int roomID, EBedType bedType, bool isAvailable, int capacity, double pricePerNight, EMealPlan MealPlan, ERoomType roomtype) : 
        base(roomID, bedType, isAvailable, capacity, pricePerNight, MealPlan, roomtype) 
        {

        }
        public override double Calculatetotalcost(int nonight)
        {
            return nonight * PricePerNight ;
        }

        public override void DisplayRoomServices()
        {
            // not complete yet
            base.DisplayRoomInfo();
        }
    }
}
