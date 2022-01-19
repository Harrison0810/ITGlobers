using Rollbar;
using System;

namespace ManageOwnerships.Infrastructure.Classes
{
    public class ErrorLoggingClient : IErrorLoggingClient
    {
        string IErrorLoggingClient.Report(Exception ex)
        {
            RollbarLocator.RollbarInstance.Error(ex);
            return ex.Message;
        }

        void IErrorLoggingClient.Warning(object obj)
        {
            RollbarLocator.RollbarInstance.Warning(obj);
        }
    }
}
