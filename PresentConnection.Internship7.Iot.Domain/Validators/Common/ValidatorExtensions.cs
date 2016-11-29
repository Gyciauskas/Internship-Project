using System.Collections.Generic;
using CodeMash.Net;
using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, IList<TElement>> MustContainAtLeastOneItem<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new ListMustContainItemValidator<TElement>());
        }
    
        public static IRuleBuilderOptions<T, TElement> UniqueNameIsInCorrectFormatAndUnique<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)  where TElement : IEntityWithUniqueName
        {
            return ruleBuilder.SetValidator(new UniqueNameValidator<TElement>());
        }
    }
}