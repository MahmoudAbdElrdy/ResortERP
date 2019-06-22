using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class EMPLOYEES
    {
        [Key]
        public short EMP_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_AR_NAME { get; set; }
        public string EMP_EN_NAME { get; set; }
        public string EMP_NATIONAL_ID { get; set; }
        public Nullable<int> ACC_ID { get; set; }
        public byte EMP_TYPE_ID { get; set; }
        public Nullable<int> COM_BRN_ID { get; set; }
        public Nullable<short> DEPT_ID { get; set; }
        public string EMP_BARCODE { get; set; }
        public byte[] EMP_BARCODE_IMAGE { get; set; }
        public byte[] EMP_PHOTO { get; set; }
        public string EMP_REMARKS { get; set; }
        public short NATIONALITY_ID { get; set; }
        public short NATION_ID { get; set; }
        public short GOV_ID { get; set; }
        public Nullable<int> TOWN_ID { get; set; }
        public Nullable<long> VILLAGE_ID { get; set; }
        public string EMP_ADDR_REMARKS { get; set; }
        public string EMP_STATE { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public string Visa_Number { get; set; }
        public string Pass_Number { get; set; }
        public Nullable<System.DateTime> Visa_ExpDateM { get; set; }
        public Nullable<System.DateTime> Visa_ExpDateH { get; set; }
        public Nullable<System.DateTime> Pass_ExpDate { get; set; }
    }
}
