using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{

    public class UID_View
    {
        [Key]
        public int ID { get; set; }
        public string UID { get; set; }
        public string UPWD { get; set; }
        public Nullable<bool> SavePWD { get; set; }
        public Nullable<double> SHIFT_NUMBER { get; set; }
        public string USER_LETTER { get; set; }
        public int USER_TYPE { get; set; }
        public Nullable<int> UserGroupID { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public string CS_DataSource { get; set; }
        public string CS_InitialCatalog { get; set; }
        public string CS_UserId { get; set; }
        public string CS_Password { get; set; }
        public string DbDescriptionName { get; set; }
        public Nullable<System.DateTime> DbCreationDate { get; set; }
        public Nullable<System.DateTime> DbFirstPeriodDate { get; set; }
        public Nullable<System.DateTime> DbEndPeriodDate { get; set; }
        public string COMPANY_AR_NAME { get; set; }
        public string COMPANY_EN_NAME { get; set; }
        public string COMPANY_AR_ADRESS { get; set; }
        public string COMPANY_EN_ADRESS { get; set; }
        public string COMPANY_TELEPHONE { get; set; }
        public string COMPANY_FAX { get; set; }
        public byte[] COMPANY_LOGO { get; set; }
        public string DELETEDORNOT { get; set; }
        public string COMPANY_EMAIL { get; set; }
        public string COMPANY_ACTIVATION_CODE { get; set; }
        public Nullable<bool> IS_ACTIVATED { get; set; }
        public Nullable<short> MaxUsersNumber { get; set; }
        public string CommercialRegister { get; set; }
        public string TaxNumber { get; set; }
    }
}
