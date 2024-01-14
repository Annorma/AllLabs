using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_11.App
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public double Value { get; set; }
        public int StudentNo { get; set; }

        private static int _objectCount = 0;
        public Grade()
        {
            Id = ++_objectCount;
        }
        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
