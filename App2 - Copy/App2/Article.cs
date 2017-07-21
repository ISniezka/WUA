using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrecketWonsDiary
{
    class Article
    {
        public string id { get; set; }
        public string name { get; set; }
        public string body { get; set; }

        public Article(string id, string name, string body)
        {
            this.id = id;
            this.name = name;
            this.body = body;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
