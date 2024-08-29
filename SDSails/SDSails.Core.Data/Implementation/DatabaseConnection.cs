namespace SDSails.Core.Data
{
    using System.Collections.Generic;
    using SDSails.Core.Data.Entities;

    public static class DatabaseConnection
    {
        private static Dictionary<string, Connection> _connectionStrings = new Dictionary<string, Connection>();

        public static Dictionary<string, Connection> ConnectionStrings
        {
            get
            {
                return _connectionStrings;
            }
        }
    }
}
