using ConstructionLine.CodingChallenge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructionLine.CodingChallenge
{
    public class ResultsProcessor : IResultsProcessor
    {
        public void AddNotFoundResultEntries(SearchResults results)
        {
            // Add all the count entries for sizes and colours not found
            foreach(var size in Size.All)
            {
                // If we don't have an entry for this size then add one with 0 count
                if (!results.SizeCounts.Where(sc => sc.Size.Id.Equals(size.Id)).Any())
                {
                    results.SizeCounts.Add(new SizeCount() { Size = size, Count = 0 });
                }
            }

            foreach (var colour in Color.All)
            {
                // If we don't have an entry for this size then add one with 0 count
                if (!results.ColorCounts.Where(sc => sc.Color.Id.Equals(colour.Id)).Any())
                {
                    results.ColorCounts.Add(new ColorCount() { Color = colour, Count = 0 });
                }
            }
        }

        public void ProcessResults(SearchResults result, SearchResults overallResults)
        {
            // Add the results to the overall results
            overallResults.Shirts.AddRange(result.Shirts);

            var sizeCount = result.SizeCounts.First();
            var colourCount = result.ColorCounts.First();

            // Search if the size already has a count and process accordingly
            if (overallResults.SizeCounts.Where(sc => sc.Size.Id.Equals(sizeCount.Size.Id)).Any())
            {
                overallResults.SizeCounts.First(sc => sc.Size.Id.Equals(sizeCount.Size.Id)).Count += sizeCount.Count;
            }
            else
            {
                overallResults.SizeCounts.Add(sizeCount);
            }

            // Search if the colour already has a count and process accordingly
            if (overallResults.ColorCounts.Where(sc => sc.Color.Id.Equals(colourCount.Color.Id)).Any())
            {
                overallResults.ColorCounts.First(sc => sc.Color.Id.Equals(colourCount.Color.Id)).Count += colourCount.Count;
            }
            else
            {
                overallResults.ColorCounts.Add(colourCount);
            }
        }
    }
}
