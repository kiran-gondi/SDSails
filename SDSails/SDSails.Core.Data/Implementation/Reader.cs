namespace SDSails.Core.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class Reader : IDataRecord, IDataReader
    {
        private readonly DbDataReader _reader;

        private readonly SortedDictionary<string, int> _columnOrdinals = new SortedDictionary<string, int>();

        public Reader(DbDataReader reader)
        {
            this._reader = reader;
        }

        public int GetColumn(string name)
        {
            int value = 0;
            if (!this._columnOrdinals.TryGetValue(name, out value))
            {
                value = this._reader.GetOrdinal(name);
                this._columnOrdinals.Add(name, value);
            }

            return value;
        }

        public T GetValue<T>(int i)
        {
            return (T)this._reader.GetValue(i);
        }

        public bool GetBoolean(int i)
        {
            return this._reader.GetBoolean(i);
        }

        public bool? GetBooleanNullable(int ordinal)
        {
            if (!this._reader.IsDBNull(ordinal))
            {
                return this._reader.GetBoolean(ordinal);
            }

            return null;
        }

        public string GetString(int i)
        {
            return this._reader.GetString(i);
        }

        public string GetStringNullable(int ordinal)
        {
            if (!this._reader.IsDBNull(ordinal))
            {
                return this._reader.GetString(ordinal);
            }

            return string.Empty;
        }

        public int? GetIntNullable(int ordinal)
        {
            if (!this._reader.IsDBNull(ordinal))
            {
                return this._reader.GetInt32(ordinal);
            }

            return null;
        }

        public decimal GetDecimal(int i)
        {
            return this._reader.GetDecimal(i);
        }

        public decimal? GetDecimalNullable(int ordinal)
        {
            if (!this._reader.IsDBNull(ordinal))
            {
                return this._reader.GetDecimal(ordinal);
            }

            return null;
        }


        public Guid GetGuid(int i)
        {
            return this._reader.GetGuid(i);
        }


        public DateTime GetDateTime(int i)
        {
            return this._reader.GetDateTime(i);
        }


        public DateTime? GetDateTimeNullable(int ordinal)
        {
            if (!this._reader.IsDBNull(ordinal))
            {
                return this._reader.GetDateTime(ordinal);
            }
            else
            {
                return null;
            }
        }

        public T GetValue<T>(string name)
        {
            return (T)this._reader.GetValue(this.GetColumn(name));
        }


        public bool GetBoolean(string name)
        {
            var iCol = this.GetColumn(name);
            return this._reader.GetBoolean(iCol);
        }


        public bool GetBoolean(string name, bool defaultValue)
        {
            return this.GetBooleanNullable(name) ?? defaultValue;
        }


        public bool? GetBooleanNullable(string name)
        {
            var iCol = this.GetColumn(name);
            if (!this._reader.IsDBNull(iCol))
            {
                return this._reader.GetBoolean(iCol);
            }

            return null;
        }

        public string GetString(string name)
        {
            var iCol = this.GetColumn(name);
            return this._reader.GetString(iCol);
        }

        public string GetStringNullable(string name)
        {
            var iCol = this.GetColumn(name);
            if (!this._reader.IsDBNull(iCol))
            {
                return this._reader.GetString(iCol);
            }

            return string.Empty;
        }

        public int? GetIntNullable(string name)
        {
            var iCol = this.GetColumn(name);
            if (!this._reader.IsDBNull(iCol))
            {
                return this._reader.GetInt32(iCol);
            }

            return null;
        }


        public decimal GetDecimal(string name)
        {
            var iCol = this.GetColumn(name);
            return this._reader.GetDecimal(iCol);
        }

        public decimal GetDecimal(string name, decimal defaultValue)
        {
            return this.GetDecimalNullable(name) ?? defaultValue;
        }


        public decimal? GetDecimalNullable(string name)
        {
            var iCol = this.GetColumn(name);
            if (!this._reader.IsDBNull(iCol))
            {
                return this._reader.GetDecimal(iCol);
            }

            return null;
        }


        public Guid GetGuid(string name)
        {
            var iCol = this.GetColumn(name);
            return this._reader.GetGuid(iCol);
        }

        public DateTime GetDateTime(string name)
        {
            var icol = this.GetColumn(name);
            return this._reader.GetDateTime(icol);
        }

        public DateTime? GetDateTimeNullable(string name)
        {
            var iCol = this.GetColumn(name);
            if (!this._reader.IsDBNull(iCol))
            {
                return this._reader.GetDateTime(iCol);
            }
            else
            {
                return null;
            }
        }

        public bool Read()
        {
            return this._reader.Read();
        }

        public bool NextResult()
        {
            this._columnOrdinals.Clear();
            return this._reader.NextResult();
        }

        public IEnumerable<TResult> Select<TResult>(Func<Reader, TResult> selector)
        {
            while (this.Read())
            {
                yield return selector.Invoke(this);
            }
        }

        public TResult Single<TResult>(Func<Reader, TResult> selector)
        {
            if (this.Read())
            {
                return selector.Invoke(this);
            }
            else
            {
                return default(TResult);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this._reader.GetEnumerator();
        }

        public void Close()
        {
            this._reader.Close();
        }

        public int Depth
        {
            get { return this._reader.Depth; }
        }

        public DataTable GetSchemaTable()
        {
            return this._reader.GetSchemaTable();
        }

        public bool IsClosed
        {
            get
            {
                return this._reader.IsClosed;
            }
        }

        public int RecordsAffected
        {
            get { return this._reader.RecordsAffected; }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._reader.Dispose();
            }
        }

        public int FieldCount
        {
            get { return this._reader.FieldCount; }
        }

        public byte GetByte(int i)
        {
            return this._reader.GetByte(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return this._reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            return this._reader.GetChar(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return this._reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        public IDataReader GetData(int i)
        {
            return this._reader.GetData(i);
        }

        public string GetDataTypeName(int i)
        {
            return this._reader.GetDataTypeName(i);
        }

        public double GetDouble(int i)
        {
            return this._reader.GetDouble(i);
        }

        public Type GetFieldType(int i)
        {
            return this._reader.GetFieldType(i);
        }

        public float GetFloat(int i)
        {
            return this._reader.GetFloat(i);
        }

        public short GetInt16(int i)
        {
            return this._reader.GetInt16(i);
        }

        public int GetInt32(int i)
        {
            return this._reader.GetInt32(i);
        }
        public long GetInt64(int i)
        {
            return this._reader.GetInt64(i);
        }
        public string GetName(int i)
        {
            return this._reader.GetName(i);
        }

        public int GetOrdinal(string name)
        {
            return this._reader.GetOrdinal(name);
        }

        public object GetValue(int i)
        {
            return this._reader.GetValue(i);
        }

        public object GetValue(string name)
        {
            return this._reader.GetValue(this.GetColumn(name));
        }

        public int GetValues(object[] values)
        {
            return this._reader.GetValues(values);
        }

        public bool IsDBNull(int i)
        {
            return this._reader.IsDBNull(i);
        }


        public object this[string name]
        {
            get
            {
                return this._reader[name];
            }
        }

        public object this[int i]
        {
            get
            {
                return this._reader[i];
            }
        }
    }
}
