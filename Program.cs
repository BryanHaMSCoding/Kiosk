using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Transactions;
using static CurrencyDenomination;

namespace KioskFinalProject {
    internal class Program {
        static void Main(string[] args) {
            #region Variables
            //The program should keep track of how many of each denomination of bill and coin the kiosk currently has.Use a user defined datatype to do this.
            //currency.Deposit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
            //The program should allow any number of items costs to be input until no cost has been supplied.
            //Once all items have been input the kiosk should display a total and ask the user to insert money.
            //The user should be able to insert any denomination of bills or coins until their total amount exceeds the cost of all the items. 
            //The kiosk should calculate how much change should be dispensed and dispense the change. (Research and use a greedy algorithm to dispense the change)
            //If the kiosk does not have enough physical money to supply the change then the transaction will be cancelled, and the user should be offered another way to make payments(you do not have to develop other methods of payment)
            #endregion

            #region Randomly Generated Credit Card Numbers

            /*
            string visaPrefix = "4"; // Visa cards start with '4'
            int visaLength = 16; // Visa cards have 16 digits

            string generatedVisaCard = GenerateCardNumber(visaPrefix, visaLength);
            Console.WriteLine($"Generated Visa Card: {generatedVisaCard}");

            string masterCardPrefix = "5"; // MasterCard cards start with '5'
            int masterCardLength = 16; // MasterCard cards have 16 digits

            string generatedMasterCard = GenerateCardNumber(masterCardPrefix, masterCardLength);
            Console.WriteLine($"Generated MasterCard: {generatedMasterCard}");

            string amexPrefix = "34"; // American Express cards start with '34'
            int amexLength = 15; // American Express cards have 15 digits

            string generatedAmexCard = GenerateCardNumber(amexPrefix, amexLength);
            Console.WriteLine($"Generated American Express Card: {generatedAmexCard}");

            string discoverPrefix = "6011"; // Discover cards start with '6011'
            int discoverLength = 16; // Discover cards have 16 digits

            string generatedDiscoverCard = GenerateCardNumber(discoverPrefix, discoverLength);
            Console.WriteLine($"Generated Discover Card: {generatedDiscoverCard}");
            */

            #endregion

            CurrencyDenomination currency = new CurrencyDenomination();
            ShoppingCart cart = new ShoppingCart();
            bool newTransaction = true;

            currency.Deposit(1, 2, 5, 10, 20, 4, 0, 200, 20, 20, 40, 5, 10, 10);
            //currency.Deposit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
            
            while (newTransaction) {
                Console.Clear();
                cart.Clear();
                int paymentNumber = 1;
                int itemNumber = 1;
                bool stopAdding = false;

                //currency.DisplayCurrency();

                Console.WriteLine("Enter the cost of the item (or type 0 to finish adding items): ");
                while (!stopAdding) {
                    double cost = PrompTryDouble($"Item {itemNumber}: ");
                        if (cost == 0) { 
                            stopAdding = true;
                        } else if (cost > 0) {
                            cart.AddItemCost(cost);
                            itemNumber++;
                        }//end if
                }//end while loop

                string paymentMethod;
                do {
                    paymentMethod = Prompt("Enter payment method (---Cash---/---Credit---): ").ToLower();
                    if (paymentMethod != "cash" && paymentMethod != "credit") {
                        Console.WriteLine("Invalid input. Please type 'Cash' or 'Credit'. ");
                    }//end if
                } while (paymentMethod != "cash" && paymentMethod != "credit");//end while

                double totalCost = cart.GetTotalCost();
                double totalInserted = cart.GetTotalInserted();
                Console.WriteLine("\nTotal: $" + Math.Round(totalCost, 2));

                bool paymentSuccessful = false;
                decimal cashbackAmount = 0;

                if (paymentMethod == "cash") {
                    paymentSuccessful = ProcessCashPayment((decimal)totalCost, (decimal)totalInserted, currency, cart);
                    if (!paymentSuccessful) {
                        PayRemainingBalanceOrGetRefund((decimal)totalCost, currency, cart);
                    }//end if
                }else if (paymentMethod == "credit") {
                    paymentSuccessful = ProcessCreditCardPayment((decimal)totalCost, currency, out cashbackAmount);
                    if (paymentSuccessful) {
                        //currency.DispenseChange(cashbackAmount);
                        currency.SubDepositedAmount(cashbackAmount);
                        //currency.DisplayCurrency();
                    } else {
                        Console.WriteLine("Insuficient funds in kiosk to dispense cashback");
                        paymentSuccessful = false;
                    }
                    if (!paymentSuccessful) {
                        PayRemainingBalanceOrGetRefund((decimal)totalCost, currency, cart);
                    }//end if
                } else {
                    Console.WriteLine("Payment Failed");
                }//end if
                newTransaction = NewTransaction();
            }//end while loop
                Console.WriteLine("Thank You for shopping!");

            #region INITAL CODE
            /*
            while (cart.GetTotalInserted() < cart.GetTotalCost()) {
                if (paymentMethod == "cash") {
                    double money = PrompTryDouble($"\nPayment {paymentNumber}: $");
                    if (money == 100 || money == 50 || money == 20 || money == 10 || money == 5 || money == 2 || money == 1 || money == 0.5 || money == 0.25 || money == 0.1 || money == 0.05 || money == 0.01) {
                        cart.AddInsertedAmount(money);
                        double totalInserted = cart.GetTotalInserted();
                        double totalAmount = currency.TotalAmount();
                        paymentNumber++;
                        double remaining = totalCost - totalInserted;
                        if (remaining > 0) {
                            Console.WriteLine($"Remaining Amount:{remaining:C} ");
                        } else {
                            double change = totalInserted - totalCost;
                            if (change > 0) {
                                Console.WriteLine($"Change:{change:C}\n");
                                if (totalAmount >= change && currency.DispenseChange((decimal)change)) {//then
                                    currency.SubDepositedAmount((decimal)(change));
                                    currency.DisplayCurrency();
                                } else {
                                    Console.WriteLine("\nInsufficient funds in Kiosk.\nPlease provide another form of payment.");
                                    cart.RefundInsertedAmount((decimal)cart.GetTotalInserted());
                                    cart.Clear();
                                    break;
                                }//end if
                            } else {
                                Console.WriteLine();
                            }//end if
                        }//end if
                    }else {
                        Console.WriteLine("\nInvalid Amount: Please insert correct currency\n");
                    }//end if

                }else if (paymentMethod == "credit") {
                    string cardNumber = Prompt("\nEnter your credit card number: ");
                        if (IsValidCreditCard(cardNumber)) {
                            Console.WriteLine("The card is valid");
                        } else {
                            Console.WriteLine("The card is NOT valid");
                        }//end if
                }//end if

            }//end while loop
                    newTransaction = NewTransaction();

            */
            #endregion
        }//end main

