using Logika;
using System;
using System.Reactive.Linq;
using System.ComponentModel;
using System.Reactive;


namespace Model
{
    public abstract class ModelAbstractAPI : IObservable<IModelBall>
    {
        public static ModelAbstractAPI CreateModelAPI(int x, int y, int amount)
        {
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI(700, 300, amount);
            Model model = new Model(api, amount);
            return model;
        }
        public abstract void StartSim();
        public abstract void StopSim();
        public abstract IDisposable Subscribe(IObserver<IModelBall> observer);
        public abstract ModelBall[] getBalls();

        public abstract event PropertyChangedEventHandler PropertyChanged;
    }


    internal class Model : ModelAbstractAPI
    {
        private IObservable<EventPattern<BallChangeEventArgs>> eventObservable = null;

        public LogicAbstractAPI sim { get; set; }
        public override event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<BallChangeEventArgs> BallChanged;
        public IObservable<EventHandler> ballsChanged;
        ModelBall[] ModelBall;

        public override ModelBall[] getBalls()
        {
            return ModelBall;
        }

        public Model(LogicAbstractAPI api, int ilosc)
        {
            eventObservable = Observable.FromEventPattern<BallChangeEventArgs>(this, "BallChanged");
            sim = api;
            ModelBall = new ModelBall[ilosc];
            for (int i = 0; i < ilosc; i++)
            {
                ModelBall ball = new ModelBall(api.getPozycja()[i][0], api.getPozycja()[i][1]);
                ModelBall[i] = ball;
                api.PropertyChanged += OnBallChanged;
            }
        }
        private void OnBallChanged(object sender, PropertyChangedEventArgs args)
        {
            if (ModelBall[0].x != sim.getPozycja()[0][0] && ModelBall[0].y != sim.getPozycja()[0][1])
            {
                UpdatePosition();
            }
        }
        private void UpdatePosition()
        {
            for (int i = 0; i < sim.getPozycja().Length; i++)
            {
                ModelBall[i].x = sim.getPozycja()[i][0];
                ModelBall[i].y = sim.getPozycja()[i][1];
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