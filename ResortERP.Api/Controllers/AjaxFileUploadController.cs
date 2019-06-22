using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ResortERP.Api.Controllers
{
    public class FileUploadParams
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    [Authorize]
    public class AjaxFileUploadController : Controller
    {
        [HttpPost]
        public JsonResult UploadSingleFile(HttpPostedFileBase UploadedFiles, string SubDirectory,
            string Params, long TS)
        {
            string fname = "";

            if (UploadedFiles != null)
            {
                if (UploadedFiles.ContentLength == 0)
                {
                    return Json(new { error = "Wrong data" }, JsonRequestBehavior.AllowGet);
                }
                if (Params != null && Params != "")
                {
                    var ListParams = new JavaScriptSerializer().Deserialize<List<FileUploadParams>>(Params);
                    foreach (var it in ListParams)
                    {

                    }
                }
                Guid guid = Guid.NewGuid();
                fname = guid.ToString() + UploadedFiles.FileName;
                string path = "";
                if (SubDirectory != "" && SubDirectory != null)
                    path = Path.Combine(Server.MapPath("~/UploadDir/" + SubDirectory + "/"), fname);
                else
                    path = Server.MapPath("~/UploadDir/") + fname;
                UploadedFiles.SaveAs(path);
                dynamic[] previewConfig = new dynamic[1];
                previewConfig[0] = new
                {
                    url = System.Configuration.ConfigurationManager.AppSettings["SystemUrl"] + "/AjaxFileUpload/DeleteFile",
                    key = fname,
                    caption = UploadedFiles.FileName,
                    size = UploadedFiles.ContentLength,
                    extra = new
                    {
                        FileName = fname,
                        SubDirectory = SubDirectory,
                        Params = Params
                    }
                };

                dynamic[] preview = new dynamic[1];
                preview[0] = "<span>" + UploadedFiles.FileName + "</span>";

                return Json(new
                {
                    FileName = fname,
                    FullPath = path,
                    initialPreviewConfig = previewConfig/*,
                    initialPreview = preview,
                    append=true*/
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { error = "Wrong data" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteFile(string FileName, string SubDirectory, string Params)
        {
            try
            {
                if (FileName != "")
                {
                    if (Params != null && Params != "")
                    {
                        var ListParams = new JavaScriptSerializer().Deserialize<List<FileUploadParams>>(Params);
                        foreach (var it in ListParams)
                        {

                        }
                    }
                    string path = "";
                    if (SubDirectory != "" && SubDirectory != null)
                        path = Path.Combine(Server.MapPath("~/UploadDir/" + SubDirectory + "/"), FileName);
                    else
                        path = Server.MapPath("~/UploadDir/") + FileName;

                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) { }
            return Json(new { error = "Error deleting file" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UploadMultipleFiles(HttpPostedFileBase[] files, string SubDirectory)
        {
            string fname = "";
            if (files != null)
            {
                if (files.Length == 0)
                {
                    Response.StatusCode = 400;
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                foreach (var f in files)
                {

                    Guid guid = Guid.NewGuid();
                    string tmpf = guid.ToString() + f.FileName;
                    string path = Path.Combine(Server.MapPath("~/UploadDir/" + SubDirectory + "/"), tmpf);
                    f.SaveAs(path);
                    fname += tmpf + "/";
                }
            }
            return Json(fname, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public PartialViewResult GetUploadView(string SubFolder, string FileControlName = "", string HiddenControlName = "")
        {
            ViewBag.SubFolder = SubFolder;
            ViewBag.FileControlName = FileControlName;
            ViewBag.HiddenControlName = HiddenControlName;

            return PartialView("_FilesUpload");
        }
    }
}