using System;
using System.Threading.Tasks;

namespace Dane
{
    public interface ITable
    {
        IBall[] getBalls();
        void checkBorderCollision();
        float[][] getCoordinates();
    }

    internal class Table : DataAbstractAPI
    {
        public override int rozmiarX { get; set; }
        public override int rozmiarY { get; set; }

        public IBall[] balls;

        public override void clear()
        {
            balls = new IBall[0];
        }

        public override IBall[] getBalls()
        {
            return balls;
        }

        public override void setBalls(IBall[] balls)
        {
            this.balls = balls;
        }

        public override void setTableParam(int rozmiarX, int rozmiarY, int ilosc)
        {
            this.rozmiarX = rozmiarX;
            this.rozmiarY = rozmiarY;
            createBalls(rozmiarX, rozmiarY, ilosc);
        }

        public void createBalls(int maxX, int maxY, int amount)
        {
            IBall[] balls = new IBall[amount];
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = new Ball(maxX, maxY);
            }
            this.balls = balls;
        }

        public Table() { }
        /*
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
        */
        public override void updatePozycja(IBall ball) 
        {
            foreach (IBall b in balls)
            {
                if (b == ball)
                {
                    ball.updatePozycja();
                    ball.RaisePropertyChanged(nameof(ball.x));
                    ball.RaisePropertyChanged(nameof(ball.y));
                }
            }

        }

        public override float[][] getPozycja()
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

