using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessingDataForWEB_APP
{
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

        public List<A> SetA()
        {
            ActorsDirectorsNames names = new ActorsDirectorsNames();
            ActorsDirectorsCodes codes = new ActorsDirectorsCodes();

            var first = names.SetIdName("C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\ActorsDirectorsNames_IMDB.txt");
            var second = codes.SetIdRole("C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\ActorsDirectorsCodes_IMDB.tsv");

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

        public B()
        {

        }

        public B(string personName, string personJob, string filmName, string filmIdIMDB)
        {
            this.PersonName = personName;
            this.PersonJob = personJob;
            this.FilmName = filmName;
            this.FilmIdIMDB = filmIdIMDB;
        }

        public List<B> SetB()
        {
            var movieCodesIMDB = new MovieCodesIMDB();
            var a = new A();

            var first = a.SetA();
            var second = movieCodesIMDB.SetMovieCodes("C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\MovieCodes_IMDB.tsv");

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

        public C()
        {

        }

        public C(string personName, string personJob, string filmName, double filmRating, string filmIdIMDB)
        {
            this.PersonName = personName;
            this.PersonJob = personJob;
            this.FilmName = filmName;
            this.FilmRating = filmRating;
            this.FilmIdIMDB = filmIdIMDB;
        }

        public List<C> SetC()
        {
            var ratingsIMDB = new RatingsIMDB();
            var b = new B();

            var first = b.SetB();
            var second = ratingsIMDB.SetRatings("C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\Ratings_IMDB.tsv");

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
    
    //C + linksIMDBMovieLens
    public class D
    {
        public string PersonName;
        public string PersonJob;
        public string FilmName;
        public double FilmRating;
        public string FilmId;

        public D()
        {

        }

        public D(string personName, string personJob, string filmName, double filmRating, string filmId)
        {
            PersonName = personName;
            PersonJob = personJob;
            FilmName = filmName;
            FilmRating = filmRating;
            FilmId = filmId;
        }

        public List<D> SetD()
        {
            var linksIMDBMovieLens = new linksIMDBMovieLens();
            var c = new C();

            var first = c.SetC();
            var second = linksIMDBMovieLens.SetLinks("C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\links_IMDB_MovieLens.csv");

            var bag = new ConcurrentBag<D>();

            var secondDictionary = new ConcurrentDictionary<string, Link>(
                second.ToDictionary(s => "tt" + s.FilmIdIMDB, s => s)
            );

            Parallel.ForEach(first, f =>
            {
                if (secondDictionary.TryGetValue(f.FilmIdIMDB, out var link))
                {
                    if (link != null)
                    {
                        bag.Add(new D(f.PersonName, f.PersonJob, f.FilmName, f.FilmRating, f.FilmIdIMDB));
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

        public E()
        {

        }

        public E(string personName, string personJob, string filmName, double filmRating, string tagId)
        {
            PersonName = personName;
            PersonJob = personJob;
            FilmName = filmName;
            FilmRating = filmRating;
            TagId = tagId;
        }

        public List<E> SetE()
        {
            var tagScoresMovieLens = new TagScoresMovieLens();
            var d = new D();

            var first = d.SetD();
            var second = tagScoresMovieLens.SetTagScores("C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\TagScores_MovieLens.csv");

            var bag = new ConcurrentBag<E>();

            var secondDictionary = new ConcurrentDictionary<string, List<TagScore>>(
                second.GroupBy(s => s.FilmId)
                      .ToDictionary(g => g.Key, g => g.ToList())
            );

            Parallel.ForEach(first, f =>
            {
                var filmId = CreateNdFilmId(f.FilmId);

                if (secondDictionary.TryGetValue(filmId, out var tagScoresList))
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

        private string CreateNdFilmId(string input)
        {
            string outP = input.Remove(0, 2);
            outP.TrimStart('0');

            return outP.TrimStart('0');
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

        public F()
        {

        }

        public F(string personName, string personJob, string filmName, double filmRating, string tag)
        {
            this.PersonName = personName;
            this.PersonJob = personJob;
            this.FilmName = filmName;
            this.FilmRating = filmRating;
            this.Tag = tag;
        }

        public List<F> SetF()
        {
            var tagCodesMovieLens = new TagCodesMovieLens();
            var e = new E();

            var first = e.SetE();
            var second = tagCodesMovieLens.SetTagCodes("C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\TagCodes_MovieLens.csv");

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
 