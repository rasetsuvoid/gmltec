using gmltec.application.Contracts.Persistence;
using gmltec.Domain.Entities;
using gmltec.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;


        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {

            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task MarkAsDeletedAsync(Expression<Func<T, bool>> predicate)
        {
            T entity = await _context.Set<T>().Where(predicate)?.FirstOrDefaultAsync()!;

            if (entity != null)
            {
                // Marcar la entidad como eliminada en lugar de eliminarla físicamente
                entity.Active = false;
                entity.IsDeleted = true;
                entity.UpdatedDate = DateTime.Now;

                // Actualizar la entidad en el contexto
                _context.Set<T>().Update(entity);
            }
            else
            {
                throw new Exception("No se encontró el registro");
            }
        }

        public async Task<T> GetByIdWithoutExpressionAsync(long id)
        {
            try
            {
                return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id)!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> GetByIdWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var query = _context.Set<T>().Where(predicate);

                // Agregar includes a la consulta
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
