using MocidadeMobile.Enums;
using MocidadeMobile.Models;
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

        public async Task<bool> RegistraPresenca(int idEvento, UsuarioViewModel usuario)
        {

            if (usuario.Id  < 1)
            {
                return false;
            }
            else
            {
                var query = $"INSERT INTO tbRegistroPresenca (idEvento, idUsuario) VALUES ({idEvento}, {usuario.Id})";

                int rowsAffected = await _databaseService.ExecuteNonQueryAsync(query);

                return rowsAffected > 0;
            }
        }

        public async Task<int> RetornaIdUsuario(string cpfUsuario)
        {
            var query = $"SELECT * FROM usuario WHERE cpf= {cpfUsuario}";

            DataTable result = await _databaseService.ExecuteQueryAsync(query);

            if (result.Rows.Count > 0)
            {
                return Convert.ToInt32(result.Rows[0]["Id"]);
            }
            else
            {
                return 0;
            }
        }

        public async Task<UsuarioViewModel> RetornaUsuario(string cpfUsuario)
        {
            var usuario = new UsuarioViewModel();

            var query = $"SELECT * FROM usuario WHERE cpf= {cpfUsuario}";

            DataTable result = await _databaseService.ExecuteQueryAsync(query);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];

                usuario.Id = Convert.ToInt32(row["Id"]);
                usuario.CPF = row["cpf"].ToString() ?? "";
                usuario.Nome = row["nome"].ToString() ?? "";

                return usuario;
            }
            else
            {
                return usuario;
            }
        }
    }
}
