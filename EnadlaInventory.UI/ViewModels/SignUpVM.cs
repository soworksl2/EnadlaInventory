using CommunityToolkit.Mvvm.ComponentModel;
using EnadlaInventory.Core.Communication.Models;

namespace EnadlaInventory.UI.ViewModels
{
    internal class SignUpVM : ObservableObject
    {
        private UserInfo _currentUserInfo = new UserInfo();

        public UserInfo CurrentUserInfo
        {
            get
            {
                return this._currentUserInfo;
            }
            set
            {
                this.SetProperty<UserInfo>(ref this._currentUserInfo, value, nameof(CurrentUserInfo));
            }
        }
    }
}
