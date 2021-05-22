using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionClientWPF.Models.Utilit
{
    class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
