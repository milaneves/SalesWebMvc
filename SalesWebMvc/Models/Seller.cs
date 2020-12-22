﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

       /* [Required(ErrorMessage = "{0} Obrigatório")]
        [Display(Name = "Nome")]*/
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve ser entre [2} e {1}")] //A chave 0 é o nome do atributo e a chave 
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [EmailAddress(ErrorMessage ="Email inválido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate ,Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Department = department;
     
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
