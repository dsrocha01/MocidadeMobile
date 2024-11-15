using MocidadeMobile.Enums;
using MocidadeMobile.Models;
using MocidadeMobile.Services;
using MocidadeMobile.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MocidadeMobile.Controllers
{
    public class LoginController
    {
        private readonly DatabaseService _databaseService;
        private readonly Page _page;
        private readonly SessionService _sessionService;

        public LoginController(Page page)
        {
            _databaseService = new DatabaseService();
            _sessionService = new SessionService();
            _page = page;
        }

        public async Task<Usuario> Logar(string cpf, string senha)
        {
            Usuario usuario = new Usuario();

            try
            {
                string query = $"SELECT * FROM usuario WHERE cpf = '{cpf}' AND senha = '{senha}'";
                DataTable result = await _databaseService.ExecuteQueryAsync(query);
                
                if (result.Rows.Count > 0)
                {
                    var row = result.Rows[0];

                    usuario.Id = Convert.ToInt32(row["Id"]);
                    usuario.CPF = row["cpf"].ToString() ?? "";                    
                    usuario.Senha = row["senha"].ToString() ?? "";
                    usuario.NivelAcesso = (EnumNivelAcesso)Convert.ToInt32(row["tipo"]);

                    return usuario;
                }

                return usuario;
            }
            catch (Exception ex)
            {
                // Logar a exceção
                await _page.DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
                return usuario;
            }
        }


        public async void OnLogout()
        {
            // Limpe qualquer estado de autenticação aqui, se necessário

            // Verifique se Application.Current e Application.Current.MainPage não são nulos
            if (Application.Current?.MainPage?.Navigation != null)
            {
                _sessionService.ClearUserSession();
                // Navegue de volta para a página de login
                ////await Application.Current.MainPage.Navigation.PushAsync(new Login());
                // Navegue de volta para a página de login usando Shell
                await Shell.Current.GoToAsync("//Login");
            }
            else
            {
                // Lidar com o caso onde Application.Current ou Application.Current.MainPage é nulo
                await _page.DisplayAlert("Erro", "Não foi possível navegar para a página de login.", "OK");
            }
        }
    }
}
