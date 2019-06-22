using ResortERP.Core;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using ResortERP.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ResortERP.Repository
{
    public class Logger : ActionFilterAttribute
    {
        static UserLogFile UserFormObj;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Method.Method != "GET")
            {
                string y = "";
                int? COMBRNID = null;
                var urlStrF = "";
                string controllerName =
                    actionContext.ControllerContext.ControllerDescriptor.ControllerName;

                string actionName =
                    actionContext.ActionDescriptor.ActionName;
                var ss = actionContext.Request.Headers.Where(a => a.Key == "userNameLog").SingleOrDefault();
                if (ss.Value != null)
                {
                    string[] s = actionContext.Request.Headers.Where(a => a.Key == "userNameLog").SingleOrDefault().Value.ToArray();
                    y = s[0];
                    if (y.Split('@').Count() > 0)
                    {
                        y = y.Split('@')[0];
                    }
                };
                var urlV = actionContext.Request.Headers.Where(a => a.Key == "myCustomUrl").SingleOrDefault();
                if (urlV.Value != null)
                {
                    string[] s = actionContext.Request.Headers.Where(a => a.Key == "myCustomUrl").SingleOrDefault().Value.ToArray();
                    urlStrF = s[0];

                    if (urlStrF.Contains("foo"))
                    {
                        urlStrF = System.Text.RegularExpressions.Regex.Split(urlStrF, "&foo")[0];
                        var x = urlStrF;
                        if (x.Contains("foo"))
                        {
                            urlStrF = x.Split('?')[0];
                        }
                    }
                };
                
                var CompB = actionContext.Request.Headers.Where(a => a.Key == "COMBRNID").SingleOrDefault();
                if (CompB.Value != null)
                {
                    string[] CompBs = actionContext.Request.Headers.Where(a => a.Key == "COMBRNID").SingleOrDefault().Value.ToArray();
                    if (CompBs[0] == "")
                    {
                        COMBRNID = 0;
                    }
                    else
                    {
                        COMBRNID = int.Parse(CompBs[0]);
                    }
                };

                UserFormObj = new UserLogFile()
                {
                    UID = y,
                    DateOnly = DateTime.Now.Date,
                    FormName = controllerName,
                    OpDate = DateTime.Now,
                    OpName = actionName,
                    Comp_Brach=COMBRNID,
                    URL= urlStrF

                };
            }

        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //var x = actionExecutedContext.Request.RequestUri.Query;
        }

        public UserLogFile LogDataMethod(string code = null, string id = null,string notes=null,string editR=null)
        {
            if (id != null)
            {
                UserFormObj.IDCRUD = id;
            }

            if (notes != null)
            {
                UserFormObj.Notes = notes;
            }

            if (editR != null)
            {
                var temp = UserFormObj.Notes;
                UserFormObj.Notes = temp + " - " + "  سبب التعديل  :  "+ editR;
                UserFormObj.OpName = "update";
            }

            if (code != null)
            {
                if (code.Split('&').Count() > 1 || code.Split('_').Count() > 1||code.Split('?').Count()>1)
                {
                    UserFormObj.FormName += code;
                    return UserFormObj;
                }
                else
                {
                    UserFormObj.Code = code;
                }

            }


            return UserFormObj;
        }

    }
}