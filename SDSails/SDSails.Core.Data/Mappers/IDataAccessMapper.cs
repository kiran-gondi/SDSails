namespace SDSails.Core.Data.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;

    /// <summary>
    /// Interface IDataAccessMapper.
    /// </summary>
    /// <remarks></remarks>
    public interface IDataAccessMapper
    {
        /// <summary>
        /// Maps the record.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="record">The record.</param>
        /// <returns>Returns record.</returns>
        /// <remarks></remarks>
        T MapRecord<T>(IDataRecord record);

        /// <summary>
        /// Maps the records.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="records">The records.</param>
        /// <returns>Returns records.</returns>
        /// <remarks></remarks>
        [Obsolete("Instead call the method : void MapRecords<T>(IDataReader records, Collection<T> destination)")]
        Collection<T> MapRecords<T>(IDataReader records);

        /// <summary>
        /// Maps the records to list.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="records">The records.</param>
        /// <returns>Returns record.</returns>
        /// <remarks></remarks>
        IList<T> MapRecordsToList<T>(IDataReader records);

        /// <summary>
        /// Maps the records.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="records">The records.</param>
        /// <param name="destination">The destination.</param>
        /// <remarks></remarks>
        void MapRecords<T>(IDataReader records, Collection<T> destination);

        /// <summary>
        /// Maps the enumerable records.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="records">The records.</param>
        /// <returns>Returns record.</returns>
        /// <remarks></remarks>
        IEnumerable<T> MapEnumerableRecords<T>(IDataReader records);
    }
}
