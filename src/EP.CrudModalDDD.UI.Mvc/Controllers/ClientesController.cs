using EP.CrudModalDDD.Application.Interfaces;
using EP.CrudModalDDD.Application.ViewModels;
using EP.CrudModalDDD.Infra.CrossCutting.MvcFilters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EP.CrudModalDDD.UI.Mvc.Controllers
{
    [Authorize]
    [RoutePrefix("administrativo-clientes")]
    [Route("{action=listar}")]
    public class ClientesController : BaseController
    {
        private readonly IClienteAppService _clienteAppService;


        public ClientesController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;

        }

        [ClaimsAuthorize("PermissoesCliente", "CL")]
        [Route("listar")]
        public ActionResult Index(string buscar, int pageNumber = 1)
        {
            var paged = _clienteAppService.ObterTodos(buscar, PageSize, pageNumber);
            ViewBag.TotalCount = Math.Ceiling((double)paged.Count / PageSize);
            ViewBag.PageNumber = pageNumber;
            ViewBag.SearchData = buscar;

            return View(paged.List);
        }

        [ClaimsAuthorize("PermissoesCliente", "CV")]
        [Route("detalhes/{id:guid}")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteViewModel clienteViewModel = _clienteAppService.ObterPorId(id.Value);
            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clienteViewModel);
        }

        [ClaimsAuthorize("PermissoesCliente", "CI")]
        [Route("incluir-novo")]
        public ActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("PermissoesCliente", "CI")]
        [Route("incluir-novo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteEnderecoViewModel clienteEnderecoViewModel)
        {
            if (ModelState.IsValid)
            {
                clienteEnderecoViewModel = _clienteAppService.Adicionar(clienteEnderecoViewModel);

                if (Notifications.HasNotifications())
                {
                    foreach (var item in Notifications.Notify())
                    { 
                        ModelState.AddModelError(string.Empty, item.Value);
                    }

                    return View(clienteEnderecoViewModel);
                }

                ViewBag.Sucesso = "Cliente cadastrado com sucesso :)";
                return View(clienteEnderecoViewModel);
            }

            return View(clienteEnderecoViewModel);
        }

        [ClaimsAuthorize("PermissoesCliente", "CE")]
        [Route("editar/{id:guid}")]
        public ActionResult Edit(Guid? id)
        {
            ViewBag.Acao = "Editar Cliente";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteViewModel clienteViewModel = _clienteAppService.ObterPorId(id.Value);
            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClienteId = id;
            return View(clienteViewModel);
        }

        [ClaimsAuthorize("PermissoesCliente", "CE")]
        [Route("editar/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                _clienteAppService.Atualizar(clienteViewModel);

                if (Notifications.HasNotifications())
                {
                    foreach (var item in Notifications.Notify())
                    {
                        ModelState.AddModelError(string.Empty, item.Value);
                    }

                    return View(clienteViewModel);
                }

                return RedirectToAction("Index");
            }
            return View(clienteViewModel);
        }

        [ClaimsAuthorize("PermissoesCliente", "CX")]
        [Route("excluir/{id:guid}")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteViewModel clienteViewModel = _clienteAppService.ObterPorId(id.Value);
            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clienteViewModel);
        }

        [ClaimsAuthorize("PermissoesCliente", "CX")]
        [Route("excluir/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                _clienteAppService.Remover(id);

                if (Notifications.HasNotifications())
                {
                    var clienteViewModel = _clienteAppService.ObterPorId(id);

                    foreach (var item in Notifications.Notify())
                    {
                        ModelState.AddModelError(string.Empty, item.Value);
                    }

                    return View(clienteViewModel);
                }

                //ViewBag.Sucesso = "Cliente removido com sucesso :)";
                return RedirectToAction("Index");
            }

            return View();
        }

        [ClaimsAuthorize("PermissoesCliente", "CV")]
        public ActionResult ListarEnderecos(Guid id)
        {
            ViewBag.ClienteId = id;
            return PartialView("_EnderecosList", _clienteAppService.ObterPorId(id).Enderecos);
        }

        [ClaimsAuthorize("PermissoesCliente", "CE")]
        [Route("adicionar-endereco")]
        public ActionResult AdicionarEndereco(Guid id)
        {
            ViewBag.ClienteId = id;
            return PartialView("_AdicionarEndereco");
        }

        [ClaimsAuthorize("PermissoesCliente", "CE")]
        [Route("adicionar-endereco")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarEndereco(EnderecoViewModel enderecoViewModel)
        {
            if (ModelState.IsValid)
            {
                _clienteAppService.AdicionarEndereco(enderecoViewModel);

                string url = Url.Action("ListarEnderecos", "Clientes", new { id = enderecoViewModel.ClienteId });
                return Json(new { success = true, url = url });
            }

            return PartialView("_AdicionarEndereco", enderecoViewModel);
        }

        [ClaimsAuthorize("PermissoesCliente", "CE")]
        [Route("adicionar-endereco/{id:guid}")]
        public ActionResult AtualizarEndereco(Guid id)
        {
            return PartialView("_AtualizarEndereco", _clienteAppService.ObterEnderecoPorId(id));
        }

        [ClaimsAuthorize("PermissoesCliente", "CE")]
        [Route("adicionar-endereco/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AtualizarEndereco(EnderecoViewModel enderecoViewModel)
        {
            if (ModelState.IsValid)
            {
                _clienteAppService.AtualizarEndereco(enderecoViewModel);

                string url = Url.Action("ListarEnderecos", "Clientes", new { id = enderecoViewModel.ClienteId });
                return Json(new { success = true, url = url });
            }

            return PartialView("_AtualizarEndereco", enderecoViewModel);
        }

        [ClaimsAuthorize("PermissoesCliente", "CE")]
        [Route("excluir-endereco/{id:guid}")]
        public ActionResult DeletarEndereco(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var enderecoViewModel = _clienteAppService.ObterEnderecoPorId(id.Value);
            if (enderecoViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeletarEndereco", enderecoViewModel);
        }

        [ClaimsAuthorize("PermissoesCliente", "CE")]
        [Route("excluir-endereco/{id:guid}")]
        [HttpPost, ActionName("DeletarEndereco")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarEnderecoConfirmed(Guid id)
        {
            var clienteId = _clienteAppService.ObterEnderecoPorId(id).ClienteId;
            _clienteAppService.RemoverEndereco(id);

            string url = Url.Action("ListarEnderecos", "Clientes", new { id = clienteId });
            return Json(new { success = true, url = url });
        }

        public ActionResult ObterImagemCliente(Guid id)
        {
            var root = @"D:\";
            var foto = Directory.GetFiles(root, id+"*").FirstOrDefault();
           
            if (foto != null && !foto.StartsWith(root))
            {
                // Validando qualquer acesso indevido além da pasta permitida
                throw new HttpException(403, "Acesso Negado");
            }

            if(foto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return File(foto, "image/jpeg");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clienteAppService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
