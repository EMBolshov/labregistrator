using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabRegistrator
{
    public class SpicemenSelectionViewModel
    {
        public Specimen[] _specimen { get; set; } 
        public SpicemenSelectionViewModel(NomWrapper nmList)
        {
            _specimen = nmList.specimen;
        }

        public void SelectSpecimenCommand()
        {
            
        }
    }
}
