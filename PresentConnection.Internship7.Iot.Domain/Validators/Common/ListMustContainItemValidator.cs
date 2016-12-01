using System.Collections.Generic;
using FluentValidation.Validators;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class ListMustContainItemValidator<T> : PropertyValidator
    {
        public ListMustContainItemValidator()
            : base("Property {PropertyName} should contain at least one item!")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var list = context.PropertyValue as IList<T>;
            return list != null && list.Count > 0;
        }
    }
}