using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using InsumosAPI.Entities;
using InsumosAPI.Repositories.LoginRepository;

namespace InsumosAPI.Utils
{
    public class CRUDInterceptor : ISaveChangesInterceptor
    {
        private readonly IUserAccessRepository _userAccessRepository;

        public CRUDInterceptor(IUserAccessRepository userAccessRepository)
        {
            _userAccessRepository = userAccessRepository;
        }

        public void OnBeforeSaveChanges(DbContext dbContext)
        {
            var currentUser = _userAccessRepository.ObtenerUsuarioLogin();
            foreach (var entry in dbContext.ChangeTracker.Entries<CRUDBase>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.FechaCreacion = DateTime.Now;
                    entry.Entity.UsuarioCreacion = currentUser;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.FechaCreacion = DateTime.Now;
                    entry.Entity.UsuarioModificacion = currentUser;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.FechaCreacion = DateTime.Now;
                    entry.Entity.UsuarioEliminacion = currentUser;
                }
            }
        }

    }

}
