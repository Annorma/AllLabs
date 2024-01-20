using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_Kolokwium_02.Classes
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public int FilmId { get; set; }
        private static int _objectCount = 0;
        public Review()
        {
            Id = ++_objectCount;
        }

        public override string ToString()
        {
            return Rating.ToString(CultureInfo.InvariantCulture);
        }
    }
}
