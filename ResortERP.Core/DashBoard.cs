using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class DashBoard
    {
        [Key]
        public int DashBoardId { get; set;}
        public string accountShow { get; set; }
        public int userId { get; set; }
    }
}
