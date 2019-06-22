using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class Resources
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string Code { get; set; }

        public string ResourceName { get; set; }

        public string ARName { get; set; }

        public string LatName { get; set; }

        public int UserGroupID { get; set; }


        public string ARResourceTitle { get; set; }
        public string LatResourceTitle { get; set; }
        public string ARHeader { get; set; }
        public string LatHeader { get; set; }
        public string ARHeaderText { get; set; }
        public string LatHeaderText { get; set; }
        public string ARAddHeader { get; set; }
        public string LatAddHeader { get; set; }
        public string ARAddHeaderText { get; set; }
        public string LatAddHeaderText { get; set; }
        public string AREditHeader { get; set; }
        public string LatEditHeader { get; set; }
        public string AREditHeaderText { get; set; }
        public string LatEditHeaderText { get; set; }
        public string ARGridHeader { get; set; }
        public string LatGridHeader { get; set; }


        public string ARGridHeaderText { get; set; }
        public string LatGridHeaderText { get; set; }
        public string ARNewButton { get; set; }
        public string LatNewButton { get; set; }
        public string NewButtonColor { get; set; }
        public string ARNewButtonToolTip { get; set; }
        public string LatNewButtonToolTip { get; set; }
        public string ARAddButton { get; set; }
        public string LatAddButton { get; set; }
        public string AddButtonColor { get; set; }
        public string ARAddButtonToolTip { get; set; }
        public string LatAddButtonToolTip { get; set; }
        public string ARUpdateButton { get; set; }
        public string LatUpdateButton { get; set; }
        public string UpdateButtonColor { get; set; }
        public string ARUpdateButtonToolTip { get; set; }
        public string LatUpdateButtonToolTip { get; set; }
        public string ARDeleteButton { get; set; }
        public string LatDeleteButton { get; set; }
        public string DeleteButtonColor { get; set; }
        public string ARDeleteButtonToolTip { get; set; }
        public string LatDeleteButtonToolTip { get; set; }
        public string ARRefreshButton { get; set; }
        public string LatRefreshButton { get; set; }
        public string RefreshButtonColor { get; set; }
        public string ARRefreshButtonToolTip { get; set; }
        public string LatRefreshButtonToolTip { get; set; }
        public string ARSearchButton { get; set; }
        public string LatSearchButton { get; set; }
        public string SearchButtonColor { get; set; }
        public string ARSearchButtonToolTip { get; set; }
        public string LatSearchButtonToolTip { get; set; }
        public string ARCloseButton { get; set; }
        public string LatCloseButton { get; set; }
        public string CloseButtonColor { get; set; }
        public string ARCloseButtonToolTip { get; set; }
        public string LatCloseButtonToolTip { get; set; }
        public string ARBrowseButton { get; set; }
        public string LatBrowseButton { get; set; }
        public string BrowseButtonColor { get; set; }
        public string ARBrowseButtonToolTip { get; set; }
        public string LatBrowseButtonToolTip { get; set; }
        public string ARPrintButton { get; set; }
        public string LatPrintButton { get; set; }
        public string PrintPrintColor { get; set; }
        public string ARPrintButtonToolTip { get; set; }
        public string LatPrintButtonToolTip { get; set; }
        public string ARImportButton { get; set; }
        public string LatImportButton { get; set; }
        public string ImportPrintColor { get; set; }
        public string ARImportButtonToolTip { get; set; }
        public string LatImportButtonToolTip { get; set; }
        public string ARExportButton { get; set; }
        public string LatExportButton { get; set; }
        public string ExportPrintColor { get; set; }
        public string ARExportButtonToolTip { get; set; }
        public string LatExportButtonToolTip { get; set; }
        public string ARCreateMessage { get; set; }
        public string LatCreateMessage { get; set; }
        public int? CreateMessageDisplayORNot { get; set; }
        public string ARUpdateMessage { get; set; }
        public string LatUpdateMessage { get; set; }
        public int? UpdateMessageDisplayORNot { get; set; }
        public string ARAddDoneMessage { get; set; }
        public string LatAddDoneMessage { get; set; }
        public int? AddDoneMessageDisplayORNot { get; set; }

        public string ARUpdateDoneMessage { get; set; }
        public string LatUpdateDoneMessage { get; set; }
        public int? UpdateDoneMessageDisplayORNot { get; set; }
        public string ARDeleteDoneMessage { get; set; }
        public string LatDeleteDoneMessage { get; set; }
        public int? DeleteDoneMessageDisplayORNot { get; set; }
        public string ARDeleteConfirmationMessage { get; set; }
        public string LatDeleteConfirmationMessage { get; set; }
        public int? DeleteConfirmationMessageDisplayORNot { get; set; }
        public string ARDeleteCancelMessage { get; set; }
        public string LatDeleteCancelMessage { get; set; }
        public string ARDeleteRestoreMessage { get; set; }
        public string LatDeleteRestoreMessage { get; set; }

        public string ARTotalRecordsText { get; set; }
        public string LatTotalRecordsText { get; set; }

        public string ARAddOtherButton { get; set; }
        public string LatAddOtherButton { get; set; }

        public string  ARDoneText { get; set; }
        public string LatDoneText { get; set; }
        public string AROkButton { get; set; }
        public string LatOkButton { get; set; }
        public string ARCancelButton { get; set; }
        public string LatCancelButton { get; set; }

        public string ARSorryText { get; set; }
        public string LatSorryText { get; set; }

        public string ARErrorMessage { get; set; }
        public string LatErrorMessage { get; set; }


        public string ARAddErrorMessage { get; set; }
        public string LatAddErrorMessage { get; set; }
        public string  ARUpdateErrorMessage { get; set; }
        public string LatUpdateErrorMessage { get; set; }

        public string ARGroupOne { get; set; }
        public string LatGroupOne { get; set; }
        public string ARGroupTwo { get; set; }
        public string LatGroupTwo { get; set; }
        public string ARGroupThree { get; set; }
        public string LatGroupThree { get; set; }
        public string ARGroupFour { get; set; }
        public string LatGroupfour { get; set; }
        public string Notes { get; set; }


    }
}
