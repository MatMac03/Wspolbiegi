using Dane;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Logika
{
    public abstract class LogicAbstractAPI
    {
        public abstract Table getTable();
        public abstract bool isRunning();
        public abstract void startSim();

        public abstract void stopSim();

        public static LogicAbstractAPI CreateLogicAPI(int x, int y, int ilosc)
        {
            return new Sim(new Table(x, y, ilosc));
        }
        public abstract float[][] getPozycja();
    }
}