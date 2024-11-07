using MocidadeMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocidadeMobile.Controllers
{
    public class CarteirinhaController
    {
        private readonly DatabaseService _databaseService;
        private readonly Page _page;

        public CarteirinhaController(Page page)
        {
            string connectionString = "server=mysql995.umbler.com:41890;user=mocidade;password=doug94887192;database=mocidade";
            _databaseService = new DatabaseService(connectionString);
            _page = page;
        }

        public async Task ObterCarteirinha(string cpf)
        {
            // Simulação de geração de carteirinha
            
        }
    }
}
