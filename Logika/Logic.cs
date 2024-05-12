using Dane;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("LogikaTest")]

namespace Logika
{
    public abstract class LogicAbstractAPI
    {
        public abstract event PropertyChangedEventHandler PropertyChanged;
        internal abstract DataAbstractAPI getTable();
        public abstract bool isRunning();
        public abstract void startSim();

        public abstract void stopSim();

        public abstract IBall[] getBalls();

        public abstract void getTableParam(int x, int y, int ilosc);

        public static LogicAbstractAPI CreateLogicAPI()
        {
            return new Sim();
        }
        public abstract float[][] getPozycja();
        public abstract void setBalls(IBall[] balls);
    }
    internal class Logic
    {
        static public DataAbstractAPI[] createBalls(int maxX, int maxY, int ilosc)
        {
            DataAbstractAPI[] balls = new DataAbstractAPI[ilosc];
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = DataAbstractAPI.CreateDataAPI();
            }
            return balls;
        }

        static public void zmienKierunekX(IBall ball)
        {
            ball.setXPredkosc(-ball.getXPredkosc());
            //ball.RaisePropertyChanged(nameof(ball.getYPredkosc));
        }

        static public void zmienKierunekY(IBall ball)
        {
            ball.setYPredkosc(-ball.getYPredkosc());
            //ball.RaisePropertyChanged(nameof(ball.getYPredkosc));
        }

        /*
        static public void aktualizujPozycje(IBall ball)
        {
            ball.x += ball.getXPredkosc();
            ball.y += ball.getYPredkosc();
            ball.RaisePropertyChanged(nameof(ball.x));
            ball.RaisePropertyChanged(nameof(ball.y));
        }
        
        static public void aktualizujStol(Table table)
        {
            for (int i = 0; i < table.balls.Length; i++)
            {
                aktualizujPozycje(table.balls[i]);
            }
        }
        */
    }
}
