using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator_ex
{
    public class Refrigerator
    {
        private static int lastRefrigeratorId = 0;
        private string model;
        private string color;
        private int numberOfShelves;
        private List<Shelf> shelves;

        public int RefrigeratorId { get; }

        public string Model
        {
            get { return model; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    model = value;
                }
                else
                {
                    Console.WriteLine("Model cannot be null or empty.");
                }
            }
        }

        public string Color
        {
            get { return color; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    color = value;
                }
                else
                {
                    Console.WriteLine("Color cannot be null or empty.");
                }
            }
        }

        public int NumberOfShelves
        {
            get { return numberOfShelves; }
            set
            {
                if (value >= 0 && value <= 10)
                {
                    numberOfShelves = value;
                }
                else
                {
                    Console.WriteLine("Number of shelves cannot be negative or more then 10.");
                }
            }
        }

        public List<Shelf> Shelves
        {
            get { return shelves; }
            set { shelves = value; }
        }

        public Refrigerator(string model, string color, int numberOfShelves)
        {
            RefrigeratorId = SetRefrigeratorId();
            Model = model;
            Color = color;
            NumberOfShelves = numberOfShelves;
            Shelves = new List<Shelf>();
        }
        private int SetRefrigeratorId()
        {
            return ++lastRefrigeratorId;
        }

        public void AddShelf(Shelf shelf)
        {
            if (Shelves.Count() <= NumberOfShelves)
                Shelves.Add(shelf);
            else
                Console.WriteLine("there is no place in the refrigerator to the new shelf ");
        }
        public override string ToString()
        {
            return ("thr Refrigerator id no. " + RefrigeratorId + "  of model " + Model + "  it's color is " + Color
                + "  the number of shelfs is: " + NumberOfShelves + "  the shelves are: \n" + string.Join(", ", Shelves));
        }
        public int AvilableSpace()
        {
            int space = 0;
            foreach (Shelf shelf in Shelves)
            {
                space += shelf.CurrentSpace;
            }
            return space;
        }
        public void EnterItem(Item item)
        {
            int avilableSpace = AvilableSpace();
            if (avilableSpace != null && avilableSpace >= item.SpaceInCm)
            {

                for (int i = 0; i < Shelves.Count(); i++)
                {
                    if (Shelves[i].CurrentSpace >= item.SpaceInCm)
                    {
                        Shelves[i].Items.Add(item);
                        Shelves[i].CurrentSpace -= item.SpaceInCm;
                        item.ShelfId = Shelves[i].ShelfId;
                        break;
                    }
                }
            }
            else
                Console.WriteLine("there is no place to enter the item");
        }
        public void RemoveItem(int itemID)
        {
            Item itemToRemove;
            try
            {
                for (int i = 0; i < Shelves.Count(); i++)
                {
                    itemToRemove = Shelves[i].Items.Find(x => x.ItemId == itemID);
                    if (itemToRemove != null)
                    {
                        Shelves[i].Items.Remove(itemToRemove);
                        //Shelves.RemoveAt(i);
                        Shelves[i].CurrentSpace += itemToRemove.SpaceInCm;
                        Console.WriteLine("the item " + itemToRemove.ToString() + "was removed ");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("the item was not found");
            }

        }
        public void CleanRefrig()
        {
            foreach (Shelf shelf in Shelves)
            {
                foreach (Item item in shelf.Items)
                {
                    if (item.ExpiryDate > DateTime.Today)
                        RemoveItem(item.ItemId);
                }
            }
        }
        public List<Item> ChooseFood(KosherType kosher, ItemType type)
        {
            List<Item> FoodToEat = new List<Item>();
            foreach (Shelf shelf in Shelves)
            {
                foreach (Item item in shelf.Items)
                {
                    if (item.Type == type && item.Kosher == kosher && item.ExpiryDate >= DateTime.Today)
                    {
                        FoodToEat.Add(item);
                    }
                }
            }

            //to print in main massge if the list is empty
            return FoodToEat;
        }
        public bool Check()
        {
            int avilableSpace = AvilableSpace();
            if (avilableSpace >= 20)
                return true;
            return false;
        }

        public void ThrewDiary(out List<Item> items)
        {
            items = new List<Item>();
            DateTime currentDate = DateTime.Now;
            TimeSpan moreDays = TimeSpan.FromDays(3);
            foreach (Shelf shelf in Shelves)
            {
                foreach (Item item in shelf.Items)
                {
                    if (item.Kosher == KosherType.Dairy && item.ExpiryDate <= currentDate + moreDays)
                    {
                        items.Add(item);
                        RemoveItem(item.ItemId);
                    }
                }
            }
        }
        public void ThrewMeat(out List<Item> items)
        {
            items = new List<Item>();
            DateTime currentDate = DateTime.Now;
            TimeSpan moreDays = TimeSpan.FromDays(7);
            moreDays = TimeSpan.FromDays(7);
            foreach (Shelf shelf in Shelves)
            {
                foreach (Item item in shelf.Items)
                {
                    if (item.Kosher == KosherType.Meat && item.ExpiryDate <= currentDate + moreDays)
                    {
                        items.Add(item);
                        RemoveItem(item.ItemId);
                    }
                }
            }
        }
        public void ThrewParve(out List<Item> items)
        {
            items = new List<Item>();
            DateTime currentDate = DateTime.Now;
            TimeSpan moreDays = TimeSpan.FromDays(1);
            moreDays = TimeSpan.FromDays(7);
            foreach (Shelf shelf in Shelves)
            {
                foreach (Item item in shelf.Items)
                {
                    if (item.Kosher == KosherType.Parve && item.ExpiryDate <= currentDate + moreDays)
                    {
                        items.Add(item);
                        RemoveItem(item.ItemId);
                    }
                }
            }
        }

        public void ReadyToShopping()
        {
            List<Item> items1 = new List<Item>();
            List<Item> items2 = new List<Item>();
            List<Item> items3 = new List<Item>();
            bool isFree = Check();
            if (isFree == true)
                Console.WriteLine("you can do shopping");

            else
            {
                CleanRefrig();
                isFree = Check();
                if (isFree == true)
                    Console.WriteLine("you can do shopping");
                else
                {
                    ThrewDiary(out items1);
                    isFree = Check();
                    if (isFree == true)
                        Console.WriteLine("you can do shopping Because " +
                            "we threw out all the dairy products that are valid for less than 3 days ");
                    else
                    {
                        ThrewMeat(out items2);
                        isFree = Check();
                        if (isFree == true)
                            Console.WriteLine("you can do shopping Because" +
                                 " we threw out  all the dairy products that are valid " +
                                 " less than 3 days and all the " +
                                 " meat products that are valid for less than 7 days ");
                        else
                        {
                            ThrewParve(out items3);
                            isFree = Check();
                            if (isFree == true)
                                Console.WriteLine("you can do shopping Because " +
                                       " we threw out  all the dairy products that are valid " +
                                       " less than 3 days and all the " +
                                       " meat products that are valid for less than 7 days " +
                                       " and all the parve products that are valid for less than 1 days ");
                            else
                            {
                                items1.AddRange(items2);
                                items2.AddRange(items3);
                                foreach (Item item in items1)
                                {
                                    EnterItem(item);
                                }
                                Console.WriteLine(" you cann't do shopping now becouse there is no space!");
                            }
                        }
                    }
                }
            }
        }
    }
}
