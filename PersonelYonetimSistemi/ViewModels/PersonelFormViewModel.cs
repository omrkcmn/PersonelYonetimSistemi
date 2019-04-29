using PersonelYonetimSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonelYonetimSistemi
{
    public class PersonelFormViewModel
    {
        public IEnumerable<Departman> Departmanlarlar { get; set; }
        public Personel Personel { get; set; }
    }

}