using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    // Enum representing the type of room
    public enum ERoomType
    {
        Standard,
        Deluxe
    }
    // Enum representing the status of a reservation
    public enum EReservationStatus
    {
        Pending,
        Confirmed,
        CheckedIn,
        CheckedOut,
        Cancelled,
        NoShow
    }
    // Enum representing the type of the bed
    public enum EBedType
    {
        Single,
        Double,
        Queen,
        King,
        Twin,
        TwinDouble,
        SofaBed,
        BunkBed,
        MurphyBed
    }
    // Enum representing the type of the bed
    public enum EMealPlan
    {
        RoomOnly,
        BreakfastOnly,
        HalfBoard,
        FullBoard,
        AllInclusive,
        UltraAllInclusive
    }
}
