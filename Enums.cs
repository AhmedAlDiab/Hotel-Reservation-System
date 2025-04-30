using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Enum representing the type of room
    /// </summary>
    public enum ERoomType
    {
        Standard,
        Deluxe
    }
    /// <summary>
    /// Enum representing the status of a reservation
    /// </summary>
    public enum EReservationStatus
    {
        Pending,
        Confirmed,
        CheckedIn,
        CheckedOut,
        Cancelled,
        NoShow
    }
    /// <summary>
    /// Enum representing the type of the bed
    /// </summary>
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
    /// <summary>
    /// Enum representing the type of MealPlan
    /// </summary>
    public enum EMealPlan
    {
        RoomOnly,
        BreakfastOnly,
        HalfBoard,
        FullBoard,
        AllInclusive,
        UltraAllInclusive
    }
    /// <summary>
    /// Enum representing the payment method.
    /// </summary>
    public enum EPaymentMethod
    {
        Cash,
        CreditCard
    }
}
