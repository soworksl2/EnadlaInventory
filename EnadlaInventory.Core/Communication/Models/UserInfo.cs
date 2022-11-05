using EnadlaInventory.Core.Validation;
using EnadlaInventory.Core.Validation.Validators;
using System.Text.Json.Serialization;

namespace EnadlaInventory.Core.Communication.Models
{
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public class UserInfo : ObservableAndValidatableObject<UserInfo>
    {
        private string _uid;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private bool _isVerified;
        private string _ownerName;
        private ExtraUserInfo _extraInfo;

        [JsonPropertyName("uid")]
        public string UID
        {
            get
            {
                return this._uid;
            }
            set
            {
                this.SetProperty<string>(ref this._uid, value, true, nameof(UID));
            }
        }

        [JsonPropertyName("email")]
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this.SetProperty<string>(ref this._email, value, true, nameof(Email));
            }
        }

        [JsonPropertyName("password")]
        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this.SetProperty<string>(ref this._password, value, true, nameof(Password));
            }
        }

        [JsonIgnore]
        public string ConfirmPassword
        {
            get
            {
                return this._confirmPassword;
            }
            set
            {
                this.SetProperty<string>(ref this._confirmPassword, value, true, nameof(ConfirmPassword));
            }
        }

        [JsonPropertyName("is_verified")]
        public bool IsVerified
        {
            get
            {
                return this._isVerified;
            }
            set
            {
                this.SetProperty<bool>(ref this._isVerified, value, true, nameof(IsVerified));
            }
        }

        [JsonPropertyName("owner_name")]
        public string OwnerName
        {
            get
            {
                return this._ownerName;
            }
            set
            {
                this.SetProperty<string>(ref this._ownerName, value, true, nameof(OwnerName));
            }
        }

        [JsonPropertyName("extra_info")]
        public ExtraUserInfo ExtraInfo
        {
            get
            {
                return this._extraInfo;
            }
            set
            {
                this.SetProperty<ExtraUserInfo>(ref this._extraInfo, value, true, nameof(ExtraInfo));
            }
        }

        public UserInfo(params string[] defaultRuleSets)
        {
            DefaultRuleSets = defaultRuleSets;
            PrepareValidation(this,new UserInfoValidator());
        }
    }
}
