using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Input;
using LabRegistrator.Models;
using System.Runtime.Serialization;
using Microsoft.Win32.SafeHandles;

namespace LabRegistrator
{
    public class SpicemenSelectionViewModel
    {
        private PropertyChangedEventHandler PropertyChanged;
        public ICommand  ClosingRequest { get; set; }

        public analyticsrequests[] SpecimensForQuestinary;
        private readonly NomWrapper _nmList;
        private string _testJson;
        public string testJson
        {
            get
            {
                return _testJson;
            }
            set
            {
                _testJson = value;
                OnPropertyChanged(nameof(testJson));
            }
        }
        public Specimen[] Specimen { get; set; }
        private ObservableCollection<SpecWrapper> _SelectedSpecimen;
        public ObservableCollection<SpecWrapper> SelectedSpecimen
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
       

        private ObservableCollection<SpecWrapper> _NomWrapperSpecimens;

        public ObservableCollection<SpecWrapper> NomWrapperSpecimens
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
            _nmList = nmList;
            Specimen = nmList.specimen;
            NomWrapperSpecimens = new ObservableCollection<SpecWrapper>();
            WrapAllSpecimensForCurrentResearch(Specimen);


        }

        public void WrapAllSpecimensForCurrentResearch(Specimen[] AvalibleSpecimens)
        {
            foreach (Specimen singleAvalibleSpecimen in AvalibleSpecimens)
            {
                try
                {
                    if (singleAvalibleSpecimen != null)
                    {
                        NomWrapperSpecimens.Add(new SpecWrapper()
                        {
                            addToRequest = false,
                            bodysite_code = (string) singleAvalibleSpecimen.bodysite_code,
                            container_type = (string) singleAvalibleSpecimen.container_type,
                            id = _nmList.id,
                            specimen_code = singleAvalibleSpecimen.specimen_code,
                            description = singleAvalibleSpecimen.description
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







        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class SpecWrapper : analyticsrequests
        {
            private bool _addToRequest;

            [IgnoreDataMember]
            public bool addToRequest
            {
                get { return _addToRequest; }
                set
                {
                    _addToRequest = value;
                }
            }
            [IgnoreDataMember]
            public string description { get; set; }

        }
    }
    }


