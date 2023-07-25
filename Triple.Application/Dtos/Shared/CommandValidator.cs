using FluentValidation;
using FluentValidation.Results;
using Triple.Application.Dtos.Shared;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Triple.Application.Exteptions;

namespace Triple.Application.Dtos.Shared
{
    public abstract class CommandValidator
    {
        protected async Task CommandMustBeValidAsync(object dto)
        {
            var typeOfValidator = (Validate)dto.GetType().GetCustomAttribute(typeof(Validate));

            var validator = (dynamic)Activator.CreateInstance(typeOfValidator.Validator);

            var validationResult = (ValidationResult)(await ((dynamic)validator).ValidateAsync((dynamic)dto));

            if (!validationResult.IsValid)
                throw new DtoNotValidException(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        public abstract Task CommandMustBeValidAsync();
    }
}
