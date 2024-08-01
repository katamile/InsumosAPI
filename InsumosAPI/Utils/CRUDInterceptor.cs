using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using InsumosAPI.Entities;

namespace InsumosAPI.Utils
{
    public class CRUDInterceptor : ISaveChangesInterceptor
    {
        public void OnBeforeSaveChanges(DbContext dbContext)
        {
            foreach (var entry in dbContext.ChangeTracker.Entries<CRUDBase>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.FechaCreacion = DateTime.UtcNow;
                    entry.Entity.UsuarioCreacion = "System";
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.FechaModificacion = DateTime.UtcNow;
                    entry.Entity.UsuarioModificacion = "System";
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.FechaEliminacion = DateTime.UtcNow;
                    entry.Entity.UsuarioEliminacion = "System";
                }
            }
        }
    }

}
