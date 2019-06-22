using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{

    public class SmartSysDataBases_View
    {
        [Key]
        public int DBID { get; set; }
        public string DbName { get; set; }
        public string DbDescriptionName { get; set; }
        public System.DateTime DbCreationDate { get; set; }
        public System.DateTime DbFirstPeriodDate { get; set; }
        public System.DateTime DbEndPeriodDate { get; set; }
        public string COMPANY_AR_NAME { get; set; }
        public string COMPANY_EN_NAME { get; set; }
        public string COMPANY_AR_ADRESS { get; set; }
        public string COMPANY_EN_ADRESS { get; set; }
        public string COMPANY_TELEPHONE { get; set; }
        public string COMPANY_FAX { get; set; }
        public byte[] COMPANY_LOGO { get; set; }
        public string DELETEDORNOT { get; set; }
    }
}
