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
        { get { return MealPlan; }
            set { if(value== EMealPlan.AllInclusive||value== EMealPlan.UltraAllInclusive||
                    value== EMealPlan.RoomOnly||value== EMealPlan.BreakfastOnly||
                    value== EMealPlan.HalfBoard||value== EMealPlan.FullBoard)
                     MealPlan = value;
else
                    throw new ArgumentException("Invalid Meal plan.");
            }
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

        public int RoomID { get { return roomID; }

            set { if(value<0)
                {
                    throw new ArgumentOutOfRangeException("You must write room id");
                }
            else
                    { roomID = value; }
                    }
        }
        public bool ISAvailable { get { return isAvailable; } set { isAvailable = value; } }
        public int Capacity
        {
            get { return capacity; }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("You must write capacity");
                }
                else
                { capacity = value; }
            }
        }
        public EBedType Bedtype
        {
            get { return bedtype; }
            set
            {
                if (value == EBedType.Single ||
                    value == EBedType.Double ||
                    value == EBedType.Queen ||
                    value == EBedType.King ||
                    value == EBedType.Twin ||
                    value == EBedType.TwinDouble ||
                    value == EBedType.SofaBed ||
                    value == EBedType.BunkBed ||
                    value == EBedType.MurphyBed)
                {
                    bedtype = value;
                }
                else
                {
                    throw new ArgumentException("Invalid bed type.");
                }
            }
        }
        public ERoomType Roomtype
        {
            get { return roomtype; }
            set
            {
                if (value == ERoomType.Deluxe || value == ERoomType.Standard)
                    roomtype = value;
                else
                    throw new ArgumentException("Invalid Room type ");
            }
        }
        public abstract double Calculatetotalcost(float cost);
        public abstract void DisplayRoomServices();
        public Room(int roomID,EBedType bedType, bool isAvailable,int capacity)
        {
            RoomID = roomID;
            ISAvailable = isAvailable;
            Capacity = capacity;
            Bedtype= bedType;
        }
        public Room()
        {
            RoomID= 0;
            ISAvailable= false;
            Capacity= 0;
           PricePerNight = 0;
            Bedtype = EBedType.Single;
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

