using System.Diagnostics;

//fetch values from "KioskPayment"
string paymentCashAmount = "";
string cardVendor = "";
string paymentCardAmount = "";
string changeGiven = "";
for (int i = 0; i < Environment.GetCommandLineArgs().Length; i++)
{
    if (i == 1)
    {
        paymentCashAmount = Environment.GetCommandLineArgs()[i];
    }
    else if (i == 2)
    {
        cardVendor = Environment.GetCommandLineArgs()[i];
    }
    else if (i == 3)
    {
        paymentCardAmount = Environment.GetCommandLineArgs()[i];
    }
    else if (i == 4)
    {
        changeGiven = Environment.GetCommandLineArgs()[i];
    }
}

//generate tranaction number/get date/time
Random rnd = new Random();
int transactionNumber = rnd.Next(99999999, 999999999);
string transactionDate = DateTime.Now.ToString("MM-dd-yy").Replace("/", "-");
char[] arr = new char[transactionDate.Length];
transactionDate.CopyTo(0, arr, 0, transactionDate.Length);
string transactionTime = DateTime.Now.ToShortTimeString();

//generate text for log file (refer to chapter 7 in textbook for help.)
string userName = Environment.UserName;
string path = "C:\\Users\\" + userName + "\\Desktop\\" + arr[0] + arr[1] + arr[2] + arr[3] + arr[4] + arr[5] + arr[6] + arr[7] + ".txt";//MAKE SURE YOUR USERNAME IS USED.
string data = "\r\n\tTransaction Number: " + transactionNumber;
data += "\r\n\tDate: " + transactionDate;
data += "\r\n\tTime: " + transactionTime;
if (cardVendor != "N/A")
{
    data += "\r\n\tCard vendor: " + cardVendor;
    data += "\r\n\tPayments with card: " + paymentCardAmount;
}
if (paymentCashAmount != "0")
{
    data += "\r\n\tPayments in cash: " + paymentCashAmount;
    data += "\r\n\tChange/cashback given: " + changeGiven;
}
data += "\r\n\t ";

//write text to log file
if (File.Exists(path))
{
    Console.WriteLine("The file exists. Adding text to file...");
    File.AppendAllText(path, data);
}
else
{
    try
    {
        Console.WriteLine("The file doesn't exist. Attempting to create the file...");
        File.WriteAllText(path, data);
    }
    catch (Exception error)
    {
        Console.WriteLine(error.Message);
    }
}
