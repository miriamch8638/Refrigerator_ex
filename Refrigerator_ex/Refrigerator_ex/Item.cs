using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator_ex
{
    public enum KosherType
    {
        Meat,
        Dairy,
        Parve
    }
    public enum ItemType
    {
        Food,
        Drink
    }
    public class Item
    {
        private static int lastItemfId = 0;
        private string productName;
        private int shelfId;
        private ItemType type;
        private KosherType kosher;
        private DateTime expiryDate;
        private int spaceInCm;

        public int ItemId { get; }
        public string ProductName
        {
            get { return productName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    productName = value;
                }
                else
                {
                    Console.WriteLine("Product Name cannot be null or empty.");
                }
            }
        }
        public int ShelfId
        {
            get { return shelfId; }
            set { shelfId = value; }
        }
        public ItemType Type
        {
            get { return type; }
            set
            {
                if (value == ItemType.Food || value == ItemType.Drink)
                {
                    type = value;
                }
                else
                {
                    Console.WriteLine("Invalid item type.");
                }
            }
        }
        public KosherType Kosher
        {
            get { return kosher; }
            set
            {
                if (value == KosherType.Parve || value == KosherType.Meat || value == KosherType.Dairy)
                {
                    kosher = value;
                }
                else
                {
                    Console.WriteLine("Invalid Kosher type.");
                }
            }
        }
        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set
            {
                if (value >= DateTime.Now)
                {
                    expiryDate = value;
                }
                else
                {
                    Console.WriteLine("Expiry date should be in the future.");
                }
            }
        }
        public int SpaceInCm
        {
            get { return spaceInCm; }
            set
            {
                if (value > 0 && value <= 20)
                {
                    spaceInCm = value;
                }

            }
        }

        public Item(string productName, ItemType type, KosherType kosher, DateTime expiryDate, int spaceInCm)
        {
            ItemId = SetItemId();
            ProductName = productName;
            ShelfId = 0;
            Type = type;
            Kosher = kosher;
            ExpiryDate = expiryDate;
            SpaceInCm = spaceInCm;
        }

        private int SetItemId()
        {
            return ++lastItemfId;
        }
        public override string ToString()
        {
            return ("the prouduct name is: " + ProductName + "the number id is: " + ItemId + " is locating in shelf: " + "it is take: " + SpaceInCm + " cm" + (ShelfId + 1) + "the type is: " +
               Type + "the kosher is: " + Kosher + "the ex. date is: " + ExpiryDate + "\n");
        }
    }
}
