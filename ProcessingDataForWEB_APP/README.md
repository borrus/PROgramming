# ProcessingDataForWEB_APP

A C# console application for ETL (Extract, Transform, Load) processing of open IMDb datasets.

## Overview
The program merges data from four different sources:
1. Actors and directors names and professions.
2. Actors and directors codes (IDs) and movie IDs.
3. IMDb movie titles.
4. IMDb movie ratings.

The output is two aggregated dictionaries: one grouped by movies and one grouped by persons (actors/directors).

## Tech Stack & Features
- C# / .NET 8
- `Parallel.ForEach` and PLINQ for high-performance processing of large datasets.
- `ConcurrentDictionary` for thread-safe data aggregation.
- `StreamReader` for line-by-line reading to optimize memory usage.

## How to Run
Pass the file paths as command-line arguments:
`dotnet run -- "path/to/ActorsDirectorsNames.txt" "path/to/ActorsDirectorsCodes.tsv" "path/to/MovieCodes_IMDB.tsv" "path/to/Ratings_IMDB.tsv"`
