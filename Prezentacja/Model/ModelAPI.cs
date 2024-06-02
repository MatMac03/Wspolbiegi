using Logika;
using System;
using System.Reactive.Linq;
using System.ComponentModel;
using System.Reactive;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Model
{
    public abstract class ModelAbstractAPI : IObservable<IModelBall>
    {
        public static ModelAbstractAPI CreateModelAPI()
        {
            return new Model();
        }
        public abstract void StartSim();
        public abstract void StopSim();
        public abstract IDisposable Subscribe(IObserver<IModelBall> observer);
        public abstract ModelBall[] getBalls();
        public abstract void getTableParam(int x, int y, int ilosc);
        #nullable enable
        public abstract event EventHandler<ModelEventArgs>? ChangedPosition;
    }


    internal class Model : ModelAbstractAPI
    {
        private IObservable<EventPattern<BallChangeEventArgs>> eventObservable = null;

        public LogicAbstractAPI sim { get; set; }
        #nullable enable
        public override event EventHandler<ModelEventArgs>? ChangedPosition;
        public event EventHandler<BallChangeEventArgs> BallChanged;
        public IObservable<EventHandler> ballsChanged;
        ModelBall[] modelBall;

        public override ModelBall[] getBalls()
        {
            return modelBall;
        }

        public override void getTableParam(int x, int y, int ilosc)
        {
            sim.getTableParam(x, y, ilosc);
            modelBall = new ModelBall[ilosc];
            Vector2[] poss = sim.getPozycja();
            for (int i = 0; i < ilosc; i++)
            {
                ModelBall ball = new ModelBall(poss[i]);
                modelBall[i] = ball;
                sim.ChangedPosition += OnBallChanged;
            }
        }


        public Model(LogicAbstractAPI api = null)
        {
            if (api == null)
            {
                this.sim = LogicAbstractAPI.CreateLogicAPI();
            }
            else
            {
                this.sim = api;
            }
            eventObservable = Observable.FromEventPattern<BallChangeEventArgs>(this, "BallChanged");
        }

        public void setLogicAPI(LogicAbstractAPI api)
        {
            sim = api;
        }

        private void OnBallChanged(object sender, LogicEventArgs e)
        {
            LogicAbstractAPI api = (LogicAbstractAPI)sender;
            Dane.IBall[] balls = api.getBalls();
            foreach (Dane.IBall ball in balls)
            {
                UpdatePosition();
            }
        }
        private void UpdatePosition()
        {
            for (int i = 0; i < sim.getPozycja().Length; i++)
            {
                Vector2 pos = sim.getPozycja()[i];
                modelBall[i].x = pos.X;
                modelBall[i].y = pos.Y;
            }
        }

        public override void StartSim()
        {
            //button
            sim.startSim();
        }

        public override void StopSim()
        {
            //button
            sim.stopSim();
        }

        public override IDisposable Subscribe(IObserver<IModelBall> observer)
        {
            return eventObservable.Subscribe(x => observer.OnNext((IModelBall)x.EventArgs.Ball), ex => observer.OnError(ex), observer.OnCompleted);
        }
    }
}