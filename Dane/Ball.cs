using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

[assembly: InternalsVisibleTo("Program.XmlSerializers")]

namespace Dane
{
    public interface IBall
    {
        //float x { get; set; }
        //float y { get; set; }
        Vector2 Pos { get; }
        Vector2 predkosc { get; set; }
        //event PropertyChangedEventHandler PropertyChanged;
        float getPromien();
        float getXPredkosc();
        float getYPredkosc();
        void setXPredkosc(float xPredkosc);
        void setYPredkosc(float yPredkosc);
        float getMasa();
        //void updatePozycja();
        void destroy();
        #nullable enable
        event EventHandler<DataEventArgs>? ChangedPosition;
    }

    [DataContract]
    internal class Ball : IBall
    {
    #nullable enable
        public event EventHandler<DataEventArgs>? ChangedPosition;

        private float promien;
        private float predkoscX;
        private float predkoscY;
        private float masa { get; set; }
        //public float x { get; set; }
        // float y { get; set; }

        [DataMember]
        public Vector2 pos { get; private set; }
        private Vector2 _predkosc { get; set; }

        [DataMember]
        public int ID;
        Stopwatch stopwatch;
        private Logger logger;
        private bool running;
        private Thread thread;

        public Ball(int ID, int maxX, int maxY, Logger logger)
        {
            this.ID = ID;
            this.logger = logger;
            this.promien = 15.0f;
            this.masa = 5.0f;
            this.pos = new Vector2(losowaPozycja(maxX), losowaPozycja(maxY));
            //this.x = losowaPozycja(maxX);
            //this.y = losowaPozycja(maxY);
            //this.predkoscX = LosowaPredkosc(-10, 10);
            //this.predkoscY = LosowaPredkosc(-10, 10);
            this.predkosc = new Vector2(LosowaPredkosc(-2, 2), LosowaPredkosc(-2, 2));
            this.running = true;
            stopwatch = new Stopwatch();
            this.thread = new Thread(() =>
            {
                try
                {
                    while (this.running)
                    {
                        float time = stopwatch.ElapsedMilliseconds;
                        stopwatch.Restart();
                        stopwatch.Start();
                        move(time);
                        logger.addToQueue(this);
                        Thread.Sleep(10);
                    }
                }
                catch (ThreadInterruptedException)
                {
                    Debug.WriteLine("Thread killed");
                }
            });
            thread.IsBackground = true;
            thread.Start();

        }
        public Vector2 predkosc
        {
            get { return _predkosc; }
            set
            {
                if (_predkosc != value)
                {
                    _predkosc = value;
                    move(5.0f);
                }
            }
        }
        public Vector2 Pos
        {
            get => pos;
        }

        private void move(float time)
        {
            this.pos += time * predkosc;
            DataEventArgs args = new DataEventArgs(pos);
            ChangedPosition?.Invoke(this, args);
        }

        public void destroy()
        {
            this.running = false;
            this.thread.Interrupt();
        }

        public float getPromien()
        {
            return promien;
        }

        public float getXPredkosc()
        {
            return predkoscX;
        }

        public float getYPredkosc()
        {
            return predkoscY;
        }

        public float getMasa()
        {
            return masa;
        }

        public void setXPredkosc(float xPredkosc)
        {
            predkoscX = xPredkosc;
        }

        public void setYPredkosc(float yPredkosc)
        {
            predkoscY = yPredkosc;
        }

        Random rand = new Random();

        private float losowaPozycja(int maxPozycja)
        {
            return (float)rand.NextDouble() * (maxPozycja - 2 * this.promien) + this.promien;
        }

        public float LosowaPredkosc(int minPredkosc, int maxPredkosc)
        {
            return (float)rand.NextDouble()*(maxPredkosc-minPredkosc)+minPredkosc;
        }

        /*public bool IsWithinBounds(int rozmiarX, int rozmiarY)
        {
            bool isWithinXBounds = this.x + this.predkoscX + this.promien < rozmiarX && this.x + this.predkoscX - this.promien > 0;
            bool isWithinYBounds = this.y + this.predkoscY + this.promien < rozmiarY && this.y + this.predkoscY - this.promien > 0;
            return isWithinXBounds && isWithinYBounds;
        }*/

       /* public void updatePozycja()
        {
            lock (this)
            {
                x += getXPredkosc();
                y += getYPredkosc();

                RaisePropertyChanged(nameof(x));
                RaisePropertyChanged(nameof(y));
            }
        }*/

        /*public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }
}
