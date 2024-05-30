#region Currency Class
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

public class CurrencyDenomination {

    #region USA CURRENCY
    decimal _oneDollar = 0;
    decimal _twoDollar = 0;
    decimal _fiveDollar = 0;
    decimal _tenDollar = 0;
    decimal _twentyDollar = 0;
    decimal _fiftyDollar = 0;
    decimal _hundredDollar = 0;

    decimal _penny = 0m;
    decimal _nickel = 0m;
    decimal _dime = 0m;
    decimal _quarter = 0m;
    decimal _halfDollar = 0m;
    decimal _eisenhower = 0m;
    decimal _dollarCoin = 0m;

    decimal _depositedAmount = 0;
    #endregion

    #region Credit Card Generator
    public class CreditCardGenerator {
        private static Random _random = new Random();

        public static string GenerateCardNumber(string prefix, int length) {
            // Generate the card number with the given prefix
            string cardNumber = prefix;
            while (cardNumber.Length < length - 1) {
                cardNumber += _random.Next(0, 10).ToString();
            }

            // Calculate the check digit using the Luhn algorithm
            int checkDigit = CalculateLuhnCheckDigit(cardNumber);
            cardNumber += checkDigit.ToString();

            return cardNumber;
        }

        private static int CalculateLuhnCheckDigit(string cardNumber) {
            char[] digits = cardNumber.ToCharArray();
            int sum = 0;
            bool shouldDouble = true;

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

            int mod = sum % 10;
            int checkDigit = (mod == 0) ? 0 : 10 - mod;
            return checkDigit;
        }
    }

    #endregion

    #region Currency Properties

        public decimal OneDollar { get { return _oneDollar; } set { _oneDollar = value; } }
        public decimal TwoDollar { get { return _twoDollar; } set { _twoDollar = value; } }
        public decimal FiveDollar { get { return _fiveDollar; } set { _fiveDollar = value; } }
        public decimal TenDollar { get { return _tenDollar; } set { _tenDollar = value; } }
        public decimal TwentyDollar { get { return _twentyDollar; } set { _twentyDollar = value; } }
        public decimal FiftyDollar { get { return _fiftyDollar; } set { _fiftyDollar = value; } }
        public decimal HundredDollar { get { return _hundredDollar; } set { _hundredDollar = value; } }

        public decimal Penny { get { return _penny; } set { _penny = value; } }
        public decimal Nickel { get { return _nickel; } set { _nickel = value; } }
        public decimal Dime { get { return _dime; } set { _dime = value; } }
        public decimal Quarter { get { return _quarter; } set { _quarter = value; } }
        public decimal HalfDollar { get { return _halfDollar; } set { _halfDollar = value; } }
        public decimal DollarCoin { get { return _dollarCoin; } set { _dollarCoin = value; } }
        public decimal Eisenhower { get { return _eisenhower; } set { _eisenhower = value; } }

        public decimal depositedAmount { get { return _depositedAmount; }}
    #endregion

    #region Currency Methods

        //deposit and withdraw cash

        #region Deposit

            public void Deposit(decimal hundredDollarBill, decimal fiftyDollarBill, decimal twentyDollarBill, decimal tenDollarBill, decimal fiveDollarBill, decimal twoDollarBill, decimal oneDollarBill, decimal pennyCoin, decimal nickelCoin, decimal dimeCoin, decimal quarterCoin, decimal halfDollarCoin, decimal eisenhowerCoin, decimal dollarCoin) {
                _hundredDollar += hundredDollarBill;
                _fiftyDollar += fiftyDollarBill;
                _twentyDollar += twentyDollarBill;
                _tenDollar += tenDollarBill;
                _fiveDollar += fiveDollarBill;
                _twoDollar += twoDollarBill;
                _oneDollar += oneDollarBill;
                _penny += pennyCoin;
                _nickel += nickelCoin;
                _dime += dimeCoin;
                _quarter += quarterCoin;
                _halfDollar += halfDollarCoin;
                _dollarCoin += dollarCoin;

                Console.WriteLine("Deposit Successful!");

            }//end deposit method

            #endregion

        #region Withdraw

