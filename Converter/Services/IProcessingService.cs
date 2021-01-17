namespace Converter.Services
{
    public interface IProcessingService<T>
    {
        bool IsValidService(T parameter);
    }
}
