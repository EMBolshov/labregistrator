using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabRegistrator
{
    public class NmWindowViewModel
    {
        
            public string Description { get; set; }
            public string ContainerType { get; set; }
            public string NomenclatureID { get; set; }

            public string[] PatientPrep { get; set; }


            public NmWindowViewModel(NomWrapper nmWrap)
            {
                DescriptionReturn(nmWrap);
                PatientPrep = nmWrap.patient_preparation;
                NomenclatureID = nmWrap.id;
            }

            private void DescriptionReturn(NomWrapper nmWrap)
            {
                Description = nmWrap.description;
            }



        

    }
}
