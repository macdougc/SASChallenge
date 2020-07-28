using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionLine.CodingChallenge.Interfaces
{
    public interface IShirtSearch
    {
        SearchResults SearchForShirt(Size size, Color colour, List<Shirt> shirts);
    }
}
