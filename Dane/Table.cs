using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Dane
{
    public interface ITable
    {
        IBall[] getBalls();
        void checkBorderCollision();
        float[][] getPozycja();
    }

    internal class Table : DataAbstractAPI
    {
        Logger logger;
        public override int rozmiarX { get; set; }
        public override int rozmiarY { get; set; }

        public IBall[] balls;

        public override void clear()
        {
            foreach (IBall ball in balls)
            {
                ball.destroy();
            }
            balls = [];
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

        public void createBalls(int maxX, int maxY, int ilosc)
        {
            IBall[] balls = new IBall[ilosc];
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = new Ball(i, maxX, maxY, logger);
            }
            this.balls = balls;
        }

        public Table() {
            logger = new Logger();
        }
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
       /* public override void updatePozycja(IBall ball) 
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

        }*/

        public override Vector2[] getPozycja()
        {
            Vector2[] pozycja = new Vector2[balls.Length];
            for (int i = 0; i < balls.Length; i++)
            {
                Vector2 pos = balls[i].Pos;
                pozycja[i] = pos;
            }
            return pozycja;
        }
    }
}

