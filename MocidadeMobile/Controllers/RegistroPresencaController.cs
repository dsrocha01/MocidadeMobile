using MocidadeMobile.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocidadeMobile.Controllers
{
    public class RegistroPresencaController
    {
        private readonly DatabaseService _databaseService;

        public RegistroPresencaController()
        {
            _databaseService = new DatabaseService();
        }

        public async Task<bool> RegistraPresenca(int idEvento, int idUsuario)
        {
            // Implementar a lógica de registro de presença
            var query = $"INSERT INTO tbRegistroPresenca (idEvento, idUsuario) VALUES ({idEvento}, {idUsuario})";

            DataTable result = await _databaseService.ExecuteQueryAsync(query);

            if (result.Rows.Count > 0)
            {
                return true; 
            } 
            else 
            { 
                return false; 
            }
        }
    }
}
