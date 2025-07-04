using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry.Ecommerce
{
    public interface IShippable
    {
        string GetName();
        double GetWeight();
    }
}
