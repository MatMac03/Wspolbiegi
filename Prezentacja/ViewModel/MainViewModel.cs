using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public RelayCommand startSim { get; set; }
        public RelayCommand stopSim { get; set; }
 
        private ModelAbstractAPI api;
        public int ilosc;
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
            api = ModelAbstractAPI.CreateModelAPI(750, 350, chooseBallAmount);
            IDisposable observer = api.Subscribe<IModelBall>(x => ballsToDraw.Add(x));
            foreach (IModelBall b in api.getBalls())
            {
                ballsToDraw.Add(b);
            }
            api.StartSim();
        }

        private void stopSimHandler(object obj)
        {
            api.StopSim();
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