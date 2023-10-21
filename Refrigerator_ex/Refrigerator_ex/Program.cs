using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator_ex
{
    internal class Program
    {
        public static List<Item> SortItemsByDate(Refrigerator r)
        {

            List<Item> SortedItems = new List<Item>();
            foreach (Shelf s in r.Shelves)
            {
                foreach (Item item in s.Items)
                {
                    SortedItems.Add(item);
                }
            }
            SortedItems = SortedItems.OrderBy(x => x.ExpiryDate).ToList();
            return SortedItems;

        }

        public static List<Shelf> SortShelvesBySpace(List<Shelf> ListShelves)
        {

            List<Shelf> SortedShelves = new List<Shelf>();
            foreach (Shelf s in ListShelves)
            {
                SortedShelves.Add(s);
            }
            SortedShelves = SortedShelves.OrderByDescending(x => x.CurrentSpace).ToList();
            return SortedShelves;
        }
        public static List<Refrigerator> SortRefrigeratorsBySpace(List<Refrigerator> r)
        {

            Dictionary<Refrigerator, int> refrigeratorSpaces = new Dictionary<Refrigerator, int>();

            foreach (var refrigerator in r)
            {
                int totalSpace = refrigerator.Shelves.Sum(shelf => shelf.CurrentSpace);
                refrigeratorSpaces.Add(refrigerator, totalSpace);
            }

            List<Refrigerator> SortedRefig = r.OrderByDescending(refrigerator => refrigeratorSpaces[refrigerator]).ToList();
            return SortedRefig;
        }
        public static Item CreateItemFromUserInput()
        {
            Console.WriteLine("Enter product name:");
            string productName = Console.ReadLine();

            Console.WriteLine("Enter item type (Food or Drink):");
            string itemTypeInput = Console.ReadLine();
            ItemType itemType = ItemType.Food; // Default to Food
            if (Enum.TryParse(itemTypeInput, true, out ItemType parsedType))
            {
                itemType = parsedType;
            }

            Console.WriteLine("Enter Kosher type (Meat, Dairy, or Parve):");
            string kosherTypeInput = Console.ReadLine();
            KosherType kosherType = KosherType.Parve; // Default to Parve
            if (Enum.TryParse(kosherTypeInput, true, out KosherType parsedKosher))
            {
                kosherType = parsedKosher;
            }

            Console.WriteLine("Enter expiry date (yyyy-MM-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime expiryDate) && expiryDate >= DateTime.Now)
            {
                Console.WriteLine("Enter space in centimeters (1-20):");
                if (int.TryParse(Console.ReadLine(), out int spaceInCm) && spaceInCm > 0 && spaceInCm <= 20)
                {
                    // All input is valid, create and return the Item
                    return new Item(productName, itemType, kosherType, expiryDate, spaceInCm);
                }
                else
                {
                    Console.WriteLine("Invalid space in centimeters. Item creation failed.");
                }
            }

            return null;
        }
        public static void oparateChooseFood(Refrigerator r)
        {
            List<Item> items = new List<Item>();
            Console.WriteLine("enter the type of food that you want to eat");
            Console.WriteLine("Enter item type (Food or Drink):");
            string itemTypeInput = Console.ReadLine();
            ItemType itemType = ItemType.Food;
            if (Enum.TryParse(itemTypeInput, true, out ItemType parsedType))
            {
                itemType = parsedType;
            }
            else
            {
                Console.WriteLine("enter correct type");
            }
            Console.WriteLine("Enter Kosher type (Meat, Dairy, or Parve):");
            string kosherTypeInput = Console.ReadLine();
            KosherType kosherType = KosherType.Parve;
            if (Enum.TryParse(kosherTypeInput, true, out KosherType parsedKosher))
            {
                kosherType = parsedKosher;
            }
            else
            {
                Console.WriteLine("enter correct kosher");
            }
            items = r.ChooseFood(kosherType, itemType);
            foreach (var item in items)
            {
                item.ToString();
                Console.WriteLine("\n");
            }
        }
        public static void OptionsOperating(int choice, ref bool isRun, Refrigerator r, List<Refrigerator> refrigeratorsList)
        {
            switch (choice)
            {
                case 1:
                    if (r.Shelves != null)
                    { r.ToString(); }
                    else
                        Console.WriteLine("the refrigerator is empty");
                    break;
                case 2:
                    Console.WriteLine(r.AvilableSpace().ToString());
                    break;
                case 3:
                    Item item = CreateItemFromUserInput();
                    if (item != null)
                    {
                        r.EnterItem(item);
                        Console.WriteLine("was success");
                    }
                    else
                    {
                        Console.WriteLine("was failed");
                    }
                    break;
                case 4:
                    Console.WriteLine("enter the number of item id");
                    int Id = int.Parse(Console.ReadLine());
                    r.RemoveItem(Id);
                    break;
                case 5:
                    r.CleanRefrig();
                    r.ToString();
                    break;
                case 6:
                    oparateChooseFood(r);
                    break;
                case 7:
                    SortItemsByDate(r);
                    break;
                case 8:
                    SortShelvesBySpace(r.Shelves);
                    break;
                case 9:
                    SortRefrigeratorsBySpace(refrigeratorsList);
                    break;
                case 10:
                    r.ReadyToShopping();
                    break;
                case 100:
                    isRun = false;
                    return;

            }
        }
        static void Main(string[] args)
        {

            Refrigerator r1 = new Refrigerator("Midea", "White", 7);
            Shelf s1 = new Shelf(1);
            Shelf s2 = new Shelf(2);
            Shelf s3 = new Shelf(3);
            Shelf s4 = new Shelf(4);
            Shelf s5 = new Shelf(5);
            Shelf s6 = new Shelf(6);
            Shelf s7 = new Shelf(7);
            r1.AddShelf(s1);
            r1.AddShelf(s2);
            r1.AddShelf(s3);
            r1.AddShelf(s4);
            r1.AddShelf(s5);
            r1.AddShelf(s6);
            r1.AddShelf(s7);

            Item milk = new Item("milk", ItemType.Drink, KosherType.Dairy, new DateTime(2023, 10, 28), 2);
            Item kola = new Item("koka-kola", ItemType.Drink, KosherType.Parve, new DateTime(2023, 10, 19), 2);
            Item juice = new Item("Apple-juice", ItemType.Drink, KosherType.Parve, new DateTime(2023, 10, 30), 2);
            //Console.WriteLine(r1.AvilableSpace().ToString());
            //Console.WriteLine(r1);
            r1.EnterItem(milk);
            r1.EnterItem(kola);
            r1.EnterItem(juice);
            //Console.WriteLine(r1.AvilableSpace());
            //Console.WriteLine(r1.ToString());
            List<Item> list1 = new List<Item>();
            //7
            //list1 = r1.ChooseFood(KosherType.Parve, ItemType.Drink);
            //foreach (Item item in list1)
            //     Console.WriteLine(item);
            //8
            List<Item> list2 = new List<Item>();


            //list2 = SortItemsByDate(r1);
            //foreach (Item item in list2)
            //    Console.WriteLine(item);

            //r1.RemoveItem(1 );
            //Console.WriteLine(r1.ToString());
            //Console.WriteLine(r1.AvilableSpace());

            Refrigerator r2 = new Refrigerator("LG", "White", 6);
            Shelf s8 = new Shelf(1);
            Shelf s9 = new Shelf(2);
            Shelf s10 = new Shelf(3);
            Shelf s11 = new Shelf(4);
            Shelf s12 = new Shelf(5);
            Shelf s13 = new Shelf(6);
            r2.AddShelf(s8);
            r2.AddShelf(s9);
            r2.AddShelf(s10);
            r2.AddShelf(s11);
            r2.AddShelf(s12);
            r2.AddShelf(s13);

            //Console.WriteLine(r2);
            //8.3
            Item milk1 = new Item("milk", ItemType.Drink, KosherType.Dairy, new DateTime(2023, 10, 28), 4);
            Item kola1 = new Item("koka-kola", ItemType.Drink, KosherType.Parve, new DateTime(2023, 10, 19), 4);
            Item juice1 = new Item("Apple-juice", ItemType.Drink, KosherType.Parve, new DateTime(2023, 10, 30), 4);
            r2.EnterItem(milk1);
            r2.EnterItem(kola1);
            r2.EnterItem(juice1);

            List<Refrigerator> listRefrij = new List<Refrigerator>();

            listRefrij.Add(r1);
            listRefrij.Add(r2);
            // listRefrij= SortRefrigeratorsBySpace(listRefrij);
            // foreach (var item in listRefrij)
            // {
            //     Console.WriteLine(item);
            // }

            //*****************************************************************************
            //The Console
            int choice;
            bool isRun = true;
            while (isRun==true)
            {
                Console.WriteLine(" Press 1: the program will print all the items on the fridge and all its contents.\r\n" +
            "Press 2: the program will print how much space is left in the refrigerator" +
            "\r\nClick 3: the program will allow the user to enter the refrigerator.\r\n" +
            "Click 4: the program will allow the user to remove the item from the refrigerator.\r\n" +
            "Click 5: the program will clean your refrigerator and print to the user all the checked items.\r\n" +
            "Click 6: the program will ask the user \"what do you want to eat\"? and will bring you the function to bring a product.\r\nClick 7: the program will print all the products sorted by their expiration date.\r\n" +
            "Click 8: the program will print all the shelves arranged according to the free space left by them.\r\nClick 9: the program will print all the refrigerators arranged according to the free space left in them.\r\nClick 10: the program will prepare your refrigerator for shopping\r\n" +
            "Press 100: close the system.\n");
                Console.WriteLine("enter the number of choice");
                choice = int.Parse(Console.ReadLine());
                if (choice >= 1 && choice <= 100)
                {
                    OptionsOperating(choice, ref isRun, r1, listRefrij);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer between 1 and 100.");
                }
            }
            Console.ReadLine();
        }
    }
}
