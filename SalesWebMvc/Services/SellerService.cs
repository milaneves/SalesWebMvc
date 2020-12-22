using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
         
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //retorna lista com os vendedores do banco
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
    
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
             //retorna o Id do departamento, usando o join
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);    //removendo o objeto do dbset
            _context.SaveChanges(); //confirmando a alteração no entity
        }


        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id ))//verificando se existe algum vendedor x cujo o id seja igual o id do objeto
            { //se não existir
                throw new NotFoundException("Não existe esse Id");
            }
            _context.Update(obj);

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
          
        }
       
    }
}
