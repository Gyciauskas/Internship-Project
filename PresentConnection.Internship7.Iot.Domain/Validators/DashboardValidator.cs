﻿using FluentValidation;


namespace PresentConnection.Internship7.Iot.Domain
{
    public class DashboardValidator : AbstractValidator<Dashboard>
    {
        public DashboardValidator()
        {
            RuleFor(r => r.ClientId).NotEmpty();
            RuleFor(r => r.Widgets).SetCollectionValidator(new WidgetValidator());

        }

    }

    public class WidgetValidator : InlineValidator<Widget> 
    {
        public WidgetValidator()
        {
            RuleFor(x => x.WidgetType).NotEqual(WidgetType.NotSet);
        }
    }
}