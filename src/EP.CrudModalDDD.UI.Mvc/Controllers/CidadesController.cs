using EP.CrudModalDDD.Application.Interfaces;
using EP.CrudModalDDD.Application.ViewModels;
using EP.CrudModalDDD.Infra.CrossCutting.MvcFilters;
using System;
using System.Net;
using System.Web.Mvc;

namespace EP.CrudModalDDD.UI.Mvc.Controllers
{
    [Authorize]
    [RoutePrefix("administrativo-cidades")]
    [Route("{action=listar}")]
    public class CidadesController : BaseController
    {
        private readonly ICidadeAppService _cidadeAppService;

        public CidadesController(ICidadeAppService cidadeAppService)
        {
            _cidadeAppService = cidadeAppService;
        }

        [ClaimsAuthorize("PermissoesCidade", "CL")]
        [Route("listar")]
        public ActionResult Index(string buscar, int pageNumber = 1)
        {
            var paged = _cidadeAppService.ObterTodos(buscar, PageSize, pageNumber);
            ViewBag.TotalCount = Math.Ceiling((double)paged.Count / PageSize);
            ViewBag.PageNumber = pageNumber;
            ViewBag.SearchData = buscar;

            return View(paged.List);

            //return View(_cidadeAppService.ObterTodos());
        }

        [ClaimsAuthorize("PermissoesCidade", "CV")]
        [Route("detalhes/{id:guid}")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CidadeViewModel cidadeViewModel = _cidadeAppService.ObterPorId(id.Value);
            if (cidadeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(cidadeViewModel);
        }

        [ClaimsAuthorize("PermissoesCidade", "CI")]
        [Route("incluir-novo")]
        public ActionResult Create()
        {
            return View();
        }


        [ClaimsAuthorize("PermissoesCidade", "CI")]
        [Route("incluir-novo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CidadeViewModel cidadeViewModel)
        {
            if (ModelState.IsValid)
            {
                cidadeViewModel = _cidadeAppService.Adicionar(cidadeViewModel);

                if (Notifications.HasNotifications())
                {
                    foreach (var item in Notifications.Notify())
                    {
                        ModelState.AddModelError(string.Empty, item.Value);
                    }

                    return View(cidadeViewModel);
                }
                
                //ViewBag.Sucesso = "Cidade cadastrado com sucesso :)";
                return RedirectToAction("Index");
                //return View(cidadeViewModel);

            }

            return View(cidadeViewModel);
        }

        [ClaimsAuthorize("PermissoesCidade", "CE")]
        [Route("editar/{id:guid}")]
        public ActionResult Edit(Guid? id)
        {
            ViewBag.Acao = "Editar Cidade";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CidadeViewModel cidadeViewModel = _cidadeAppService.ObterPorId(id.Value);
            if (cidadeViewModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id = id;
            return View(cidadeViewModel);
        }

        [ClaimsAuthorize("PermissoesCidade", "CE")]
        [Route("editar/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CidadeViewModel cidadeViewModel)
        {
            if (ModelState.IsValid)
            {
                _cidadeAppService.Atualizar(cidadeViewModel);

                if (Notifications.HasNotifications())
                {
                    foreach (var item in Notifications.Notify())
                    {
                        ModelState.AddModelError(string.Empty, item.Value);
                    }

                    return View(cidadeViewModel);
                }

                return RedirectToAction("Index");
            }
            return View(cidadeViewModel);
        }

        [ClaimsAuthorize("PermissoesCidade", "CX")]
        [Route("excluir/{id:guid}")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CidadeViewModel cidadeViewModel = _cidadeAppService.ObterPorId(id.Value);
            if (cidadeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(cidadeViewModel);
        }

        [ClaimsAuthorize("PermissoesCidade", "CX")]
        [Route("excluir/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                _cidadeAppService.Remover(id);

                if (Notifications.HasNotifications())
                {
                    var cidadeViewModel = _cidadeAppService.ObterPorId(id);

                    foreach (var item in Notifications.Notify())
                    {
                        ModelState.AddModelError(string.Empty, item.Value);
                    }

                    return View(cidadeViewModel);
                }

                //ViewBag.Sucesso = "Cliente removido com sucesso :)";
                return RedirectToAction("Index");
            }

            return View();
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cidadeAppService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
