using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;

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
    }
}
