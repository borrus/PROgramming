using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingDataForWEB_APP
{
    public class TagCode
    {
        public string TagId;
        public string Tag;

        public TagCode(string id, string tag)
        {
            this.TagId = id;
            this.Tag = tag;
        }
    }

    public class TagCodesMovieLens
    {
        public List<TagCode> SetTagCodes(string Fpath)
        {
            var list = new List<TagCode>();

            using (var sr = new StreamReader(Fpath))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var data = line.Split(',');
                    list.Add(new TagCode(data[0], data[1]));
                }
            }

            return list;
        }
    }
}
