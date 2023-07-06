using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Kiosk
{
    class AssembleFunctions
    {
        
        public struct DefineGlobalBankStructure
        {
            public int Pennies;
            public int Nickels;
            public int Dimes;
            public int Quarters;
            public int Ones;
            public int Fives;
            public int Tens;
            public int Twenties;
            public int Fifties;
            public int Hundreds;
        }

        static string UserInputOfItemsAndValidation(bool plural)
        {
            string itemInput = "";
            bool valid = false;
            while (valid == false)
            {
                if (plural == false)
                {
                    Console.WriteLine("Input the cost of an item.");
                }
                else
                {
                    Console.WriteLine("Input the cost of an another item. (Or press ENTER without typing to proceed to pay.)");
                }
                itemInput = Console.ReadLine();
                char[] arr = new char[itemInput.Length];
                itemInput.CopyTo(0, arr, 0, itemInput.Length);
                bool decimalUsed = false;
                bool errorFound = false;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (errorFound == false)
                    {
                        if (arr[i] != '1' && arr[i] != '2' && arr[i] != '3' && arr[i] != '4' && arr[i] != '5' && arr[i] != '6' && arr[i] != '7' && arr[i] != '8' && arr[i] != '9' && arr[i] != '0')
                        {
                            if (arr[i] == '.' && decimalUsed == false)
                            {
                                decimalUsed = true;
                            }//It's a decimal
                            else
                            {
                                Console.WriteLine("ERROR: You must enter a number value. Try again.");
                                errorFound = true;
                                decimalUsed = false;
                            }//It's not a valid character.
                        }//It is not a number.
                    }
                }//Analyse each character of the user's input.
                if (errorFound == false)
                {
                    valid = true;
                }//The while loop ends.
                else
                {
                    valid = false;
                }//The while loop resets.
            }
            return itemInput;
        }

        static double DisplayItemTotal()
        {
            double totalCost = 0.00;
            bool plural = false;
            string itemInput = UserInputOfItemsAndValidation(plural);
            while (itemInput != "")
            {
                double itemCost = Math.Round(Convert.ToDouble(itemInput), 2);
                totalCost = Math.Round(totalCost + itemCost, 2);
                plural = true;
                itemInput = UserInputOfItemsAndValidation(plural);
            }//Accept the value as an item's price.
            totalCost = Math.Round(totalCost, 2);
            string displayTotalCost = formatMoneyValue(totalCost);
            Console.WriteLine("The total is $" + displayTotalCost + ".");
            Console.WriteLine(" ");
            return totalCost;
        }

        static double UserInputOfFundsAndValidation(int[] kioskArray, double totalCost, bool endProgram)
        {
            double remainingCost = totalCost;
            while (remainingCost > 0 && endProgram == false)
            {
                string fundInput = "";
                bool valid = false;
                double payment = 0;
                while (valid == false)
                {
                    Console.WriteLine("Insert a bill or coin. (Enter the value of the bill/coin inserted.)");
                    fundInput = Console.ReadLine();
                    char[] arr = new char[fundInput.Length];
                    fundInput.CopyTo(0, arr, 0, fundInput.Length);
                    bool decimalUsed = false;
                    bool errorFound = false;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (errorFound == false)
                        {
                            if (arr[i] != '1' && arr[i] != '2' && arr[i] != '3' && arr[i] != '4' && arr[i] != '5' && arr[i] != '6' && arr[i] != '7' && arr[i] != '8' && arr[i] != '9' && arr[i] != '0')
                            {
                                if (arr[i] == '.' && decimalUsed == false)
                                {
                                    decimalUsed = true;
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: Not a valid bill or coin value. Try again.");
                                    errorFound = true;
                                    decimalUsed = false;
                                }
                            }
                        }
                    }
                    if (errorFound == false && fundInput != "")
                    {
                        payment = Convert.ToDouble(fundInput);
                        if (payment == 0.01 || payment == 0.05 || payment == 0.1 || payment == 0.25 || payment == 1 || payment == 5 || payment == 10 || payment == 20 || payment == 50 || payment == 100)
                        {
                            valid = true;
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Not a possible value for a bill or coin.");
                            valid = false;
                        }
                    }
                    else
                    {
                        valid = false;
                    }
                }//See lines 41-59.
                payment = Math.Round(payment, 2);
                kioskArray = kioskStockAddition(kioskArray, payment, endProgram);
                remainingCost -= payment;
                remainingCost = Math.Round(remainingCost, 2);
                if (remainingCost > 0)
                {
                    string displayRemainingCost = formatMoneyValue(remainingCost);
                    Console.WriteLine("Remaining cost: $" + displayRemainingCost);
                }//Display remaining cost and another payment input.
            }//Accept user payments as inputs.
            return remainingCost;
        }//FOR CASH.

        static double CalculateChange(double remainingCost)
        {
            double change = -1 * (remainingCost);
            Math.Round(change, 2);
            return change;
        }
        static bool CheckIfEnoughFundsAreInBank(int[] kioskArray, double totalCost, double change, bool endProgram)
        {
            bool kioskIsValid = false;
            if (endProgram == false)
            {
                while (change >= 100.00 && kioskArray[0] > 0)
                {
                    kioskArray[0] -= 1;
                    change -= 100;
                    change = Math.Round(change, 2);
                }//Dispense this bill/coin to the user.
                while (change >= 50.00 && kioskArray[1] > 0)
                {
                    kioskArray[1] -= 1;
                    change -= 50;
                    change = Math.Round(change, 2);
                }//See line 170.
                while (change >= 20.00 && kioskArray[2] > 0)
                {
                    kioskArray[2] -= 1;
                    change -= 20;
                    change = Math.Round(change, 2);
                }//See line 170.
                while (change >= 10.00 && kioskArray[3] > 0)
                {
                    kioskArray[3] -= 1;
                    change -= 10;
                    change = Math.Round(change, 2);
                }//See line 170.
                while (change >= 5.00 && kioskArray[4] > 0)
                {
                    kioskArray[4] -= 1;
                    change -= 5;
                    change = Math.Round(change, 2);
                }//See line 170.
                while (change >= 1.00 && kioskArray[5] > 0)
                {
                    kioskArray[5] -= 1;
                    change -= 1;
                    change = Math.Round(change, 2);

                }//See line 170.
                while (change >= 0.25 && kioskArray[6] > 0)
                {
                    kioskArray[6] -= 1;
                    change -= 0.25;
                    change = Math.Round(change, 2);
                }//See line 170.
                while (change >= 0.1 && kioskArray[7] > 0)
                {
                    kioskArray[7] -= 1;
                    change -= 0.1;
                    change = Math.Round(change, 2);
                }//See line 170.
                while (change >= 0.05 && kioskArray[8] > 0)
                {
                    kioskArray[8] -= 1;
                    change -= 0.05;
                    change = Math.Round(change, 2);
                }//See line 170.
                while (change >= 0.01 && kioskArray[9] > 0)
                {
                    kioskArray[9] -= 1;
                    change -= 0.01;
                    change = Math.Round(change, 2);
                }//See line 170.
                if (change > 0)
                {
                    kioskIsValid = false;
                }//The kiosk doesn't have enough bills/coins to dispense.
                else
                {
                    kioskIsValid = true;
                }//The kiosk has the necessary amount of change to dispense.
            }
            return kioskIsValid;
        }
        static void DispenseChange(int[] kioskArray, double totalCost, double change, bool kioskIsValid, bool endProgram)
        {
            if (kioskIsValid == true && endProgram == false)
            {
                if (change == 0)
                {
                    change = 0;
                }//Ensure the value of change doesn't equal a negative zero. (WHY WAS THAT EVEN A THING!?)
                string newChange = formatMoneyValue(change);
                Console.WriteLine("Your change is $" + Convert.ToDecimal(newChange) + ". Have a great day!");
            }//The payment has succeeded. Change is dispensed.
            else if (endProgram == false)
            {
                Console.Write("Unfortunately, we do not have the sufficient change for this purchase. (Refunds $" + (totalCost + change) + ".) Please choose another method of payment.");
                Console.WriteLine("Enter your card number:");
            }//The payment has failed and a refund of payment inputs is dispensed. Converts to card method.
        }
        static string[] cardRecognition(string method, int[] kioskArray, double totalCost, double CashBackRequest, double cashPaid, bool forceCardMethod, bool endProgram)
        {
            string cardNumber = " ";
            string cardIsRecognized = "false";
            string cardVendor = " ";
            if (endProgram == false)
            {
                Console.WriteLine("Please enter your card number. (Do not include spaces.)");
                cardNumber = Console.ReadLine();
                while (cardNumber.Length == 0)
                {
                    Console.WriteLine("Um...we actually have to see your card number to use it...");
                    cardNumber = Console.ReadLine();
                }//The user didn't type anything? Huh. Better give them another chance to do so.
                char[] arr = new char[cardNumber.Length];
                cardNumber.CopyTo(0, arr, 0, cardNumber.Length);
                int vendorNumber = Convert.ToInt32(new string(string.Join("", arr[0], arr[1], arr[2], arr[3], arr[4], arr[5])));
                if (vendorNumber >= 400000 && vendorNumber <= 499999)
                {
                    Console.WriteLine("Visa detected.");
                    cardIsRecognized = "true";
                    cardVendor = "Visa";
                }//The card, if valid, is a visa.
                else if ((vendorNumber >= 222100 && vendorNumber <= 272099) || (vendorNumber >= 510000 && vendorNumber <= 559999))
                {
                    Console.WriteLine("Mastercard detected.");
                    cardIsRecognized = "true";
                    cardVendor = "Mastercard";
                }//The card, if valid, is a MasterCard.
                else if ((vendorNumber >= 601100 && vendorNumber <= 601199) || (vendorNumber >= 622126 && vendorNumber <= 622925) || (vendorNumber >= 624000 && vendorNumber <= 626999) || (vendorNumber >= 628200 && vendorNumber <= 628899) || (vendorNumber >= 640000 && vendorNumber <= 659999))
                {
                    Console.WriteLine("Discover Card detected.");
                    cardIsRecognized = "true";
                    cardVendor = "Discover Card";
                }//The card, if valid, is a Discover Card.
                else if ((vendorNumber >= 340000 && vendorNumber <= 349999) || (vendorNumber >= 370000 && vendorNumber <= 379999))
                {
                    Console.WriteLine("American Express detected.");
                    cardIsRecognized = "true";
                    cardVendor = "American Express";
                }//The card, if valid, is a American Express.
                else
                {
                    Console.WriteLine("ERROR: No vendor found. Press ENTER to choose another payment method.");
                    paymentMethodDirectory(method, kioskArray, totalCost, CashBackRequest, cashPaid, forceCardMethod, endProgram);
                }//The card number is not recognized as any brand, and is therefore invalid.
            }
            string[] card = new string[3] { cardNumber, cardIsRecognized, cardVendor };
            return card;
        }//FOR CARD.
        static bool cardValidation(string method, int[] kioskArray, double totalCost, double CashBackRequest, string cardNumber, double cashPaid, bool forceCardMethod, bool endProgram)
        {
            bool cardIsValid = false;
            if (endProgram == false)
            {
                char[] arr = new char[cardNumber.Length];
                cardNumber.CopyTo(0, arr, 0, cardNumber.Length);
                bool errorFound = false;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (errorFound == false)
                    {
                        if (arr[i] != '1' && arr[i] != '2' && arr[i] != '3' && arr[i] != '4' && arr[i] != '5' && arr[i] != '6' && arr[i] != '7' && arr[i] != '8' && arr[i] != '9' && arr[i] != '0')
                        {
                            Console.WriteLine("ERROR: the card must consist of only numbers. (Do not include spaces!) Try again.");
                            errorFound = true;
                        }//The user tried to put letters in a card number...? I hope this code never has to run outside of testing.
                    }
                }//Condition 1: the card is strictly numeric.
                if (cardNumber.Length > 19 || cardNumber.Length < 12)
                {
                    Console.WriteLine("ERROR: the card must be 12-19 digits long.");
                    errorFound = true;
                }//Condition 2: card Length.
                if (errorFound == false)
                {
                    bool doublerSwitch = false;
                    int runningTotal = 0;
                    for (int i = (cardNumber.Length - 1); i > -1; i--)
                    {
                        int convertedArr = Convert.ToInt32(Convert.ToString(arr[i]));
                        if (doublerSwitch == false)
                        {
                            runningTotal += convertedArr;
                        }//Add the single number without changes.
                        else
                        {
                            string doubledNumber = Convert.ToString(convertedArr * 2);
                            if (doubledNumber.Length > 1)
                            {
                                char[] DblArr = new char[doubledNumber.Length];
                                doubledNumber.CopyTo(0, DblArr, 0, doubledNumber.Length);
                                int doubledNumberSum = Convert.ToInt32(Convert.ToString(DblArr[0])) + Convert.ToInt32(Convert.ToString(DblArr[1]));
                                runningTotal += doubledNumberSum;
                            }//Added the tens and ones place and add the sum to the algorithm.
                            else
                            {
                                runningTotal += Convert.ToInt32(doubledNumber);
                            }//Add the doubled number to the sum of the algorithm.
                        }//Double the number.
                        if (doublerSwitch == false)
                        {
                            doublerSwitch = true;
                        }//The next number should be modified before being added to the algorithm's sum.
                        else
                        {
                            doublerSwitch = false;
                        }//The next number should be added to the algorithm's sum wihtout changes.
                    }//Use the designated algorithm to determine the card's validity.
                    while (runningTotal > 0)
                    {
                        runningTotal -= 10;
                        if (runningTotal == 0)
                        {
                            cardIsValid = true;
                        }
                    }//Check if the algorithm's sum is divisible by 10. If so, it passes as valid.
                }//Condition 3: the LUHN algorithm.
                if (cardIsValid == false)
                {
                    Console.WriteLine("ERROR: This card is not valid. Press ENTER to choose another form of payment.");
                    paymentMethodDirectory(method, kioskArray, totalCost, CashBackRequest, cashPaid, forceCardMethod, endProgram);
                }
            }
            return cardIsValid;
        }
        static double cashBack(bool endProgram)
        {

            double CashBackRequest = 0.00;
            if (endProgram == false)
            {
                Console.WriteLine("Select the amount of cash back you would like to recieve (Enter the corresponding number).");
                Console.WriteLine("Press 1 for NO CASH BACK.");
                Console.WriteLine("Press 2 for $20.00.");
                Console.WriteLine("Press 3 for $40.00.");
                Console.WriteLine("Press 4 for $60.00.");
                Console.WriteLine("Press 5 for $80.00.");
                Console.WriteLine("Press 6 for $100.00.");
                string rawCashBackRequest = Console.ReadLine();
                if (rawCashBackRequest == "2")
                {
                    CashBackRequest = 20.00;
                }
                else if (rawCashBackRequest == "3")
                {
                    CashBackRequest = 40.00;
                }
                else if (rawCashBackRequest == "4")
                {
                    CashBackRequest = 60.00;
                }
                else if (rawCashBackRequest == "5")
                {
                    CashBackRequest = 80.00;
                }
                else if (rawCashBackRequest == "6")
                {
                    CashBackRequest = 100.00;
                }
            }
            return CashBackRequest;
        }
        static string[] MoneyRequest(string account_number, decimal amount)
        {
            Random rnd = new Random();//50% CHANCE TRANSACTION PASSES OR FAILS
            bool pass = rnd.Next(100) < 50;//50% CHANCE THAT A FAILED TRANSACTION IS DECLINED
            bool declined = rnd.Next(100) < 50;
            if (pass)
            {
                return new string[] { account_number, amount.ToString() };
            }
            else
            {
                if (!declined)
                {
                    return new string[] { account_number, (amount / rnd.Next(2, 6)).ToString() };
                }
                else
                {
                    return new string[] { account_number, "declined" };
                }//end if
            }//end if
        }//DO NOT CHANGE THIS FUNCTION!
        static void detectFailWithoutCashBack(string method, int[] kioskArray, double totalCost, double CashBackRequest, string[] cardResults, double cashPaid, bool forceCardMethod, bool endProgram)
        {
            if (CashBackRequest == 0.00 && endProgram == false)
            {
                if (cardResults[1] != "declined")
                {
                    if (Math.Round(Convert.ToDouble(cardResults[1]), 2) != totalCost)
                    {
                        string newCardResults1 = formatMoneyValue(Math.Round(Convert.ToDouble(cardResults[1]), 2));
                        Console.WriteLine("This card has insufficient funds. Your remaining balance is $" + newCardResults1 + ".");
                        Console.WriteLine("Press '1' to choose another way to pay, or press '2' to cancel the payment.");
                        bool repeat = true;
                        while (repeat == true)
                        {
                            string howToProceed = Console.ReadLine();
                            if (howToProceed == "1")
                            {
                                repeat = false;
                                paymentMethodDirectory(method, kioskArray, totalCost, CashBackRequest, cashPaid, forceCardMethod, endProgram);
                            }
                            else if (howToProceed == "2")
                            {
                                repeat = false;
                                Console.WriteLine("Maybe next time. Have a nice day!");
                                endProgram = true;
                                Console.ReadKey();//END FUNCTION HERE
                            }
                            else
                            {
                                Console.WriteLine("ERROR: must type either '1' or '2'.");
                            }
                        }
                    }
                }
                else
                {
                    {
                        Console.WriteLine("This card has been declined.");
                        Console.WriteLine("Press '1' to choose another way to pay, or press '2' to cancel the payment.");
                        bool repeat = true;
                        while (repeat == true)
                        {
                            string howToProceed = Console.ReadLine();
                            if (howToProceed == "1")
                            {
                                repeat = false;
                                paymentMethodDirectory(method, kioskArray, totalCost, CashBackRequest, cashPaid, forceCardMethod, endProgram);
                            }
                            else if (howToProceed == "2")
                            {
                                repeat = false;
                                Console.WriteLine("Maybe next time. Have a nice day!");
                                endProgram = true;
                            }
                            else
                            {
                                Console.WriteLine("ERROR: must type either '1' or '2'.");
                            }
                        }
                    }
                }
            }
        }
        static void detectFailWithCashBack(string method, int[] kioskArray, double totalCost, double CashBackRequest, string[] cardResults, double cashPaid, bool forceCardMethod, bool endProgram)
        {
            if (CashBackRequest > 0.00 && endProgram == false)
            {
                if (cardResults[1] != "declined")
                {
                    if (Math.Round(Convert.ToDouble(cardResults[1]), 2) != totalCost)
                    {
                        string newCardResults1 = formatMoneyValue(Math.Round(Convert.ToDouble(cardResults[1]), 2));
                        Console.WriteLine("This card has insufficient funds. Your remaining balance is $" + newCardResults1 + ".");
                        Console.WriteLine("Press '1' to enter using a different card, or press '2' to cancel the payment.");
                        bool repeat = true;
                        while (repeat == true)
                        {
                            string howToProceed = Console.ReadLine();
                            if (howToProceed == "1")
                            {
                                forceCardMethod = true;
                                paymentMethodDirectory(method, kioskArray, totalCost, CashBackRequest, cashPaid, forceCardMethod, endProgram);
                            }
                            else if (howToProceed == "2")
                            {
                                repeat = false;
                                Console.WriteLine("Maybe next time. Have a nice day!");
                                endProgram = true;
                            }
                            else
                            {
                                Console.WriteLine("ERROR: must type either '1' or '2'.");
                            }
                        }
                    }
                }
                else if (cardResults[1] == "declined")
                {
                    Console.WriteLine("This card has been declined");
                    Console.WriteLine("Press '1' to enter a different card, or press '2' to cancel the payment.");
                    bool repeat = true;
                    while (repeat == true)
                    {
                        string howToProceed = Console.ReadLine();
                        if (howToProceed == "1")
                        {
                            repeat = false;
                            paymentMethodDirectory(method, kioskArray, totalCost, CashBackRequest, cashPaid, forceCardMethod, endProgram);
                        }
                        else if (howToProceed == "2")
                        {
                            repeat = false;
                            Console.WriteLine("Maybe next time. Have a nice day!");
                            endProgram = true;
                        }
                        else
                        {
                            Console.WriteLine("ERROR: must type either '1' or '2'.");
                        }
                    }
                }
            }
            else if (endProgram == false)//If no failure is detected then cashback can be applied, so this else if statement will update the kiosk value accordingly.
            {
                kioskArray = kioskStockRemaining(kioskArray, totalCost, CashBackRequest, endProgram);
            }
        }

        static string[] paymentMethodDirectory(string method, int[] kioskArray, double totalCost, double CashBackRequest, double cashPaid, bool forceCardMethod, bool endProgram)
        {
            bool success = false;
            double change = 0.00;
            string[] card = new string[3];
            if (endProgram == false)
            {
                if (forceCardMethod == false)
                {
                    Console.WriteLine("Press '1' to use cash, or press '2' to use a card.");
                }
                bool repeat = true;
                while (repeat == true)
                {
                    if (forceCardMethod == false)
                    {
                        method = Console.ReadLine();
                    }
                    if (method == "1" && forceCardMethod == false)
                    {
                        repeat = false;
                        double remainingCost = UserInputOfFundsAndValidation(kioskArray, totalCost, endProgram);
                        change = CalculateChange(remainingCost);
                        bool kioskIsValid = CheckIfEnoughFundsAreInBank(kioskArray, totalCost, change, endProgram);
                        DispenseChange(kioskArray, totalCost, change, kioskIsValid, endProgram);
                        cashPaid += totalCost;
                        success = true;
                        endProgram = true;
                    }
                    else if (method == "2" || forceCardMethod == true)
                    {
                        forceCardMethod = false;
                        repeat = false;
                        card = cardRecognition(method, kioskArray, totalCost, CashBackRequest, cashPaid, forceCardMethod, endProgram);
                        string cardNumber = card[0];
                        bool cardIsRecognized = false;
                        if (card[1] == "true")
                        {
                            cardIsRecognized = true;
                        }
                        bool cardIsValid = cardValidation(method, kioskArray, totalCost, CashBackRequest, cardNumber, cashPaid, forceCardMethod, endProgram);
                        if (CashBackRequest == -1)
                        {
                            CashBackRequest = cashBack(endProgram);
                            totalCost += CashBackRequest;
                            totalCost = Math.Round(totalCost, 2);
                            string newTotalCost = formatMoneyValue(totalCost);
                            Console.WriteLine("The new total (including the cash back) is $" + newTotalCost + ".");
                        }//If the value is not -1, then the customer has already been asked about cashback, therefore the function shall not be Called more than once.
                        string[] cardResults = MoneyRequest(cardNumber, Convert.ToDecimal(totalCost));
                        detectFailWithoutCashBack(method, kioskArray, totalCost, CashBackRequest, cardResults, cashPaid, forceCardMethod, endProgram);
                        detectFailWithCashBack(method, kioskArray, totalCost, CashBackRequest, cardResults, cashPaid, forceCardMethod, endProgram);
                        if (cardResults[1] != "declined")
                        {
                            if (Math.Round(Convert.ToDouble(cardResults[1]), 2) == totalCost)
                            {
                                kioskArray = kioskStockRemaining(kioskArray, totalCost, CashBackRequest, endProgram);
                            }
                        }
                        string newCashBackRequest = formatMoneyValue(CashBackRequest);
                        if (endProgram == false)
                        {
                            Console.WriteLine("Payment accepted. (Dispenses $" + newCashBackRequest + " for cash back.)");
                        }
                        success = true;
                        endProgram = true;
                    }
                    else
                    {
                        Console.WriteLine("ERROR: must type either '1' or '2'.");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("Kiosk Change left:");
                Console.WriteLine(kioskArray[0] + " \t$100 bills,");
                Console.WriteLine(kioskArray[1] + " \t$50 bills,");
                Console.WriteLine(kioskArray[2] + " \t$20 bills,");
                Console.WriteLine(kioskArray[3] + " \t$10 bills,");
                Console.WriteLine(kioskArray[4] + " \t$5 bills,");
                Console.WriteLine(kioskArray[5] + " \t$1 bills,");
                Console.WriteLine(kioskArray[6] + " \tquarters,");
                Console.WriteLine(kioskArray[7] + " \tdimes,");
                Console.WriteLine(kioskArray[8] + " \tnickels,");
                Console.WriteLine(kioskArray[9] + " \tpennies.");
            }
            string[] report = new string[4];
            report[0] = Convert.ToString(Math.Round(cashPaid, 2));                            //Amount of cash paid.
            report[1] = card[2];                                                             //Card vendor.
            report[2] = Convert.ToString(Math.Round(totalCost - cashPaid, 2));               //Amount paid with card.
            report[3] = Convert.ToString(Math.Round(change, 2));                              //Amount of change given.
            if (totalCost - cashPaid == 0)
            {
                report[1] = "N/A";
                report[2] = "N/A";
            }//The array cannot contain any null values.
            if (report[3] == "-0")
            {
                report[3] = "0.00";
            }
            return report;
        }//FOR BOTH.

        static string formatMoneyValue(double rawMoneyValue)
        {
            string analysis = Convert.ToString(Math.Round(rawMoneyValue, 2));
            char[] arr = new char[analysis.Length];
            analysis.CopyTo(0, arr, 0, analysis.Length);
            bool decimalUsed = false;
            int afterDecimalCounter = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (decimalUsed == true)
                {
                    afterDecimalCounter += 1;
                }
                if (arr[i] == '.')
                {
                    decimalUsed = true;
                }
            }
            if (afterDecimalCounter == 0)
            {
                analysis += ".00";
            }
            else if (afterDecimalCounter == 1)
            {
                analysis += "0";
            }
            return analysis;
        }

        static int[] kioskStockAddition(int[] kioskArray, double payment, bool endProgram)
        {
            if (endProgram == false)
            {
                if (payment == 0.01)
                {
                    kioskArray[9] += 1;
                }
                else if (payment == 0.05)
                {
                    kioskArray[8] += 1;
                }
                else if (payment == 0.1)
                {
                    kioskArray[7] += 1;
                }
                else if (payment == 0.25)
                {
                    kioskArray[6] += 1;
                }
                else if (payment == 1)
                {
                    kioskArray[5] += 1;
                }
                else if (payment == 5)
                {
                    kioskArray[4] += 1;
                }
                else if (payment == 10)
                {
                    kioskArray[3] += 1;
                }
                else if (payment == 20)
                {
                    kioskArray[2] += 1;
                }
                else if (payment == 50)
                {
                    kioskArray[1] += 1;
                }
                else if (payment == 100)
                {
                    kioskArray[0] += 1;
                }
            }
            return kioskArray;
        }

        static int[] kioskStockRemaining(int[] kioskArray, double totalCost, double change, bool endProgram)
        {
            if (endProgram == false)
            {
                while (change >= 100 && kioskArray[0] > 0)
                {
                    kioskArray[0] -= 1;
                    change -= 100;
                }
                while (change >= 50 && kioskArray[1] > 0)
                {
                    kioskArray[1] -= 1;
                    change -= 50;
                }
                while (change >= 20 && kioskArray[2] > 0)
                {
                    kioskArray[2] -= 1;
                    change -= 20;
                }
                while (change >= 10 && kioskArray[3] > 0)
                {
                    kioskArray[3] -= 1;
                    change -= 10;
                }
                while (change >= 5 && kioskArray[4] > 0)
                {
                    kioskArray[4] -= 1;
                    change -= 5;
                }
                while (change >= 1 && kioskArray[5] > 0)
                {
                    kioskArray[5] -= 1;
                    change -= 1;
                }
                while (change >= 0.25 && kioskArray[6] > 0)
                {
                    kioskArray[6] -= 1;
                    change -= 0.25;
                }
                while (change >= 0.1 && kioskArray[7] > 0)
                {
                    kioskArray[7] -= 1;
                    change -= 0.1;
                }
                while (change >= 0.05 && kioskArray[8] > 0)
                {
                    kioskArray[8] -= 1;
                    change -= 0.05;
                }
                while (change >= 0.01 && kioskArray[9] > 0)
                {
                    kioskArray[9] -= 1;
                    change -= 0.01;
                }
            }
            return kioskArray;
        }

        static void Main(string[] args)
        {
            DefineGlobalBankStructure kiosk;
            kiosk.Pennies = 5;
            kiosk.Nickels = 5;
            kiosk.Dimes = 5;
            kiosk.Quarters = 5;
            kiosk.Ones = 5;
            kiosk.Fives = 5;
            kiosk.Tens = 5;
            kiosk.Twenties = 5;
            kiosk.Fifties = 5;
            kiosk.Hundreds = 5;
            string method = " ";
            int[] kioskArray = new int[] { kiosk.Hundreds, kiosk.Fifties, kiosk.Twenties, kiosk.Tens, kiosk.Fives, kiosk.Ones, kiosk.Quarters, kiosk.Dimes, kiosk.Nickels, kiosk.Pennies };
            double totalCost = DisplayItemTotal();
            double CashBackRequest = -1;
            double cashPaid = 0;
            bool forceCardMethod = false;
            bool endProgram = false; //This variable will be checked before the contents of most funcitons are ran, to ensure that each function does not continue the program after it should conclude.
            string[] report = paymentMethodDirectory(method, kioskArray, totalCost, CashBackRequest, cashPaid, forceCardMethod, endProgram);
            string generic = report[0] + " " + report[1] + " " + report[2] + " " + report[3];
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "C:\\Users\\MCA C013\\source\\repos\\Kiosk_LogRecorder\\Kiosk_LogRecorder\\bin\\Debug\\net6.0\\Kiosk_LogRecorder.exe";
            startInfo.Arguments = generic;
            Process.Start(startInfo);
        }
    }
}
