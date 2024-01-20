using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uwagi.App
{
    public class Uwaga
    {
        [Key]
        public int Id { get; set; }
        public int Linia { get; set; }

        public string Wartosc { get; set; }
    }
}
