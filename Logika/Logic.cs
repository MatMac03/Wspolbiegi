using Dane;
using System;
using System.Threading.Tasks;

namespace Logika
{
    public class Logic
    {
        static public DataAbstractAPI[] createBalls(int maxX, int maxY, int ilosc)
        {
            DataAbstractAPI[] balls = new DataAbstractAPI[ilosc];
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = DataAbstractAPI.CreateDataAPI(maxX, maxY);
            }
            return balls;
        }

        static public void zmienKierunekX(DataAbstractAPI ball)
        {
            ball.setXPredkosc(-ball.getXPredkosc());
            ball.RaisePropertyChanged(nameof(ball.getYPredkosc));
        }

        static public void zmienKierunekY(DataAbstractAPI ball)
        {
            ball.setYPredkosc(-ball.getYPredkosc());
            ball.RaisePropertyChanged(nameof(ball.getYPredkosc));
        }

        static public void aktualizujPozycje(DataAbstractAPI ball)
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
    }
}
