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
            int itemNumber = 1;
            int paymentNumber = 1;
            bool continueAdding = false;

            #endregion

            //The program should keep track of how many of each denomination of bill and coin the kiosk currently has.Use a user defined datatype to do this.
            CurrencyDenomination currency = new CurrencyDenomination();
            // currency.Deposit(1, 2, 5, 10, 20, 4, 50, 200, 20, 20, 40, 5, 10, 10);
            currency.Deposit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
            //currency.DisplayCurrency();

            // The program should allow any number of items costs to be input until no cost has been supplied.
            ShoppingCart cart = new ShoppingCart(currency);
            Console.WriteLine("Enter the cost of the item (or type 0 to finish adding items): ");
            while (!continueAdding) {
                double cost = PrompTryDouble($"Item {itemNumber}: ");
                    if (cost == 0) { 
                        continueAdding = true;
                    } else if (cost > 0) {
                        cart.AddItemCost(cost);
                        itemNumber++;
                    }//end if
            }//end while loop

            Console.WriteLine("\nTotal: $" + Math.Round(cart.GetTotalCost(), 2));
            
            // Once all items have been input the kiosk should display a total and ask the user to insert money.
            // The user should be able to insert any denomination of bills or coins until their total amount exceeds the cost of all the items. 
            while (cart.GetTotalInserted() < cart.GetTotalCost()) {
                double money = PrompTryDouble($"\nPayment {paymentNumber}: $");
                if (money == 100 || money == 50 || money == 20 || money == 10 || money == 5 || money == 2 || (money <= 1 && money > 0)) {
                    cart.AddInsertedAmount(money);
                    paymentNumber++;
                    double remaining = cart.GetTotalInserted() - cart.GetTotalCost();
                    if (remaining < 0) {
                        Console.WriteLine($"Remaining Amount:{remaining:C} ");
                    } else {
                        //Console.WriteLine("Insufficient funds. Please insert correct currency.");
                        double change = cart.GetTotalInserted() - cart.GetTotalCost();
                        Console.WriteLine($"Change:{change:C} ");
                        if (currency.TotalAmount() >= change) {//then
                            currency.DispenseChange((decimal)change);
                            currency.SubDepositedAmount((decimal)(change));
                            Console.WriteLine($"Change:{change:C} \n----------------------------------------------");
                        } else {
                            Console.WriteLine("\nInsufficient funds in Kiosk.\nPlease provide another form of payment");
                            cart.RefundInsertedAmount((decimal)cart.GetTotalInserted());
                            cart.Clear();
                        }//end nested if

                    }//end if

                }else {
                    Console.WriteLine("\nInvalid Amount: Please insert correct currency\n");
                }//end if
            }//end while loop
            Console.WriteLine();
            currency.DisplayCurrency();


                            



            //The kiosk should calculate how much change should be dispensed and dispense the change. (Research and use a greedy algorithm to dispense the change)

            //If the kiosk does not have enough physical money to supply the change then the transaction will be cancelled, and the user should be offered another way to make payments(you do not have to develop other methods of payment)
            
            
        }//end main


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
