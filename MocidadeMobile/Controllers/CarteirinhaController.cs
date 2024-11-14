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

        public async Task<Carteirinha> ObtemCarteirinha(string cpf)
        {
            Carteirinha carteirinha = new Carteirinha();

            try
            {
                string query = $"SELECT a.Id, a.nome, a.cpf, c.nome AS ala FROM usuario a LEFT JOIN tbUsuarioAla b ON a.Id = b.idUsuario INNER JOIN tbAla c ON b.idAla = c.id WHERE a.cpf='{cpf}'";
                DataTable result = await _databaseService.ExecuteQueryAsync(query);

                if (result.Rows.Count > 0)
                {
                    var row = result.Rows[0];

                    carteirinha.Usuario.Id = Convert.ToInt32(row["Id"]);
                    carteirinha.Usuario.Nome = row["nome"].ToString() ?? "";
                    carteirinha.Usuario.CPF = row["cpf"].ToString() ?? "";
                    carteirinha.Ala.Nome = row["ala"].ToString() ?? "";

                    return carteirinha;
                }

                return carteirinha;
            }
            catch (Exception ex)
            {
                // Logar a exceção
                await _page.DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
                return carteirinha;
            }
        }
    }
}
