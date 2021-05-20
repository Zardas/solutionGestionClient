using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace gestionRelationClient.Models
{

    class Service : Article
    {
        public string Conditions { get; set; }
    }
}
