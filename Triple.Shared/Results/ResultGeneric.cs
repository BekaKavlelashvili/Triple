using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Triple.Shared.Resources;

namespace Triple.Shared.Results
{
    public class Result<T>
    {
        private readonly IList<Error> _errors = new List<Error>();

        /// <summary>
        /// Errors list
        /// </summary>
        /// 
        [JsonProperty("errors")]
        public IEnumerable<Error> Errors => _errors;

        /// <summary>
        /// Result object will be persisted here
        /// </summary>
        public T Object { get; private set; }

        /// <summary>
        /// Current result status whether is failed or succeeded
        /// </summary>
        public ResultStatus Status { get; private set; }

        /// <summary>
        /// Date when this event occured
        /// </summary>
        public DateTime Occured { get; } = DateTime.Now;

        public bool Succeeded => Status == ResultStatus.Succeeded;

        public Guid EntityId { get; set; }

        public Result(T @object)
        {
            Object = @object;
            MakeSucceded();
        }

        public Result(Error error)
        {
            _errors.Add(error);
            MakeFailed();
        }

        public Result(string errorMessage)
        {
            _errors.Add(new Error(errorMessage));
            MakeFailed();
        }

        public Result(List<string> errors)
        {
            errors.ForEach(x =>
            {
                this._errors.Add(new Error(x));
            });
        }

        public Result()
        {

        }

        public static Result CreateSucceeded()
        {
            var result = new Result();
            result.MakeSucceded();
            return result;
        }

        public static Result<T> CreateNotFound()
        {
            var result = new Result<T>();
            result.MakeNotFound();
            return result;
        }

        public void MakeFailed()
        {
            Status = ResultStatus.Failed;
        }

        protected void MakeNotFound()
        {
            Status = ResultStatus.NotFound;
            this._errors.Add(new Error(ApplicationStrings.EntityNotFound));
        }

        protected void MakeSucceded()
        {
            Status = ResultStatus.Succeeded;
        }

        public void AddError(string error)
        {
            this._errors.Add(new Error(error));
        }

        public static implicit operator Result(Result<T> result)
        {
            var newResult = new Result(result.Errors)
            {
                Object = result.Object,
                Status = result.Status
            };
            return newResult;
        }
    }
}