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
using System.Windows.Shapes;
using System.ComponentModel;


namespace LabRegistrator
{
    /// <summary>
    /// Interaction logic for NomenclatureInfo.xaml
    /// </summary>
    public partial class NomenclatureInfo : Window
    {
        public NomenclatureInfo(NomWrapper datacontext)
        {
            InitializeComponent();
            DataContext = new NmWindowViewModel(datacontext);
           
        }
        
        public class NmWindowViewModel
        {
            private string description { get; set; }
            private string 
            public NmWindowViewModel(NomWrapper nmWrap)
            {

            }

            private string DescriptionReturn (NomWrapper nmWrap)
            {
                return nmWrap.description;
            }

        }

    }
}
