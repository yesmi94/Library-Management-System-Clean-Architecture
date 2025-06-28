namespace LibraryManagementCleanArchitecture.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
    }

}
