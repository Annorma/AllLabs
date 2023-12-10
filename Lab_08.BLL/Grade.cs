using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_08.BLL
{
    public class Grade
    {
        public string Subject { get; set; }
        public double Value { get; set; }

        public Grade()
        {
            Subject = string.Empty;
            Value = 0;
        }

        public Grade(string subject, double value)
        {
            Subject = subject;
            Value = value;
        }
    }
}