        #region Pay Remaining Balance or Refund

        static void PayRemainingBalanceOrGetRefund(decimal remainingBalance, CurrencyDenomination currency, ShoppingCart cart) {

            Console.WriteLine($"\nRemaining Balance: {remainingBalance:C}");
            string response = Prompt("\nWould you like to pay the remaining balance with a credit card? (---Y---/---N---): ").ToLower();
            if (response == "y") {
                decimal cashbackAmount = 0;
                if (ProcessCreditCardPayment(remainingBalance, currency, out cashbackAmount)) {
                    Console.WriteLine("Payment Successful");
                } else {
                    Console.WriteLine("Credit Card payment failed. ");
                    cart.RefundInsertedAmount((decimal)cart.GetTotalInserted());
                    cart.Clear();
                }//end if
            }else {
                Console.WriteLine("Refunding your cash. ");
                cart.RefundInsertedAmount((decimal)cart.GetTotalInserted());
                cart.Clear();
            }
        }//end function

        #endregion

        #region Luhn Algorithm

        static bool IsValidCreditCard(string cardNumber) {

                char[] digits = cardNumber.ToCharArray();

                int sum = 0;
                bool shouldDouble = false;

                for (int i = digits.Length - 1; i >= 0; i--) {
                    int digit = digits[i] - '0';
                    if (shouldDouble) {
                        digit *= 2;
                        if (digit > 9) {
                            digit -= 9;
                        }//end if
                    }//end if

                    sum += digit;

                    shouldDouble = !shouldDouble;
                }//end for loop

                return (sum % 10 == 0);

            }//end function

    #endregion

        #region New Transaction

            static bool NewTransaction() {
            
            while (true) {
                Console.WriteLine();
                string newTransaction = Prompt("Do you want to start a new transaction? (---Y---/---N---): ");
                    if (newTransaction.ToLower() == "y") {
                        return true;
                    } else if (newTransaction.ToLower() == "n") {
                        return false;
                    } else {
                    Console.WriteLine("Invalid response! Please type 'Y' or 'N'\n");
                    }//end if

            }//end while loop

        }//end function
        #endregion

        #region Cash Payment

        static bool ProcessCashPayment(decimal totalCost, decimal totalInserted, CurrencyDenomination currency, ShoppingCart cart) {

            int paymentNumber = 1;

            while (totalInserted < totalCost) {
                double money = PrompTryDouble($"\nPayment {paymentNumber}: $");
                    if (money == 100 || money == 50 || money == 20 || money == 10 || money == 5 || money == 2 || money == 1 || money == 0.5 || money == 0.25 || money == 0.1 || money == 0.05 || money == 0.01) {
                        cart.AddInsertedAmount(money);
                        totalInserted = (decimal)cart.GetTotalInserted();
                        double totalAmount = currency.TotalAmount();
                        paymentNumber++;
                        double remaining = (double)(totalCost - totalInserted);
                            if (remaining > 0) {
                                Console.WriteLine($"Remaining Amount:{remaining:C} ");
                            } else {
                                double change = (double)(totalInserted - totalCost);
                                if (change > 0) {
                                    Console.WriteLine($"Change:{change:C}\n");
                                    if (totalAmount >= change && currency.DispenseChange((decimal) change)) {//then
                                        currency.SubDepositedAmount((decimal)(change));
                                        currency.DisplayCurrency();
                                        return true; //PAYMENT SUCCESSFUL
                                    } else {
                                        Console.WriteLine("\nInsufficient funds in Kiosk.\nPlease provide another form of payment.");
                                        //cart.RefundInsertedAmount((decimal)cart.GetTotalInserted());
                                        //cart.Clear();
                                        return false; //PAYMENT UNSUCCESSFUL
                                    }//end if
                                } else {
                                    Console.WriteLine("Payment Complete. ");
                                    return true; //PAYMENT SUCCESSFUL
                                }
                            }//end if
                    }else {
                        Console.WriteLine("\nInvalid Amount: Please insert correct currency\n");
                    }//end if
            }//end while
            return true;
        }//end function
        #endregion

