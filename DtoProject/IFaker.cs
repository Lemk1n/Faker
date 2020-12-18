using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoProject
{
    interface IFaker
    {
        ObjectType Create<ObjectType>(); 
    }
}
