using ShowCoords.Services;

namespace ShowCoords
{
    public class CoordsItemViewModel : ViewModelBase
    {
        private readonly INetherCoordsEvaluator _coordsEvaluator;

        public CoordsItemViewModel(INetherCoordsEvaluator coordsEvaluator)
        {
            _coordsEvaluator = coordsEvaluator;
        } 

        private const decimal Multiplier = 8;
        private bool _manualEntry = false;

        private string _coordsX;
        public string CoordsX
        {
            get { return _coordsX; }
            set
            {
                _coordsX = value;
                NotifyPropertyChanged("CoordsX");
                if (!_manualEntry)
                {
                    _manualEntry = true;
                    NetherCoordsX = _coordsEvaluator.EvaluateCoordsToNether(CoordsX).ToString();
                }

                _manualEntry = false;
            }
        }

        private string _coordsZ;
        public string CoordsZ
        {
            get { return _coordsZ; }
            set
            {
                _coordsZ = value;
                NotifyPropertyChanged("CoordsZ");

                if (!_manualEntry)
                {
                    _manualEntry = true;
                    NetherCoordsZ = _coordsEvaluator.EvaluateCoordsToNether(CoordsZ).ToString();
                }

                _manualEntry = false;
            }
        }

        private string _netherCoordsX;
        public string NetherCoordsX
        {
            get { return _netherCoordsX; }
            set
            {
                _netherCoordsX = value;
                NotifyPropertyChanged("NetherCoordsX");
                if (!_manualEntry)
                {
                    _manualEntry = true;
                    CoordsX = _coordsEvaluator.EvaluateCoordsToOverworld(NetherCoordsX).ToString();
                }

                _manualEntry = false;
            }
        }

        private string _nethercoordsZ;
        public string NetherCoordsZ
        {
            get { return _nethercoordsZ; }
            set
            {
                _nethercoordsZ = value;
                NotifyPropertyChanged("NetherCoordsZ");

                if (!_manualEntry)
                {
                    _manualEntry = true;
                    CoordsZ = _coordsEvaluator.EvaluateCoordsToOverworld(NetherCoordsZ).ToString();
                }

                _manualEntry = false;
            }
        }

        private string _desc;
        public string Desc
        {
            get { return _desc; }
            set
            {
                _desc = value;
                NotifyPropertyChanged("Desc");
            }
        }
    }
}