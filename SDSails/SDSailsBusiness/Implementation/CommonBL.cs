namespace SDSailsBusiness.Implementation
{
    using SDSailsBusiness.Interfaces;
    using SDSailsGateway.Implementation;
    using SDSailsGateway.Interfaces;
    using SDSailsModel.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommonBL : ICommonBL
    {
        private readonly ICommonGateway _commonGateway;

        public CommonBL(ICommonGateway commonGateway)
        {
            _commonGateway = commonGateway;
        }

        public UserInfo Login(UserInfo userInfo)
        {
            return _commonGateway.Login(userInfo);
        }
    }
}
