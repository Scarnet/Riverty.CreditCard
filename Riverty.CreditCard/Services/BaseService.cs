using Riverty.CreditCard.Commands;
using Riverty.CreditCard.Queries;
using Riverty.CreditCard.Validators;

namespace Riverty.CreditCard.Services
{
    public abstract class BaseService<T, N> where T : BaseCommand where N : BaseQuery
    {
        protected T Command { get; set; }
        protected Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
        protected List<IValidator<T>> Validators { get; set; } = new List<IValidator<T>>();

        public virtual bool Validate()
        {
            foreach(var validator in Validators)
            {
                var isValid = validator.IsValid(Command);

                if(!isValid)
                {
                    if (Errors.ContainsKey(validator.Field))
                        Errors[validator.Field].Add(validator.ErrorMessage);
                    else
                        Errors[validator.Field] = new List<string> { validator.ErrorMessage };
                }
            }

            return !Errors.Any();
        }

        public virtual Task<N> Execute(T command) 
        {
            Command = command;
            Validate();
            return null;
        }
    }
}
