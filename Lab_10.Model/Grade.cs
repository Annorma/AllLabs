using Lab_10.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10.Model
{
    [DbTab]
    public class Grade
    {
        [DbCol]
        [DbPrimaryKey]
        public int Id { get; set; }
        [DbCol]
        public DateTime Date { get; set; }
        [DbCol]
        public string Subject { get; set; }
        [DbCol]
        public double Value { get; set; }
        [DbCol]
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
