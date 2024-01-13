using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColAttribute : Attribute
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string ForeignKey { get; set; }
    }
}
