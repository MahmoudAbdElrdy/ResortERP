using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class Telephones
    {
        [Key, Column(Order = 1)]
        public int TELE_ID { get; set; }
        [Key, Column(Order = 2)]
        public short TELE_TYPE_ID { get; set; }
        [Key, Column(Order = 3)]
        public string TELE_NUMBER { get; set; }
        [Key, Column(Order = 4)]
        public short TELE_CAT_ID { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
    }
}
