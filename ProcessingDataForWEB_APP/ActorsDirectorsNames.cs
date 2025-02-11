using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingDataForWEB_APP
{
    public class Person
    {
        public string PersonId;
        public string PersonName;

        public Person(string id, string name)
        { 
            this.PersonId = id;
            this.PersonName = name;
        }
    }

    public class ActorsDirectorsNames
    {

        public List<Person> SetIdName(string Fpath)
        {
            var list = new List<Person>();

            using (var sr = new StreamReader(Fpath))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var data = line.Split('\t');
                    list.Add(new Person(data[0], data[1]));
                }
            }

            return list;
        }
    }
}
