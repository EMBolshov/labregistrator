namespace LabRegistrator
{
    public class Rootobject
    {
        public NomenclatureList[] Nomenclature { get; set; }
    }

    public class NomenclatureList
    {
        public string id { get; set; }
        public string lab_id { get; set; }
        public string caption { get; set; }
        public string group { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string[] patient_preparation { get; set; }
        public Specimen[] specimen { get; set; }
        public Required_Specimen[] required_specimen { get; set; }
    }

    public class Specimen
    {
        public string description { get; set; }
        public string specimen_code { get; set; }
        public string specimen_name { get; set; }
        public object bodysite_code { get; set; }
        public string bodysite_name { get; set; }
        public object container_type { get; set; }
        public string container_name { get; set; }
    }

    public class Required_Specimen
    {
        public string description { get; set; }
        public string specimen_code { get; set; }
        public string specimen_name { get; set; }
        public object bodysite_code { get; set; }
        public string bodysite_name { get; set; }
        public object container_type { get; set; }
        public string container_name { get; set; }
    }
}