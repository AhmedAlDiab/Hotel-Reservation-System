using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public class Standard : Room
    {
        public Standard()
        {
            
        }
        public Standard(int roomID, EBedType bedType, bool isAvailable, int capacity) : base(roomID, bedType, isAvailable, capacity)
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
