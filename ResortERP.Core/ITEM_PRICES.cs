using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public  class ITEM_PRICES
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int ITEM_UNIT_ID { get; set; }
        public Nullable<double> WHOLE_PRICE { get; set; }
        public Nullable<double> HALF_WHOLE_PRICE { get; set; }
        public Nullable<double> EMP_PRICE { get; set; }
        public Nullable<double> EXPORT_PRICE { get; set; }
        public Nullable<double> RETAIL_PRICE { get; set; }
        public Nullable<double> CONSUMER_PRICE { get; set; }
        public Nullable<double> LAST_BUY_PRICE { get; set; }
    }
  

}
