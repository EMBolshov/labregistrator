using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabRegistrator
{
    public class SpicemenSelectionViewModel
    {
        private PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly NomWrapper _nmList;
        public Specimen[] Specimen { get; set; }
        private ObservableCollection<NomWrapper> _SelectedSpecimens;

        public ObservableCollection<NomWrapper> SelectedSpecimens
        {
            get
            {
                return _SelectedSpecimens;
            }
            set
            {
                _SelectedSpecimens = value;
                OnPropertyChanged(nameof(SelectedSpecimens));
            }
        }
        public SpicemenSelectionViewModel(NomWrapper nmList)
        {
            _nmList = nmList;
            Specimen = nmList.specimen;
        }

    }
}
