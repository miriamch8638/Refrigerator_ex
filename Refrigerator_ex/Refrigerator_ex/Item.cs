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
        public int ItemId { get; }
        public string ProductName { get; set; }
        public int ShelfId { get; set; }
        public ItemType Type { get; set; }
        public KosherType Kosher { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int SpaceInCm { get; set; }

        public Item(string productName, int shelfId, ItemType type, KosherType kosher, DateTime expiryDate, int spaceInCm)
        {
            ItemId = SetItemId();
            ProductName = productName;
            ShelfId = shelfId;
            Type = type;
            Kosher = kosher;
            ExpiryDate = expiryDate;
            SpaceInCm = spaceInCm;
        }
        private int SetItemId()
        {
            return ++lastItemfId;
        }
    }


}
