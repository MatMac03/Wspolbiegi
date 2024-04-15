using Dane;
using System;
using System.Threading.Tasks;

namespace Logika
{
    public class Table
    {
        public int rozmiarX { get; }
        public int rozmiarY { get; }
        internal DataAbstractAPI[] balls;

        public DataAbstractAPI[] getBalls()
        {
            return balls;
        }

        public Table(int rozmiarX, int rozmiarY, int ilosc)
        {
            this.rozmiarX = rozmiarX;
            this.rozmiarY = rozmiarY;
            this.balls = Logic.createBalls(rozmiarX, rozmiarY, ilosc);
        }

        public void checkBorderCollision()
        {
            foreach (DataAbstractAPI ball in balls)
            {
                if (ball.x + ball.getPromien() >= this.rozmiarX || ball.x + ball.getXPredkosc() + ball.getPromien() >= this.rozmiarX ||
                ball.x <= 0 || ball.x + ball.getXPredkosc() <= 0)
                {
                    Logic.zmienKierunekX(ball);
                    Logic.aktualizujPozycje(ball);
                }
                if (ball.y + ball.getPromien() >= this.rozmiarY || ball.y + ball.getYPredkosc() + ball.getPromien() >= this.rozmiarY ||
                    ball.y <= 0 || ball.y + ball.getYPredkosc() <= 0)
                {
                    Logic.zmienKierunekY(ball);
                    Logic.aktualizujPozycje(ball);
                }

            }
        }

        public float[][] getPozycja()
        {
            float[][] pozycja = new float[balls.Length][];
            for (int i = 0; i < balls.Length; i++)
            {
                float[] a = new float[2];
                a[0] = balls[i].x;
                a[1] = balls[i].y;
                pozycja[i] = a;
            }
            return pozycja;
        }
    }
}