            public void Withdraw(decimal hundredDollarBill, decimal fiftyDollarBill, decimal twentyDollarBill, decimal tenDollarBill, decimal fiveDollarBill, decimal twoDollarBill, decimal oneDollarBill, decimal pennyCoin, decimal nickelCoin, decimal dimeCoin, decimal quarterCoin, decimal halfDollarCoin, decimal eisenhowerCoin, decimal dollarCoin) {
                _hundredDollar -= hundredDollarBill;
                _fiftyDollar -= fiftyDollarBill;
                _twentyDollar -= twentyDollarBill;
                _tenDollar -= tenDollarBill;
                _fiveDollar -= fiveDollarBill;
                _twoDollar -= twoDollarBill;
                _oneDollar -= oneDollarBill;
                _penny -= pennyCoin;
                _nickel -= nickelCoin;
                _dime -= dimeCoin;
                _quarter -= quarterCoin;
                _halfDollar -= halfDollarCoin;
                _dollarCoin -= dollarCoin;

                Console.WriteLine("Withdraw Successful!");
            }//end withdraw method

    #endregion


    //increment and decrement cash

       
        

        public void SubDepositedAmount(decimal amount) {
        while (amount > 0) {
            if (amount >= 100 && _hundredDollar > 0) {
                amount -= 100;
                _hundredDollar--;
            } else if (amount >= 50 && _fiftyDollar > 0) {
                amount -= 50;
                _fiftyDollar--;
            } else if (amount >= 20 && _twentyDollar > 0) {
                amount -= 20;
                _twentyDollar--;
            } else if (amount >= 10 && _tenDollar > 0) {
                amount -= 10;
                _tenDollar--;
            } else if (amount >= 5 && _fiveDollar > 0) {
                amount -= 5;
                _fiveDollar--;
            } else if (amount >= 2 && _twoDollar > 0) {
                amount -= 2;
                _twoDollar--;
            } else if (amount >= 1 && _oneDollar > 0) {
                amount -= 1;
                _oneDollar--;
            } else if (amount >= 1 && _dollarCoin > 0 && _oneDollar == 0) {
                amount -= 1;
                _dollarCoin--;
            } else if (amount > 0.5m && _halfDollar > 0) {
                amount -= 0.5m;
                _halfDollar--;
            } else if (amount > 0.25m && _quarter > 0) {
                amount -= 0.25m;
                _quarter--;
            } else if (amount > 0.10m && _dime > 0) {
                amount -= 0.10m;
                _dime--;
            } else if (amount > 0.05m && _nickel > 0) {
                amount -= 0.05m;
                _nickel--;
            } else if (amount >= 0.01m && _penny > 0) {
                amount -= 0.01m;
                _penny--;
            }//end if
        }//end while
        }//end method
    
        #region Increment

            public void AddHundredDollar(int count) {
                _hundredDollar += count;
            }//end method

            public void AddFiftyDollar(int count) {
                _fiftyDollar += count;
            }//end method

            public void AddTwentyyDollar(int count) {
                _twentyDollar += count;
            }//end method

            public void AddTenDollar(int count) {
                _tenDollar += count;
            }//end method

            public void AddFiveDollar(int count) {
                _fiveDollar += count;
            }//end method

            public void AddTwoDollar(int count) {
                _twoDollar += count;
            }//end method

            public void AddOneDollar(int count) {
                _oneDollar += count;
            }//end method

            public void AddPenny(int count) {
                _penny += count;
            }//end method

            public void AddNickel(int count) {
                _nickel += count;
            }//end method

            public void AddDime(int count) {
                _dime += count;
            }//end method

            public void AddQuarter(int count) {
                _quarter += count;
            }//end method

            public void AddHalfDollar(int count) {
                _halfDollar += count;
            }//end method

            public void AddEisenhowerCoin(int count) {
                _eisenhower += count;
            }//end method

            public void AddDollarCoin(int count) {
                _dollarCoin += count;
            }//end method
            #endregion

        #region Decrement
            public void SubHundredDollar(int count) {
                _hundredDollar -= count;
            }//end method

            public void SubFiftyDollar(int count) {
                _fiftyDollar -= count;
            }//end method

            public void SubTwentyyDollar(int count) {
                _twentyDollar -= count;
            }//end method

