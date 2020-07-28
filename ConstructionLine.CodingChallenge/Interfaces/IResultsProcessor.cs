using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionLine.CodingChallenge.Interfaces
{
    public interface IResultsProcessor
    {
        void ProcessResults(SearchResults result, SearchResults overallResults);

        void AddNotFoundResultEntries(SearchResults results, List<Size> sizes, List<Color> colours);
    }
}
