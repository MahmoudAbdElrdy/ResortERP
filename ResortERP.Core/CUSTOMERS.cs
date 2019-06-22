using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class CUSTOMERS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ACC_ID { get; set; }
        public string CUST_CODE { get; set; }
        public string CUST_AR_NAME { get; set; }
        public string CUST_EN_NAME { get; set; }
        public string CUST_TITLE { get; set; }
        public short NATIONALITY_ID { get; set; }
        public short NATION_ID { get; set; }
        public short GOV_ID { get; set; }
        public Nullable<int> TOWN_ID { get; set; }
        public Nullable<long> VILLAGE_ID { get; set; }
        public string CUST_ADDR_REMARKS { get; set; }
        public Nullable<int> Comp_Bran_ID { get; set; }
        public Nullable<int> Dept_ID { get; set; }

        public string CUST_EMAIL { get; set; }
        public string CUST_WEB_SITE { get; set; }
        public byte[] CUST_PHOTO { get; set; }
        public string CUST_ZIP_CODE { get; set; }
        public string CUST_POST_BOX { get; set; }
        public byte SELL_TYPE_ID { get; set; }
        public string SHASIHNUMBER { get; set; }
        public string ENGINENUMBER { get; set; }
        public string CARMETERVALUE { get; set; }
        public string CARNUMBER { get; set; }
        public Nullable<int> THINKABOUTSTORE { get; set; }
        public Nullable<int> HEARABOUTUS { get; set; }
        public Nullable<int> ISTHISTHEFIRSTTIME { get; set; }
        public string ANYCOMMENTS { get; set; }
        public string ANYCOMPLAINT { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public bool? SubjectToVAT { get; set; }
        public double? VATRate { get; set; }
        public string TaxNumber { get; set; }
        public string CommercialRegister { get; set; }

        public double? CreditOpeningAccount { get; set; }
        public double? DepitOpeningAccount { get; set; }

        public string CustomerAdminARName { get; set; }
        public string CustomerAdminENName { get; set; }
        public string CustomerAdminTelephone { get; set; }
    }
}
