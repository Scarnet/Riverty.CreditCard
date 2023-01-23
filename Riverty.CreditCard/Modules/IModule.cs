namespace Riverty.CreditCard.Modules
{
    public interface IModule
    {
        void DefineEndpoints(WebApplication app);
        void DefineServices(IServiceCollection services, IConfiguration configuration);
    }
}
