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

        private ObservableCollection<EventoViewModel> ListaEventos
        {
            get => _eventoList;
            set
            {
                _eventoList = value;
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
    }
}
