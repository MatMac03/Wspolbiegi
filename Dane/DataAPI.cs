using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Dane
{
    public abstract class DataAbstractAPI
    {
        public abstract float x { get; set; }
        public abstract float y { get; set; }
        public abstract event PropertyChangedEventHandler PropertyChanged;
        public abstract float getPromien();
        public abstract float getXPredkosc();
        public abstract float getYPredkosc();
        public abstract void setXPredkosc(float xPredkosc);
        public abstract void setYPredkosc(float yPredkosc);
        public static DataAbstractAPI CreateDataAPI(int maxX, int maxY)
        {
            return new Ball(maxX, maxY);
        }
        public abstract void RaisePropertyChanged([CallerMemberName] string propertyName = "");
    }
}
