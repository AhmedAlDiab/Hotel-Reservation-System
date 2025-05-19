using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public abstract class Payment
    {
        //fields
        private int paymentID;
        private int reservationID;
        private DateTime paymentDate;
        private double totalAmount;
        private EPaymentMethod paymentMethod;

        //Set & Get Property of "paymentID"
        public int PaymentID
        {
            set
            {
                if (value > 0)
                    paymentID = value;
                else
                {
                    paymentID = 0;
                    throw new ArgumentException("Payment ID Must be Greater than Zero");
                    
                }
            }
            get
            {
                return paymentID;
            }
        }
        //Set & Get Property of "reservationID"
        public int ReservationID
        {
            set
            {
                if (value > 0)
                    reservationID = value;
                else
                {
                    reservationID = 0;
                    //throw new ArgumentException("Reservation ID Must be Greater than Zero");

                }
            }
            get
            {
                return reservationID;
            }
        }

        //Set & Get Property of "paymentDate"
        public DateTime PaymentDate
        {
            set
            {
                   paymentDate = value;
            }
            get
            {
                return paymentDate;
            }
        }

        //Set & Get Property of "totalAmount"
        public double TotalAmount
        {
            set
            {
                if (value > 0)
                    totalAmount = value;
                else
                {
                    totalAmount = 0;
                    //throw new ArgumentException("Total Amount Must be Greater Than Zero");
                }

            }
            get
            {
                return totalAmount;
            }
        }

        //Set & Get Property for paymentMethod
        public EPaymentMethod PaymentMethod
        {
            set
            {
                paymentMethod = value;
            }
            get
            {
                return paymentMethod;
            }
        }

        //Default Constructor
        public Payment()
        {
            paymentID=0;
            reservationID=0;
            paymentMethod=EPaymentMethod.Cash;
            paymentDate=DateTime.Now;
            totalAmount=0;
        }

        //Parameterized Constructor To Intialize All Fields 
        public Payment(int paymentID,int reservationID,EPaymentMethod paymentMethod, DateTime paymentDate, double totalAmount)
        {
            PaymentID = paymentID;
            ReservationID = reservationID;
            PaymentMethod = paymentMethod;
            PaymentDate = paymentDate;
            TotalAmount = totalAmount;

        }

        //Process Payment Abstract Method That Change Reservation Status
        public abstract void ProcessPayment(ref EReservationStatus ReservationStatus);
    }
}
