using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace Dane
{
    public interface IBall
    {
        float x { get; set; }
        float y { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
        float getPromien();
        float getXPredkosc();
        float getYPredkosc();
        void setXPredkosc(float xPredkosc);
        void setYPredkosc(float yPredkosc);
        float getMasa();
        void updatePozycja();
        void RaisePropertyChanged([CallerMemberName] string propertyName = null);
    }

    internal class Ball : INotifyPropertyChanged, IBall
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float promien;
        private float predkoscX;
        private float predkoscY;
        private float masa { get; set; }
        public float x { get; set; }
        public float y { get; set; }

        public Ball(int maxX, int maxY)
        {
            this.promien = 2.0f;
            this.masa = 5.0f;
            this.x = losowaPozycja(maxX);
            this.y = losowaPozycja(maxY);
            this.predkoscX = LosowaPredkosc(-10, 10);
            this.predkoscY = LosowaPredkosc(-10, 10);
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

        public bool IsWithinBounds(int rozmiarX, int rozmiarY)
        {
            bool isWithinXBounds = this.x + this.predkoscX + this.promien < rozmiarX && this.x + this.predkoscX - this.promien > 0;
            bool isWithinYBounds = this.y + this.predkoscY + this.promien < rozmiarY && this.y + this.predkoscY - this.promien > 0;
            return isWithinXBounds && isWithinYBounds;
        }

        public void updatePozycja()
        {
            lock (this)
            {
                x += getXPredkosc();
                y += getYPredkosc();

                RaisePropertyChanged(nameof(x));
                RaisePropertyChanged(nameof(y));
            }
        }

        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
