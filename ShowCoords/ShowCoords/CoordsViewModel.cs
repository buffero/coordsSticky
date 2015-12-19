using ShowCoords.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShowCoords
{
    internal class CoordsViewModel : ViewModelBase
    {
        public CoordsViewModel(INetherCoordsEvaluator coordsEvaluator)
        {
            IsExtendedUiVisible = false;
            QuitAppCommand = new RelayCommand(QuitAppExecute, () => true);
            ExtendPanelCommand = new RelayCommand(ExtendPanel, () => true);
            DisplayedCoords = new CoordsItemViewModel(coordsEvaluator);
        }        

        public ICommand QuitAppCommand
        {
            get;
            internal set;
        }

        public ICommand ExtendPanelCommand
        {
            get;
            internal set;
        }       

        private void ExtendPanel()
        {
            IsExtendedUiVisible = !_isExtendedUiVisible;
        }


        private void QuitAppExecute()
        {
            App.Current.Shutdown();
        }

        private CoordsItemViewModel _displayedCoords;
        public CoordsItemViewModel DisplayedCoords
        {
            get { return _displayedCoords; }
            set
            {
                _displayedCoords = value;
                NotifyPropertyChanged("DisplayedCoords");
            }
        }

        private ObservableCollection<CoordsItemViewModel> _coordsList;
        public ObservableCollection<CoordsItemViewModel> CoordsList
        {
            get { return _coordsList; }
            set
            {
                _coordsList = value;
                NotifyPropertyChanged("CoordsList");
            }            
        }
        
        private bool _isExtendedUiVisible;
        public bool IsExtendedUiVisible
        {
            get { return _isExtendedUiVisible; }
            set {
                _isExtendedUiVisible = value;
                NotifyPropertyChanged("IsExtendedUiVisible");
            }
        }
    }
}