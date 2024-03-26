using AgendamentoDeTarefas.Context;
using AgendamentoDeTarefas.Entites;
using Microsoft.AspNetCore.Identity;

namespace AgendamentoDeTarefas.Models
{
    public class MeuUsuarioStore : IUserStore<MeuUsuario>, IUserPasswordStore<MeuUsuario>
    {
        private readonly OrganizadorContext _context;
        public MeuUsuarioStore(OrganizadorContext context)
        {
            _context = context;
        }
        public async Task<IdentityResult> CreateAsync(MeuUsuario user, CancellationToken cancellationToken)
        {
            _context.MeusUsuarios.Add(user);
            _context.SaveChanges();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(MeuUsuario user, CancellationToken cancellationToken)
        {
            var UsuarioBanco =  _context.MeusUsuarios.Find(user.Id);

            _context.Remove(UsuarioBanco);
            _context.SaveChanges();

            return IdentityResult.Success;
        }        

        public void Dispose()
        {

        }

        public async Task<MeuUsuario?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var usuario =  _context.MeusUsuarios.ToList().Find(
                x => x.Id == userId);
            return usuario;
        }

        public async Task<MeuUsuario?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var usuario = _context.MeusUsuarios.ToList().Find(
                x => x.NormalizedUserName == normalizedUserName);
            return usuario;
        }

        public Task<string?> GetNormalizedUserNameAsync(MeuUsuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string?> GetPasswordHashAsync(MeuUsuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(MeuUsuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string?> GetUserNameAsync(MeuUsuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(MeuUsuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetNormalizedUserNameAsync(MeuUsuario user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(MeuUsuario user, string? passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(MeuUsuario user, string? userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(MeuUsuario user, CancellationToken cancellationToken)
        {
            var usuarioBanco = _context.MeusUsuarios.Find(user.Id);

            usuarioBanco.UserName = user.UserName;
            usuarioBanco.NormalizedUserName = user.NormalizedUserName;
            usuarioBanco.PasswordHash = user.PasswordHash;

            _context.MeusUsuarios.Update(usuarioBanco);
            _context.SaveChanges();

            return IdentityResult.Success;

        }
    }
}
