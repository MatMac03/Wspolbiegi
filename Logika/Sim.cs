﻿using Dane;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Logika
{
    public class Sim : LogicAbstractAPI
    { 
        private DataAbstractAPI table;
        private bool running;
        private Thread collisionThread;
        //public List<Thread> threads = new List<Thread>();
        public ObservableCollection<IBall> observableData = new ObservableCollection<IBall>();
        private IBall[] balls;
        public readonly object zamek = new object();

        public Sim(DataAbstractAPI table = null)
        {
            if (table == null)
            {
                this.table = DataAbstractAPI.CreateDataAPI();
            }
            else
            {
                this.table = table;
            }
            this.running = false;
        }

        internal override DataAbstractAPI getTable()
        {
            return table;
        }

        public void setTable(ITable table)
        {
            this.table = (DataAbstractAPI)table;
        }

        public override IBall[] getBalls()
        {
            return getTable().getBalls();
        }

        private void ballCollision(IBall ball1, IBall ball2)
        {
            // Oblicz wektor normalny
            float dx = ball2.Pos.X - ball1.Pos.X;
            float dy = ball2.Pos.Y - ball1.Pos.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy); // odległość między kulami
            float n_x = dx / distance; // składowa x wektora normalnego
            float n_y = dy / distance; // składowa y wektora normalnego

            // Oblicz wektor styczny
            float t_x = -n_y; // składowa x wektora stycznego
            float t_y = n_x;  // składowa y wektora stycznego

            // Prędkości wzdłuż normalnej i stycznej
            float v1n = ball1.predkosc.X * (n_x) + ball1.predkosc.Y * (n_y);
            float v1t = ball1.predkosc.X * (t_x) + ball1.predkosc.Y * (t_y);

            float v2n = ball2.predkosc.X * (n_x) + ball2.predkosc.Y * (n_y);
            float v2t = ball2.predkosc.X * (t_x) + ball2.predkosc.Y * (t_y);

            // Nowe prędkości wzdłuż normalnej po zderzeniu
            float u1n = ((ball1.getMasa() - ball2.getMasa()) * v1n + 2 * ball2.getMasa() * v2n) / (ball1.getMasa() + ball2.getMasa());
            float u2n = ((ball2.getMasa() - ball1.getMasa()) * v2n + 2 * ball1.getMasa() * v1n) / (ball2.getMasa() + ball1.getMasa());

            // Nowe prędkości całkowite dla każdej kuli
            Vector2 predkosc1 = new Vector2(u1n * n_x + v1t * t_x, u1n * n_y + v1t * t_y);
            Vector2 predkosc2 = new Vector2(u2n * n_x + v2t * t_x, u2n * n_y + v2t * t_y);
            lock (zamek)
            {
                ball1.predkosc = predkosc1;
                ball2.predkosc = predkosc2;
            }
        }

        public void checkBorderCollisionForBall(IBall ball)
        {
            lock (zamek)
            {
                if (ball.Pos.X + ball.getPromien() >= table.rozmiarX || ball.Pos.X + ball.predkosc.X + ball.getPromien() >= table.rozmiarX ||
                   ball.Pos.X <= 0 || ball.Pos.X + ball.predkosc.X <= 0)
                {
                    Logic.zmienKierunekX(ball);
                }
                if (ball.Pos.Y + ball.getPromien() >= table.rozmiarY || ball.Pos.Y + ball.predkosc.Y + ball.getPromien() >= table.rozmiarY ||
                    ball.Pos.Y <= 0 || ball.Pos.Y + ball.predkosc.Y <= 0)
                {
                    Logic.zmienKierunekY(ball);
                }
            }
        }

        private void lookForCollisions()
        {
            foreach (IBall ball1 in balls)
            {
                lock (zamek)
                {
                    checkBorderCollisionForBall(ball1);
                    foreach (IBall ball2 in balls)
                    {
                        if (ball1 == ball2)
                        { continue; }
                        Vector2 tmp1 = ball1.Pos;
                        Vector2 tmp2 = ball2.Pos;
                        if (Math.Sqrt((tmp1.X - tmp2.X) * (tmp1.X - tmp2.X) + (tmp1.Y - tmp2.Y) * (tmp1.Y - tmp2.Y)) <= ball1.getPromien() + ball2.getPromien())
                        {
                            ballCollision(ball1, ball2);
                        }
                    }
                }
            }
        }

        public override bool isRunning() { return running; }

        public override void startSim()
        {
            if (!running)
            {
                this.running = true;
                collisionThread = new Thread(() =>
                {
                    try
                    {
                        while (running)
                        {
                            lookForCollisions();
                            Thread.Sleep(2);
                        }
                    }
                    catch (ThreadInterruptedException)
                    {
                        Debug.WriteLine("Thread killed");
                    }
                });
                collisionThread.IsBackground = true;
                collisionThread.Start();
            }
        }

        public override void stopSim()
        {
            if (running)
            {
                this.running = false;
                this.collisionThread.Interrupt();
                foreach (IBall b in balls)
                {
                    b.destroy();
                }
            }
        }

        public override Vector2[] getPozycja()
        {
            return table.getPozycja();
        }

        public override void getTableParam(int x, int y, int ilosc)
        {
            table.setTableParam(x, y, ilosc);
            foreach (IBall ball in table.getBalls())
            {
                this.observableData.Add(ball);
                ball.ChangedPosition += update;
            }
            this.balls = table.getBalls();
        }

        public override void setBalls(IBall[] balls)
        {
            this.table.setBalls(balls);
        }

        #nullable enable
        public override event EventHandler<LogicEventArgs>? ChangedPosition;

        private void OnPropertyChanged(LogicEventArgs args)
        {
            ChangedPosition?.Invoke(this, args);
        }

        private void update(object sender, DataEventArgs e)
        {
            IBall ball = (IBall)sender;
            Vector2 pos = ball.Pos;
            LogicEventArgs args = new LogicEventArgs(pos);
            OnPropertyChanged(args);
        }
        /* public void updatePosition(IBall ball)
         {
             table.updatePozycja(ball);

             ball.RaisePropertyChanged(nameof(ball.x));
             ball.RaisePropertyChanged(nameof(ball.y));
         }

         public override event PropertyChangedEventHandler PropertyChanged;

         private void OnPropertyChanged(PropertyChangedEventArgs args)
         {
             PropertyChanged?.Invoke(this, args);
         }
         private void RelayBallUpdate(object source, PropertyChangedEventArgs args)
         {
             this.OnPropertyChanged(args);
         }*/
    }
}