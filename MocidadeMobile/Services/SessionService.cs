using MocidadeMobile.Models;
using Microsoft.Maui.Storage;
using MocidadeMobile.Enums;

namespace MocidadeMobile.Services
{
    public class SessionService
    {
        private const string UserIdKey = "UserId";
        private const string UserNameKey = "UserName";
        private const string UserCPFKey = "UserCPF";
        private const string UserAccessLevelKey = "UserAccessLevel";

        public void SaveUserSession(UsuarioViewModel usuario)
        {
            Preferences.Set(UserIdKey, usuario.Id);
            Preferences.Set(UserNameKey, usuario.Nome);
            Preferences.Set(UserCPFKey, usuario.CPF);
            Preferences.Set(UserAccessLevelKey, (int)usuario.NivelAcesso);
        }

        public UsuarioViewModel GetUserSession()
        {
            if (Preferences.ContainsKey(UserIdKey))
            {
                return new UsuarioViewModel
                {
                    Id = Preferences.Get(UserIdKey, 0),
                    Nome = Preferences.Get(UserNameKey, string.Empty),
                    CPF = Preferences.Get(UserCPFKey, string.Empty),
                    NivelAcesso = (EnumNivelAcesso)Preferences.Get(UserAccessLevelKey, 0)
                };
            }
            return new UsuarioViewModel();
        }

        public void ClearUserSession()
        {
            Preferences.Remove(UserIdKey);
            Preferences.Remove(UserNameKey);
            Preferences.Remove(UserCPFKey);
            Preferences.Remove(UserAccessLevelKey);
        }
    }
}