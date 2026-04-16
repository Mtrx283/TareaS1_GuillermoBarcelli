using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Granja_Guillermo_Barcelli
{
    internal class Entity
    {
        protected string name;

        public string Name { get { return name; } }

        public Entity(string name)
        {
            this.name = name;
        }
    }
}
