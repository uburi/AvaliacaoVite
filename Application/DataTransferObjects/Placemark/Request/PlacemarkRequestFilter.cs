using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects.Placemark.Request
{
    public class PlacemarkRequestFilter
    {
        public string? Cliente { get; set; }
        public string? Situacao { get; set; }
        public string? Bairro { get; set; }
        [MinLength(3, ErrorMessage = "A referência deve ter no mínimo 3 caracteres.")]
        public string? Referencia { get; set; } 
        [MinLength(3, ErrorMessage = "A rua/cruzamento deve ter no mínimo 3 caracteres.")]
        public string? RuaCruzamento { get; set; }
    }
}
