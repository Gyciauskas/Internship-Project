using System.Linq;
using CodeMash.Net;
using FluentValidation.Validators;
using MongoDB.Bson;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class UniqueNameValidator<T> : PropertyValidator  where T : IEntityWithUniqueName
    { 
        public UniqueNameValidator()
            : base("Property {PropertyName} should be unique in database and in correct format !")
        {

        }

        protected override bool IsValid(PropertyValidatorContext context) 
        {
            var entityWithUniqueName = (T)context.PropertyValue;

            if (string.IsNullOrWhiteSpace(entityWithUniqueName.UniqueName))
            {
                return false;
            }

            if (entityWithUniqueName.UniqueName.Contains(" ") || entityWithUniqueName.UniqueName.Any(char.IsUpper))
            {
                return false;
            }

            var deviceFromDb = Db.FindOne<T>(x => x.UniqueName ==  entityWithUniqueName.UniqueName && x.Id != entityWithUniqueName.Id);

            // TODO : CodeMash should return null instead of default object
            return deviceFromDb.Id == ObjectId.Empty;
        }
    }
}