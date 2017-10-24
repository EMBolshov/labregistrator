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
    /// Interaction logic for SpicemenSelection.xaml
    /// </summary>
    public partial class SpicemenSelection : Window
    {
        public SpicemenSelection(SpicemenSelectionViewModel datacontext)
        {
            InitializeComponent();
            DataContext = datacontext;
            datacontext.ClosingRequest = new BaseCommand(()=>this.Close()) ;

        }

    }
}
