using System;
using System.ComponentModel;

namespace ShowCoords
{
    internal class CoordsViewModel : INotifyPropertyChanged
    {
        private const decimal Multiplier = 8;

        private bool _manualEntry = false;

        public CoordsViewModel() 
        {
        }

        private string _coordsX;
        public string CoordsX
        {
            get { return _coordsX; }
            set {
                _coordsX = value;
                NotifyPropertyChanged("CoordsX");
                if (!_manualEntry)
                {
                    _manualEntry = true;
                    NetherCoordsX = EvaluateCoordsToNether(CoordsX).ToString();
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
                    NetherCoordsZ = EvaluateCoordsToNether(CoordsZ).ToString();
                }

                _manualEntry = false;
            }
        }        

        private decimal EvaluateCoordsToOverworld(string coord) {
            return EvaluateCoordsOperand(coord, (a, b) => a * b);
        }

        private decimal EvaluateCoordsToNether(string coord)
        {
            return EvaluateCoordsOperand(coord, (a, b) => a / b);
        }

        private decimal EvaluateCoordsOperand(string coord, Func<decimal, decimal, decimal> operand)
        {
            decimal result;
            if (decimal.TryParse(coord, out result))
            {
                return Math.Floor(operand.Invoke(result, Multiplier));                
            };
            return 0;
        }

        private string _netherCoordsX;
        public string NetherCoordsX
        {
            get { return _netherCoordsX; }
            set { _netherCoordsX = value;
                NotifyPropertyChanged("NetherCoordsX");
                if (!_manualEntry)
                {
                    _manualEntry = true;
                    CoordsX = EvaluateCoordsToOverworld(NetherCoordsX).ToString();
                }

                _manualEntry = false;
            }
        }

        private string _nethercoordsZ;
        public string NetherCoordsZ
        {
            get { return _nethercoordsZ; }
            set { _nethercoordsZ = value;
                NotifyPropertyChanged("NetherCoordsZ");

                if (!_manualEntry)
                {
                    _manualEntry = true;
                    CoordsZ = EvaluateCoordsToOverworld(NetherCoordsZ).ToString();
                }

                _manualEntry = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}