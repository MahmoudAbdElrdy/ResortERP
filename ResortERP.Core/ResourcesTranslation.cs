using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class ResourcesTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int LanguageID { get; set; }

        public int ResourcesID { get; set; }

        public string FieldName { get; set; }

        public string ARDescription { get; set; }

        public string LatDescription { get; set; }

        public int? Position { get; set; }

        public int? DisplayORNot { get; set; }

        public string ValidationSymbole { get; set; }

        public string ARValidationMessage { get; set; }

        public string LatValidationMessage { get; set; }

        public string ARGridColumnText { get; set; }

        public string LatGridColumnText { get; set; }

        public int? GridColumnHeader { get; set; }
        public int? GridColumnWidth { get; set; }
        public int? GridColumnHeight { get; set; }

        public string ResourceGroup { get; set; }

        public string Notes { get; set; }

        public string ARPlaceholderText { get; set; }

        public string LatPlaceholderText { get; set; }

        public bool IsRequired { get; set; }

        public int? InputDataType { get; set; }

    }
}
