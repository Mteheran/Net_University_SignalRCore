using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetUniversitySignalRChat.Models
{
    public class Mensaje
    {
        public string Usuario { get; set; }
        public string Contenido { get; set; }
        public string Sala { get; set; }
    }
}
