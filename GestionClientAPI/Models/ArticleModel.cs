using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class ArticleModel
    {
        public String Nom { get; set; }
        public String Description { get; set; }
        public int Prix { get; set; }
        public String Image { get; set; }
        public String Type { get; set; }
        public String Manuel { get; set; }
    }
}
