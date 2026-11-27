using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingDataForWEB_APP
{
    public class Movie
    {
        public string FilmName { get; set; }
        public HashSet<string> Actors { get; set; } = new HashSet<string>();
        public string Director { get; set; }
        public HashSet<string> Tags { get; set; } = new HashSet<string>();

        public double Rating { get; set; }

        public Movie()
        {
        }

        public Movie(string filmName, HashSet<string> actors, string director, HashSet<string> tags, double rating)
        {
            FilmName = filmName;
            Actors = actors;
            Director = director;
            Tags = tags;
            Rating = rating;
        }
    }

    public class Dictionary
    {
        // first dict
        public ConcurrentDictionary<string, Movie> CreateMovieDictionary(List<F> fList)
        {
            var movieDictionary = new ConcurrentDictionary<string, Movie>();

            foreach (var f in fList)
            {
                string filmName = f.FilmName;
                string job = f.PersonJob?.ToLowerInvariant();

                if (!movieDictionary.ContainsKey(filmName))
                {
                    var movie = new Movie
                    {
                        FilmName = filmName,
                        Rating = f.FilmRating,
                    };

                    movieDictionary.TryAdd(filmName, movie);
                }

                if (movieDictionary.TryGetValue(filmName, out var existingMovie))
                {
                    // Пункт 4: режиссёра назначаем только если он ещё не найден.
                    if (string.IsNullOrEmpty(existingMovie.Director) && job == "director")
                    {
                        existingMovie.Director = f.PersonName;
                    }

                    // Пункт 4: актёров добавляем независимо от того, найден ли уже режиссёр.
                    if (job == "actor")
                    {
                        existingMovie.Actors.Add(f.PersonName);
                    }

                    if (!string.IsNullOrEmpty(f.Tag))
                    {
                        existingMovie.Tags.Add(f.Tag);
                    }

                    movieDictionary.AddOrUpdate(filmName, existingMovie, (key, oldValue) => existingMovie);
                }
            }

            return movieDictionary;
        }

        // sec dict
        public ConcurrentDictionary<string, HashSet<Movie>> CreateActorMovieDictionary(List<F> fList)
        {
            var actorMovieDictionary = new ConcurrentDictionary<string, HashSet<Movie>>();

            foreach (var f in fList)
            {
                string job = f.PersonJob?.ToLowerInvariant();

                if (job == "actor" || job == "director")
                {
                    string personName = f.PersonName;
                    actorMovieDictionary.TryAdd(personName, new HashSet<Movie>());

                    if (actorMovieDictionary.TryGetValue(personName, out var movieSet))
                    {
                        Movie existingMovie = movieSet.FirstOrDefault(m => m.FilmName == f.FilmName);

                        if (existingMovie == null)
                        {
                            Movie movie = new Movie
                            {
                                FilmName = f.FilmName,
                                Rating = f.FilmRating
                            };

                            if (job == "director")
                            {
                                movie.Director = f.PersonName;
                            }
                            else if (job == "actor")
                            {
                                movie.Actors.Add(f.PersonName);
                            }

                            if (!string.IsNullOrEmpty(f.Tag))
                            {
                                movie.Tags.Add(f.Tag);
                            }

                            movieSet.Add(movie);
                        }
                        else
                        {
                            existingMovie.Rating = f.FilmRating;

                            if (job == "director")
                            {
                                existingMovie.Director = f.PersonName;
                            }
                            else if (job == "actor")
                            {
                                existingMovie.Actors.Add(f.PersonName);
                            }

                            if (!string.IsNullOrEmpty(f.Tag))
                            {
                                existingMovie.Tags.Add(f.Tag);
                            }
                        }

                        actorMovieDictionary.AddOrUpdate(personName, movieSet, (key, oldValue) => movieSet);
                    }
                }
            }

            return actorMovieDictionary;
        }
    }
}
