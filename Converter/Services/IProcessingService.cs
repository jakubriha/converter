namespace Converter.Services
{
    /// <summary>
    /// Represents a service, which can determine whether it is a valid service.
    /// </summary>
    public interface IProcessingService<T>
    {
        /// <summary>
        /// Determines, whether this service is valid given parameter.
        /// </summary>
        /// <param name="parameter">Parameter used to determine validity.</param>
        /// <returns>true if the service is a valid service; otherwise, false.</returns>
        bool IsValidService(T parameter);
    }
}
