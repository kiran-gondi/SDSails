namespace SDSailsGateway.Interfaces
{
    using SDSailsModel.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICommonGateway
    {
        UserInfo Login(UserInfo userInfo);
    }
}
