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

namespace LabRegistrator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Token postmantoken = new Token();
        // Contract contract = new Contract();
        // Response httpResp = new Response();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new WindowViewModel();
        }
    }
    public class WindowViewModel : INotifyPropertyChanged
    {
        Token postmantoken = new Token();
        Contract contract = new Contract();
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
                if (_status == value)
                    return;
                _status = value;
                OnPropertyChanged(nameof(Status));
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
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged(nameof(Name));
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




        public ICommand Msg { get; set; }
        public ICommand GetNomen { get; set; }
        public ICommand AddNomen { get; set; }

        private ObservableCollection<NomWrapper> _items;
        public ObservableCollection<NomWrapper> Items { get { return _items; } set { _items = value; OnPropertyChanged(nameof(Items)); } }

        private string _contract = "C000035569";
        private string _token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjU2NjU0MzNCMjg2ODM1QjFERDg2OTRDRTUzRkYzQUE1RTYyNDFBNUQiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJWbVZET3lob05iSGRocFRPVV84NnBlWWtHbDAifQ.eyJuYmYiOjE1MDIwOTI3NjMsImV4cCI6MTUwMjEyODc2MywiaXNzIjoiaHR0cHM6Ly9hdXRoLXN0YWdlLm1lZGxpbngub25saW5lIiwiYXVkIjpbImh0dHBzOi8vYXV0aC1zdGFnZS5tZWRsaW54Lm9ubGluZS9yZXNvdXJjZXMiLCJmaGlyQVBJIl0sImNsaWVudF9pZCI6InRlc3RwZXB5YWthIiwic3ViIjoiZDE3OTBmODEtODQyMi00OWI1LWJkZWYtZjFhMjgwYTZlMWM1IiwiYXV0aF90aW1lIjoxNTAyMDkyNzYyLCJpZHAiOiJsb2NhbCIsImZoaXItZHN0dTIiOiJmaGlyLyovJCoiLCJzY29wZSI6WyJtaXMiXSwiYW1yIjpbInB3ZCJdfQ.TzkLJ6Lj22RfNZ1V_PWx0kyoOCvST7R8GDcinkspfiWD900vUmlgdqAul03JuA36vOqD809fu24aqgr-f1wtqwgp3H12KTaP5tztNYpNlXmJZQtcFqzKX5c1GexwD2ydw8oakNYL5f-Vl7ba4Kcq26XE8YQt0ox4PkpUHGtAvtCzlXg_srgltruX3MJEqdQF8j8eqHuiDvsmGcDjvtYl2DjPThbvl6aahclxkVFk9DcWYy4NRAszNMFwcFPD9YZrKEM77JYbrD7bina6554FawMjZQMIlpBRMLFLeqWtWU4xkEkuS91CAzkOknopymgh9mmE35l8wgcn1mU80Y9fPw";

        public ObservableCollection<NomWrapper> ChosenItems { get; set; }
        public WindowViewModel()
        {
            Msg = new BaseCommand(Auth, true);
            GetNomen = new BaseCommand(ShowNom, true);
            Name = "Text";
            Status = "Выберите действие";
            ChosenItems = new ObservableCollection<NomWrapper>();
        }

        #region Commands

        public void Auth()
        {
            // postmantoken.Value = TokenTb.Text;
            postmantoken.Value = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjU2NjU0MzNCMjg2ODM1QjFERDg2OTRDRTUzRkYzQUE1RTYyNDFBNUQiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJWbVZET3lob05iSGRocFRPVV84NnBlWWtHbDAifQ.eyJuYmYiOjE1MDIwOTI3NjMsImV4cCI6MTUwMjEyODc2MywiaXNzIjoiaHR0cHM6Ly9hdXRoLXN0YWdlLm1lZGxpbngub25saW5lIiwiYXVkIjpbImh0dHBzOi8vYXV0aC1zdGFnZS5tZWRsaW54Lm9ubGluZS9yZXNvdXJjZXMiLCJmaGlyQVBJIl0sImNsaWVudF9pZCI6InRlc3RwZXB5YWthIiwic3ViIjoiZDE3OTBmODEtODQyMi00OWI1LWJkZWYtZjFhMjgwYTZlMWM1IiwiYXV0aF90aW1lIjoxNTAyMDkyNzYyLCJpZHAiOiJsb2NhbCIsImZoaXItZHN0dTIiOiJmaGlyLyovJCoiLCJzY29wZSI6WyJtaXMiXSwiYW1yIjpbInB3ZCJdfQ.TzkLJ6Lj22RfNZ1V_PWx0kyoOCvST7R8GDcinkspfiWD900vUmlgdqAul03JuA36vOqD809fu24aqgr-f1wtqwgp3H12KTaP5tztNYpNlXmJZQtcFqzKX5c1GexwD2ydw8oakNYL5f-Vl7ba4Kcq26XE8YQt0ox4PkpUHGtAvtCzlXg_srgltruX3MJEqdQF8j8eqHuiDvsmGcDjvtYl2DjPThbvl6aahclxkVFk9DcWYy4NRAszNMFwcFPD9YZrKEM77JYbrD7bina6554FawMjZQMIlpBRMLFLeqWtWU4xkEkuS91CAzkOknopymgh9mmE35l8wgcn1mU80Y9fPw";
            // ListV.Text = postmantoken.Value.ToString();
            contract.Value = "C000035569";
            Status = "Установлены значения для токена и контракта.";
        }

        public void ShowNom()
        {
            Response httpResp = new Response();
            reqNomenclature requestN = new reqNomenclature();
            var response = httpResp.ResponseBasicHandler<NomenclatureList[]>((requestN.basicRequest("nomenclature", _contract, _token)));
            Items = new ObservableCollection<NomWrapper>(response.Select(x =>
            {
                var add = new OneTimeCommand(() => { AddSelected(); }, true);
                var rem = new EnableInnerCommand(() => { DeleteSelected(); }, true, add);
                var inf = new BaseCommand(() => { showAdditional(); }, true);

                var n = new NomWrapper(x, add, rem, inf);
                return n;
            }));

        }

        private void showAdditional()
        {
            var comparePatientPrep = "Указания для исследования: \n";
            if (SelectedItem == null) return;
            foreach (var cpp in SelectedItem.patient_preparation)
            {
                comparePatientPrep += cpp + "\n";
            }
            MessageBox.Show(comparePatientPrep);
            // KEK
        }

        private void AddSelected()
        {
            if (SelectedItem != null)
                ChosenItems.Add(SelectedItem);
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
            Invert();
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
