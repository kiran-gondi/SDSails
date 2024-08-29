namespace SDSails.Core.Data.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using AutoMapper;

    /// <summary>
    /// Class DataAccessMapper.
    /// </summary>
    public class DataAccessMapper : IDataAccessMapper
    {
        /// <summary>
        /// Maps the record.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="record">The param record.</param>
        /// <returns> Returns dataObject.</returns>
        /// <remarks></remarks>
        public T MapRecord<T>(IDataRecord record)
        {
            T dataObject = Map<T>(record);
            return dataObject;
        }

        /// <summary>
        /// Maps the records.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="records">The records.</param>  
        /// <returns>Returns Collection.</returns>      
        [Obsolete("Instead call the method : void MapRecords<T>(IDataReader records, Collection<T> destination)")]
        public Collection<T> MapRecords<T>(IDataReader records)
        {
            IList<T> listItems = Map<IList<T>>(records);
            return ToCollection<T>(listItems);
        }

        /// <summary>
        /// Also can be used when the destination collection property is having private set(er) or even readonly.
        /// </summary>
        /// <typeparam name="T">Destination Type.</typeparam>
        /// <param name="records">Instance of IDataReader implementation.</param>     
        /// <returns>Returns enumItems.</returns>   
        public IEnumerable<T> MapEnumerableRecords<T>(IDataReader records)
        {
            IEnumerable<T> enumItems = Map<IEnumerable<T>>(records);
            return enumItems;
        }

        /// <summary>
        /// Also can be used when the destination collection property is having private set(er) or even readonly.
        /// </summary>
        /// <typeparam name="T">Destination Type.</typeparam>
        /// <param name="records">Instance of IDataReader implementation.</param>
        /// <param name="destination">Collection of type T(Destination Type).</param>
        public void MapRecords<T>(IDataReader records, Collection<T> destination)
        {
            IList<T> listItems = Map<IList<T>>(records);
            for (int index = 0; index < listItems.Count; index++)
            {
                destination.Add(listItems[index]);
            }
        }

        /// <summary>
        /// Maps the records to list.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="records">The records.</param>
        /// <returns>Returns records.</returns>
        /// <remarks></remarks>
        public IList<T> MapRecordsToList<T>(IDataReader records)
        {
            return Map<IList<T>>(records);
        }

        /// <summary>
        /// Maps the specified record.
        /// </summary>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="record">The record.</param> 
        /// <returns>Returns record.</returns>       
        /// <remarks></remarks>
        private static TReturn Map<TReturn>(IDataRecord record)
        {
            return Mapper.Map<IDataRecord, TReturn>(record);
        }

        /// <summary>
        /// Maps the specified records.
        /// </summary>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="records">The records.</param>
        /// <returns>Returns records.</returns>
        /// <remarks></remarks>
        private static TReturn Map<TReturn>(IDataReader records)
        {
            return Mapper.Map<IDataReader, TReturn>(records);
        }

        /// <summary>
        /// To collection.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="listItems">The list items.</param>
        /// <returns>Returns collectionItems.</returns>
        /// <remarks></remarks>
        private static Collection<T> ToCollection<T>(IList<T> listItems)
        {
            Collection<T> collectionItems = new Collection<T>();
            for (int index = 0; index < listItems.Count; index++)
            {
                collectionItems.Add(listItems[index]);
            }

            return collectionItems;
        }
    }
}
