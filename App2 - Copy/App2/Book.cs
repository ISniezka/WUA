using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    class Book
    {
        public string name { get; set; }

        public Book(string name)
        {
            this.name = name;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