            public void SubTenDollar(int count) {
                _tenDollar -= count;
            }//end method

            public void SubFiveDollar(int count) {
                _fiveDollar -= count;
            }//end method

            public void SubTwoDollar(int count) {
                _twoDollar -= count;
            }//end method

            public void SubOneDollar(int count) {
                _oneDollar -= count;
            }//end method

            public void SubPenny(int count) {
                _penny -= count;
            }//end method

            public void SubNickel(int count) {
                _nickel -= count;
            }//end method

            public void SubDime(int count) {
                _dime -= count;
            }//end method

            public void SubQuarter(int count) {
                _quarter -= count;
            }//end method

            public void SubHalfDollar(int count) {
                _halfDollar -= count;
            }//end method

            public void SubEisenhowerCoin(int count) {
                _eisenhower -= count;
            }//end method

            public void SubDollarCoin(int count) {
                _dollarCoin -= count;
            }//end method

            #endregion

        //display currency in kiosk
        #region Display

            public void DisplayCurrency() {
                Console.WriteLine("Current Amount of Money in the Kiosk:");
                Console.WriteLine($"One Dollar Bills: {OneDollar}");
                Console.WriteLine($"Two Dollar Bills: {TwoDollar}");
                Console.WriteLine($"Five Dollar Bills: {FiveDollar}");
                Console.WriteLine($"Ten Dollar Bills: {TenDollar}");
                Console.WriteLine($"Twenty Dollar Bills: {TwentyDollar}");
                Console.WriteLine($"Fifty Dollar Bills: {FiftyDollar}");
                Console.WriteLine($"Hundred Dollar Bills: {HundredDollar}");
                Console.WriteLine($"Pennies: {Penny}");
                Console.WriteLine($"Nickels: {Nickel}");
                Console.WriteLine($"Dimes: {Dime}");
                Console.WriteLine($"Quarters: {Quarter}");
                Console.WriteLine($"Half Dollars: {HalfDollar}");
                Console.WriteLine($"Dollar Coins: {DollarCoin}");
            }//end display method

    #endregion 
    
    #region Display

        public double TotalAmount() {
            
            double total = 0;
            total += (double)OneDollar * 1;
            total += (double)TwoDollar * 2;
            total += (double)FiveDollar * 5;
            total += (double)TenDollar * 10;
            total += (double)TwentyDollar * 20;
            total += (double)FiftyDollar * 50;
            total += (double)HundredDollar * 100;
            total += (double)Penny * 0.01;
            total += (double)Nickel * 0.05;
            total += (double)Dime * 0.10;
            total += (double)Quarter * 0.25;
            total += (double)HalfDollar * 0.50;
            total += (double)DollarCoin * 1;

            return total;
        }//end TotalAmount method

    #endregion

