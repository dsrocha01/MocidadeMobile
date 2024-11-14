using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocidadeMobile.Models
{
    public class Carteirinha
    {
        public Usuario Usuario { get; set; } = new Usuario();

        public Ala Ala { get; set; } = new Ala();
    }
}
