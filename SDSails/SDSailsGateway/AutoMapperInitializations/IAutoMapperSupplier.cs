namespace SDSailsGateway.AutoMapperInitializations
{
    /// <summary>
    /// <see cref="IAutoMapperSupplier" /> is a marker interface.  It allows an implementing type to indicate to the 
    /// <see cref="ApplicationBootstrapper" /> that the type provides services that need to be registered during the 
    /// bootstrapping process.
    /// </summary>
    public interface IAutoMapperSupplier
    {
        /// <summary>
        /// Allows an implementer to register mappers during the bootstrapping process.
        /// </summary>
        /// <remarks>
        /// <code>
        /// </code>
        /// </remarks>
        void RegisterMappers();
    }
}
