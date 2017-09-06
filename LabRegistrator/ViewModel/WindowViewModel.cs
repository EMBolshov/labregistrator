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

//using LabRegistrator.View;

namespace LabRegistrator
{
    public class WindowViewModel : INotifyPropertyChanged
    {
        private int _tabNumber;
        
        public int TabNumber { get { return _tabNumber; } set { if (_tabNumber == value) return; _tabNumber = value; OnPropertyChanged(nameof(TabNumber)); } }

        private string _name;
        private string _status;
        public string Status
        {get { return _status; } set { if (_status == value) return; _status = value; OnPropertyChanged(nameof(Status)); } }
        public string Name { get { return _name; } set { if (_name == value) return; _name = value; OnPropertyChanged(nameof(Name)); } }

        public ICommand Authorize { get; set; }
        public ICommand GetNomen { get; set; }
        public ICommand AddNomen { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand NextStepCommand { get; set; }
        public ICommand SendOrderCommand { get; set; }

        public ICommand GetTestQuesti { get; set; }

        private ObservableCollection<NomWrapper> _items;
        public ObservableCollection<NomWrapper> Items { get { return _items; } set { _items = value; OnPropertyChanged(nameof(Items)); } }

        private string _contract;
        private string _token;
        public string Token
        { get { return _token; } set { if (_token == value) return; _token = value; OnPropertyChanged(nameof(Token)); } }
        public string Contract
        { get { return _contract; } set { if (_contract == value) return; _contract = value; OnPropertyChanged(nameof(Contract)); } }
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
        }

        #region Commands
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

        public void getTestQuest()
        {
            var httpResp = new Response();
            var requestn = new RequestToMDO();
            var response = httpResp.ResponseBasicHandler<QuestinaryBasicModel>(requestn.getQuestinary());
        }

        public void ShowNom()
        {
            var httpResp = new Response();
            var requestN = new RequestToMDO();
            var response = httpResp.ResponseBasicHandler<NomenclatureList[]>(requestN.getNomenclature());
            Items = new ObservableCollection<NomWrapper>(response.Select(x =>
            {
                var add = new OneTimeCommand(() => { AddSelected(); }, true);
                var rem = new EnableInnerCommand(() => { DeleteSelected(); }, true, add);
                var inf = new BaseCommand(() => { showAdditional(); }, true);

                var n = new NomWrapper(x, add, rem, inf);
                return n;
            }));

        }

        //private void WrapNomenList(NomenclatureList[] nomen)
        //{
            
        //}

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
        private void showAdditional()
        {
            var vm = new NmWindowViewModel(SelectedItem);
            var showAdd = new NomenclatureInfo(vm);
            showAdd.ShowDialog();
        }

        private void AddSelected()
        {
            if (SelectedItem != null)
            {
                ChosenItems.Add(SelectedItem);
                if (SelectedItem.specimen.Length > 1)
                {
                var vm = new SpicemenSelectionViewModel(SelectedItem);
                var showAdd = new SpicemenSelection(vm);
                showAdd.ShowDialog();
                }
               
            }
        }

        private void DeleteSelected()
        {
            if (CartSelectedItem != null)
                ChosenItems.Remove(CartSelectedItem);
        }

        #endregion

        #region INPC


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class NomWrapper : NomenclatureList
    {
        public ICommand Select { get; set; }
        public ICommand Delete { get; set; }
        public ICommand ShowInfo { get; set; }

        public NomWrapper(NomenclatureList source, BaseCommand selectAction, BaseCommand deleteAction, BaseCommand showInfo)
        {
            if (selectAction == null) throw new ArgumentNullException(nameof(selectAction));
            if (deleteAction == null) throw new ArgumentNullException(nameof(deleteAction));
            if (showInfo == null) throw new ArgumentNullException(nameof(showInfo));
            id = source.id;
            caption = source.caption;
            group = source.group;
            description = source.description;
            patient_preparation = source.patient_preparation;
            specimen = source.specimen;
            Select = selectAction;
            Delete = deleteAction;
            ShowInfo = showInfo;
        }
    }
    public class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _action;
        private bool _enabled = true;
        public BaseCommand(Action action, bool enabled)
        {
            _enabled = enabled;
            _action = action;
        }

        public void Invert()
        {
            _enabled = false;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Enable()
        {
            _enabled = true;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return _enabled;
        }

        public virtual void Execute(object parameter)
        {
            _action.Invoke();
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

