using System.Collections.Generic;
using System.Linq;
using FluentValidation.Validators;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class SensitiveListDataValidator<T> : PropertyValidator  where T : IEntityWithSensitiveData
    {
        private readonly string clientId;

        public SensitiveListDataValidator(string clientId)
            : base("Access is denied")
        {
            this.clientId = clientId;
        }

        protected override bool IsValid(PropertyValidatorContext context) 
        {
            var resposibleClients = context.PropertyValue as IList<T>;

            if (resposibleClients != null && resposibleClients.Any())
            {
                return resposibleClients.All(x => x.ClientId == clientId);
            }
            return true;
        }
    }
}