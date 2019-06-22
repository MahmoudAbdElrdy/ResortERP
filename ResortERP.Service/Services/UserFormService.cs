using ResortERP.Core;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;

namespace ResortERP.Service.Services
{
    public class UserFormService : IUserFormService, IDisposable
    {
        IGenericRepository<UserForm> UserFormRepo;
        IGenericRepository<UserLogFile> UserLogFileRepo;
        IGenericRepository<UserMenu> UserMenuRepo;
        IGenericRepository<User> UserRepo;
        IGenericRepository<COMPANY_BRANCHES> CompBranchRepo;
        //public UserFormService()
        //{
        //}
        public UserFormService(
            IGenericRepository<UserForm> UserFormRepo,
            IGenericRepository<UserLogFile> UserLogFileRepo,
            IGenericRepository<UserMenu> UserMenuRepo,
            IGenericRepository<User> UserRepo,
            IGenericRepository<COMPANY_BRANCHES> CompBranchRepo
            )
        {
            this.UserFormRepo = UserFormRepo;
            this.UserLogFileRepo = UserLogFileRepo;
            this.UserMenuRepo = UserMenuRepo;
            this.UserRepo = UserRepo;
            this.CompBranchRepo = CompBranchRepo;
        }

        public void Dispose()
        {
            UserFormRepo.Dispose();
        }

        public List<User> GetAllUsers()
        {
            return UserRepo.GetAll().ToList();
        }
        public List<COMPANY_BRANCHES> GetAllBranshes()
        {
            return CompBranchRepo.GetAll().ToList();
        }
        public List<menuFormSelVM> GetAll()
        {
            var q_parent = from u in UserFormRepo.GetAll()
                           join m in UserMenuRepo.GetAll()
                           on u.UserMenuID equals m.ID
                           where m.Active == true && m.MenuID == null
                           select new UserForm
                           {
                               ID = m.ID,
                               ARName = u.ARName,
                           };

            var q_child = from u in UserFormRepo.GetAll()
                          join m in UserMenuRepo.GetAll()
                          on u.UserMenuID equals m.ID
                          where m.Active == true && m.MenuID != null
                          select new UserMenu
                          {
                              ID = u.ID,
                              ARName = u.ARName,
                              MenuID = m.MenuID
                          };
            List<menuFormSelVM> result = new List<menuFormSelVM>();
            foreach (var item in q_parent)
            {
                //var x = new menuFormSelVM
                //{
                //    name = item.ARName,
                //    category= item.ARName
                //};
                //menuFormSelVM parentData = new menuFormSelVM()
                //{
                //    value = item.ARName,
                //    type = ""
                //};
                //result.Add(parentData);

                foreach (var itemChild in q_child)
                {
                    if (item.ID == itemChild.MenuID)
                    {
                        menuFormSelVM childData = new menuFormSelVM()
                        {
                            id = itemChild.ID,
                            value = itemChild.ARName,
                            type = item.ARName
                        };
                        result.Add(childData);
                    }
                }

                //result.Add(x);
            }
            //foreach(var item in )
            return result;
            //  return UserFormRepo.GetAll().Where(a=>a.UserMenuID!=0).ToList();
        }

        public Task<int> CountAsync(string[] Forms, string StartDate, string EndDate, string UID, string branchId)
        {

            return Task.Run(
                () =>
                {
                    DateTime? SDate = null;
                    DateTime? EDate = null;
                    int? compBranch = null;
                    if (UID == "null")
                    {
                        UID = null;
                    }
                    if (StartDate != "null")
                    {
                        SDate = DateTime.Parse(StartDate);
                    }

                    if (EndDate != "null")
                    {
                        EDate = DateTime.Parse(EndDate);
                    }

                    if (branchId != "null")
                    {
                        compBranch = int.Parse(branchId);
                    }
                    else
                    {
                        compBranch = null;
                    }


                    if (Forms.Length == 0)
                    {
                   //int x= Convert.ToInt32(UserLogFileRepo.CountAsync(a => a.FormName != null && (a.DateOnly >= SDate || SDate == null) && (a.DateOnly <= EDate || EDate == null) && (a.UID == UID || UID == null) && (a.Comp_Brach == compBranch || compBranch == null)).Result);
                    return  UserLogFileRepo.CountAsync(a => a.FormName != null && (a.DateOnly >= SDate || SDate == null) && (a.DateOnly <= EDate || EDate == null) && (a.UID == UID || UID == null) && (a.Comp_Brach == compBranch || compBranch == null));

                   }
                    int x = 0;
                    foreach (var item in Forms)
                    {
                         x += Convert.ToInt32(UserLogFileRepo.CountAsync(a => (a.FormName == item||a.TypeName== item) && (a.DateOnly >= SDate || SDate == null) && (a.DateOnly <= EDate || EDate == null) && (a.UID == UID || UID == null) && (a.Comp_Brach == compBranch || compBranch == null)).Result);

                    }
                    return Task.FromResult(x);

                });
        }

