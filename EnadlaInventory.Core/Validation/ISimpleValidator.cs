using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnadlaInventory.Core.Validation
{
    public interface ISimpleValidator<TValidableObj>
    {
        public ValidationResult Validate(TValidableObj obj, string? propertyName = null, string[]? ruleSets = null);
    }
}
