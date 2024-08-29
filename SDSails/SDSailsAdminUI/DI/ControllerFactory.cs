namespace SDSailsAdminUI
{
    using Microsoft.Practices.Unity;
    using SDSailsCommon.Utility;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class ControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                    throw new ArgumentNullException("controllerType");

                if (!typeof(IController).IsAssignableFrom(controllerType))
                    throw new ArgumentException(string.Format(
                        "Type requested is not a controller: {0}",
                        controllerType.Name),
                        "controllerType");

                return MvcUnityContainer.Container.Resolve(controllerType) as IController;
            }
            catch(Exception ex)
            {
                ServerLog.LogIt("GetControllerInstance Factory Controller Failure",
                    ServerLog.DisplayInTextArea(ServerLog.DisplayInTextArea(ServerLog.ObjectToXMLString(ex.Message))));

                return null;
            }

        }
    }

    public static class MvcUnityContainer
    {
        public static UnityContainer Container { get; set; }
    }
}