        public Task<int> InsertUserForm(UserMenuVM entity,int id)
        {
            return Task.Run(() =>
            {
                UserForm userForm = new UserForm()
                {
                    UserMenuID = id,
                    ARName = entity.ARName,
                    LatName=entity.LatName

                };
                //var q=UserMenuRepo.GetAll().Where(a => a.ID == entity.MenuID).SingleOrDefault();
                //if (q != null)
                //{
                    if(entity.BillSetiingID!=null && entity.BillSetiingID!=0)
                    {
                        userForm.Code = "BillMaster" + "/" + entity.BillSetiingID;
                    }
                        
                    if (entity.EntrySettingID != null && entity.EntrySettingID != 0)
                        userForm.Code = "EntryMaster" + "/" + entity.EntrySettingID;
                //}

               UserFormRepo.Add(userForm);
                var obj = UserMenuRepo.GetAll().Where(a => a.ID == id).SingleOrDefault();
                if (obj != null)
                {
                    obj.Code= userForm.Code;
                    UserMenuRepo.Update(obj, obj.ID);
                }
                   
                return entity.ID;
            });

        }

        public List<UserLogFile> GetBySelForms(string ID)
        {
            return UserLogFileRepo.GetAll().Where(a => a.FormName == ID).ToList();
        }
       public static bool flag=false;
        public Task<List<UserLogFile>> getByRangDate(string[] Forms, string StartDate, string EndDate, string UID, string branchId, int pageNum, int pageSize)
        {
            return Task.Run<List<UserLogFile>>(
             () =>
             {

                 int rowCount;
                 //var SDate = DateTime.Parse(StartDate);
                 //var EDate = DateTime.Parse(EndDate);
                 DateTime? SDate = null;
                 DateTime? EDate = null;
                 int? compBranch = null;
                 if (UID == "null")
                 {
                     UID = null;
                 }
                 if (StartDate != "null")
                 {
                     SDate = DateTime.Parse(StartDate);
                 }

                 if (EndDate != "null")
                 {
                     EDate = null;
                 }

                 if (branchId != "null")
                 {
                     compBranch = int.Parse(branchId);
                 }
                 else
                 {
                     compBranch = null;
                 }
                 List<UserLogFile> resF = new List<UserLogFile>();
                 if (Forms.Length == 0)
                 {
                    // List<UserLogFile> data = new List<UserLogFile>();
                   //  if (UID != "null")
                   //  {
                         return UserLogFileRepo.GetPaged<int>(pageNum, pageSize, a => (a.DateOnly >= SDate || SDate == null) && (a.DateOnly <= EDate || EDate == null) && (a.UID == UID || UID == null) && (a.Comp_Brach == compBranch || compBranch == null), a => a.ID, false, out rowCount).ToList();
                  //   }
                  //   else
                  //   {
                        // resF = UserLogFileRepo.GetPaged<int>(pageNum, pageSize, a => a.DateOnly >= DateTime.Parse(StartDate) && a.DateOnly <= DateTime.Parse(EndDate), a => a.ID, false, out rowCount).ToList();
                   //  }

                     //if (branchId != "null")
                     //{
                     //    var t = int.Parse(branchId);
                     //    resF = UserLogFileRepo.GetPaged<int>(pageNum, pageSize, a => a.DateOnly >= DateTime.Parse(StartDate) && a.DateOnly <= DateTime.Parse(EndDate), a => a.ID, false, out rowCount).ToList();

                     //    resF = resF.Where(b => b.Comp_Brach == t).ToList();
                     //}
                 }
                 List<UserLogFile> data = new List<UserLogFile>();
                 foreach (var item in Forms)
                 {
                     //List<UserLogFile> data = new List<UserLogFile>();
                     //if (UID != "null")
                     //{
                     //    data = UserLogFileRepo.GetAll().Where(a => (a.FormName == item || a.TypeName == item) && a.UID == UID && a.DateOnly >= DateTime.Parse(StartDate) && a.DateOnly <= DateTime.Parse(EndDate)).ToList();
                     //}
                     //else
                     //{
                     //    data = UserLogFileRepo.GetAll().Where(a => (a.FormName == item || a.TypeName == item) && a.DateOnly >= DateTime.Parse(StartDate) && a.DateOnly <= DateTime.Parse(EndDate)).ToList();

                     //}
                     //if (branchId != "null")
                     //{
                     //    var t = int.Parse(branchId);
                     //    data = data.Where(b => b.Comp_Brach == t).ToList();
                     //}
                     //foreach (var item1 in data)
                     //{
                     //    resF.Add(item1);
                     //}
                     resF = UserLogFileRepo.GetAll().Where(a => (a.FormName == item || a.TypeName == item) && (a.DateOnly >= SDate || SDate == null) && (a.DateOnly <= EDate || EDate == null) && (a.UID == UID || UID == null) && (a.Comp_Brach == compBranch || compBranch == null)).ToList();
                     //if(resF.Count()>= pageSize)
                     //{
                     //    flag = true;
                     //    return resF;
                     //}
                     foreach (var item1 in resF)
                     {
                         data.Add(item1);
                     }
                 }

                 if (pageSize <= 0) pageSize = 10;
                 //Total result count
                 var rowsCount = data.Count();
                 //If page number should be > 0 else set to first page
                 if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;
                 //Calculate nunber of rows to skip on pagesize
                 int excludedRows = (pageNum - 1) * pageSize;
                 //Skip the required rows for the current page and take the next records of pagesize count
                 // return data.Skip(excludedRows).Take(pageSize);
                 return data.Skip(excludedRows).Take(pageSize).ToList();
                 //return resF;


                 //if (StartDate != "null" && EndDate != "null")
                 //{
                 //    var SDate = DateTime.Parse(StartDate);
                 //    var EDate = DateTime.Parse(EndDate);
                 //    List<UserLogFile> resF = new List<UserLogFile>();
                 //    if (Forms.Length == 0)
                 //    {
                 //        List<UserLogFile> data = new List<UserLogFile>();
                 //        if (UID != "null")
                 //        {
                 //            resF = UserLogFileRepo.GetPaged<int>(pageNum, pageSize, a => a.UID == UID && a.DateOnly >= SDate && a.DateOnly <= EDate, a => a.ID, false, out rowCount).ToList();
                 //        }
                 //        else
                 //        {
                 //            resF = UserLogFileRepo.GetPaged<int>(pageNum, pageSize, a => a.DateOnly >= DateTime.Parse(StartDate) && a.DateOnly <= DateTime.Parse(EndDate), a => a.ID, false, out rowCount).ToList();
                 //        }

                 //        if (branchId != "null")
                 //        {
                 //            var t = int.Parse(branchId);
                 //            resF = UserLogFileRepo.GetPaged<int>(pageNum, pageSize, a => a.DateOnly >= DateTime.Parse(StartDate) && a.DateOnly <= DateTime.Parse(EndDate), a => a.ID, false, out rowCount).ToList();

                 //            resF = resF.Where(b => b.Comp_Brach == t).ToList();
                 //        }
                 //    }
                 //    foreach (var item in Forms)
                 //    {
                 //        List<UserLogFile> data = new List<UserLogFile>();
                 //        if (UID != "null")
                 //        {
                 //            data = UserLogFileRepo.GetAll().Where(a => (a.FormName == item || a.TypeName == item) && a.UID == UID && a.DateOnly >= DateTime.Parse(StartDate) && a.DateOnly <= DateTime.Parse(EndDate)).ToList();
                 //        }
                 //        else
                 //        {
                 //            data = UserLogFileRepo.GetAll().Where(a => (a.FormName == item || a.TypeName == item) && a.DateOnly >= DateTime.Parse(StartDate) && a.DateOnly <= DateTime.Parse(EndDate)).ToList();

                 //        }
                 //        if (branchId != "null")
                 //        {
                 //            var t = int.Parse(branchId);
                 //            data = data.Where(b => b.Comp_Brach == t).ToList();
                 //        }
                 //        foreach (var item1 in data)
                 //        {
                 //            resF.Add(item1);
                 //        }
                 //    }
                 //    return resF;
                 //}
                 //else if (StartDate != "null")
                 //{
                 //    List<UserLogFile> resF = new List<UserLogFile>();
                 //    if (Forms.Length == 0)
                 //    {
                 //        List<UserLogFile> data = new List<UserLogFile>();
                 //        if (UID != "null")
                 //        {
                 //            resF = UserLogFileRepo.GetAll().Where(a => a.UID == UID && a.DateOnly >= DateTime.Parse(StartDate)).ToList();
                 //        }
                 //        else
                 //        {
                 //            resF = UserLogFileRepo.GetAll().Where(a => a.DateOnly >= DateTime.Parse(StartDate)).ToList();
                 //        }

                 //        if (branchId != "null")
                 //        {
                 //            var t = int.Parse(branchId);
                 //            resF = resF.Where(b => b.Comp_Brach == t).ToList();
                 //        }
                 //    }
                 //    foreach (var item in Forms)
                 //    {
                 //        List<UserLogFile> data = new List<UserLogFile>();
                 //        if (UID != "null")
                 //        {
                 //            data = UserLogFileRepo.GetAll().Where(a => (a.FormName == item || a.TypeName == item) && a.UID == UID && a.DateOnly >= DateTime.Parse(StartDate)).ToList();
                 //        }
                 //        else
                 //        {
                 //            data = UserLogFileRepo.GetAll().Where(a => (a.FormName == item || a.TypeName == item) && a.DateOnly >= DateTime.Parse(StartDate)).ToList();

                 //        }

                 //        if (branchId != "null")
                 //        {
                 //            var t = int.Parse(branchId);
                 //            data = data.Where(b => b.Comp_Brach == t).ToList();
                 //        }
                 //        foreach (var item1 in data)
                 //        {
                 //            resF.Add(item1);
                 //        }

                 //    }
                 //    return resF;
                 //}
                 //else
                 //{
                 //    List<UserLogFile> resF = new List<UserLogFile>();

                 //    if (Forms.Length == 0)
                 //    {
                 //        List<UserLogFile> data = new List<UserLogFile>();
                 //        if (UID != "null")
                 //        {
                 //            resF = UserLogFileRepo.GetAll().Where(a => a.UID == UID).ToList();
                 //        }
                 //        else
                 //        {
                 //            resF = UserLogFileRepo.GetAll().ToList();
                 //        }
                 //    }

                 //    foreach (var item in Forms)
                 //    {
                 //        List<UserLogFile> data = new List<UserLogFile>();
                 //        if (UID != "null")
                 //        {
                 //            data = UserLogFileRepo.GetAll().Where(a => (a.FormName == item || a.TypeName == item) && a.UID == UID).ToList();
                 //        }
                 //        else
                 //        {
                 //            data = UserLogFileRepo.GetAll().Where(a => (a.FormName == item || a.TypeName == item)).ToList();

                 //        }

                 //        if (branchId != "null")
                 //        {
                 //            var t = int.Parse(branchId);
                 //            data = data.Where(b => b.Comp_Brach == t).ToList();
                 //        }

                 //        foreach (var item1 in data)
                 //        {
                 //            resF.Add(item1);
                 //        }
                 //    }
                 //    return resF;
                 //}
             });

        }
    }

    public class menuFormSelVM
    {
        public int id { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        //public List<menuFormSelVM> children = new List<menuFormSelVM>();
    }
}
