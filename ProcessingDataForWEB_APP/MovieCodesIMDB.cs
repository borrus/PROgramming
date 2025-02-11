using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingDataForWEB_APP
{
    public class MovieCode
    {
        public string FilmIdIMDB;
        public string FilmName;

        public MovieCode(string id, string name) 
        {
            this.FilmIdIMDB = id;
            this.FilmName = name;
        }
    }

    public class MovieCodesIMDB
    {
        public List<MovieCode> SetMovieCodes(string Fpath)
        {
            var list = new List<MovieCode>();

            using (var sr = new StreamReader(Fpath))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var data = line.Split('\t');

                    if (data[3].Equals("RU") || data[3].Equals("US") || data[4].Equals("RU") || data[4].Equals("US"))
                    {
                        list.Add(new MovieCode(data[0], data[2]));
                    } 
                }
            }

            return list;
        }
    }
}
