using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        public PaymentWindow()
        {
            InitializeComponent();
        }

        //--------------------------------------------------
        // ------------------ VALIDATION -------------------
        //--------------------------------------------------
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string cardNumber = CreditCardNumber.Text.Trim();
            string cvv = Cvv.Text.Trim();
            string selectedCardType = (CreditCardType.SelectedItem as ComboBoxItem)?.Content.ToString();
            string selectedExpireDate = ExpireDate.SelectedItem?.ToString();

            // Checker
            bool isValid = true;

            //Submission Error If One Or More Field Has No Value
            if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(cvv) || string.IsNullOrEmpty(selectedCardType) || string.IsNullOrEmpty(selectedExpireDate))
            {
                MessageBox.Show("Please Fill all Required Fields Correctly.", "Submission Error");
                return;
            }

            //After Checking That All fields Has Value Then:


            //---- 1st Check ----
            // Credit Card Number: Must be 16 digits
            if (cardNumber.Length != 16)
            {
                MessageBox.Show("Credit Card Number Must Be Exactly 16 digits.", "Input Error");
                isValid = false;
            }

            //---- 2nd Check ----
            //Credit Card Number: Must contain digits only
            else if (!cardNumber.All(char.IsDigit))
            {
                MessageBox.Show("Credit Card Number Must Contain Digits Only (No Letters or Symbols).", "Input Error");
                isValid = false;
            }

            //---- 3rd Check ----
            // CVV: Must be 3 digits 
            else if (cvv.Length != 3)
            {
                MessageBox.Show("CVV Must Be Exactly 3 Digits.", "Input Error");
                isValid = false;
            }

            //---- 4th Check ----
            // CVV: Must be digits only 
            else if (!cvv.All(char.IsDigit))
            {
                MessageBox.Show("CVV Must Contain Digits Only (No Letters or Symbols).", "Input Error");
                isValid = false;
            }

            //---- 5th Check ----
            // Card Type: Must be Choosen 
            else if (string.IsNullOrEmpty(selectedCardType))
            {
                MessageBox.Show("You Must Choose A Credit Card Type.", "Input Error");
                isValid = false;
            }

            //---- 6th Check ----
            // Expire Date: Must be Choosen 
            else if (string.IsNullOrEmpty(selectedExpireDate))
            {
                MessageBox.Show("You Must Choose An Expiry Date.", "Input Error");
                isValid = false;
            }


            //Finally If All 6 Check Points ----> True Then: submitted successfully
            if (isValid)
            {
                MessageBox.Show("Payment Submitted Successfully!", "Successful Process");
            }

          
        }



        //ComboBox of ExpireDate Display Options From 06/25 To 06/35
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int startYear = 2025;
            int endYear = 2035;

            for (int year = startYear; year <= endYear; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    if (year == 2025 && month < 6)
                        continue; // Skip months before June 2025

                    string formattedMonth = month.ToString("D2"); // Ex: 01, 02, ..., 12
                    string formattedYear = year.ToString().Substring(2); // Ex: 25, 26, ..., 35
                    ExpireDate.Items.Add($"{formattedMonth}/{formattedYear}");
                }
            }
        }

        private void PaymentMethodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedMethod = (PaymentMethodComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            bool isCredit = selectedMethod == "Credit";

            // Show or hide each credit-related control
            CreditCardNumber.Visibility = isCredit ? Visibility.Visible : Visibility.Collapsed;
            CreditCardNumberLabel.Visibility = isCredit ? Visibility.Visible : Visibility.Collapsed;

            Cvv.Visibility = isCredit ? Visibility.Visible : Visibility.Collapsed;
            CvvLabel.Visibility = isCredit ? Visibility.Visible : Visibility.Collapsed;

            CreditCardType.Visibility = isCredit ? Visibility.Visible : Visibility.Collapsed;
            CreditCardTypeLabel.Visibility = isCredit ? Visibility.Visible : Visibility.Collapsed;

            ExpireDate.Visibility = isCredit ? Visibility.Visible : Visibility.Collapsed;
            ExpireDateLabel.Visibility = isCredit ? Visibility.Visible : Visibility.Collapsed;

        }

    }
}
