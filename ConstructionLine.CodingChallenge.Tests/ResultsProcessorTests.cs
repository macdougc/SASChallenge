using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class ResultsProcessorTests
    {
        private readonly ResultsProcessor _resultsProcessor = new ResultsProcessor();

        [Test]
        public void TestAddNotFoundResultEntries_EmptyResults()
        {
            var results = new SearchResults()
            {
                SizeCounts = new List<SizeCount>(),
                ColorCounts = new List<ColorCount>(),
                Shirts = new List<Shirt>()
            };

            _resultsProcessor.AddNotFoundResultEntries(results);

            results.Shirts.Should().BeEmpty();

            foreach(var size in Size.All)
            {
                results.SizeCounts.First(s => s.Size.Id.Equals(size.Id)).Count.Should().Be(0);
            }

            foreach (var colour in Color.All)
            {
                results.ColorCounts.First(s => s.Color.Id.Equals(colour.Id)).Count.Should().Be(0);
            }
        }

        [Test]
        public void TestAddNotFoundResultEntries()
        {
            var results = new SearchResults()
            {
                SizeCounts = new List<SizeCount>() { new SizeCount() { Size = Size.Small, Count = 2 } },
                ColorCounts = new List<ColorCount>() { new ColorCount() { Color = Color.Red, Count = 1 } },
                Shirts = new List<Shirt>()
            };

            _resultsProcessor.AddNotFoundResultEntries(results);

            results.SizeCounts.First(s => s.Size.Id.Equals(Size.Small.Id)).Count.Should().Be(2);
            results.SizeCounts.First(s => s.Size.Id.Equals(Size.Medium.Id)).Count.Should().Be(0);
            results.SizeCounts.First(s => s.Size.Id.Equals(Size.Large.Id)).Count.Should().Be(0);

            results.ColorCounts.First(s => s.Color.Id.Equals(Color.Red.Id)).Count.Should().Be(1);
            results.ColorCounts.First(s => s.Color.Id.Equals(Color.Black.Id)).Count.Should().Be(0);
            results.ColorCounts.First(s => s.Color.Id.Equals(Color.Blue.Id)).Count.Should().Be(0);
            results.ColorCounts.First(s => s.Color.Id.Equals(Color.Yellow.Id)).Count.Should().Be(0);
            results.ColorCounts.First(s => s.Color.Id.Equals(Color.White.Id)).Count.Should().Be(0);
        }

        [Test]
        public void TestProcessResults_AddingToExisting()
        {
            var overallResults = new SearchResults()
            {
                SizeCounts = new List<SizeCount>() { new SizeCount() { Size = Size.Small, Count = 2 } },
                ColorCounts = new List<ColorCount>() { new ColorCount() { Color = Color.Red, Count = 1 } },
                Shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                    new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                }
        };

            var result = new SearchResults()
            {
                SizeCounts = new List<SizeCount>() { new SizeCount() { Size = Size.Small, Count = 1 } },
                ColorCounts = new List<ColorCount>() { new ColorCount() { Color = Color.Red, Count = 1 } },
                Shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black)
                }
            };

            _resultsProcessor.ProcessResults(result, overallResults);

            overallResults.Shirts.Count.Should().Be(4);
            overallResults.SizeCounts.First(s => s.Size.Id.Equals(Size.Small.Id)).Count.Should().Be(3);
            overallResults.ColorCounts.First(s => s.Color.Id.Equals(Color.Red.Id)).Count.Should().Be(2);
        }

        [Test]
        public void TestProcessResults_NewEntry()
        {
            var overallResults = new SearchResults()
            {
                SizeCounts = new List<SizeCount>() { new SizeCount() { Size = Size.Small, Count = 2 } },
                ColorCounts = new List<ColorCount>() { new ColorCount() { Color = Color.Red, Count = 1 } },
                Shirts = new List<Shirt>()
            };

            var result = new SearchResults()
            {
                SizeCounts = new List<SizeCount>() { new SizeCount() { Size = Size.Medium, Count = 1 } },
                ColorCounts = new List<ColorCount>() { new ColorCount() { Color = Color.Blue, Count = 1 } },
                Shirts = new List<Shirt>()
            };

            _resultsProcessor.ProcessResults(result, overallResults);

            overallResults.SizeCounts.First(s => s.Size.Id.Equals(Size.Small.Id)).Count.Should().Be(2);
            overallResults.SizeCounts.First(s => s.Size.Id.Equals(Size.Medium.Id)).Count.Should().Be(1);
            overallResults.ColorCounts.First(s => s.Color.Id.Equals(Color.Red.Id)).Count.Should().Be(1);
            overallResults.ColorCounts.First(s => s.Color.Id.Equals(Color.Blue.Id)).Count.Should().Be(1);
        }
    }
}
