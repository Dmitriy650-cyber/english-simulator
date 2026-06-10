namespace EnglishSimulator.Desktop.Models.RepositoryResponses
{
    public record RepositoryResponse(bool IsFail, string ErrorMessage)
    {
        public static RepositoryResponse Success() => new(false, null!);
        public static RepositoryResponse Fail(string errorMessage) => new(true, errorMessage); 
    }

    public record RepositoryResponse<TData>(bool IsFail, TData Data, string ErrorMessage)
    {
        public static RepositoryResponse<TData> Success(TData data) => new(false, data, null!);
        public static RepositoryResponse<TData> Fail(string errorMessage) => new(true, default!, errorMessage);
    }
}
