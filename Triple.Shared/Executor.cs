using Triple.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Shared
{
    public class Executor
    {
        public Result Succeeded()
        {
            return Result.CreateSucceeded();
        }

        public Result NotFound()
        {
            return Result.CreateNotFound();
        }

        public Result<T> Succeeded<T>(T @object)
        {
            return new Result<T>(@object);
        }

        public Result Succeed() => new Result();

        public Result Succeeded(Guid entityId) => new Result() { EntityId = entityId };

        public Result Failed(string message) => new Result(message);

        public Result<T> Failed<T>(string failed)
        {
            return new Result<T>(failed);
        }

        public Result<T> Failed<T>(Error error)
        {
            return new Result<T>(error);
        }

        public Result<T> CreateNotFound<T>()
        {
            return Result<T>.CreateNotFound();
        }
    }
}
