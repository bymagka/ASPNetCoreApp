using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ASPNetCoreApp.Domain.Identity;
using ASPNetCoreApp.Services.Infostructure;
using Microsoft.AspNetCore.Identity;
using ASPNetCoreApp.Interfaces.Services.Identity;

namespace ASPNetCoreApp.WebAPI.Clients.Identity
{
    public class RolesClient : BaseClient, IRolesClient
    {
        public RolesClient(HttpClient client) : base(client,WebApiAdresses.Identity.Roles)
        {

        }


        #region IRoleStore<Role>

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancel)
        {
            var response = await PostAsync(Adress, role, cancel).ConfigureAwait(false);
            var result = await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadFromJsonAsync<bool>(cancellationToken: cancel).ConfigureAwait(false);

            return result
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancel)
        {
            var response = await PutAsync(Adress, role, cancel).ConfigureAwait(false);
            var result = await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadFromJsonAsync<bool>(cancellationToken: cancel).ConfigureAwait(false);

            return result
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancel)
        {
            var response = await PostAsync($"{Adress}/Delete", role, cancel).ConfigureAwait(false);
            var result = await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadFromJsonAsync<bool>(cancellationToken: cancel).ConfigureAwait(false);

            return result
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancel)
        {
            var response = await PostAsync($"{Adress}/GetRoleId", role, cancel).ConfigureAwait(false);
            return await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadAsStringAsync(cancel)
               .ConfigureAwait(false);
        }

        public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancel)
        {
            var response = await PostAsync($"{Adress}/GetRoleName", role, cancel).ConfigureAwait(false);
            return await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadAsStringAsync(cancel)
               .ConfigureAwait(false);
        }

        public async Task SetRoleNameAsync(Role role, string name, CancellationToken cancel)
        {
            var response = await PostAsync($"{Adress}/SetRoleName/{name}", role, cancel).ConfigureAwait(false);
            role.Name = await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadAsStringAsync(cancel)
               .ConfigureAwait(false);
        }

        public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancel)
        {
            var response = await PostAsync($"{Adress}/GetNormalizedRoleName", role, cancel).ConfigureAwait(false);
            return await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadAsStringAsync(cancel)
               .ConfigureAwait(false);
        }

        public async Task SetNormalizedRoleNameAsync(Role role, string name, CancellationToken cancel)
        {
            var response = await PostAsync($"{Adress}/SetNormalizedRoleName/{name}", role, cancel).ConfigureAwait(false);
            role.NormalizedName = await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadAsStringAsync(cancel)
               .ConfigureAwait(false);
        }

        public async Task<Role> FindByIdAsync(string id, CancellationToken cancel)
        {
            return await GetAsync<Role>($"{Adress}/FindById/{id}", cancel)
               .ConfigureAwait(false);
        }

        public async Task<Role> FindByNameAsync(string name, CancellationToken cancel)
        {
            return await GetAsync<Role>($"{Adress}/FindByName/{name}", cancel)
               .ConfigureAwait(false);
        }

        #endregion

    }
}
