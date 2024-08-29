namespace SDSailsGateway.AutoMapperInitializations
{
    using AutoMapper;
    using System.Data;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using SDSailsModel.Models;
    using SDSails.Core.Data.Entities;
    using SDSails.Core.Data.Mappers;
    using SDSails.Core.Data;

    public class ApplicationdataAutomapperSupplier : IAutoMapperSupplier
    {

        public ApplicationdataAutomapperSupplier()
        {
            this.RegisterMappers();
        }

        public void RegisterMappers()
        {
            Mapper.CreateMap<IDataReader, Connection>();
            Mapper.CreateMap<IDataRecord, UserInfo>();
        }
    }
}
