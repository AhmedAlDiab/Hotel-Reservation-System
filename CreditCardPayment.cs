using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public class CreditCardPayment:Payment
    {
        //Field Tax for Bank Taxes
        private double tax = 0.05;


        //Set & Get Property of tax
        public double Tax
        {
            set
            {
                if (value > 0)
                    tax = value;
                else
                {                   
                    throw new ArgumentException("Tax Must be Positive Number");
                }
            }
            get
            {
                return tax;
            }
        }

        
        //Default Constructor
        public CreditCardPayment()
        {
            Tax = 0.05;
        }

        //Parameterized Constructor 
        public CreditCardPayment(int paymentID,int reservationID, EPaymentMethod paymentMethod, DateTime paymentDate, double totalAmount, double tax) : base(paymentID,reservationID, paymentMethod, paymentDate, totalAmount)
        {
            //Tax = tax;
            Tax = 0.05;
        }

        //Process Payment Method To Change Reservation Status From Pending To Confirmed After Payment
        public override void ProcessPayment(ref EReservationStatus ReservationStatus)
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
