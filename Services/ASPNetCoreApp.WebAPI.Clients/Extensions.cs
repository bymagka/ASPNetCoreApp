using ASPNetCoreApp.Domain.Identity;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Interfaces.TestApi;
using ASPNetCoreApp.WebAPI.Clients;
using ASPNetCoreApp.WebAPI.Clients.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASPNetCoreApp.Services.Infostructure
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityAppWebApiClients(this IServiceCollection serviceCollection)
        {
           serviceCollection.AddHttpClient("ASPNetCoreWebAPI", (s,client)=> client.BaseAddress = new(s.GetRequiredService<IConfiguration>()["WebAPI"]))
                .AddTypedClient<IValuesService, ValuesClient>()
                .AddTypedClient<IEmployeeService, EmployyesClient>()
                .AddTypedClient<IProductData, ProductsClient>()
                .AddTypedClient<IOrderService, OrdersClient>()
                .AddTypedClient<IUserStore<User>, UsersClient>()
                .AddTypedClient<IUserRoleStore<User>, UsersClient>()
                .AddTypedClient<IUserPasswordStore<User>, UsersClient>()
                .AddTypedClient<IUserClaimStore<User>, UsersClient>()
                .AddTypedClient<IUserPhoneNumberStore<User>, UsersClient>()
                .AddTypedClient<IUserLoginStore<User>, UsersClient>()
                .AddTypedClient<IUserTwoFactorStore<User>, UsersClient>()
                .AddTypedClient<IRoleStore<Role>, RolesClient>();

            return serviceCollection;
        }

        public static IdentityBuilder AddIdentityAppWebApiClients(this IdentityBuilder identityBuilder)
        {
            identityBuilder.Services.AddIdentityAppWebApiClients();

            return identityBuilder;
        }
    }
}
