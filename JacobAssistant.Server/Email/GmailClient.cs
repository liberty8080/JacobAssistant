using MailKit.Net.Proxy;
using MailKit.Security;

namespace JacobAssistant.Email
{
    public class GmailClient : CustomEmailImapClient
    {
        public GmailClient()
        {
            Host = "imap.gmail.com";
            Port = 993;
            UseSsl = true;
            // ProxyClient = new Socks5Client("192.168.98.1",7890);
        }

        public new void Connect()
        {
            base.Connect(Host, Port, SecureSocketOptions.SslOnConnect);
        }
    }
}