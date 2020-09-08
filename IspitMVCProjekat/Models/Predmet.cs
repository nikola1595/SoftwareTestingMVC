using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IspitMVCProjekat.Models
{
    public partial class Predmet
    {
        public Predmet()
        {
            Ispit = new HashSet<Ispit>();
        }
        [Display(Name = "Šifra predmeta")]
        public int PredmetId { get; set; }
        [Display(Name = "Naziv predmeta")]
        public string ImePredmeta { get; set; }

        public virtual ICollection<Ispit> Ispit { get; set; }
    }
}
