using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Data
{
    public class SeedingService
    {

        private SalesWebMvcContext _context;
        public SeedingService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //populando a base de dados
        public void Seed()
        {
            // if ( _context.Department.Any() ||
             //    _context.Seller.Any() ||
            //    _context.SalesRecord.Any()
            //    )
            //  {
              //  return; //banco já foi populado
            //  }

            //Department d1 = new Department("Mila");

            //usando o enity para adcionar no banco e confirmando o salvamento
            //_context.Department.AddRange(d1);
            //_context.SaveChanges();

        }
    }
}
