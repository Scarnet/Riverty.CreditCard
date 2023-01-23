using Riverty.CreditCard.Queries;

namespace Riverty.CreditCard.Features
{
    public class BaseModule
    {
        protected IResult MapResponse(BaseQuery query)
        {
            return query.IsSuccessful ? Results.Ok(query) : Results.BadRequest(query);
        }
    }
}
