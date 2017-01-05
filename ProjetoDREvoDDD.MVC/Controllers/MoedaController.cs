using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Web;
using ProjetoDREvoDDD.MVC.Models;
using ProjetoDREvoDDD.MVC.Helpers;
using ProjetoDREvoDDD.Domain.Interfaces.Services;
using ProjetoDREvoDDD.Domain.Services;
using ProjetoDREvo.Infra.SqlServer.Repositories;
using ProjetoDREvoDDD.Domain.Entities;

namespace ProjetoDREvoDDD.MVC.Controllers
{
    public class MoedaController : Controller
    {
        private readonly IMoedaService _moedaService;

        public MoedaController()
        {
            this._moedaService = new MoedaService(new MoedaRepository());
        }

        // GET: Moeda
        public ActionResult Index()
        {
            return View();

        }

        [HttpGet]
        public JsonResult GetMoedas(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            var records = this._moedaService.GetAll()
                           .Select(i =>  new MoedaModel
                           {
                               IdMoeda = i.IdMoeda,
                               Moeda = i.Nome,
                               Sigla = i.Sigla

                           }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                records = records.Where(p => p.Moeda.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    records = SortHelper.OrderBy(records, sortBy);
                }
                else
                {
                    records = SortHelper.OrderByDescending(records, sortBy);
                }
            }

            int total = records.Count();

            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value);
            }

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult Save(MoedaModel moeda)
        {
            var data = new JSonErrorResult();

            try
            {
                if (moeda != null)
                {
                    Moeda item = new Moeda();
                    item.IdMoeda = moeda.IdMoeda;
                    item.Nome = moeda.Moeda;
                    item.Sigla = moeda.Sigla;

                    if (item.IdMoeda > 0)
                        this._moedaService.Update(item);
                    else
                        this._moedaService.Add(item);
                }

                data.Message = "Ok";
                return Json(moeda, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                data.Message = "Errors";
                data.Items.Add(ex.Message);

                return Json(data);
            }
        }


        [HttpPost]
        public JsonResult Remove(int id)
        {
            if (id > 0)
            {
                var moeda = this._moedaService.Get(i => i.IdMoeda == id).FirstOrDefault();
                if (moeda != null)
                    this._moedaService.Remove(moeda);
            }

            return Json(id, JsonRequestBehavior.AllowGet);
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public sealed class NoCacheAttribute : ActionFilterAttribute
        {
            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
                filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                filterContext.HttpContext.Response.Cache.SetNoStore();
                base.OnResultExecuting(filterContext);
            }
        }

        public class JSonErrorResult
        {
            public string Message { get; set; }
            public List<string> Items { get; set; }

            public JSonErrorResult()
            {
                this.Items = new List<string>();
            }
        }
    }
}
