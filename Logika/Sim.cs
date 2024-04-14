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

        private Table table;
        private bool running;
        public List<Thread> threads = new List<Thread>();
        public ObservableCollection<DataAbstractAPI> observableData = new ObservableCollection<DataAbstractAPI>();

        public Sim(Table table)
        {
            this.table = table;
            this.running = false;
            foreach (var ball in table.getBalls())
            {
                this.observableData.Add(ball);
            }
        }

        public override Table getTable()
        {
            return table;
        }

        public override bool isRunning() { return running; }

        public override void startSim()
        {
            if (!running)
            {
                this.running = true;
            }
            foreach (var ball in this.table.getBalls())
            {
                Thread thread = new Thread(() =>
                {
                    while (this.running)
                    {
                        this.table.checkBorderCollision();
                        Logic.aktualizujPozycje(ball);
                        ball.PropertyChanged += RelayBallUpdate;
                        Thread.Sleep(10);
                    }
                });
                thread.IsBackground = true;
                thread.Start();
                threads.Add(thread);
            }
        }

        public override void stopSim()
        {

            if (running)
            {
                this.running = false;
                foreach (var thread in threads)
                {
                    thread.Interrupt();
                }
                threads.Clear();
            }
        }

        public override float[][] getPozycja()
        {
            return table.getPozycja();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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