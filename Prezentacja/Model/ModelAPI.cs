using Logika;
using System;
using System.Reactive.Linq;
using System.ComponentModel;
using System.Reactive;


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
        public abstract event PropertyChangedEventHandler PropertyChanged;
    }


    internal class Model : ModelAbstractAPI
    {
        private IObservable<EventPattern<BallChangeEventArgs>> eventObservable = null;

        public LogicAbstractAPI sim { get; set; }
        public override event PropertyChangedEventHandler PropertyChanged;
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
            for (int i = 0; i < ilosc; i++)
            {
                ModelBall ball = new ModelBall(sim.getPozycja()[i][0], sim.getPozycja()[i][1]);
                modelBall[i] = ball;
                sim.PropertyChanged += OnBallChanged;
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

        private void OnBallChanged(object sender, PropertyChangedEventArgs args)
        {
            if (modelBall[0].x != sim.getPozycja()[0][0] && modelBall[0].y != sim.getPozycja()[0][1])
            {
                UpdatePosition();
            }
        }
        private void UpdatePosition()
        {
            for (int i = 0; i < sim.getPozycja().Length; i++)
            {
                modelBall[i].x = sim.getPozycja()[i][0];
                modelBall[i].y = sim.getPozycja()[i][1];
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
            return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.Ball), ex => observer.OnError(ex), () => observer.OnCompleted());
        }
    }
}