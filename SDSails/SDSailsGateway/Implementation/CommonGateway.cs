namespace SDSailsGateway.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SDSails.Core.Data;
    using SDSailsGateway.Interfaces;
    using SDSailsModel.Models;
    using SDSails.Core.Data.Mappers;
    using SDSails.Core.Data.Enumerations;
    using SDSailsGateway.Constants;
    using System.Data;
    using SDSailsCommon.Utility;
    using System.Data.SqlClient;
    using System.Globalization;
    using SDSailsGateway.AutoMapperInitializations;

    public class CommonGateway : ICommonGateway
    {
        private IDatabase db;

        private readonly IDataAccessMapper _dataAccessMapper;

        public CommonGateway(IDataAccessMapper dataAccessMapper)
        {
            this.db = Database.GetDatabase(DatabaseName.CPT_WO_SD);
            _dataAccessMapper = dataAccessMapper;
        }

        public UserInfo Login(UserInfo userInfo)
        {
            UserInfo userInfoResult = new UserInfo();

            try
            {
                string storedProcedureName = CommonStoredProcedures.ValidatUser;

                Reader reader = db.GetProcedureCommand(storedProcedureName)
                .AddInParameter(@"EmailAddress", DbType.String, userInfo.UserName)
                .AddInParameter(@"Password", DbType.String, userInfo.Password)
                .AsReader();

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        userInfoResult = this._dataAccessMapper.MapRecord<UserInfo>(reader);
                        reader.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                ServerLog.LogIt("Login Gateway Method Failure",
                    ServerLog.DisplayInTextArea(ServerLog.DisplayInTextArea(ServerLog.ObjectToXMLString(ex.Message))));
            }

            return userInfoResult;
        }

    }
}
