using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Placemark
    {
        public string Cliente { get; set; }
        public string Situacao { get; set; }
        public string Bairro { get; set; }
        public string Referencia { get; set; }
        public string RuaCruzamento { get; set; }
        public DateTime Data { get; set; }
        public string Coordenadas { get; set; }
        public string GxMediaLinks { get; set; }

    }
}
