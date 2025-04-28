using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Hotel_Reservation_System
{
  
    public abstract class Room
    {
        private int roomID;
        private bool isAvailable;
        private int capacity;
        private ERoomType roomtype;
        private EBedType bedtype;
        private double pricePerNight;
        private EMealPlan MealPlan;
        public EMealPlan eMealPlan 
        { 
            get { return MealPlan; }
            set { MealPlan = value; }
        }
        public double PricePerNight
        {
            get {  return pricePerNight; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid value of price Per Night");
                else
                    pricePerNight = value;
            } 
        }
        public int RoomID 
        { 
            get { return roomID; }
            set 
            { 
                if(value<0)                
                    throw new ArgumentOutOfRangeException("You must write room id");                
                else
                    roomID = value;
            }
        }
        public bool ISAvailable 
        { 
            get { return isAvailable; } 
            set { isAvailable = value; } 
        }
        public int Capacity
        {
            get { return capacity; }
            set
            {
                if (value < 0)                
                    throw new ArgumentOutOfRangeException("You must write capacity");                
                else
                    capacity = value;
            }
        }
        public EBedType Bedtype
        {
            get { return bedtype; }
            set { bedtype = value; }
        }
        public ERoomType Roomtype
        {
            get { return roomtype; }
            set { roomtype = value; }
        }
        public abstract double Calculatetotalcost(int nonight);
        public abstract void DisplayRoomServices();
        public Room(int roomID,EBedType bedType, bool isAvailable,int capacity, double pricePerNight, EMealPlan MealPlan, ERoomType roomtype)
        {
            RoomID = roomID;
            ISAvailable = isAvailable;
            Capacity = capacity;
            Bedtype= bedType;
            eMealPlan = MealPlan;
            Roomtype = roomtype;
            PricePerNight = pricePerNight;
        }
        public Room()
        {
            RoomID= 0;
            ISAvailable= false;
            Capacity= 0;
            PricePerNight = 0;
            Bedtype = EBedType.Single;
            eMealPlan = EMealPlan.RoomOnly;
            Roomtype = ERoomType.Standard;
        }
        public virtual void DisplayRoomInfo()
        {
            Console.WriteLine($"Room ID : {RoomID}");
            Console.WriteLine($" Avability of Room : {isAvailable}");
            Console.WriteLine($"Room Type : {Roomtype}");
            Console.WriteLine($"Bed type : {Bedtype}");
            Console.WriteLine($"Capacity : {Capacity}");
        }
    }
}

