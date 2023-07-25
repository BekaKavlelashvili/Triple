using System;
using System.Collections.Generic;

namespace Triple.Shared.Results
{
    public class Result : Result<object>
    {
        public Result()
        {
            MakeSucceded();
        }

        public Result(object @object) : base(@object)
        {
        }

        public Result(Error error) : base(error)
        {
        }

        public Result(List<string> errors) : base(errors)
        {

        }

        public Result(string errorMessage) : base(errorMessage)
        {
        }

        public void AddError(object somethingWentWrong)
        {
            throw new NotImplementedException();
        }
    }
}