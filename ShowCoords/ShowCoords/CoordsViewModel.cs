﻿using ShowCoords.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;

namespace ShowCoords
{
    internal class CoordsViewModel : ViewModelBase
    {
        private readonly INetherCoordsEvaluator _coordsEvaluator;
        private readonly ICoordsCollectionPersistanceService<CoordsItemViewModel> _persistanceService;

        public CoordsViewModel(INetherCoordsEvaluator coordsEvaluator, ICoordsCollectionPersistanceService<CoordsItemViewModel> persistanceService)
        {            
            _persistanceService = persistanceService;
            _coordsEvaluator = coordsEvaluator;
            IsExtendedUiVisible = false;
            QuitAppCommand = new RelayCommand(QuitAppExecute, () => true);
            ExtendPanelCommand = new RelayCommand(ExtendPanel, () => true);
            DisplayedCoords = new CoordsItemViewModel(coordsEvaluator);
            CoordsList = new ObservableCollection<CoordsItemViewModel>(PrepareCoordsList());
            DisplayedCoords = CoordsList.First();            
        }

        private IEnumerable<CoordsItemViewModel> PrepareCoordsList()
        {
            var count = System.Configuration.ConfigurationSettings.AppSettings["NumberOfInitialItems"].ToString();
            var numberOfItems = int.Parse(count);
            var persistedCoords = _persistanceService.LoadFromFile();

            return Enumerable.Range(1, numberOfItems).Select(p => persistedCoords != null &&  p - 1 < persistedCoords.Count() ? persistedCoords.ElementAt(p - 1) : new CoordsItemViewModel(_coordsEvaluator));
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
            _persistanceService.PersistToFile(CoordsList);
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