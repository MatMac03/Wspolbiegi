using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace Dane
{
    public class Ball : DataAbstractAPI, INotifyPropertyChanged
    {
        public override event PropertyChangedEventHandler PropertyChanged;

        private float promien;
        private float predkoscX;
        private float predkoscY;
        public override float x { get; set; }
        public override float y { get; set; }

        public Ball(int maxX, int maxY)
        {
            this.promien = 3.0f;
            this.x = losowaPozycja(maxX);
            this.y = losowaPozycja(maxY);
            this.predkoscX = LosowaPredkosc(-10, 10);
            this.predkoscY = LosowaPredkosc(-10, 10);
        }

        public override float getPromien()
        {
            return promien;
        }

        public override float getXPredkosc()
        {
            return predkoscX;
        }

        public override float getYPredkosc()
        {
            return predkoscY;
        }

        public override void setXPredkosc(float xPredkosc)
        {
            predkoscX = xPredkosc;
        }

        public override void setYPredkosc(float yPredkosc)
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
        public override void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
