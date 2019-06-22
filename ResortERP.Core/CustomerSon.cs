using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class CustomerSon
    {
        [Key]
        public int CUSTOMER_SON_ID { get; set; }
        public int CUSTOMER_ID { get; set; }
        public string CUSTOMER_SON_NAME { get; set; }
        public System.DateTime CUSTOMER_BIRTHDATE { get; set; }
        public bool CUSTOMER_SON_SEX { get; set; }
        public int CUSTOMER_SON_ORDER { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
    }
}
