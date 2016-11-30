using FluentValidation.Validators;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class SensitiveDataValidator<T> : PropertyValidator  where T : IEntityWithSensitiveData
    {
        private readonly string clientId;

        public SensitiveDataValidator(string clientId)
            : base("Access is denied")
        {
            this.clientId = clientId;
        }

        protected override bool IsValid(PropertyValidatorContext context) 
        {
            var entityWithResponsibleUser = (T)context.PropertyValue;

            if (string.IsNullOrWhiteSpace(entityWithResponsibleUser.ClientId) || string.IsNullOrWhiteSpace(clientId))
            {
                return false;
            }
            return clientId == entityWithResponsibleUser.ClientId;
        }
    }
}