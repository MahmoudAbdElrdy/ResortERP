using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class ENTRY_GRID_COLUMNS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ENTRY_SETTING_ID { get; set; }
        public byte CREDIT_INDEX { get; set; }
        public short CREDIT_WIDTH { get; set; }
        public int CREDIT_COLOR { get; set; }
        public string CREDIT_COLOR_HEXA { get; set; }
        public byte DEBIT_INDEX { get; set; }
        public short DEBIT_WIDTH { get; set; }
        public int DEBIT_COLOR { get; set; }
        public string DEBIT_COLOR_HEXA { get; set; }
        public byte ACC_INDEX { get; set; }
        public short ACC_WIDTH { get; set; }
        public int ACC_COLOR { get; set; }
        public string ACC_COLOR_HEXA { get; set; }
        public byte COST_CENTER_INDEX { get; set; }
        public short COST_CENTER_WIDTH { get; set; }
        public int COST_CENTER_COLOR { get; set; }
        public string COST_CENTER_COLOR_HEXA { get; set; }
        public byte REMARKS_INDEX { get; set; }
        public short REMARKS_WIDTH { get; set; }
        public int REMARKS_COLOR { get; set; }
        public string REMARKS_COLOR_HEXA { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }

        public bool? IS_CREDIT { get; set; }
        public bool? IS_DEBIT { get; set; }
        public bool? IS_ACC { get; set; }
        public bool? IS_COST_CENTER { get; set; }
        public bool? IS_REMARKS { get; set; }



        public byte? TaxValue_INDEX { get; set; }
        public short? TaxValue_WIDTH { get; set; }
        public int? TaxValue_COLOR { get; set; }
        public string TaxValue_COLOR_HEXA { get; set; }
        public bool? IS_TaxValue { get; set; }


        public byte? Taxable_INDEX { get; set; }
        public short? Taxable_WIDTH { get; set; }
        public int? Taxable_COLOR { get; set; }
        public string Taxable_COLOR_HEXA { get; set; }
        public bool? IS_Taxable { get; set; }


        public byte? GoldDepit24_INDEX { get; set; }
        public short? GoldDepit24_WIDTH { get; set; }
        public int? GoldDepit24_COLOR { get; set; }
        public string GoldDepit24_COLOR_HEXA { get; set; }
        public bool? IS_GoldDepit24 { get; set; }


        public byte? GoldCredit24_INDEX { get; set; }
        public short? GoldCredit24_WIDTH { get; set; }
        public int? GoldCredit24_COLOR { get; set; }
        public string GoldCredit24_COLOR_HEXA { get; set; }
        public bool? IS_GoldCredit24 { get; set; }


        public byte? GoldDepit22_INDEX { get; set; }
        public short? GoldDepit22_WIDTH { get; set; }
        public int? GoldDepit22_COLOR { get; set; }
        public string GoldDepit22_COLOR_HEXA { get; set; }
        public bool? IS_GoldDepit22 { get; set; }


        public byte? GoldCredit22_INDEX { get; set; }
        public short? GoldCredit22_WIDTH { get; set; }
        public int? GoldCredit22_COLOR { get; set; }
        public string GoldCredit22_COLOR_HEXA { get; set; }
        public bool? IS_GoldCredit22 { get; set; }

        public byte? GoldDepit21_INDEX { get; set; }
        public short? GoldDepit21_WIDTH { get; set; }
        public int? GoldDepit21_COLOR { get; set; }
        public string GoldDepit21_COLOR_HEXA { get; set; }
        public bool? IS_GoldDepit21 { get; set; }


        public byte? GoldCredit21_INDEX { get; set; }
        public short? GoldCredit21_WIDTH { get; set; }
        public int? GoldCredit21_COLOR { get; set; }
        public string GoldCredit21_COLOR_HEXA { get; set; }
        public bool? IS_GoldCredit21 { get; set; }

        public byte? GoldDepit18_INDEX { get; set; }
        public short? GoldDepit18_WIDTH { get; set; }
        public int? GoldDepit18_COLOR { get; set; }
        public string GoldDepit18_COLOR_HEXA { get; set; }
        public bool? IS_GoldDepit18 { get; set; }


        public byte? GoldCredit18_INDEX { get; set; }
        public short? GoldCredit18_WIDTH { get; set; }
        public int? GoldCredit18_COLOR { get; set; }
        public string GoldCredit18_COLOR_HEXA { get; set; }
        public bool? IS_GoldCredit18 { get; set; }

        public byte? TaxRate_INDEX { get; set; }
        public short? TaxRate_WIDTH { get; set; }
        public int? TaxRate_COLOR { get; set; }
        public string TaxRate_COLOR_HEXA { get; set; }
        public bool? IS_TaxRate { get; set; }

        public byte? CheckNumber_INDEX { get; set; }
        public short? CheckNumber_WIDTH { get; set; }
        public int? CheckNumber_COLOR { get; set; }
        public string CheckNumber_COLOR_HEXA { get; set; }
        public bool? IsCheckNumber { get; set; }

        public byte? CheckDate_INDEX { get; set; }
        public short? CheckDate_WIDTH { get; set; }
        public int? CheckDate_COLOR { get; set; }
        public string CheckDate_COLOR_HEXA { get; set; }
        public bool? IsCheckDate { get; set; }

        public byte? CheckIssueDate_INDEX { get; set; }
        public short? CheckIssueDate_WIDTH { get; set; }
        public int? CheckIssueDate_COLOR { get; set; }
        public string CheckIssueDate_COLOR_HEXA { get; set; }
        public bool? IsCheckIssueDate { get; set; }



        public byte? ExemptOfTax_INDEX { get; set; }
        public short? ExemptOfTax_WIDTH { get; set; }
        public int? ExemptOfTax_COLOR { get; set; }
        public string ExemptOfTax_COLOR_HEXA { get; set; }
        public bool? IsExemptOfTax { get; set; }



        public string ARExemptOfTax { get; set; }
        public string ENExemptOfTax { get; set; }



        public string ARCheckNumber { get; set; }
        public string ENCheckNumber { get; set; }

        public string ARCheckDate { get; set; }
        public string ENCheckDate { get; set; }

        public string ARCheckIssueDate { get; set; }
        public string ENCheckIssueDate { get; set; }

        public string ARTaxValue { get; set; }
        public string ENTaxValue { get; set; }

        public string ARCode { get; set; }
        public string ENCode { get; set; }
        public string ARAccountName { get; set; }
        public string ENAccountName { get; set; }
        public string ARCredit { get; set; }
        public string ENCredit { get; set; }
        public string ARDepit { get; set; }
        public string ENDepit { get; set; }
        public string ARCredit24 { get; set; }
        public string ENCredit24 { get; set; }
        public string ARDepit24 { get; set; }
        public string ENDepit24 { get; set; }
        public string ARCredit22 { get; set; }
        public string ENCredit22 { get; set; }
        public string ARDepit22 { get; set; }
        public string ENDepit22 { get; set; }
        public string ARCredit21 { get; set; }
        public string ENCredit21 { get; set; }
        public string ARDepit21 { get; set; }
        public string ENDepit21 { get; set; }
        public string ARCredit18 { get; set; }
        public string ENCredit18 { get; set; }
        public string ARDepit18 { get; set; }
        public string ENDepit18 { get; set; }
        public string ARCostCenter { get; set; }
        public string ENCostCenter { get; set; }
        public string ARRemarks { get; set; }
        public string ENRemarks { get; set; }
        public string ARTaxable { get; set; }
        public string EnTaxable { get; set; }
        public string ARTaxRate { get; set; }
        public string ENTaxRate { get; set; }

        

    }
}