        //Dispense Change
        public bool DispenseChange(decimal change) {
 
            decimal HundredDollar = _hundredDollar;
            decimal FiftyDollar = _fiftyDollar;
            decimal TwentyDollar = _twentyDollar;
            decimal TenDollar = _tenDollar;
            decimal FiveDollar = _fiveDollar;
            decimal TwoDollar = _twoDollar;
            decimal OneDollar = _oneDollar;
            decimal DollarCoin = _dollarCoin;
            decimal HalfDollar = _halfDollar;
            decimal Quarter = _quarter;
            decimal Dime = _dime;
            decimal Nickel = _nickel;
            decimal Penny = _penny;

        Console.WriteLine("Dispense Change: ");
            while (change > 0) {
            if (change >= 100 && _hundredDollar > 0) {
                Console.WriteLine("$100 x 1");
                _hundredDollar--;
                change -= 100;
            } else if (change >= 50 && _fiftyDollar > 0) {
                Console.WriteLine("$50 x 1");
                _fiftyDollar--;
                change -= 50;
            } else if (change >= 20 && _twentyDollar > 0) {
                Console.WriteLine("$20 x 1");
                _twentyDollar--;
                change -= 20;
            } else if (change >= 10 && _tenDollar > 0) {
                Console.WriteLine("$10 x 1");
                _tenDollar--;
                change -= 10;
            } else if (change >= 5 && _fiveDollar > 0) {
                Console.WriteLine("$5 x 1");
                _fiveDollar--;
                change -= 5;
            } else if (change >= 2 && _twoDollar > 0) {
                Console.WriteLine("$2 x 1");
                _twoDollar--;
                change -= 2;
            } else if (change >= 1 && _oneDollar > 0 ) {
                Console.WriteLine("$1 x 1");
                _oneDollar--;
                change -= 1;
            } else if(change >= 1 && _dollarCoin > 0 &&_oneDollar == 0) {
                Console.WriteLine("$1 x 1");
                _dollarCoin--;
                change -= 1;
            } else if(change > 0.5m && _halfDollar > 0) {
                Console.WriteLine("$0.50 x 1");
                _halfDollar--;
                change -= 0.5m;
            }else if(change > 0.25m && _quarter > 0) {
                Console.WriteLine("$0.25 x 1");
                _quarter--;
                change -= 0.25m;
            }else if(change > 0.10m && _dime > 0) {
                Console.WriteLine("$0.10 x 1");
                _dime--;
                change -= 0.10m;
            }else if(change > 0.05m && _nickel > 0) {
                Console.WriteLine("$0.05 x 1");
                _nickel--;
                change -= 0.05m;
            }else if(change >= 0.01m && _penny > 0) {
                Console.WriteLine("$0.01 x 1");
                _penny--;
                change -= 0.01m;
            }else {
                return false;
            }//end else if
            }//end while

        _hundredDollar = HundredDollar;
        _fiftyDollar = FiftyDollar;
        _twentyDollar = TwentyDollar;
        _tenDollar = TenDollar;
        _fiveDollar = FiveDollar;
        _twoDollar = TwoDollar;
        _oneDollar = OneDollar;

        _dollarCoin = DollarCoin;
        _halfDollar = HalfDollar;
        _quarter = Quarter;
        _dime = Dime;
        _nickel = Nickel;
        _penny = Penny;

        return true;

        }//end method  


    #endregion

    #region Luhn Algorithm

    public static bool IsValidCreditCard(string cardNumber) {

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

    }//end method

    #endregion

    #region CCValidation
    static void CreditCardValidation() {

        Console.WriteLine("Enter your credit card number: ");
        string cardNumber = Console.ReadLine();

        if (IsValidCreditCard(cardNumber)) {
            Console.WriteLine("The card is valid");
        } else {
            Console.WriteLine("The card is NOT valid");
        }//end if


    }//end function
    #endregion

}//end Currency Class

#endregion

#region Shopping Cart Class

public class ShoppingCart {
        //Field
        double _totalCost = 0;
        double _totalInserted = 0;

        //Constructor
        /*
        public ShoppingCart() {
            _totalCost = 0;
            _totalInserted = 0;
        }//end constructor


        */
        
        //Method
        public void AddItemCost(double itemCost) {
            _totalCost += itemCost;
        }//end method

        public void AddInsertedAmount(double amount) {
            _totalInserted += amount;
        }//end method
        
        //Getter
        public double GetTotalCost () {
            return _totalCost;
        }//end get method

        public double GetTotalInserted() {
            return _totalInserted;
        }

        //Clear the Cart
        public void Clear() {
            _totalCost = 0;
            _totalInserted = 0;
        }//end method

        //RefundInsertedAmount
        public void RefundInsertedAmount(decimal refund) {
            double amount = (double)refund;
            _totalInserted = _totalInserted - amount;
            Console.WriteLine($"\nRefunding Amount: {amount}\n");
        }//end method

         
    
    }//end Shopping Cart class



    #endregion


public class TransactionLog {

    public void LogTransaction(int transactionNum, DateTime transactionDate, DateTime transactionTime, double paymentCash, string cardVendor, double paymentCard, double changeGiven) {
        string fileName = $"{transactionDate:MMM-dd-yyyy}.log";
        string logData = $"{transactionNum}, {transactionDate}, {transactionTime}, {paymentCash}, {cardVendor}, {paymentCard}, {changeGiven}";
        if (!File.Exists(fileName)) {
            using (StreamWriter sw = File.CreateText(fileName)) { sw.WriteLine(logData); } 
        }else {
            using(StreamWriter sw = File.AppendText(fileName)) { sw.WriteLine(logData); }
        }//end if
    }//end method
}//end class



