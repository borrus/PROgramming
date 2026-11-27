# ProcessingDataForWEB_APP

A C# console application for ETL (Extract, Transform, Load) processing of open IMDb and MovieLens datasets.

## Overview
The program merges data from seven sources through the pipeline A → B → C → D → E → F:

1. Actors/directors names (`ActorsDirectorsNames`).
2. Actors/directors codes (IDs) and movie IDs (`ActorsDirectorsCodes`).
3. IMDb movie titles (`MovieCodes_IMDB`).
4. IMDb movie ratings (`Ratings_IMDB`).
5. IMDb ↔ MovieLens links (`links_IMDB_MovieLens`).
6. MovieLens tag weights per movie (`TagScores_MovieLens`).
7. MovieLens tag dictionary (`TagCodes_MovieLens`).

At each stage the previous result is joined with the next source by the appropriate key:
- `PersonId` — between names and codes;
- `FilmIdIMDB` — between codes, titles, ratings and links;
- MovieLens `FilmId` — between links, tag weights and the tag dictionary.

The output is a flat list of records (`person – role – film – rating – tag`). The current build prints the number of produced records; aggregation into `CreateMovieDictionary` / `CreateActorMovieDictionary` is available in `Dictionary.cs`.

## Tech Stack & Features
- C# / .NET 8
- `Parallel.ForEach` for high-performance processing of large datasets.
- `ConcurrentDictionary` / `ConcurrentBag` for thread-safe joins and aggregation.
- Hash-based joins on `PersonId` and `FilmIdIMDB` (and on MovieLens `FilmId` at the tag stages) — O(n + m) instead of nested loops.
- `StreamReader` for line-by-line reading to optimize memory usage.

## How to Run
From the folder that contains `ProcessingDataForWEB_APP.csproj`, pass all seven file paths as command-line arguments, in this exact order:

1. names
2. codes (participants)
3. titles
4. ratings
5. links (IMDb ↔ MovieLens)
6. tag weights
7. tag dictionary

```bash
dotnet run --project ProcessingDataForWEB_APP.csproj -- \
  "path/to/ActorsDirectorsNames.tsv" \
  "path/to/ActorsDirectorsCodes.tsv" \
  "path/to/MovieCodes_IMDB.tsv" \
  "path/to/Ratings_IMDB.tsv" \
  "path/to/links_IMDB_MovieLens.csv" \
  "path/to/TagScores_MovieLens.csv" \
  "path/to/TagCodes_MovieLens.csv"
```

If fewer or more than seven arguments are passed, or any of the files does not exist, the program reports the problem and exits without processing.

## Expected Output

```text
Обработка завершена!
Количество записей: <N>
```

`N` is the number of rows in the resulting `List<F>`. One movie can appear multiple times — once per (person, tag) combination — so this is the number of records, not the number of distinct movies. To get distinct movies/persons, feed the result into `Dictionary.CreateMovieDictionary` / `Dictionary.CreateActorMovieDictionary`.
