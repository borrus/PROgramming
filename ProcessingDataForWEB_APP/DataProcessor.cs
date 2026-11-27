using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessingDataForWEB_APP
{
    public sealed record ProcessingPaths(
        string Names,
        string Codes,
        string Titles,
        string Ratings,
        string Links,
        string TagWeights,
        string Tags)
    {
        public IEnumerable<string> All =>
            new[] { Names, Codes, Titles, Ratings, Links, TagWeights, Tags };
    }

    //ActDirNames + ActDirCodes = A
    public class A
    {
        public string PersonName;
        public string PersonJob;
        public string FilmIdIMDB;

        public A() { }

        public A(string personName, string personJob, string filmIdIMDB)
        {
            this.PersonName = personName;
            this.PersonJob = personJob;
            this.FilmIdIMDB = filmIdIMDB;
        }

        public List<A> SetA(string namesPath, string codesPath)
        {
            var names = new ActorsDirectorsNames();
            var codes = new ActorsDirectorsCodes();
            var first = names.SetIdName(namesPath);
            var second = codes.SetIdRole(codesPath);

            var bag = new ConcurrentBag<A>();

            var secondDictionary = new ConcurrentDictionary<string, List<PersonFilmData>>(
                second.GroupBy(s => s.PersonId)
                      .ToDictionary(g => g.Key, g => g.ToList())
            );

            Parallel.ForEach(first, f =>
            {
                if (secondDictionary.TryGetValue(f.PersonId, out var secondList))
                {
                    foreach (var s in secondList)
                    {
                        bag.Add(new A(f.PersonName, s.PersonJob, s.FilmIdIMDB));
                    }
                }
            });

            return bag.ToList();
        }
    }

    //A + MovieCodesIMDB = B
    public class B
    {
        public string PersonName;
        public string PersonJob;
        public string FilmName;
        public string FilmIdIMDB;

        public B() { }

        public B(string personName, string personJob, string filmName, string filmIdIMDB)
        {
            this.PersonName = personName;
            this.PersonJob = personJob;
            this.FilmName = filmName;
            this.FilmIdIMDB = filmIdIMDB;
        }

        public List<B> SetB(ProcessingPaths paths)
        {
            var movieCodesIMDB = new MovieCodesIMDB();
            var a = new A();

            var first = a.SetA(paths.Names, paths.Codes);
            var second = movieCodesIMDB.SetMovieCodes(paths.Titles);

            var bag = new ConcurrentBag<B>();

            var secondDictionary = new ConcurrentDictionary<string, List<MovieCode>>(
                second.GroupBy(s => s.FilmIdIMDB)
                      .ToDictionary(g => g.Key, g => g.ToList())
            );

            Parallel.ForEach(first, f =>
            {
                if (secondDictionary.TryGetValue(f.FilmIdIMDB, out var secondList))
                {
                    if (secondList != null)
                    {
                        foreach (var s in secondList)
                        {
                            bag.Add(new B(f.PersonName, f.PersonJob, s.FilmName, s.FilmIdIMDB));
                        }
                    }
                }
            });

            return bag.ToList();
        }
    }

    //B + RatingsIMDB = C
    public class C
    {
        public string PersonName;
        public string PersonJob;
        public string FilmName;
        public double FilmRating;
        public string FilmIdIMDB;

        public C() { }

        public C(string personName, string personJob, string filmName, double filmRating, string filmIdIMDB)
        {
            this.PersonName = personName;
            this.PersonJob = personJob;
            this.FilmName = filmName;
            this.FilmRating = filmRating;
            this.FilmIdIMDB = filmIdIMDB;
        }

        public List<C> SetC(ProcessingPaths paths)
        {
            var ratingsIMDB = new RatingsIMDB();
            var b = new B();

            var first = b.SetB(paths);
            var second = ratingsIMDB.SetRatings(paths.Ratings);

            var bag = new ConcurrentBag<C>();

            var secondDictionary = new ConcurrentDictionary<string, List<Rating>>(
                second.GroupBy(s => s.FilmIdIMDB)
                      .ToDictionary(g => g.Key, g => g.ToList())
            );

            Parallel.ForEach(first, f =>
            {
                if (secondDictionary.TryGetValue(f.FilmIdIMDB, out var secondList))
                {
                    if (secondList != null)
                    {
                        foreach (var s in secondList)
                        {
                            bag.Add(new C(f.PersonName, f.PersonJob, f.FilmName, s.FilmRating, f.FilmIdIMDB));
                        }
                    }
                }
            });

            return bag.ToList();
        }
    }

    //C + linksIMDBMovieLens = D
    public class D
    {
        public string PersonName;
        public string PersonJob;
        public string FilmName;
        public double FilmRating;
        public string FilmId; // MovieLens ID

        public D() { }

        public D(string personName, string personJob, string filmName, double filmRating, string filmId)
        {
            PersonName = personName;
            PersonJob = personJob;
            FilmName = filmName;
            FilmRating = filmRating;
            FilmId = filmId;
        }

        public List<D> SetD(ProcessingPaths paths)
        {
            var linksIMDBMovieLens = new linksIMDBMovieLens();
            var c = new C();

            var first = c.SetC(paths);
            var second = linksIMDBMovieLens.SetLinks(paths.Links);

            var bag = new ConcurrentBag<D>();

            // Ключ — IMDb ID в формате "tt...", значение — Link (MovieLens ID в link.FilmId)
            var secondDictionary = new ConcurrentDictionary<string, Link>(
                second.ToDictionary(s => "tt" + s.FilmIdIMDB, s => s)
            );

            Parallel.ForEach(first, f =>
            {
                if (secondDictionary.TryGetValue(f.FilmIdIMDB, out var link))
                {
                    if (link != null)
                    {
                        // ВАЖНО: передаём link.FilmId (MovieLens ID), а не f.FilmIdIMDB
                        bag.Add(new D(f.PersonName, f.PersonJob, f.FilmName, f.FilmRating, link.FilmId));
                    }
                }
            });

            return bag.ToList();
        }
    }

    //D + TagScore
    public class E
    {
        public string PersonName;
        public string PersonJob;
        public string FilmName;
        public double FilmRating;
        public string TagId;

        public E() { }

        public E(string personName, string personJob, string filmName, double filmRating, string tagId)
        {
            PersonName = personName;
            PersonJob = personJob;
            FilmName = filmName;
            FilmRating = filmRating;
            TagId = tagId;
        }

        public List<E> SetE(ProcessingPaths paths)
        {
            var tagScoresMovieLens = new TagScoresMovieLens();
            var d = new D();

            var first = d.SetD(paths);
            var second = tagScoresMovieLens.SetTagScores(paths.TagWeights);

            var bag = new ConcurrentBag<E>();

            var secondDictionary = new ConcurrentDictionary<string, List<TagScore>>(
                second.GroupBy(s => s.FilmId)
                      .ToDictionary(g => g.Key, g => g.ToList())
            );

            Parallel.ForEach(first, f =>
            {
                // f.FilmId — это уже MovieLens ID, никаких обрезок IMDb не нужно
                if (secondDictionary.TryGetValue(f.FilmId, out var tagScoresList))
                {
                    if (tagScoresList != null)
                    {
                        foreach (var tagScore in tagScoresList)
                        {
                            bag.Add(new E(f.PersonName, f.PersonJob, f.FilmName, f.FilmRating, tagScore.TagId));
                        }
                    }
                }
            });

            return bag.ToList();
        }
    }

    //E + TagCodes
    public class F
    {
        public string PersonName;
        public string PersonJob;
        public string FilmName;
        public double FilmRating;
        public string Tag;

        public F() { }

        public F(string personName, string personJob, string filmName, double filmRating, string tag)
        {
            this.PersonName = personName;
            this.PersonJob = personJob;
            this.FilmName = filmName;
            this.FilmRating = filmRating;
            this.Tag = tag;
        }

        public static List<F> SetF(ProcessingPaths paths)
        {
            var tagCodesMovieLens = new TagCodesMovieLens();
            var e = new E();

            var first = e.SetE(paths);
            var second = tagCodesMovieLens.SetTagCodes(paths.Tags);

            var bag = new ConcurrentBag<F>();

            var secondDictionary = new ConcurrentDictionary<string, TagCode>(
                second.ToDictionary(s => s.TagId, s => s)
            );

            Parallel.ForEach(first, f =>
            {
                if (secondDictionary.TryGetValue(f.TagId, out var tagCode))
                {
                    if (tagCode != null)
                    {
                        bag.Add(new F(f.PersonName, f.PersonJob, f.FilmName, f.FilmRating, tagCode.Tag));
                    }
                }
            });

            return bag.ToList();
        }
    }
}
