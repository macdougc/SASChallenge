using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class ShirtSearchTests
    {
        private readonly ShirtSearch _shirtSearch = new ShirtSearch();

        [Test]
        public void TestNoShirts()
        {
            var shirts = new List<Shirt>();
            var colour = Color.All.First();
            var size = Size.All.First();

            var results = _shirtSearch.SearchForShirt(size, colour, shirts);

            results.Shirts.Should().BeEmpty();
            results.SizeCounts.First(s => s.Size.Id.Equals(size.Id)).Count.Should().Be(0);
            results.ColorCounts.First(s => s.Color.Id.Equals(colour.Id)).Count.Should().Be(0);
        }

        [Test]
        public void TestShirts()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var colour = Color.Black;
            var size = Size.Medium;

            var results = _shirtSearch.SearchForShirt(size, colour, shirts);

            results.Shirts.Count.Should().Be(1);
            results.SizeCounts.First(s => s.Size.Id.Equals(size.Id)).Count.Should().Be(1);
            results.ColorCounts.First(s => s.Color.Id.Equals(colour.Id)).Count.Should().Be(1);
        }
    }
}
