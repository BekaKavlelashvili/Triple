using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Triple.Shared.Results;

namespace Triple.Application.Exteptions
{
    public class DtoNotValidException : Exception
    {
        public DtoNotValidException(IEnumerable<string> errors)
        {
            this.Result = new Result(errors.ToList());
        }

        public Result Result { get; set; }
    }
}
