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
            var fechaActual = DateTime.Now;

            foreach (var entry in dbContext.ChangeTracker.Entries<CRUDBase>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.FechaCreacion = fechaActual;
                    entry.Entity.UsuarioCreacion = currentUser;
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity.FechaEliminacion == null) // Solo actualiza si no está eliminada lógicamente
                    {
                        entry.Entity.FechaModificacion = fechaActual;
                        entry.Entity.UsuarioModificacion = currentUser;
                    }
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;

                    entry.Entity.FechaEliminacion = fechaActual;
                    entry.Entity.UsuarioEliminacion = currentUser;
                    entry.Entity.Estado = Globales.INACTIVO;
                }
            }
        }


    }

}
