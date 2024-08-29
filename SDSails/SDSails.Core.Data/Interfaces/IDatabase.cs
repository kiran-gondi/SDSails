namespace SDSails.Core.Data
{
    public interface IDatabase
    {
        string ConnectionString { get; }
        System.Data.Common.DbProviderFactory DBProviderFactory { get; }
        System.Data.Common.DbConnection CreateConnection();
        SDSails.Core.Data.Command GetProcedureCommand(string procedure);
        SDSails.Core.Data.Command GetProcedureCommand(string procedure, int timeOut);
        SDSails.Core.Data.Command GetTextCommand(string text);
    }
}
