using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using MocidadeMobile.Views;
using MocidadeMobile.Enums;
using MvvmHelpers;

namespace MocidadeMobile.Models
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly UsuarioViewModel _loggedInUser;

        public MenuViewModel(UsuarioViewModel loggedInUser)
        {
            _loggedInUser = loggedInUser;
        }

        public bool IsAdministrador => _loggedInUser.NivelAcesso == EnumNivelAcesso.Administrador;
        public bool IsUsuario => _loggedInUser.NivelAcesso == EnumNivelAcesso.Usuario;
        public bool IsMembro => _loggedInUser.NivelAcesso == EnumNivelAcesso.Membro;

        //Permissões combinadas
        public bool PossuiPermissaoCarteirinha => IsAdministrador || IsUsuario || IsMembro;
        public bool PossuiPermissaoRegistroPresenca => IsAdministrador || IsUsuario;
        public bool PossuiPermissaoConfiguracao => IsAdministrador || IsUsuario;

        public new event PropertyChangedEventHandler PropertyChanged;

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}