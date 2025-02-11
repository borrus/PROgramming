using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingDataForWEB_APP
{
    public class TagScore
    {
        public string FilmId;
        public string TagId;

        public TagScore(string MovieId, string TagId)
        {
            this.FilmId = MovieId;
            this.TagId = TagId;
        }
    }

    public class TagScoresMovieLens
    {
        public List<TagScore> SetTagScores(string Fpath)
        {
            var list = new List<TagScore>();

            using (var sr = new StreamReader(Fpath))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var data = line.Split(',');

                    if (double.Parse(data[2], CultureInfo.InvariantCulture) > 0.5)
                    {
                        list.Add(new TagScore(data[0], data[1]));
                    }
                }
            }

            return list;
        }
    }
}
