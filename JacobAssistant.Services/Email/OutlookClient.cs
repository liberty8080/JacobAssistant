namespace JacobAssistant.Services.Email
{
    public class OutlookClient : CustomEmailImapClient
    {
        public OutlookClient()
        {
            Host = "outlook.office365.com";
            Port = 993;
            UseSsl = true;
        }
    }
}