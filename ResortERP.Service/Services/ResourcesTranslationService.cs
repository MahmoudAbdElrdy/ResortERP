using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class ResourcesTranslationService : IResourcesTranslationService, IDisposable
    {
        private IGenericRepository<ResourcesTranslation> resourcesTranslationRepo;
        private IGenericRepository<Resources> resourcesRepo;


        public ResourcesTranslationService(IGenericRepository<ResourcesTranslation> resourcesTranslationRepo, IGenericRepository<Resources> resourcesRepo)
        {
            this.resourcesTranslationRepo = resourcesTranslationRepo;
            this.resourcesRepo = resourcesRepo;
        }

        public Task<int> CountAsync(char type)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ResourcesTranslation entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ResourcesTranslation customer)
        {
            throw new NotImplementedException();
        }

        public List<ResourcesTranslationVM> GetAll()
        {
            var q = from entity in resourcesTranslationRepo.GetAll()
                    join r in resourcesRepo.GetAll() on entity.ResourcesID equals r.ID
                    select new ResourcesTranslationVM
                    {
                        ID = entity.ID,
                        LanguageID = entity.LanguageID,
                        ResourcesID = entity.ResourcesID,
                        FieldName = entity.FieldName,
                        ARDescription = entity.ARDescription,
                        LatDescription = entity.LatDescription,
                        Position = entity.Position,
                        DisplayORNot = entity.DisplayORNot,
                        ValidationSymbole = entity.ValidationSymbole,
                        ARValidationMessage = entity.ARValidationMessage,
                        LatValidationMessage = entity.LatValidationMessage,
                        ARGridColumnText = entity.ARGridColumnText,
                        LatGridColumnText = entity.LatGridColumnText,
                        ARPlaceholderText = entity.ARPlaceholderText,
                        LatPlaceholderText = entity.LatPlaceholderText,
                        ResourceName = r.ResourceName,
                        IsRequired= entity.IsRequired,
                        InputDataType= entity.InputDataType
                    };
            return q.ToList();
        }

        public bool Insert(ResourcesTranslation entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(ResourcesTranslation entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(ResourcesTranslation entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ResourcesTranslation entity)
        {
            return Task.Run<bool>(() =>
            {
                ResourcesTranslation resourcesTranslation = new ResourcesTranslation
                {
                    ID = entity.ID,
                    LanguageID = entity.LanguageID,
                    ResourcesID = entity.ResourcesID,
                    FieldName = entity.FieldName,
                    ARDescription = entity.ARDescription,
                    LatDescription = entity.LatDescription,
                    Position = entity.Position,
                    DisplayORNot = entity.DisplayORNot,
                    ValidationSymbole = entity.ValidationSymbole,
                    ARValidationMessage = entity.ARValidationMessage,
                    LatValidationMessage = entity.LatValidationMessage,
                    ARGridColumnText = entity.ARGridColumnText,
                    LatGridColumnText = entity.LatGridColumnText,
                    GridColumnHeader= entity.GridColumnHeader,
                    GridColumnWidth= entity.GridColumnWidth,
                    GridColumnHeight = entity.GridColumnHeight,
                    ResourceGroup= entity.ResourceGroup,
                    Notes = entity.Notes,
                    ARPlaceholderText= entity.ARPlaceholderText,
                    LatPlaceholderText= entity.LatPlaceholderText
                };
                resourcesTranslationRepo.Update(resourcesTranslation, resourcesTranslation.ID);
                return true;
            });
        }

        public void Dispose()
        {
            resourcesTranslationRepo.Dispose();
        }

        public ResourcesTranslation GetByID(int id)
        {
            return resourcesTranslationRepo.GetByID(id);
        }

        public Task<bool> UpdateAsyncByID(ResourcesTranslation entity)
        {
            return Task.Run<bool>(() =>
            {
                var curr_entity = resourcesTranslationRepo.GetByID(entity.ID);

                ResourcesTranslation resourcesTranslation = new ResourcesTranslation
                {
                    ID = curr_entity.ID,
                    LanguageID = curr_entity.LanguageID,
                    ResourcesID = curr_entity.ResourcesID,
                    FieldName = curr_entity.FieldName,
                    ARDescription = curr_entity.ARDescription,
                    LatDescription = curr_entity.LatDescription,
                    Position = curr_entity.Position,
                    DisplayORNot = entity.DisplayORNot,
                    ValidationSymbole = curr_entity.ValidationSymbole,
                    ARValidationMessage = (!string.IsNullOrEmpty(entity.ARValidationMessage)) ? entity.ARValidationMessage : curr_entity.ARValidationMessage,
                    LatValidationMessage = curr_entity.LatValidationMessage,
                    ARGridColumnText = (!string.IsNullOrEmpty(entity.ARGridColumnText)) ? entity.ARGridColumnText : curr_entity.ARGridColumnText,
                    LatGridColumnText = (!string.IsNullOrEmpty(entity.LatGridColumnText)) ? entity.LatGridColumnText : curr_entity.LatGridColumnText,
                    GridColumnHeader = curr_entity.GridColumnHeader,
                    GridColumnWidth = curr_entity.GridColumnWidth,
                    GridColumnHeight = curr_entity.GridColumnHeight,
                    ResourceGroup = curr_entity.ResourceGroup,
                    Notes = curr_entity.Notes,
                    ARPlaceholderText = (!string.IsNullOrEmpty(entity.ARPlaceholderText)) ? entity.ARPlaceholderText : curr_entity.ARPlaceholderText,
                    LatPlaceholderText = (!string.IsNullOrEmpty(entity.LatPlaceholderText)) ? entity.LatPlaceholderText : curr_entity.LatPlaceholderText,
                    IsRequired = entity.IsRequired,
                    InputDataType = entity.InputDataType
                };
                resourcesTranslationRepo.Update(resourcesTranslation, resourcesTranslation.ID);
                return true;
            });
        }
    }
}
