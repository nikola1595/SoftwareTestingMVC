using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IspitMVCProjekat.Models
{
    public partial class Ispit
    {
        public int IspitId { get; set; }
        [Display(Name = "Broj indexa")]
        public string BrojIndexa { get; set; }
        [Display(Name = "Naziv predmeta")]
        public int PredmetId { get; set; }
        public int Ocena { get; set; }

        public virtual Student BrojIndexaNavigation { get; set; }
        public virtual Predmet Predmet { get; set; }
    }
}
