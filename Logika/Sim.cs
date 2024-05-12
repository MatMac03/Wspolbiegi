﻿using Dane;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Logika
{
    public class Sim : LogicAbstractAPI, INotifyPropertyChanged
    {

        private DataAbstractAPI table;
        private bool running;
        public List<Thread> threads = new List<Thread>();
        public ObservableCollection<IBall> observableData = new ObservableCollection<IBall>();

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
            float dx = ball2.x - ball1.x;
            float dy = ball2.y - ball1.y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy); // odległość między kulami
            float n_x = dx / distance; // składowa x wektora normalnego
            float n_y = dy / distance; // składowa y wektora normalnego

            // Oblicz wektor styczny
            float t_x = -n_y; // składowa x wektora stycznego
            float t_y = n_x;  // składowa y wektora stycznego

            // Prędkości wzdłuż normalnej i stycznej
            float v1n = ball1.getXPredkosc() * (n_x) + ball1.getYPredkosc() * (n_y);
            float v1t = ball1.getXPredkosc() * (t_x) + ball1.getYPredkosc() * (t_y);

            float v2n = ball2.getXPredkosc() * (n_x) + ball2.getYPredkosc() * (n_y);
            float v2t = ball2.getXPredkosc() * (t_x) + ball2.getYPredkosc() * (t_y);

            // Nowe prędkości wzdłuż normalnej po zderzeniu
            float u1n = ((ball1.getMasa() - ball2.getMasa()) * v1n + 2 * ball2.getMasa() * v2n) / (ball1.getMasa() + ball2.getMasa());
            float u2n = ((ball2.getMasa() - ball1.getMasa()) * v2n + 2 * ball1.getMasa() * v1n) / (ball2.getMasa() + ball1.getMasa());

            // Nowe prędkości całkowite dla każdej kuli
            ball1.setXPredkosc(u1n * (n_x) + v1t * (t_x));
            ball1.setYPredkosc(u1n * (n_y) + v1t * (t_y));

            ball2.setXPredkosc(u2n * (n_x) + v2t * (t_x));
            ball2.setYPredkosc(u2n * (n_y) + v2t * (t_y));
        }

        public void checkBorderCollisionForBall(IBall ball)
        {
            if (ball.x + ball.getPromien() >= table.rozmiarX || ball.x + ball.getXPredkosc() + ball.getPromien() >= table.rozmiarX ||
               ball.x <= 0 || ball.x + ball.getXPredkosc() <= 0)
            {
                Logic.zmienKierunekX(ball);
            }
            if (ball.y + ball.getPromien() >= table.rozmiarY || ball.y + ball.getYPredkosc() + ball.getPromien() >= table.rozmiarY ||
                ball.y <= 0 || ball.y + ball.getYPredkosc() <= 0)
            {
                Logic.zmienKierunekY(ball);
            }
            updatePosition(ball);
        }

        private void lookForCollisions()
        {
            while (this.running)
            {
                IBall[] balls = table.getBalls();
                foreach (var ball1 in balls)
                {
                    foreach (var ball2 in balls)
                    {
                        if (ball1 == ball2)
                        { continue; }
                        if (Math.Sqrt((ball1.x - ball2.x) * (ball1.x - ball2.x) + (ball1.y - ball2.y) * (ball1.y - ball2.y)) <= ball1.getPromien() + ball2.getPromien())
                        {
                            lock (ball1)
                            {
                                lock (ball2)
                                {
                                    ballCollision(ball1, ball2);
                                    checkBorderCollisionForBall(ball1);
                                    ball2.updatePozycja();
                                }
                                checkBorderCollisionForBall(ball2);
                                ball1.updatePozycja();
                            }
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
                Thread tableTask = new Thread(() =>
                {
                    lookForCollisions();
                    Thread.Sleep(10);
                });
                tableTask.Start();
                foreach (var ball in this.table.getBalls())
                {
                    Thread thread = new Thread(() =>
                    {
                        while (this.running)
                        {
                            checkBorderCollisionForBall(ball);
                            ball.PropertyChanged += RelayBallUpdate;
                            Thread.Sleep(10);
                        }
                    });
                    thread.IsBackground = true;
                    thread.Start();
                    threads.Add(thread);
                }
            }
        }

        public override void stopSim()
        {
            if (running)
            {
                this.running = false;
                threads.Clear();
            }
        }

        public override float[][] getPozycja()
        {
            return table.getPozycja();
        }

        public override void getTableParam(int x, int y, int ilosc)
        {
            table.setTableParam(x, y, ilosc);
            foreach (var ball in table.getBalls())
            {
                this.observableData.Add((IBall)ball);
            }
        }

        public override void setBalls(IBall[] balls)
        {
            this.table.setBalls(balls);
        }

        public void updatePosition(IBall ball)
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
        }
    }
}