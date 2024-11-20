using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocidadeMobile.Models
{
    public class CarteirinhaViewModel
    {
        public UsuarioViewModel Usuario { get; set; } = new UsuarioViewModel();

        public Ala Ala { get; set; } = new Ala();
    }
}
