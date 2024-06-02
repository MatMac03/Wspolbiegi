using Logika;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ModelEventArgs
    {
        public Vector2 vec;
        public ModelEventArgs(Vector2 vec)
        {
            this.vec = vec;
        }

    }
}