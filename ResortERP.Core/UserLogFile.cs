using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class UserLogFile
    {
        [Key]
        public int ID { get; set; }
        public string UID { get; set; }
        public string FormName { get; set; }
        public string OpName { get; set; }
        public Nullable<System.DateTime> OpDate { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> DateOnly { get; set; }
        public string Code { get; set; }
        public string TypeName { get; set; }
        public int? Comp_Brach { get; set; }
        public string IDCRUD { get; set; }
        public string URL { get; set; }
    }
}
