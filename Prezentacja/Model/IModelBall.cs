using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dane;

namespace Model
{
    public interface IModelBall : INotifyPropertyChanged
    {
        Vector2 pos { get; }
        double x { get; }
        double y { get; }
        float r { get; }
#nullable enable
        event EventHandler<ModelEventArgs>? ChangedPosition;
        event PropertyChangedEventHandler? PropertyChanged;
        void UpdateDrawBalls(Object s, ModelEventArgs e);
        void RaisePropertyChanged([CallerMemberName] string? propertyName = null);
    }
    public class BallChangeEventArgs : EventArgs
    {
        public IBall Ball { get; internal set; }
    }
}
