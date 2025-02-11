using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingDataForWEB_APP
{
    public class PersonFilmData
    {
        public string PersonId;
        public string FilmIdIMDB;
        public string PersonJob;

        public PersonFilmData(string IdPerson, string idFilm, string Job)
        {
            this.PersonId = IdPerson;
            this.FilmIdIMDB = idFilm;
            this.PersonJob = Job;
        }
    }

    public class ActorsDirectorsCodes
    {
        public List<PersonFilmData> SetIdRole(string Fpath)
        {
            var list = new List<PersonFilmData>();

            using (var sr = new StreamReader(Fpath))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var data = line.Split('\t');

                    if (data[3].Equals("director") || data[3].Equals("actor"))
                    {
                        list.Add(new PersonFilmData(data[2], data[0], data[3]));
                    }                    
                }
            }

            return list;
        }
    }
}
