namespace LibraryManagementCleanArchitecture.Application
{
    public class Result<T>
    {
        public bool IsSuccess { get; }

        public T? Value { get; }

        public string? Error { get; }

        public Result(bool isSuccess, T? value, string? error)
        {
            this.IsSuccess = isSuccess;
            this.Value = value;
            this.Error = error;
        }

        public static Result<T> Success(T value) => new (true, value, null);

        public static Result<T> Failure(string error) => new (false, default, error);
    }
}
