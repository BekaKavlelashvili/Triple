using Triple.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Shared
{
    public interface ICommandMediator
    {
        public Task<Result> ExecuteAsync<T>(T dto);
    }
}
