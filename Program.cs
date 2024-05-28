using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Transactions;

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

            CurrencyDenomination currency = new CurrencyDenomination();
            ShoppingCart cart = new ShoppingCart();
            bool newTransaction = true;

            currency.Deposit(1, 2, 5, 10, 20, 4, 0, 200, 20, 20, 40, 5, 10, 10);
            
            while (newTransaction) {
                Console.Clear();
                cart.Clear();
                int paymentNumber = 1;
                int itemNumber = 1;
                bool stopAdding = false;
                string paymentMethod = Prompt("Enter payment method (cash/credit): ").ToLower();

                //currency.DisplayCurrency();

                Console.WriteLine("\nEnter the cost of the item (or type 0 to finish adding items): ");
                while (!stopAdding) {
                    double cost = PrompTryDouble($"Item {itemNumber}: ");
                        if (cost == 0) { 
                            stopAdding = true;
                        } else if (cost > 0) {
                            cart.AddItemCost(cost);
                            itemNumber++;
                        }//end if
                }//end while loop

                double totalCost = cart.GetTotalCost();
                Console.WriteLine("\nTotal: $" + Math.Round(totalCost, 2));

                if (paymentMethod == "cash") {
                    ProcessCashPayment(totalCost, currency, cart);
                    if () {

                    }
                }else if (paymentMethod == "credit") {
                    ProcessCreditCardPayment(totalCost, cart);
                } else {
                    Console.WriteLine("Invalid Payment Method!");
                }//end if
                newTransaction = NewTransaction();
            }//end while loop
                Console.WriteLine("Thank You for shopping!");

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
        }//end main

        #region Luhn Algorithm
            static bool IsValidCreditCard(string cardNumber) {

                char[] digits = cardNumber.ToCharArray();

                int sum = 0;
                bool shouldDouble = false;

                for (int i = digits.Length - 1; i >= 0; i--) {
                    int digit = digits[i] - '0';
                    if (shouldDouble) {
                        digit *= 2;
                        if (digit < 9) {
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

        static void ProcessCashPayment(double totalCost, CurrencyDenomination currency, ShoppingCart cart) {

            int paymentNumber = 1;

            while (cart.GetTotalInserted() < totalCost) {
                double money = PrompTryDouble($"\nPayment {paymentNumber}: $");
                    if (money == 100 || money == 50 || money == 20 || money == 10 || money == 5 || money == 2 || money == 1 || money == 0.5 || money == 0.25 || money == 0.1 || money == 0.05 || money == 0.01) {
                        cart.AddInsertedAmount(money);
                        double totalInserted = cart.GetTotalInserted();
                        double totalAmount = currency.TotalAmount();
                        double remaining = totalCost - totalInserted;
                            if (remaining > 0) {
                                Console.WriteLine($"Remaining Amount:{remaining:C} ");
                            } else {
                                double change = totalInserted - totalCost;
                                if (change > 0) {
                                    Console.WriteLine($"Change:{change:C}\n");
                                    if (totalAmount >= change && currency.DispenseChange((decimal) change)) {//then
                                        currency.SubDepositedAmount((decimal)(change));
                                        currency.DisplayCurrency();
                                    } else {
                                        Console.WriteLine("\nInsufficient funds in Kiosk.\nPlease provide another form of payment.");
                                        cart.RefundInsertedAmount((decimal)cart.GetTotalInserted());
                                        cart.Clear();
                                        break;
                                    }//end if
                                }//end if
                            }//end if
                    }else {
                        Console.WriteLine("\nInvalid Amount: Please insert correct currency\n");
                    }//end if
                    paymentNumber++;
            }//end while
        }//end function
        #endregion

        #region Credit Card Payment
        static void ProcessCreditCardPayment(double totalCost, ShoppingCart cart) {

            string cardNumber = Prompt("\nEnter your credit card number: ");
                if (IsValidCreditCard(cardNumber)) {
                    Console.WriteLine("The card is valid");
                } else {
                    Console.WriteLine("The card is NOT valid");
                }//end if
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
