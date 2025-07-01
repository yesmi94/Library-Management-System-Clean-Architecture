namespace LibraryManagementCleanArchitecture.Persistance
{
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using Microsoft.EntityFrameworkCore.Storage;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;

        private IDbContextTransaction? currentTransaction;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public async Task<int> CompleteAsync() {
            try
            {
                currentTransaction = await context.Database.BeginTransactionAsync();
                var result = await context.SaveChangesAsync();
                await currentTransaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                if(currentTransaction == null)
                {
                    await currentTransaction.RollbackAsync();
                }
                throw;
            }
            finally {

                if (currentTransaction != null) { 
                    await currentTransaction.DisposeAsync();    
                    currentTransaction = null;
                }
            }
        }
    }

}
