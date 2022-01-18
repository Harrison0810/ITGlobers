using System;

namespace ManageProperties.Infrastructure.Classes
{
    public interface IErrorLoggingClient
    {
        string Report(Exception ex);
        void Warning(object obj);
    }
}
