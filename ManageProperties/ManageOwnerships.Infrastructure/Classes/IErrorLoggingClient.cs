using System;

namespace ManageOwnerships.Infrastructure.Classes
{
    public interface IErrorLoggingClient
    {
        string Report(Exception ex);
        void Warning(object obj);
    }
}
