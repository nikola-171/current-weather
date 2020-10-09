using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projekat.Models
{
    public class Kategorija
    {
        [Key]
        public int Id { get; set; }

        public string Ime { get; set; }

        public int RedosledPrikaza { get; set; }

    }
}
