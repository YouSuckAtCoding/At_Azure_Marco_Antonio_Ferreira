using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Guitarrista
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int NumerodeGuitarras { get; set; }
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }
        public string GuitarristaFav { get; set; }
        [DisplayName("Imagem")]
        public string ImageUri { get; set; }
        [DisplayName("Views")]
        public int NumeroDeViews { get; set; }
    }
}
