using System.Text.Json.Serialization;
using EnadlaInventory.Core.Validation;
using EnadlaInventory.Core.Validation.Validators;

namespace EnadlaInventory.Core.Communication.Models
{
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public class ExtraUserInfo : ObservableAndValidatableObject<ExtraUserInfo>
    {
        private string _uid;
        private DateTime _creationDate;
        private string _creatorMachine;

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

        [JsonPropertyName("creation_date")]
        public DateTime CreationDate
        {
            get
            {
                return this._creationDate;
            }
            set
            {
                this.SetProperty<DateTime>(ref this._creationDate, value, true, nameof(CreationDate));
            }
        }

        [JsonPropertyName("creator_machine")]
        public string CreatorMachine
        {
            get
            {
                return this._creatorMachine;
            }
            set
            {
                this.SetProperty<string>(ref this._creatorMachine, value, true, nameof(CreatorMachine));
            }
        }

        public ExtraUserInfo(params string[] defaultRuleSets)
        {
            //TODO: make tha validator singleton and get the instace from there
            DefaultRuleSets = defaultRuleSets;
            PrepareValidation(this, new ExtraUserInfoValidator());
        }
    
    }
}
