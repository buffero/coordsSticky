using ShowCoords.Services;
using System;
using System.Xml.Serialization;

namespace ShowCoords
{    
    [Serializable]
    public class CoordsItemViewModel : ViewModelBase, ICanSerialize
    {
        private readonly INetherCoordsEvaluator _coordsEvaluator;        
        private const int DefaultMultiplier = 8;
        private bool _manualEntry = false;

        private CoordsItemViewModel()
        {
            _coordsEvaluator = new NetherCoordsEvaluator(DefaultMultiplier);
        }

        public CoordsItemViewModel(INetherCoordsEvaluator coordsEvaluator)
        {
            _coordsEvaluator = coordsEvaluator;            
        } 

        private string _coordsX;
        [XmlAttribute(AttributeName = "CoordsX")]
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
        [XmlAttribute(AttributeName = "CoordsZ")]
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
        [XmlIgnore]
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
        [XmlIgnore]
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
        [XmlAttribute(AttributeName = "Desc")]
        public string Desc
        {
            get { return _desc; }
            set
            {
                _desc = value;
                NotifyPropertyChanged("Desc");
            }
        }

        public bool CanSerialize()
        {
            return !string.IsNullOrEmpty(CoordsX) && !string.IsNullOrEmpty(CoordsZ);
        }
    }
}