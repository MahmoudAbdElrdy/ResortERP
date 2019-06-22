using ResortERP.Core;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class ResourcesService : IResourcesService, IDisposable
    {
        private IGenericRepository<Resources> resourcesRepo;


        public ResourcesService(IGenericRepository<Resources> resourcesRepo)
        {
            this.resourcesRepo = resourcesRepo;
        }

        public Task<int> CountAsync(char type)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Resources entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Resources customer)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            resourcesRepo.Dispose();
        }

        public List<Resources> GetAll()
        {
            var q = from entity in resourcesRepo.GetAll()
                    select new Resources
                    {
                        ID = entity.ID,
                        Code = entity.Code,
                        ResourceName = entity.ResourceName,
                        ARName = entity.ARName,
                        LatName = entity.LatName,
                        UserGroupID = entity.UserGroupID,

                        ARHeader = entity.ARHeader,
                        LatHeader = entity.LatHeader,

                        ARGridHeaderText = entity.ARGridHeaderText,
                        LatGridHeaderText = entity.LatGridHeaderText,

                        // Buttons
                        ARNewButton = entity.ARNewButton,
                        LatNewButton = entity.LatNewButton,

                        ARRefreshButton = entity.ARRefreshButton,
                        LatRefreshButton = entity.LatRefreshButton,

                        ARBrowseButton = entity.ARBrowseButton,
                        LatBrowseButton = entity.LatBrowseButton,

                        ARPrintButton = entity.ARPrintButton,
                        LatPrintButton = entity.LatPrintButton,

                        ARSearchButton = entity.ARSearchButton,
                        LatSearchButton = entity.LatSearchButton,

                        ARExportButton = entity.ARExportButton,
                        LatExportButton = entity.LatExportButton,

                        ARImportButton = entity.ARImportButton,
                        LatImportButton = entity.LatImportButton,

                        ARCloseButton = entity.ARCloseButton,
                        LatCloseButton = entity.LatCloseButton,

                        ARAddOtherButton= entity.ARAddOtherButton,
                        LatAddOtherButton= entity.LatAddOtherButton,

                        //AlertButton
                        AROkButton = entity.AROkButton,
                        LatOkButton = entity.LatOkButton,

                        ARCancelButton = entity.ARCancelButton,
                        LatCancelButton = entity.LatCancelButton,

                        ARDoneText = entity.ARDoneText,
                        LatDoneText = entity.LatDoneText,

                        ARSorryText = entity.ARSorryText,
                        LatSorryText = entity.LatSorryText,

                        ARAddErrorMessage = entity.ARAddErrorMessage,
                        LatAddErrorMessage = entity.LatAddErrorMessage,

                        ARUpdateErrorMessage = entity.ARUpdateErrorMessage,
                        LatUpdateErrorMessage = entity.LatUpdateErrorMessage,

                        //Groups
                        ARGroupOne = entity.ARGroupOne,
                        ARGroupTwo = entity.ARGroupTwo,
                        ARGroupThree = entity.ARGroupThree,
                        ARGroupFour = entity.ARGroupFour,

                        LatGroupOne = entity.LatGroupOne,
                        LatGroupTwo = entity.LatGroupTwo,
                        LatGroupThree = entity.LatGroupThree,
                        LatGroupfour = entity.LatGroupfour,

                        //Update
                        ARUpdateButton = entity.ARUpdateButton,
                        LatUpdateButton = entity.LatUpdateButton,

                        ARUpdateMessage = entity.ARUpdateMessage,
                        LatUpdateMessage = entity.LatUpdateMessage,

                        ARUpdateDoneMessage = entity.ARUpdateDoneMessage,
                        LatUpdateDoneMessage = entity.LatUpdateDoneMessage,

                        //Delete
                        ARDeleteButton = entity.ARDeleteButton,
                        LatDeleteButton = entity.LatDeleteButton,

                        ARDeleteConfirmationMessage = entity.ARDeleteConfirmationMessage,
                        LatDeleteConfirmationMessage = entity.LatDeleteConfirmationMessage,

                        ARDeleteDoneMessage = entity.ARDeleteDoneMessage,
                        LatDeleteDoneMessage = entity.LatDeleteDoneMessage,

                        ARDeleteCancelMessage = entity.ARDeleteCancelMessage,
                        LatDeleteCancelMessage = entity.LatDeleteCancelMessage,

                        ARDeleteRestoreMessage = entity.ARDeleteRestoreMessage,
                        LatDeleteRestoreMessage = entity.LatDeleteRestoreMessage,

                        //Insert
                        ARAddButton = entity.ARAddButton,
                        LatAddButton = entity.LatAddButton,

                        ARAddDoneMessage = entity.ARAddDoneMessage,
                        LatAddDoneMessage = entity.LatAddDoneMessage,

                        //Text
                        ARTotalRecordsText = entity.ARTotalRecordsText,
                        LatTotalRecordsText = entity.LatTotalRecordsText,

                    };
            return q.ToList();
        }

        public Resources GetByResourceName(string ResourceName)
        {
            return resourcesRepo.Filter(x => x.ResourceName == ResourceName).Single();
        }

        public bool Insert(Resources entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Resources entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Resources entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Resources entity)
        {
            throw new NotImplementedException();
        }
    }
}
