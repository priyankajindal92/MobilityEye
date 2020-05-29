using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PriyankaFormBuilder.HtmlBuilder;
using PriyankaFormBuilder.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace PriyankaFormBuilder.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public HttpResponseMessage Index(string jsonData)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string formId = Guid.NewGuid().ToString();
                sb.Append($"<form id=\"{formId}\" action=\"/home/form\" method=\"post\"><input type=\"hidden\" name=\"formId\" value=\"{formId}\"/>");
                JObject formData = JObject.Parse(jsonData);
                int index = 0;
                BaseControl ctrl = null;
                if (!formData.ContainsKey("form"))
                {
                    ctrl = new TextBoxControl() { Type = "text", ClassName = "form-control", Id = "hi", Label = "Hello!", Name = "hi" };
                    sb.Append(ctrl.ToString());
                    ctrl = new ButtonControl() { ClassName = "btn btn-primary", Value = "Save", Type = "submit" };
                    sb.Append(ctrl.ToString());
                    sb.Append("</form>");
                    TempData["htmlTemplate"] = sb.ToString();
                    return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
                }

                foreach (JObject item in formData["form"])
                {
                    string label = item["label"].ToString();
                    string type = item["type"].ToString();
                    switch (type)
                    {
                        case "text":
                            ctrl = new TextBoxControl() { Type = type, ClassName = "form-control", Id = index.ToString(), Label = label, Name = index.ToString() };
                            sb.Append(ctrl.ToString());
                            break;
                        case "checkbox":
                        case "radio":
                            if (item.ContainsKey("options"))
                            {
                                ctrl = new CheckBoxOrRadioButtonControl()
                                {
                                    Type = type,
                                    Label = label,
                                    Id = index.ToString(),
                                    Options = (JArray)item["options"],
                                    ClassName = "form-check-input",
                                    Name = index.ToString(),
                                };

                                sb.Append(ctrl.ToString());
                            }
                            break;
                    };

                    index++;
                }

                ctrl = new ButtonControl() { ClassName = "btn btn-primary", Value = "Save", Type = "submit" };
                sb.Append(ctrl.ToString());
                sb.Append("</form>");
                TempData["htmlTemplate"] = sb.ToString();
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Form()
        {
            try
            {
                ViewData["htmlTemplate"] = TempData["htmlTemplate"];
                return View();
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Form(IFormCollection form)
        {
            try
            {
                if (form != null && form.Keys.Count > 0)
                {
                    var entries = form.Where(x => x.Key != "formId").Select(x => string.Format("'{0}':'{1}'", x.Key, x.Value.ToString()));
                    string data = "{" + string.Join(',', entries) + "}";
                    DataStore.Save(form["formId"], data);
                }

                return RedirectToAction("ResultView");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public IActionResult ResultView()
        {
            try
            {
                ViewData["data"] = DataStore.GetItems();
                return View();
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }
    }
}
