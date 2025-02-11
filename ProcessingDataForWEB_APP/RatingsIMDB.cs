using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingDataForWEB_APP
{
    public class Rating
    {
        public string FilmIdIMDB;
        public double FilmRating;

        public Rating(string MovieId, double MovieRating)
        {
            this.FilmIdIMDB = MovieId;
            this.FilmRating = MovieRating;
        }
    }

    public class RatingsIMDB
    {
        public List<Rating> SetRatings(string Fpath)
        {
            var list = new List<Rating>();

            using (var sr = new StreamReader(Fpath))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var data = line.Split('\t');
                    list.Add(new Rating(data[0], double.Parse(data[1], CultureInfo.InvariantCulture)));                  
                }
            }

            return list;
        }
    }
}
