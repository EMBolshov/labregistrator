using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using LabRegistrator.Models;

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
        private string _testJson;

        public string testJson
        {
            get { return _testJson; }
            set
            {
                _testJson = value;
                OnPropertyChanged(nameof(testJson));
            }
        }
        public Specimen[] Specimen { get; set; }
        private ObservableCollection<NomWrapper> _SelectedSpecimen;

        public ObservableCollection<NomWrapper> SelectedSpecimen
        {
            get
            {
                return _SelectedSpecimen;
            }
            set
            {
                _SelectedSpecimen = value;
                OnPropertyChanged(nameof(SelectedSpecimen));
            }
        }

        private ObservableCollection<analyticsrequests> _NomWrapperSpecimens;

        public ObservableCollection<analyticsrequests> NomWrapperSpecimens
        {
            get
            {
                return _NomWrapperSpecimens;
            }
            set
            {
                _NomWrapperSpecimens = value;
                OnPropertyChanged(nameof(NomWrapperSpecimens));
            }
        }



        public SpicemenSelectionViewModel(NomWrapper nmList)
        {
            NomWrapperSpecimens = new ObservableCollection<analyticsrequests>();
            _nmList = nmList;
            Specimen = nmList.specimen;
            ShowSpecimens();
        
         
        }

        public void ShowSpecimens()
        {
            NomWrapperSpecimens.Add(new analyticsrequests()
            {
                bodysite_code = "dsas",
                container_type = "dsda",
                id = "dsad",
                specimen_code = "dsadas"
            });
            foreach (Specimen spec in _nmList.specimen)
            {
                try
                {
                    if (spec != null)
                    {
                        NomWrapperSpecimens.Add(new analyticsrequests()
                        {
                            bodysite_code = (string) spec.bodysite_code,
                            container_type = (string) spec.container_type,
                            id = _nmList.id,
                            specimen_code = spec.specimen_code
                        });
                    }
                }
                catch (NullReferenceException e)
                {
                    
                }
                
            }
            convertToJson();
        }

        public void convertToJson()
        {
            var json = new JavaScriptSerializer().Serialize(NomWrapperSpecimens);
            testJson = (string) json;

        }

        public class SpecWrapper : analyticsrequests
        {

            public SpecWrapper(NomWrapper source, BaseCommand selectAction, BaseCommand deleteAction)
            {
                foreach (Specimen spec in source.specimen)
                {
                    
                }
            }
        }

    }
}
