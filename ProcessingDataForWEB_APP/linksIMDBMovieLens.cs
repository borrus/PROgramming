using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingDataForWEB_APP
{
    public class Link
    {
        public string FilmId;
        public string FilmIdIMDB;

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
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var data = line.Split(',');
                    list.Add(new Link(data[0], data[1]));
                }
            }

            return list;    
        }
    }
}
