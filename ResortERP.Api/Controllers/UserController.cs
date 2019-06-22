using ResortERP.Api.Classes;
using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class UserController : ApiController
    {
        IUserService userService;
        ICommonService commonservice;
        IUIDViewService uidViewService;
        IUserPriviligeBranchesService userPriviligeBranService;
        ICompanyBranchesService companyBranchesService;
        IUserLogFileService _userLogFileService;
        public UserController(IUserService userService, ICommonService commonService, 
            IUIDViewService uidViewService,
            IUserPriviligeBranchesService userPriviligeBranService,
            ICompanyBranchesService companyBranchesService,
             IUserLogFileService _userLogFileService)
        {
            this.commonservice = commonService;
            this.userService = userService;
            this.uidViewService = uidViewService;
            this.userPriviligeBranService = userPriviligeBranService;
            this.companyBranchesService = companyBranchesService;
            this._userLogFileService = _userLogFileService;
        }

        //[Route("api/GetUser")]
        //public async Task<IHttpActionResult> GetUser(string userName, string password)
        //{
        //    UserVM user = (userService.GetUser(userName, password)).Result;
        //    UID_View uidViw = (uidViewService.GetCompany(user.CS_InitialCatalog)).Result;
        //    if (uidViw.IS_ACTIVATED != null && uidViw.IS_ACTIVATED != false)
        //    {
        //        return Ok(await userService.GetUser(userName, password));
        //    }
        //    else { return null; }
        //}

        [Route("api/GetUser")]
        public HttpResponseMessage GetUser(HttpRequestMessage request, string userName, string password)
        {
            try
            {
                UserVM user = userService.GetUser(userName, password);
                if (user != null)
                {
                    if (userName.Contains("@"))
                    {
                        UID_View uidViw = uidViewService.GetCompany(user.CS_InitialCatalog);
                        if (uidViw.IS_ACTIVATED != null && uidViw.IS_ACTIVATED != false)
                        {
                            user.CodeStatus = 200;
                            return request.CreateResponse(HttpStatusCode.OK, user);
                        }
                        else
                        {
                            user.CodeStatus = 401;
                            return request.CreateResponse(HttpStatusCode.Unauthorized, user);
                        }

                    }
                    else
                    {
                        if (user.IS_ACTIVATED != null && user.IS_ACTIVATED != false)
                        {
                            user.CodeStatus = 200;
                            return request.CreateResponse(HttpStatusCode.OK, user);
                        }
                        else
                        {
                            user.CodeStatus = 401;
                            return request.CreateResponse(HttpStatusCode.Unauthorized, user);
                        }
                    }
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.OK, new UserVM() { CodeStatus = 404 });
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return request.CreateResponse(HttpStatusCode.OK, new UserVM() { CodeStatus = 404 });
            }
        }


        [Route("User/CompanyActivate")]
        [HttpPost]
        public bool activateCompany(activationVM obj)
        {
            UserVM user = userService.GetUser(obj.username, obj.password);
            if (user.COMPANY_ACTIVATION_CODE == obj.activationCode)
            {
                user.IS_ACTIVATED = true;
                bool res = userService.UpdateAsync(user).Result;
                if (res) return true; else return false;
            }
            else { return false; }
        }

        [Route("user/getUserById/{Id}")]
        [HttpGet]
        public async Task<IHttpActionResult> getUserById(int Id)
        {
            return Ok(await userService.getById(Id));
        }

        [Route("user/getInnerAdminById/{Id}")]
        [HttpGet]
        public async Task<IHttpActionResult> getInnerAdminById(int Id)
        {
            return Ok(await userService.getInnerAdminById(Id));
        }

        [Route("User/GetUserData/{userID}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserData(int userID)
        {
            return Ok(await userService.GetUserData(userID));
        }

        [Route("user/activateUser/{Id}")]
        [HttpGet]
        public async Task<IHttpActionResult> ActivateUser(int Id)
        {
            return Ok(await userService.ActivateUser(Id));
        }

        [Route("User/ValidateCurrentPassword/{userID}/{userName}/{password}")]
        [HttpGet]
        public async Task<IHttpActionResult> ValidateCurrentPassword(int userID, string userName, string password)
        {
            return Ok(await userService.ValidateCurrentPassword(userID, userName, password));
        }

        [Route("User/chkPass")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckPassword(int userID, string userName, string password)
        {
            return Ok(await userService.ValidateCurrentPassword(userID, userName, Crypto.Decrypt(password, true)));
        }

        [Route("User/ForgetPassword/{userName}")]
        [HttpGet]
        public async Task<bool> ForgetPassword(string userName)
        {
            UserVM user = await userService.GetUser(userName);
            if (user != null)
            {
                string sendFrom = WebConfigurationManager.AppSettings["email"].ToString();
                string Password = WebConfigurationManager.AppSettings["emailPass"].ToString();
                string serverHost = WebConfigurationManager.AppSettings["serverHost"].ToString();
                int serverPort = Convert.ToInt32(WebConfigurationManager.AppSettings["serverPort"].ToString());

                string BaseUrl = WebConfigurationManager.AppSettings["siteUrllocal"].ToString();

                string emailSubject = "نسيت كلمة المرور ResortERP";
                string emailDescription = "شرفنا بزيارة موقعنا الالكتروني <br> <a href='http://www.Resorterp.com/'>http://www.Resorterp.com/</a> ";
                string emailBody = "";
                try
                {
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Resources/ForgetPassword.html")))
                    {
                        emailBody = reader.ReadToEnd();
                    }
                }
                catch (Exception ex) { string str = ex.Message; }

                string ForgetPasswordLink = BaseUrl + "forgetPassword" + "?UID=" + "" + user.ID.ToString() + "" + "&UN=" + userName + "&OPW=" + Crypto.Encrypt(user.UPWD, true);
                emailBody = emailBody.Replace("{Description}", emailDescription);
                emailBody = emailBody.Replace("{UserName}", user.UID);
                emailBody = emailBody.Replace("{Link}", "<a href='" + ForgetPasswordLink + "'>" + BaseUrl + "forgetPassword" + "</a>");

                /***********************************************************************************************************/
                var inlineLogo = new LinkedResource(HttpContext.Current.Server.MapPath("~/Resources/logo4.png"));
                inlineLogo.ContentId = Guid.NewGuid().ToString();

                /***********************************************************************************************************/
                emailBody = emailBody.Replace("{logo}", String.Format(@"<img src=""cid:{0}"" style='background-color: darkgray;width: 94%;height:160px !important'>", inlineLogo.ContentId));
                bool res = await userService.SendMail(sendFrom, Password, user.COMPANY_EMAIL, emailSubject, emailBody, null, serverHost, serverPort, inlineLogo);
                return res;
            }
            return false;
        }

        [Route("User/changePassword")]
        [HttpPost]
        public async Task<IHttpActionResult> changePassword(int UserID, string UserName, string Password)
        {
            var result = await userService.changePassword(UserID, UserName, Password);
            await LogData(null,UserID.ToString());
            return Ok(result);
        }

        [Route("User/ValidateReg")]
        [HttpPost]
        public async Task<IHttpActionResult> ValidateReg([FromBody]UserVM entity)
        {
            return Ok(await userService.ValidateReg(entity));
        }

        [Route("User/CheckEmail")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckEmailExist(string email)
        {
            return Ok(await userService.CheckEmailExist(email));
        }

        [Route("User/CheckUserName")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckUserNameExist(string userName)
        {
            return Ok(await userService.CheckUserNameExist(userName));
        }

        [Route("User/CheckARCompany")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckARCompanyNameExist(string companyArName)
        {
            return Ok(await userService.CheckARCompanyNameExist(companyArName));
        }

        [Route("User/CheckENCompany")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckENCompanyNameExist(string companyEnName)
        {
            return Ok(await userService.CheckENCompanyNameExist(companyEnName));
        }

        [Route("User/GetUser")]
        public async Task<IHttpActionResult> GetUser(string userName)
        {
            return Ok(await userService.GetUser(userName));
        }

        [Route("User/GetUserObj")]
        [HttpPost]
        public UserVM GetUserObj(string userName, string password)
        {
            return userService.getUserObj(userName, password);
        }

        [Route("User/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]UserVM entity)
        {
            string BackUpFile = ConfigurationManager.AppSettings["backUpFile"].ToString();
            string DatabaseFilePath = ConfigurationManager.AppSettings["DatabaseFilesPath"].ToString();
            string _databaseName = ConfigurationManager.AppSettings["CS_InitialCatalog"].ToString();
            _databaseName = userService.CopyDataBase(BackUpFile, DatabaseFilePath, _databaseName);
            entity.CS_DataSource = ConfigurationManager.AppSettings["CS_DataSource"].ToString();
            entity.CS_InitialCatalog = _databaseName; // ConfigurationManager.AppSettings["CS_InitialCatalog"].ToString();
            entity.CS_Password = ConfigurationManager.AppSettings["CS_Password"].ToString();
            entity.CS_UserId = ConfigurationManager.AppSettings["CS_UserId"].ToString();
            entity.COMPANY_ACTIVATION_CODE = RandomActivation.Generate(30, 40);
            entity.IS_ACTIVATED = true;

            var result = await userService.InsertAsync(entity);
            await LogData(null,entity.UID.ToString());
            return Ok(result);

        }
        [Route("User/addRegularUser")]
        [HttpPost]
        public async Task<IHttpActionResult> addRegularUser([FromBody]UserVM entity)
        {
            Repository.ConnectionString conn = (await commonservice.GetLoggedDatabase());
            entity.CS_DataSource = conn.Data_Source;
            entity.CS_InitialCatalog = conn.Intial_Catalog;
            entity.CS_Password = conn.Password;
            entity.CS_UserId = conn.User_ID;

            var result = await userService.InsertAsync(entity);
            await LogData(null,entity.UID);
            return Ok(result);

        }

        [Route("user/GetAllUsers")]
        [HttpGet]
        public List<UserVM> GetAllUsers()
        {
            return userService.GetAllUsers();
        }

        [Route("User/GetUserType")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserType()
        {
            return Ok(await userService.GetUserType());
        }



        [Route("User/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]UserVM entity)
        {
            var result = await userService.UpdateAsync(entity);
            await LogData(null,entity.UID);
            return Ok(result);

        }
        [Route("User/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]UserVM entity)
         {
            var q=userPriviligeBranService.GetById(entity.ID);
            if (q.Count == 0)
            {
                var result = await userService.DeleteAsync(entity);
                await LogData(null,entity.UID);
                return Ok(result);
            }
            else
                return Ok(false);
        }

        [Route("User/deleteBran")]
        [HttpPost]
        public async Task<IHttpActionResult> deleteBran([FromBody]UserVM entity)
        {
            var q = userPriviligeBranService.GetById(entity.ID);
            foreach(var item in q)
            {
                await userPriviligeBranService.DeleteAll(item.ID);
            }
            var result = await userService.DeleteAsync(entity);
            await LogData(null,entity.UID);
            return Ok(result);


        }

        [Route("User/changeLanguage")]
        [HttpPost]
        public async Task<IHttpActionResult> changeLanguage(string userName, int userID, int languageID)
        {
            return Ok(await userService.changeLanguage(userName, userID, languageID));
        }

        [Route("User/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await userService.GetAllAsync(pageNum, pageSize));
        }
        [Route("User/getAll")]
        [HttpGet]
        public List<UserVM> getAll()
        {
            return userService.GetAll();
        }
        [Route("User/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await userService.CountAsync());
        }

        [Route("User/companyName")]
        [HttpGet]
        public string GetCompanyNameByUserID(int ID)
        {
            return userService.GetCompanyNameByUserID(ID);
        }

        [Route("User/getCompanyName")]
        [HttpGet]
        public UserVM GetUserCompanyName(string companyEnName)
        {
            return userService.GetAdminUserByCompany(companyEnName);
        }

        [Route("api/registerCompany")]
        [HttpPost]
        public async Task<HttpResponseMessage> Registering(HttpRequestMessage request)
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var data = await Request.Content.ParseMultipartAsync();

            UserVM UserEntity = getUserEntity(data.Fields);
            if (data.Files.ContainsKey("file"))
            {
                var file = data.Files["file"].File;
                UserEntity.COMPANY_LOGO = file;
                var fileName = data.Files["file"].Filename;
            }
            if (data.Fields.ContainsKey("description"))
            {
                var description = data.Fields["description"].Value;
            }


            try
            {
                var userDB = new User();
                try
                {
                    string BackUpFile = ConfigurationManager.AppSettings["backUpFile"].ToString();
                    string DatabaseFilePath = ConfigurationManager.AppSettings["DatabaseFilesPath"].ToString();
                    string _databaseName = ConfigurationManager.AppSettings["CS_InitialCatalog"].ToString();
                    _databaseName = userService.CopyDataBase(BackUpFile, DatabaseFilePath, _databaseName);
                    UserEntity.CS_DataSource = ConfigurationManager.AppSettings["CS_DataSource"].ToString();
                    UserEntity.CS_InitialCatalog = _databaseName; // ConfigurationManager.AppSettings["CS_InitialCatalog"].ToString();
                    UserEntity.CS_Password = ConfigurationManager.AppSettings["CS_Password"].ToString();
                    UserEntity.CS_UserId = ConfigurationManager.AppSettings["CS_UserId"].ToString();
                    UserEntity.COMPANY_ACTIVATION_CODE = RandomActivation.Generate(30, 40);
                    UserEntity.IS_ACTIVATED = true;
                    UserEntity.ActivationRequestedDateTime = DateTime.Now;
                    UserEntity.LanguageID = 1;
                    UserEntity.IsAdmin = true;
                    userDB = await userService.InsertAsync(UserEntity);

                    await LogData(userDB.UID.ToString());

                    if (userDB != null)
                    {
                        UserEntity.COMPANY_LOGO = null;
                        await userService.InsertInnerUserAsync(UserEntity);
                        await LogData(UserEntity.UID.ToString());
                    }
                }
                catch (Exception ex) { string str = ex.Message; }


                string sendFrom = WebConfigurationManager.AppSettings["email"].ToString();
                string Password = WebConfigurationManager.AppSettings["emailPass"].ToString();
                string serverHost = WebConfigurationManager.AppSettings["serverHost"].ToString();
                int serverPort = Convert.ToInt32(WebConfigurationManager.AppSettings["serverPort"].ToString());

                string BaseUrl = WebConfigurationManager.AppSettings["siteUrllocal"].ToString();

                string emailSubject = "رابط تفعيل حساب الشركة علي Resorterp";
                string emailDescription = "شرفنا بزيارة موقعنا الالكتروني <br> <a href='http://www.Resorterp.com/'>http://www.Resorterp.com/</a> ";
                string emailBody = "";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("../Resources/EmailBody.html")))
                {
                    emailBody = reader.ReadToEnd();
                }

                string activationLink = BaseUrl + "activation" + "?UID=" + "" + userDB.ID.ToString() + "" + "&AC=" + userDB.COMPANY_ACTIVATION_CODE.ToString();
                emailBody = emailBody.Replace("{Description}", emailDescription);
                emailBody = emailBody.Replace("{UserName}", UserEntity.COMPANY_AR_NAME);
                emailBody = emailBody.Replace("{Link}", "<a href='" + activationLink + "'>" + BaseUrl + "activation" + "</a>");

                /***********************************************************************************************************/
                var inlineLogo = new LinkedResource(HttpContext.Current.Server.MapPath("~/Resources/logo4.png"));
                inlineLogo.ContentId = Guid.NewGuid().ToString();

                /***********************************************************************************************************/
                emailBody = emailBody.Replace("{logo}", String.Format(@"<img src=""cid:{0}"" style='background-color: darkgray;width: 94%;height:160px !important'>", inlineLogo.ContentId));

                bool res = await userService.SendMail(sendFrom, Password, UserEntity.COMPANY_EMAIL, emailSubject, emailBody, null, serverHost, serverPort, inlineLogo);
                if (res)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Register Completed...")
                    };
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Mail not sent...")
                    };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(ex.Message)
                };
            }
        }

        private UserVM getUserEntity(IDictionary<string, HttpPostedField> fields)
        {
            try
            {
                if (fields != null)
                {
                    UserVM user = new UserVM();

                    user.AddedBy = fields["AddedBy"].Value == "null" ? null : fields["AddedBy"].Value;
                    user.AddedOn = fields["AddedOn"].Value == "null" ? (DateTime?)null : DateTime.ParseExact(fields["AddedOn"].Value, "MM/dd/yyyy", null);
                    user.COMPANY_ACTIVATION_CODE = fields["COMPANY_ACTIVATION_CODE"].Value == "null" ? null : fields["COMPANY_ACTIVATION_CODE"].Value;
                    user.COMPANY_AR_ADRESS = fields["COMPANY_AR_ADRESS"].Value == "null" ? null : fields["COMPANY_AR_ADRESS"].Value;
                    user.COMPANY_AR_NAME = fields["COMPANY_AR_NAME"].Value == "null" ? null : fields["COMPANY_AR_NAME"].Value;
                    user.COMPANY_EMAIL = fields["COMPANY_EMAIL"].Value == "null" ? null : fields["COMPANY_EMAIL"].Value;
                    user.COMPANY_EN_ADRESS = fields["COMPANY_EN_ADRESS"].Value == "null" ? null : fields["COMPANY_EN_ADRESS"].Value;
                    user.COMPANY_EN_NAME = fields["COMPANY_EN_NAME"].Value == "null" ? null : fields["COMPANY_EN_NAME"].Value;
                    user.COMPANY_FAX = fields["COMPANY_FAX"].Value == "null" ? null : fields["COMPANY_FAX"].Value;
                    user.COMPANY_LOGO = null;
                    user.COMPANY_TELEPHONE = fields["COMPANY_TELEPHONE"].Value == "null" ? null : fields["COMPANY_TELEPHONE"].Value;
                    user.CS_DataSource = fields["CS_DataSource"].Value == "null" ? null : fields["CS_DataSource"].Value;
                    user.CS_InitialCatalog = fields["CS_InitialCatalog"].Value == "null" ? null : fields["CS_InitialCatalog"].Value;
                    user.CS_Password = fields["CS_Password"].Value == "null" ? null : fields["CS_Password"].Value;
                    user.CS_UserId = fields["CS_UserId"].Value == "null" ? null : fields["CS_UserId"].Value;
                    user.DbCreationDate = fields["DbCreationDate"].Value == "null" ? (DateTime?)null : Convert.ToDateTime(fields["DbCreationDate"].Value);
                    user.DbDescriptionName = fields["DbDescriptionName"].Value == "null" ? null : fields["DbDescriptionName"].Value;
                    user.DbEndPeriodDate = fields["DbEndPeriodDate"].Value == "null" ? (DateTime?)null : Convert.ToDateTime(fields["DbEndPeriodDate"].Value);
                    user.DbFirstPeriodDate = fields["DbFirstPeriodDate"].Value == "null" ? (DateTime?)null : Convert.ToDateTime(fields["DbFirstPeriodDate"].Value);
                    user.DELETEDORNOT = fields["DELETEDORNOT"].Value == "null" ? null : fields["DELETEDORNOT"].Value;
                    user.ID = 0;
                    user.IS_ACTIVATED = fields["IS_ACTIVATED"].Value == "null" ? (bool?)null : Convert.ToBoolean(fields["IS_ACTIVATED"].Value);
                    if (fields.ContainsKey("MaxUsersNumber"))
                    {
                        user.MaxUsersNumber = fields["MaxUsersNumber"].Value == "null" ? (short?)null : Convert.ToInt16(fields["MaxUsersNumber"].Value);
                    }
                    user.SavePWD = fields["SavePWD"].Value == "null" ? (bool?)null : Convert.ToBoolean(fields["SavePWD"].Value);
                    user.SHIFT_NUMBER = fields["SHIFT_NUMBER"].Value == "null" ? (double?)null : Convert.ToDouble(fields["SHIFT_NUMBER"].Value);
                    user.UID = fields["UID"].Value == "null" ? null : fields["UID"].Value;
                    user.UpdatedBy = fields["UpdatedBy"].Value == "null" ? null : fields["UpdatedBy"].Value;
                    user.updatedOn = fields["updatedOn"].Value == "null" ? (DateTime?)null : Convert.ToDateTime(fields["updatedOn"].Value);
                    user.UPWD = fields["UPWD"].Value == "null" ? null : fields["UPWD"].Value;
                    user.UserGroupID = fields["UserGroupID"].Value == "null" ? (int?)null : Convert.ToInt32(fields["UserGroupID"].Value);
                    user.USER_LETTER = fields["USER_LETTER"].Value == "null" ? null : fields["USER_LETTER"].Value;
                    user.USER_TYPE = fields["USER_TYPE"].Value == "null" ? 0 : Convert.ToInt32(fields["USER_TYPE"].Value);

                    return user;
                }
            }
            catch (Exception ex) { string str = ex.Message; }
            return null;

        }

        [Route("User/GetUserByUserName")]
        [HttpGet]
        public UserVM getUserByUserName(string userName)
        {
            return userService.getUserByUserName(userName);
        }

        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await _userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }

    }
    public static class HttpContentExtensions
    {
        public static async Task<HttpPostedData> ParseMultipartAsync(this HttpContent postedContent)
        {
            var provider = new MultipartMemoryStreamProvider();
            var files = new Dictionary<string, HttpPostedFile>(StringComparer.InvariantCultureIgnoreCase);
            var fields = new Dictionary<string, HttpPostedField>(StringComparer.InvariantCultureIgnoreCase);
            try
            {
                provider = await postedContent.ReadAsMultipartAsync();

                //files = new Dictionary<string, HttpPostedFile>(StringComparer.InvariantCultureIgnoreCase);
                //fields = new Dictionary<string, HttpPostedField>(StringComparer.InvariantCultureIgnoreCase);

                foreach (var content in provider.Contents)
                {
                    var fieldName = content.Headers.ContentDisposition.Name.Trim('"');
                    if (!string.IsNullOrEmpty(content.Headers.ContentDisposition.FileName))
                    {
                        var file = await content.ReadAsByteArrayAsync();
                        var fileName = content.Headers.ContentDisposition.FileName.Trim('"');
                        files.Add(fieldName, new HttpPostedFile(fieldName, fileName, file));
                    }
                    else
                    {
                        var data = await content.ReadAsStringAsync();
                        fields.Add(fieldName, new HttpPostedField(fieldName, data));
                    }
                }
            }
            catch (Exception ex) { string str = ex.Message; }
            return new HttpPostedData(fields, files);
        }
    }
    public class HttpPostedData
    {
        public HttpPostedData(IDictionary<string, HttpPostedField> fields, IDictionary<string, HttpPostedFile> files)
        {
            Fields = fields;
            Files = files;
        }

        public IDictionary<string, HttpPostedField> Fields { get; private set; }
        public IDictionary<string, HttpPostedFile> Files { get; private set; }
        public UserVM userData { get; private set; }
    }
    public class HttpPostedFile
    {
        public HttpPostedFile(string name, string filename, byte[] file)
        {
            File = file;
            Name = name;
            Filename = filename;
        }

        public string Name { get; private set; }
        public string Filename { get; private set; }
        public byte[] File { private set; get; }
    }
    public class HttpPostedField
    {
        public HttpPostedField(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
    }
    public class activationVM
    {
        public string username { get; set; }
        public string password { get; set; }
        public string activationCode { get; set; }
    }
}
