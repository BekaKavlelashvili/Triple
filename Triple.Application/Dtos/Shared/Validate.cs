using System;

namespace Triple.Application.Dtos.Shared
{
    public class Validate : Attribute
    {
        public Type Validator { get; set; }

        public Validate(Type validator)
        {
            this.Validator = validator;
        }
    }
}
