using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public RelayCommand startSim { get; set; }
        public RelayCommand stopSim { get; set; }
 
        private ModelAbstractAPI api;
        public int ilosc;
        private Task task;
        public ObservableCollection<IModelBall> ballsToDraw { get; } = new ObservableCollection<IModelBall>();

        public MainViewModel()
        {
            startSim = new RelayCommand(startSimHandler);
            stopSim = new RelayCommand(stopSimHandler);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void startSimHandler(object obj)
        {
            api = ModelAbstractAPI.CreateModelAPI();
            getTableParam(750, 350, chooseBallAmount);
            IDisposable observer = api.Subscribe<IModelBall>(x => ballsToDraw.Add(x));
            foreach (IModelBall b in api.getBalls())
            {
                ballsToDraw.Add(b);
            }
            task = new Task(() =>
            {
                api.StartSim();
            });
            task.Start();
        }

        private void getTableParam(int x, int y, int ilosc)
        {
            api.getTableParam(x, y, ilosc);
        }

        private void stopSimHandler(object obj)
        {
            api.StopSim();
            ballsToDraw.Clear();
            task.Dispose();
        }

        public int chooseBallAmount
        {
            get { return ilosc; }
            set
            {
                ilosc = value;
            }
        }
    }
}