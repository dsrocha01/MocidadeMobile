using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using MocidadeMobile.Models;
using MocidadeMobile.Views;
using MocidadeMobile.Enums;
using MvvmHelpers;

namespace MocidadeMobile.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly Usuario _loggedInUser;

        public MenuViewModel(Usuario loggedInUser)
        {
            _loggedInUser = loggedInUser;
        }

        public bool IsAdministrador => _loggedInUser.NivelAcesso == EnumNivelAcesso.Administrador;
        public bool IsUsuario => _loggedInUser.NivelAcesso == EnumNivelAcesso.Usuario;
        public bool IsMembro => _loggedInUser.NivelAcesso == EnumNivelAcesso.Membro;

        public new event PropertyChangedEventHandler PropertyChanged;

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}