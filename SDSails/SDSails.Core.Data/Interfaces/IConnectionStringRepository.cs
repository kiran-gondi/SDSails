namespace SDSails.Core.Data
{
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using SDSails.Core.Data.Entities;

    /// <summary>
    /// Interface ConnectionStringRepository.
    /// </summary>
    public interface IConnectionStringRepository
    {
        /// <summary>
        /// Gets the Database ConnectionStrings.
        /// </summary>        
        /// <returns>Connection Strings.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Collection<Connection> GetDatabaseConnectionStrings();
    }
}