        #region Credit Card Payment

        static bool ProcessCreditCardPayment(decimal amount, CurrencyDenomination currency,  out decimal cashbackAmount) {
            string cashbackResponse;
            cashbackAmount = 0;
            string cardNumber = CreditCardGenerator.GenerateCardNumber("4111", 16);
            //string cardNumber = "4222222222222";
            Console.WriteLine($"Generated Card Number: {cardNumber}");

            bool isValid = IsValidCreditCard(cardNumber);
            Console.WriteLine($"The card is {(isValid ? "valid" : "NOT valid")}");

            if (!isValid) {

                return false;
            }//end if

            do {
            cashbackResponse = Prompt("Would you like cashback? (---Y---/---N---): ").ToLower();
                if (cashbackResponse != "y" && cashbackResponse != "n") {
                    Console.WriteLine("Invalid input. Please enter 'Y' or 'N'. ");
                }//end if
            } while (cashbackResponse != "y" && cashbackResponse != "n"); //end while
            
            if (cashbackResponse == "y") {
                cashbackAmount = decimal.Parse(Prompt("Enter cashback amount: "));
                if (!currency.DispenseChange(cashbackAmount)) {
                    Console.WriteLine("Insufficient funds available in Kiosk to dispense cashback");
                    cashbackAmount = 0;
                } else {
                    amount += cashbackAmount;

                }//end if
                
            }//end if
            string[] transactionResult = MoneyRequest(cardNumber, amount);
            if (transactionResult[0] == "declined") {
                Console.WriteLine("Credit card transaction declined. ");

                return false;
            }//end if

            decimal amountPaid = decimal.Parse(transactionResult[0]);
            if (amountPaid < amount) {
                decimal remainingBalance = amount - amountPaid;
                Console.WriteLine($"Partial payment recieved: {amountPaid:C}");
                Console.WriteLine($"Remaining balance: {remainingBalance:C}");
                amount = remainingBalance;

                return false;
            }//end if

            Console.WriteLine("Credit card payment successful");
            return true;

        }//end function


        #endregion

        #region Money Request

        static string[] MoneyRequest(string account_number, decimal amount) {
            Random rnd = new Random();
            //50% CHANCE TRANSACTION PASSES OR FAILS
            bool pass = rnd.Next(100) < 50;
            //50% CHANCE THAT A FAILED TRANSACTION IS DECLINED
            bool declined = rnd.Next(100) < 50;

            if (pass) {
                return new string[] { account_number, amount.ToString() };
            } else {
                if (!declined) {
                    return new string[] { account_number, (amount / rnd.Next(2, 6)).ToString() };
                } else {
                    return new string[] { account_number, "declined" };
                }//end if
            }//end if
        }//end function

        #endregion

        #region PROMPT FUNCTIONS

        static double PrompTryDouble(string dataRequest) {
            //Variable
            double userInput = 0;
            bool isValid = false;

            //INPUT VALIDATION LOOP

            while (isValid == false) {
                Console.Write(dataRequest);
                isValid = double.TryParse(Console.ReadLine(), out userInput);
            }//end while loop

            return userInput;
        }//end function

        static int PromptTryParse(string dataRequest) {
            //VARIABLE
            int userInput = 0;
            bool isValid = false;

            //INPUT VALIDATION LOOP
            do {
                Console.Write(dataRequest);
                isValid = int.TryParse(Console.ReadLine(), out userInput);
            } while (isValid == false);//end do while
            return userInput;
        }//end function

        static void Header(string text) {
            Console.WriteLine("================================================================");
            Console.WriteLine(text);
            Console.WriteLine("=================================================================");
        }//end function
        static string Prompt(string data) {
            //Variable:
            string userInput = "";

            //Request info from user
            Console.Write(data);

            //Recieve Response
            userInput = Console.ReadLine();

            return userInput;
        }//end function
        static int PromptInt(string data) {
            //Variable
            int userInput = 0;

            //Request and Receive info
            userInput = int.Parse(Prompt(data));

            return userInput;
        }//end function
        static double PromptDouble(string data) {
            //Variable
            double userInput = 0.0;

            //Request and Recieve info
            userInput = double.Parse(Prompt(data));

            return userInput;
        }//end function
        #endregion
    }//end class
}//end namespace
