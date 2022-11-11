using CommunityToolkit.Mvvm.ComponentModel;
using EnadlaInventory.Core.Communication.Models;
using EnadlaInventory.Core.Validation.Validators;

namespace EnadlaInventory.UI.ViewModels
{
    internal class SignUpVM : ObservableObject
    {
        private UserInfo _currentUserInfo = new UserInfo(UserInfoValidator.RS_AUTH, UserInfoValidator.RS_SIGNUP_EXTRAS);

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
