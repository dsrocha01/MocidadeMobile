using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MocidadeMobile.Controllers;
using MocidadeMobile.Models;
using MocidadeMobile.Services;
using MocidadeMobile.Views;

namespace MocidadeMobile.Models
{
    public class ConfiguracaoViewModel : INotifyPropertyChanged
    {
        private readonly ConfiguracaoController _configuracaoController;
        private ObservableCollection<EventoViewModel> _eventoList;
        private EventoViewModel _eventoSelecionado;

        public ObservableCollection<EventoViewModel> ListaEventos
        {
            get => _eventoList;
            set
            {
                _eventoList = value;
                OnPropertyChanged();
            }
        }

        public EventoViewModel EventoSelecionado
        {
            get => _eventoSelecionado;
            set
            {
                _eventoSelecionado = value;
                OnPropertyChanged();
            }
        }

        public ConfiguracaoViewModel()
        {
            _configuracaoController = new ConfiguracaoController();
            LoadData();
        }

        private async void LoadData()
        {
            var dados = await _configuracaoController.GetEventosAsync();
            ListaEventos = new  ObservableCollection<EventoViewModel>(dados);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public async void OnSalvarConfiguracaoClicked()
        {
            if (EventoSelecionado != null)
            {
                var sucesso = await _configuracaoController.SalvaEventoEmAndamento(EventoSelecionado);
                if (sucesso)
                {
                    await MessageService.SendAlertAsync("Pronto","Configuração salva com sucesso!");
                }
                else
                {
                    await MessageService.SendAlertAsync("Atenção", "Falha ao salvar configuração.");
                }
            }
        }

    }
}
