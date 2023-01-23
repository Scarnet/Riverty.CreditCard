using Riverty.CreditCard.Commands;
using Riverty.CreditCard.Queries;

namespace Riverty.CreditCard.Services
{
    public interface IService<T, O>  where T : BaseCommand where O : BaseQuery
    {
        /// <summary>
        /// Execute designated service.
        /// </summary>
        /// <param name="command">Service command</param>
        /// <returns>Result query</returns>
        Task<O> Execute(T command);
    }
}
