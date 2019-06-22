﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class AddressPartsSettings
    {
        [Key]
        public int AddressPartID { get; set; }
        public string AddressPart1 { get; set; }
        public string AddressPart2 { get; set; }
        public string AddressPart3 { get; set; }
        public string AddressPart4 { get; set; }
        public string UID { get; set; }
    }
}
