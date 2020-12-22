using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerServices;
        private readonly DepartmentServices _departmentServices;

        public SellersController(SellerService sellerService, DepartmentServices departmentServices)
        {
            _sellerServices = sellerService;
            _departmentServices = departmentServices;
        }


        //retornando a lista de vendedores
        public IActionResult Index()
        {
            var list = _sellerServices.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentServices.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        //após inserir o vendedor será redirecionado para o index
        [HttpPost]
        [ValidateAntiForgeryToken]//previni ataques de sessões de autenticação
        public IActionResult Create(Seller seller)
        {
            _sellerServices.Insert(seller);
            return RedirectToAction(nameof(Index));


        }


        public IActionResult Delete(int? id) //recebendo um int opcional(?)
        {
            if (id == null)

            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido"});
            }

            var obj = _sellerServices.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe" }); 
            }
            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerServices.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = _sellerServices.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe" });

            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = _sellerServices.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe" });
            }
            // carregar os departamentos para povoar as lista de edição
            List<Department> departments = _departmentServices.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments }; //prenchendo o formulpário com o objeto dos dados existente
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id) //se o id do vendedor for diferente do id da url tem algo errado
            {
                return RedirectToAction(nameof(Error), new { message = "Id não correspondem" });
            }

            try
            {
                _sellerServices.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //macete do framework para pegar o id interno da requisição
            };
            return View(viewModel);
        }
       
    }
}