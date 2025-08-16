using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory_Activity_4
{
    namespace LibraryManagement
    {
        public class LibraryItem
        {
            public string Type { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime ReturnDate { get; set; }

            
            private const double BookRate = 1.0; 
            private const double DvdRate = 1.5;  
            private const double MagRate = 0.5;   

            public LibraryItem(string type, DateTime dueDate, DateTime returnDate)
            {
                Type = type;
                DueDate = dueDate;
                ReturnDate = returnDate;
            }
            public int LateDays()
            {
                if (ReturnDate > DueDate)
                {
                    return (ReturnDate - DueDate).Days;
                }
                return 0; 
            }
            public double Fee()
            {
                double rate = 0;
                switch (Type.ToLower())
                {
                    case "book":
                        rate = BookRate;
                        break;
                    case "dvd":
                        rate = DvdRate;
                        break;
                    case "mag":
                        rate = MagRate;
                        break;
                    default:
                        rate = BookRate; 
                        break;
                }
                return LateDays() * rate;
            }
        }
        class Program
        {
            static DateTime GetValidDate(string prompt)
            {
                DateTime date;
                while (true)
                {
                    Console.Write(prompt);
                    string input = Console.ReadLine();
                    if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                        return date;
                    Console.WriteLine("Invalid date format. Use yyyy-MM-dd.");
                }
            }
            static int GetValidInt(string prompt)
            {
                int value;
                while (true)
                {
                    Console.Write(prompt);
                    if (int.TryParse(Console.ReadLine(), out value) && value > 0)
                        return value;
                    Console.WriteLine("Invalid input. Please enter a positive number.");
                }
            }

            static void Main(string[] args)
            {
                const double MaxTotalCap = 50.0;

                int count = GetValidInt("Enter number of borrowed items: ");
                List<LibraryItem> items = new List<LibraryItem>();

                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"Item {i + 1}");
                    Console.Write("Enter type (book/DVD/mag): ");
                    string type = Console.ReadLine().Trim().ToLower();

                    DateTime due = GetValidDate("Enter due date (yyyy-MM-dd): ");
                    DateTime ret = GetValidDate("Enter return date (yyyy-MM-dd): ");

                    items.Add(new LibraryItem(type, due, ret));
                }

                Console.WriteLine("LATE FEES");
                double totalFees = 0;
                LibraryItem highestItem = null;

                foreach (var item in items)
                {
                    double fee = item.Fee();
                    Console.WriteLine($"{item.Type,-5} | Late Days: {item.LateDays(),2} | Fee: {fee,6:F2}");

                    totalFees += fee;

                    if (highestItem == null || fee > highestItem.Fee())
                        highestItem = item;
                }

                if (totalFees > MaxTotalCap)
                    totalFees = MaxTotalCap; 

                Console.WriteLine($"\nTotal Fees (capped at {MaxTotalCap:F2}): {totalFees:F2}");
                Console.WriteLine($"Highest Offender: {highestItem.Type} with {highestItem.Fee():F2} fee");
            }
        }
    }
}

            