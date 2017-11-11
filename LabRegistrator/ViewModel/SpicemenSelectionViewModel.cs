using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Input;
using LabRegistrator.Models;
using System.Runtime.Serialization;
using Castle.Core.Internal;
using LabRegistrator.Annotations;
using LabRegistrator.ViewModel;
using Microsoft.Win32.SafeHandles;

namespace LabRegistrator
{
    public class SpicemenSelectionViewModel
    {
        private PropertyChangedEventHandler PropertyChanged;
        public ICommand ClosingRequest { get; set; }
        private string _SendSpecimens { get; set; }
        public string SendSpecimens
        {
            get { return _SendSpecimens; }
            set
            {
                _SendSpecimens = value;
                OnPropertyChanged(nameof(SendSpecimens));
            }
        }
        public analyticsrequests[] SpecimensForQuestinary;
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
        private ObservableCollection<SpecWrapper> _SelectedSpecimen;

        public ObservableCollection<SpecWrapper> SelectedSpecimen
        {
            get { return _SelectedSpecimen; }
            set
            {
                _SelectedSpecimen = value;
                OnPropertyChanged(nameof(SelectedSpecimen));
            }
        }
     
        private ObservableCollection<SpecWrapper> _NomWrapperSpecimens;
        public ObservableCollection<SpecWrapper> NomWrapperSpecimens
        {
            get { return _NomWrapperSpecimens; }
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

        public void UpdateButton()
        {
            var b = (ClosingRequest as BaseCommand);
            if (b == null)
                throw  new Exception($"Command is not {typeof(BaseCommand).Name}");

            var isAny = NomWrapperSpecimens.Any(x => x.addToRequest);
            b.SetEnabled(isAny);
        }

        public void WrapAllSpecimensForCurrentResearch(Specimen[] AvalibleSpecimens)
        {
            foreach (Specimen singleAvalibleSpecimen in AvalibleSpecimens)
            {
                try
                {
                    if (singleAvalibleSpecimen != null)
                    {
                        var s = new SpecWrapper()
                        {
                            addToRequest = false,
                            bodysite_code = (string) singleAvalibleSpecimen.bodysite_code,
                            container_type = (string) singleAvalibleSpecimen.container_type,
                            id = _nmList.id,
                            specimen_code = singleAvalibleSpecimen.specimen_code,
                            description = singleAvalibleSpecimen.description
                        };
                        s.PropertyChanged += S_PropertyChanged;
                        NomWrapperSpecimens.Add(s);
                    }
                }
                catch (NullReferenceException e)
                {

                }

            }
            convertToJson();
        }

        private void S_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SpecWrapper.addToRequest))
                UpdateButton();
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

        public class SpecWrapper : analyticsrequests, INotifyPropertyChanged
        {
            private bool _addToRequest;
            [IgnoreDataMember]
            public bool addToRequest
            {
                get { return _addToRequest; }
                set
                {
                    if (value == _addToRequest) return;
                    _addToRequest = value;
                    OnPropertyChanged1();
                }
            }
            [IgnoreDataMember]
            public string description { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged1([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}


