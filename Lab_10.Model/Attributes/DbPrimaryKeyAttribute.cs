using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbPrimaryKeyAttribute : Attribute
    {
    }
}
