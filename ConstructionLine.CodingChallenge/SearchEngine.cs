using ConstructionLine.CodingChallenge.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly IShirtSearch _shirtSearch;
        private readonly IResultsProcessor _resultsProcessor;

        public SearchEngine(List<Shirt> shirts, IShirtSearch shirtSearch, IResultsProcessor resultsProcessor)
        {
            _shirts = shirts;
            _shirtSearch = shirtSearch ?? throw new System.ArgumentNullException(nameof(shirtSearch));
            _resultsProcessor = resultsProcessor ?? throw new System.ArgumentNullException(nameof(resultsProcessor));

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.
        }


        public SearchResults Search(SearchOptions options)
        {
            // TODO: search logic goes here.
            var searchResults = new SearchResults()
            {
                SizeCounts = new List<SizeCount>(),
                ColorCounts = new List<ColorCount>(),
                Shirts = new List<Shirt>()
            };

            // If we have no search items then return the results with all 0
            if (options.Colors.Count == 0 && options.Sizes.Count == 0)
            {
                _resultsProcessor.AddNotFoundResultEntries(searchResults);
                return searchResults;
            }

            // If there is no colour or size specified then we need to search them all, e.g. "Red" but no size means all reds.
            var colours = options.Colors.Count > 0 ? options.Colors : Color.All;
            var sizes = options.Sizes.Count > 0 ? options.Sizes : Size.All;

            foreach (var colour in colours)
            {
                foreach(var size in sizes)
                {
                    // Search for the size, colour combo and add the results to the overall results, if we have any
                    var result = _shirtSearch.SearchForShirt(size, colour, _shirts);

                    if (result != null)
                    {
                        _resultsProcessor.ProcessResults(result, searchResults);
                    }                 
                }
            }

            _resultsProcessor.AddNotFoundResultEntries(searchResults);

            return searchResults;
        }
    }
}