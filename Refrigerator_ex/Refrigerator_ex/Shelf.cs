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
        private int spaceInCm;
        private List<Item> items;
        private int currentSpace;
        public int ShelfId { get; }
        public int FloorNumber { get; set; }

        public int SpaceInCm
        {
            get { return spaceInCm; }
            set
            {
                if (value > 0)
                {
                    spaceInCm = value;
                }
                else
                {
                    Console.WriteLine("Space in centimeters should be greater than 0.");
                }
            }
        }

        public List<Item> Items
        {
            get { return items; }
            set { items = value; }
        }

        public int CurrentSpace
        {
            get { return currentSpace; }
            set { currentSpace = value; }
        }

        public Shelf(int floorNumber)
        {
            ShelfId = SetShelfId();
            FloorNumber = floorNumber;
            SpaceInCm = 20;
            Items = new List<Item>();
            CurrentSpace = spaceInCm;
        }
        public void AddItem(Item item)
        {
            Items.Add(item);
        }
        private int SetShelfId()
        {
            return ++lastShelfId;
        }

        public override string ToString()
        {
            return ("the shelf no." + (ShelfId) + " is locating in floor no." + FloorNumber +
                " it contains the items : \n" + string.Join(", ", Items));
        }


    }

}
