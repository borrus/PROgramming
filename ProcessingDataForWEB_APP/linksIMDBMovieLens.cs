using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingDataForWEB_APP
{
    public class Link
    {
        public string FilmId;      // MovieLens ID (1-й столбец links.csv)
        public string FilmIdIMDB;  // IMDb ID (2-й столбец links.csv)

        public Link(string MovieId, string IMDBId)
        {
            this.FilmId = MovieId;
            this.FilmIdIMDB = IMDBId;
        }
    }

    public class linksIMDBMovieLens
    {
        public List<Link> SetLinks(string Fpath)
        {
            var list = new List<Link>();

            using (var sr = new StreamReader(Fpath))
            {
                sr.ReadLine(); // заголовок

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var data = line.Split(',');
                    if (data.Length < 2)
                        continue;

                    list.Add(new Link(data[0].Trim(), data[1].Trim()));
                }
            }

            return list;
        }
    }
}
