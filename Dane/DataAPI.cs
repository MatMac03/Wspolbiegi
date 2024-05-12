using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Dane
{
    public abstract class DataAbstractAPI
    {
        public abstract int rozmiarX { get; set; }
        public abstract int rozmiarY { get; set; }
        public static DataAbstractAPI CreateDataAPI()
        {
            return new Table();
        }
        public abstract IBall[] getBalls();
        public abstract void setBalls(IBall[] balls);
        public abstract float[][] getPozycja();
        public abstract void setTableParam(int x, int y, int ballsAmount);
        public abstract void updatePozycja(IBall ball);
        public abstract void clear();

        /*public abstract float x { get; set; }
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
        */
    }
}
