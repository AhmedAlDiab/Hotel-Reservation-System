using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public class CashPayment:Payment
    {
        //Default Constructor
        public CashPayment()
        {

        }

        //Parameterized Constructor
        public CashPayment(int paymentID, DateTime paymentDate, double totalAmount) : base(paymentID, paymentDate, totalAmount)
        {
        }

        //Process Payment Method To Change Reservation Status From Pending to Confirmed After Payment
        public override void ProcessPayment(EReservationStatus ReservationStatus)
        {
            if (ReservationStatus == EReservationStatus.Pending)
            {
                ReservationStatus = EReservationStatus.Confirmed;
            }
            else
                throw new ArgumentException("Error In Payment Process");
        }
    }
}

