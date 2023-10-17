using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator_ex
{
    public class Shelf
    {

        private static int lastShelfId = 0;
        public int ShelfId { get; }
        public int FloorNumber { get; set; }
        public int SpaceInCm { get; set; }
        public List<Item> Items { get; set; }

 

        public Shelf(int floorNumber, int spaceInCm)
        {
            ShelfId = SetShelfId();
            FloorNumber = floorNumber;
            SpaceInCm = spaceInCm;
            Items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        // Method to generate a unique shelf ID
        private int SetShelfId()
        {
            return ++lastShelfId;
        }

    }
}
