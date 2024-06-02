using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public class DataEventArgs
    {
        public Vector2 vec;
        public DataEventArgs(Vector2 vec)
        {
            this.vec = vec;
        }
    }
}