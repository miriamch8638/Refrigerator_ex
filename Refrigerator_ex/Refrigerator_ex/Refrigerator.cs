using System;
using System.Collections.Generic;
using System.Linq;
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
            Shelves.Add(shelf);
        }
    }
}

