using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IspitMVCProjekat.Models
{
    public partial class Student
    {
        public Student()
        {
            Ispit = new HashSet<Ispit>();
        }

        [Display(Name = "Broj indexa")]
        public string BrojIndexa { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public virtual ICollection<Ispit> Ispit { get; set; }
        
    }
}
    
