using MocidadeMobile.Enums;
using MocidadeMobile.Models;
using MocidadeMobile.Services;
using MocidadeMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MocidadeMobile.Controllers
{
    public class ConfiguracaoController
    {
        private readonly DatabaseService _databaseService;
        private readonly Page _page;

        public ConfiguracaoController()
        {
            _databaseService = new DatabaseService();
            //_page = page;
        }


        public async Task<List<EventoViewModel>> GetEventosAsync()
        {
            List<EventoViewModel> eventos = new List<EventoViewModel>();

            try
            {
                string query = "SELECT * FROM tbEvento WHERE ativo=1";
                DataTable result = await _databaseService.ExecuteQueryAsync(query);

                if (result.Rows.Count > 0)
                {
                    foreach (DataRow row in result.Rows)
                    {
                        var evento = new EventoViewModel
                        {
                            CodigoEvento = Convert.ToInt32(row["Id"]),
                            Nome = row["nome"].ToString(),
                            Data = row["data_inicio"].ToString()
                        };
                        eventos.Add(evento);
                    }
                }
                return eventos;
            }
            catch (Exception ex)
            {
                // Logar a exceção
                //await _page.DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
                return eventos;
            }
        }


    }
}
