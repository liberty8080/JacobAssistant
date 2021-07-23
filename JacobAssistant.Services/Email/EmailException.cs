using System;

namespace JacobAssistant.Services.Email
{
    public class EmailException:Exception
    {
        public EmailException(string msg,Exception exception):base(msg,exception)
        {}
    }
}