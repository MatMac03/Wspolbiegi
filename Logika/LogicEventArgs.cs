using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public class LogicEventArgs
    {
        public Vector2 vec;
        public LogicEventArgs(Vector2 vec)
        {
            this.vec = vec;
        }
    }
}