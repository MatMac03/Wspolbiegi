using Logika;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ModelBall : IModelBall
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LogicAbstractAPI api;
        private float X;
        private float Y;
        public LogicAbstractAPI Api { get => api; set => api = value; }

        public float x
        {
            get { return X; }
            set
            {
                if (X != value)
                {
                    X = value;
                    RaisePropertyChanged();
                }
            }
        }

        public float y
        {
            get { return Y; }
            set
            {
                if (Y != value)
                {
                    Y = value;
                    RaisePropertyChanged();
                }
            }
        }

        public float r { get; internal set; }

        public ModelBall(float posX, float posY)
        {
            this.X = posX;
            this.Y = posY;
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}