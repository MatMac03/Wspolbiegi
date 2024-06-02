using Dane;
using Logika;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ModelBall : IModelBall, INotifyPropertyChanged
    {
        private Vector2 _pos;
#nullable enable
        public event EventHandler<ModelEventArgs>? ChangedPosition;
        private LogicAbstractAPI api;

        public event PropertyChangedEventHandler? PropertyChanged;

        public double x
        {
            get { return _pos.X; }
            set
            {
                _pos.X = (float)value;
                RaisePropertyChanged();
            }
        }
        public double y
        {
            get { return _pos.Y; }
            set
            {
                _pos.Y = (float)value;
                RaisePropertyChanged();
            }
        }
        public Vector2 pos
        {
            get { return _pos; }
            internal set
            {
                if (_pos != value)
                {
                    _pos = value;
                    RaisePropertyChanged();
                }
            }
        }
        public LogicAbstractAPI Api { get; set; }

        public float r { get; internal set; }

        public ModelBall(Vector2 pos)
        {
            this.pos = pos;
        }

        private void OnPropertyChanged(ModelEventArgs args)
        {
            ChangedPosition?.Invoke(this, args);
        }
        public void UpdateDrawBalls(Object s, ModelEventArgs e)
        {
            IBall ball = (IBall)s;
            x = ball.Pos.X;
            y = ball.Pos.Y;
        }

        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}