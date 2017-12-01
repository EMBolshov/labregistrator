using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using System.Net.Http;
using System.ComponentModel;
using System.Collections.ObjectModel;
using LabRegistrator.Models;
using LabRegistrator.View;
using LabRegistrator.ViewModel;

namespace LabRegistrator
{
    public class WindowViewModel : INotifyPropertyChanged
    {
        private int _tabNumber;

        public int TabNumber
        {
            get
            {
                return _tabNumber;
            }
            set
            {
                if (_tabNumber == value) return;
                _tabNumber = value;
                OnPropertyChanged(nameof(TabNumber));
            }
        }

        private List<analyticsrequests> testReq;

        public List<analyticsrequests> SendQuestiReq
        {
            get
            {
                return testReq;
            }

            set
            {
                testReq = value;
                OnPropertyChanged(nameof(SendQuestiReq));
            }
        }

        private string _name;
        private string _status;

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status == value) return;
                _status = value; OnPropertyChanged(nameof(Status));
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value) return;
                _name = value; OnPropertyChanged(nameof(Name));
            }

        }

        public ICommand Authorize { get; set; }
        public ICommand GetNomen { get; set; }
        public ICommand AddNomen { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand NextStepCommand { get; set; }
        public ICommand SendOrderCommand { get; set; }
        public ICommand GetTestQuesti { get; set; }

        private ObservableCollection<NomWrapper> _items;
        public ObservableCollection<NomWrapper> Items {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            } }

        private string _contract;
        private string _token;
        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                if (_token == value) return;
                _token = value; OnPropertyChanged(nameof(Token));
            }
        }
        public string Contract
        {
            get
            {
                return _contract;
            }
            set
            {
                if(_contract == value) return;
                _contract = value; OnPropertyChanged(nameof(Contract));
            }
        }
        private NomWrapper _selectedItem;
        public NomWrapper SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem == value)
                    return;
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private NomWrapper _cartSelectedItem;
        public NomWrapper CartSelectedItem
        {
            get
            {
                return _cartSelectedItem;
            }
            set
            {
                if (_cartSelectedItem == value)
                    return;
                _cartSelectedItem = value;
                OnPropertyChanged(nameof(CartSelectedItem));
            }
        }
        public ObservableCollection<NomWrapper> ChosenItems { get; set; }
        public WindowViewModel()
        {
            Authorize = new BaseCommand(Auth, true);
            GetNomen = new BaseCommand(ShowNom, true);
            CancelCommand = new BaseCommand(Cancel, true);
            NextStepCommand = new BaseCommand(NextStep, true);
            SendOrderCommand = new BaseCommand(SendOrder, true);
            GetTestQuesti = new BaseCommand(getTestQuest, true);
            Name = "Text";
            Status = "Выберите действие";
            ChosenItems = new ObservableCollection<NomWrapper>();
            SendQuestiReq = new List<analyticsrequests>();
        }
        public void Auth()
        {
            try
            {
                Status = "Установлены значения для токена и контракта.";
                TabNumber = 1;
                ShowNom();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        public void ShowNom()
        {
            var responseFromApi = new Response();
            var requestN = new RequestToMDO();
            var response = responseFromApi.ResponseToModelConverter<NomenclatureList[]>(requestN.getNomenclature());
            Items = new ObservableCollection<NomWrapper>(response.Select(x =>
            {
                var add = new OneTimeCommand(() => { AddSelected(); }, true);
                var rem = new EnableInnerCommand(() => { DeleteSelected(); }, true, add);
                var inf = new BaseCommand(() => { ShowAdditional(); }, true);

                var n = new NomWrapper(x, add, rem, inf);
                return n;
            }));
        }
        public void getTestQuest()
        {
            var httpResp = new Response();
            var requestn = new RequestToMDO();
            var response = httpResp.ResponseToModelConverter<QuestinaryBasicModel>(requestn.getQuestinary(SendQuestiReq));
        }
        private void Cancel()
        {
            TabNumber--;
        }
        private void NextStep()
        {
            TabNumber++;
        }
        private void SendOrder()
        {
            MessageBox.Show("Заказ отправлен!");
        }
        private void ShowAdditional()
        {
            var vm = new NmWindowViewModel(SelectedItem);
            var showAdd = new NomenclatureInfo(vm);
            showAdd.ShowDialog();
        }

        private void AddSelected()
        {
            List<SpicemenSelectionViewModel.SpecWrapper> TempSpecimenForRequest = new List<SpicemenSelectionViewModel.SpecWrapper>();
            if (SelectedItem != null)
            {

                {
                    var vm = new SpicemenSelectionViewModel(SelectedItem);
                    dynamic specimenSelectionWindow;
                    if (SelectedItem.multiple_specimen == "True")
                    {
                        specimenSelectionWindow = new SpecimenSelectionWithCheckbox(vm);
                    }
                    else
                    {
                        specimenSelectionWindow = new SpicemenSelection(vm);
                    }
                    //if (SelectedItem.required_specimen != null)
                    //{
                    //    TempSpecimenForRequest.AddRange(
                    //        addRequiredSpecimensToRequestList(SelectedItem.id, SelectedItem.required_specimen));
                    //}
                    if (vm.Specimen.Length != 0)
                    {
                        specimenSelectionWindow.ShowDialog();
                        TempSpecimenForRequest.AddRange(AddSpecimenToRequestList(vm));
                    }
                    SendQuestiReq.AddRange(TempSpecimenForRequest);
                    SelectedItem.currentSpecimens.AddRange(TempSpecimenForRequest);
                    ChosenItems.Add(SelectedItem);
                }
            }
        }

        private List<SpicemenSelectionViewModel.SpecWrapper> AddSpecimenToRequestList(SpicemenSelectionViewModel vm)
        {
            var ListWithSpecimens = new List<SpicemenSelectionViewModel.SpecWrapper>();
            foreach (SpicemenSelectionViewModel.SpecWrapper WSpec in vm.NomWrapperSpecimens)
            {
                if (WSpec.addToRequest)
                {
                    ListWithSpecimens.Add(WSpec);
                }
            }

            return ListWithSpecimens;


        }

        private List<SpicemenSelectionViewModel.SpecWrapper> addRequiredSpecimensToRequestList(string resID, Required_Specimen[] specs)
        {
            var ListWithRequariedSpecimens = new List<SpicemenSelectionViewModel.SpecWrapper>();

            foreach (Required_Specimen ReqSpec in specs)
            {
                if (ReqSpec != null)
                {
                    ListWithRequariedSpecimens.Add(new SpicemenSelectionViewModel.SpecWrapper()
                    {
                        bodysite_code = (string)ReqSpec.bodysite_code,
                        container_type = (string)ReqSpec.container_type,
                        description = ReqSpec.description,
                        id = resID,
                        specimen_code = (string)ReqSpec.specimen_code
                    });
                }
            }
            return ListWithRequariedSpecimens;
        }

        private void DeleteSelected()
        {
            if (CartSelectedItem != null)
            {
                DeleteSpecimen(CartSelectedItem);
                ChosenItems.Remove(CartSelectedItem);
                
            }
        }

        private void DeleteSpecimen(NomWrapper CartSelectedItem)
        {
            foreach (analyticsrequests SpecimensForRequest in CartSelectedItem.currentSpecimens)
            {
                SendQuestiReq.Remove(SpecimensForRequest);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    

    public class NomWrapper : NomenclatureList
    {
        public ICommand Select { get; set; }
        public ICommand Delete { get; set; }
        public ICommand ShowInfo { get; set; }

        public List<analyticsrequests> currentSpecimens { get; set; }
        //TODO price
        public NomWrapper(NomenclatureList source, BaseCommand selectAction, BaseCommand deleteAction, BaseCommand showInfo)
        {
            if (selectAction == null) throw new ArgumentNullException(nameof(selectAction));
            if (deleteAction == null) throw new ArgumentNullException(nameof(deleteAction));
            if (showInfo == null) throw new ArgumentNullException(nameof(showInfo));
            id = source.id;
            lab_id = source.lab_id;
            caption = source.caption;
            group = source.group;
            description = source.description;
            multiple_specimen = source.multiple_specimen;
            patient_preparation = source.patient_preparation;
            specimen = source.specimen;
            required_specimen = source.required_specimen;
            currentSpecimens = new List<analyticsrequests>();
            price = source.price;
            Select = selectAction;
            Delete = deleteAction;
            ShowInfo = showInfo;
        }
    }

    public class OneTimeCommand : BaseCommand
    {
        public OneTimeCommand(Action action, bool enabled) : base(action, enabled)
        {
        }

        public override void Execute(object parameter)
        {
            base.Execute(parameter);
            //Invert();
        }
    }

    public class EnableInnerCommand : BaseCommand
    {
        private BaseCommand _command;
        public EnableInnerCommand(Action action, bool enabled, BaseCommand c) : base(action, enabled)
        {
            _command = c;
        }

        public override void Execute(object parameter)
        {
            base.Execute(parameter);
            _command.Enable();
        }
    }
}



