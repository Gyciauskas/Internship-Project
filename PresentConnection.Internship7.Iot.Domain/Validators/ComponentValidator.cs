﻿using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class ComponentValidator : AbstractValidator<Component>
    {
        public ComponentValidator()
        {
            RuleFor(r => r.ModelName).NotEmpty();
            RuleFor(r => r.UniqueName).NotEmpty();
            RuleFor(r => r.Images).MustContainAtLeastOneItem();
            RuleFor(x => x).UniqueNameIsInCorrectFormatAndUnique();
        }

    }
}
