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
        private DateTime paymentDate;
        private double totalAmount;

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
                    //throw new ArgumentException("Payment ID Must be Greater than Zero");
                    
                }
            }
            get
            {
                return paymentID;
            }
        }

        //Set & Get Property of "paymentDate"
        public DateTime PaymentDate
        {
            set
            {
                
                //Sooooooon

                //if(value <= DateTime.Now)
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

        //Default Constructor
        public Payment()
        {
            paymentID=0;
            paymentDate=DateTime.Now.AddYears(-200);
            totalAmount=0;
        }

        //Parameterized Constructor To Intialize All Fields 
        public Payment(int paymentID, DateTime paymentDate, double totalAmount)
        {
            PaymentID = paymentID;
            PaymentDate = paymentDate;
            TotalAmount = totalAmount;
        }

        //Process Payment Abstract Method That Change Reservation Status
        public abstract void ProcessPayment(EReservationStatus ReservationStatus);
    }
}
