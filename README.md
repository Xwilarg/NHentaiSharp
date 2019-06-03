[![Build status](https://ci.appveyor.com/api/projects/status/ncmwvujaxi3te108/branch/master?svg=true)](https://ci.appveyor.com/project/Xwilarg/nhentaisharp/branch/master)
[![NuGet](https://img.shields.io/nuget/v/NHentaiSharp.svg)](https://www.nuget.org/packages/NHentaiSharp/)
[![codecov](https://codecov.io/gh/Xwilarg/NHentaiSharp/branch/master/graph/badge.svg)](https://codecov.io/gh/Xwilarg/NHentaiSharp)
[![Unit tests](https://img.shields.io/appveyor/tests/xwilarg/nhentaisharp.svg)](https://ci.appveyor.com/project/Xwilarg/nhentaisharp/branch/master/tests)
[![CodeFactor](https://www.codefactor.io/repository/github/xwilarg/nhentaisharp/badge)](https://www.codefactor.io/repository/github/xwilarg/nhentaisharp)

## NHentaiSharp
NHentaiSharp is a C# library to use NHentai.net API.

### How to download it?
You can download it from [NuGet](https://www.nuget.org/packages/NHentaiSharp) with the following command line:
```
Install-Package NHentaiSharp
```

### Example: Getting a random doujinshi
```Cs
string[] tags = new[] { // Tags for the upcoming searches
  "loli", // We add the tag loli
  NHentaiSharp.Core.SearchClient.GetExcludeTag("rape"), // We exclude the tag rape
  NHentaiSharp.Core.SearchClient.GetCategoryTag("kantai collection", NHentaiSharp.Search.TagType.Parody)
  // We search doujinshi of the anime/game Kantai Collection
};

Random r = new Random();
// We do a search with the tags
var result = await NHentaiSharp.Core.SearchClient.SearchWithTagsAsync(tags);
int page = r.Next(0, result.numPages) + 1; // Page count begin at 1
// We do a new search at a random page
result = await NHentaiSharp.Core.SearchClient.SearchWithTagsAsync(tags, page);
var doujinshi = result.elements[r.Next(0, result.elements.Length)]; // We get a random doujinshi

Console.WriteLine("Random doujinshi: " + doujinshi.prettyTitle + Environment.NewLine +
  "URL: " + doujinshi.url);
```

For more information about using this library, please check the [wiki](https://github.com/Xwilarg/NHentaiSharp/wiki).
