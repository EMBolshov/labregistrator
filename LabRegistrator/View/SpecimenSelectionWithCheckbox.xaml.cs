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

namespace LabRegistrator.View
{
    /// <summary>
    /// Interaction logic for SpecimenSelectionWithCheckbox.xaml
    /// </summary>
    public partial class SpecimenSelectionWithCheckbox : Window
    {
        public SpecimenSelectionWithCheckbox(SpicemenSelectionViewModel datacontext)
        {
            InitializeComponent();
            DataContext = datacontext;
            datacontext.ClosingRequest = new BaseCommand(() => this.Close());
        }
    }
}
