using ConstructionLine.CodingChallenge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructionLine.CodingChallenge
{
    public class ShirtSearch : IShirtSearch
    {
        public SearchResults SearchForShirt(Size size, Color colour, List<Shirt> shirts)
        {
            var matchingShirts = shirts.Where(s => s.Size.Id.Equals(size.Id) && s.Color.Id.Equals(colour.Id));

            var results = new SearchResults()
            {
                Shirts = matchingShirts.ToList(),
                SizeCounts = new List<SizeCount>() { new SizeCount() { Count = matchingShirts.Count(), Size = size } },
                ColorCounts = new List<ColorCount>() { new ColorCount() { Count = matchingShirts.Count(), Color = colour } }
            };

            return results;
        }
    }
}
