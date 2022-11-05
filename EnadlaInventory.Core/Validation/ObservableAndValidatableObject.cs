using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace EnadlaInventory.Core.Validation
{
    //TODO: analize the posibilities to create a property to access the internal saved error
    //in the class instead of GetErrors method - Jimy Aguasviva 3-November-2022
    public abstract class ObservableAndValidatableObject<TSelf> : ObservableObject, INotifyDataErrorInfo
    {
        [MemberNotNullWhen(true, new string[] { nameof(_target), nameof(_validator) })]
        private bool _isPreparedToValidate => _target is not null && _validator is not null;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private TSelf? _target;
        private ISimpleValidator<TSelf>? _validator;


        public string[]? DefaultRuleSets { get; set; }


        private void SetAllPropertiesErrors(IEnumerable<ValidationFailure> errors)
        {
            _errors.Clear();

            foreach (var error in errors)
            {
                if (!_errors.ContainsKey(error.PropertyName))
                    _errors.Add(error.PropertyName, new List<string>());

                _errors[error.PropertyName].Add(error.ErrorCode);
            }
        }

        private void SetSpecificPropertyErrors(IEnumerable<ValidationFailure> errors, string propertyName)
        {
            if (errors.Count() <= 0)
            {
                if (_errors.ContainsKey(propertyName))
                    _errors.Remove(propertyName);
            }
            else
            {
                var specificPropertyErrors = new List<string>(
                    errors
                        .Where(x => x.PropertyName == propertyName)
                        .Select(x => x.ErrorCode));

                _errors[propertyName] = specificPropertyErrors;
            }
        }


        protected void SetProperty<TProperty>(ref TProperty fieldRef, TProperty newValue, bool validate, [CallerMemberName] string? propertyName = null)
        {
            SetProperty(ref fieldRef, newValue, propertyName);

            if (validate)
                Validate(propertyName);
        }


        public void PrepareValidation(TSelf target, ISimpleValidator<TSelf> validator)
        {
            _target = target;
            _validator = validator;
        }

        public void Validate(string? propertyName = null, string[]? ruleSets = null)
        {
            if (!_isPreparedToValidate)
                throw new InvalidOperationException("the object was not prepared to be validated yet");

            if (ruleSets is null)
                ruleSets = DefaultRuleSets;

            var result = _validator.Validate(_target, propertyName, ruleSets);

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                SetAllPropertiesErrors(result.Errors);
                propertyName = null;
            }
            else
                SetSpecificPropertyErrors(result.Errors, propertyName);

            ErrorsChanged?.Invoke(_target, new DataErrorsChangedEventArgs(propertyName));
        }


        #region InotifyDataErrorInfo Implementation

        public bool HasErrors => _errors.Count >= 1;


        public IEnumerable GetErrors(string? propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                List<string> allErrors = new List<string>();

                foreach (var pair in _errors)
                    allErrors.AddRange(pair.Value);

                return allErrors;
            }

            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : new List<string>();

        }


        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        #endregion
    }
}
