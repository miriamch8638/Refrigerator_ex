using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator_ex
{
    public class Refrigerator
    {
        private static int lastRefrigeratorId = 0;

        public int RefrigeratorId { get; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int NumberOfShelves { get; set; }
        public List<Shelf> Shelves { get; set; }

        public Refrigerator(string model, string color, int numberOfShelves)
        {
            RefrigeratorId = ++lastRefrigeratorId;
            Model = model;
            Color = color;
            NumberOfShelves = numberOfShelves;
            Shelves = new List<Shelf>();
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

        //foreach (Shelf shelf in Shelves)
        //{
        //    if (shelf.CurrentSpace >= item.SpaceInCm)
        //    {
        //        shelf.Items.Add(item);
        //        shelf.CurrentSpace -= item.SpaceInCm;
        //        item.ShelfId = shelf.ShelfId;
        //        break;
        //    }
        //}
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
            }catch(Exception ex) {
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
    }
}

