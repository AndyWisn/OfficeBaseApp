using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeBaseApp.Entities
{
    internal class BusinessCustomer : Customer
    {
        public BusinessCustomer() {}
        public BusinessCustomer(string name)
        {
        }
        public override string ToString() => "Business Customer " + this.ToString();
    }
}
