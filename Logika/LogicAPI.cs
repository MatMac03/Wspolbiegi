/*using Dane;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Logika
{
    public abstract class LogicAbstractAPI
    {
        public abstract event PropertyChangedEventHandler PropertyChanged;
        public abstract DataAbstractAPI getTable();
        public abstract bool isRunning();
        public abstract void startSim();

        public abstract void stopSim();

        public abstract void getTableParam(int x, int y, int ilosc);

        public static LogicAbstractAPI CreateLogicAPI()
        {
            return new Sim();
        }
        public abstract float[][] getPozycja();
        public abstract void setBalls(IBall[] balls);
    }
}*/