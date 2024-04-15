using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IModelBall : INotifyPropertyChanged
    {
        float x { get; }
        float y { get; }
        float r { get; }
    }
    public class BallChangeEventArgs : EventArgs
    {
        public IModelBall Ball { get; internal set; }
    }
}