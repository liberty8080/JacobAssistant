using static JacobAssistant.Email.EmailTypes;

namespace JacobAssistant.Models
{
    public partial class EmailAccount
    {
        
        public void SetSmtp()
        {
            switch (Type)
            {
                case (int)Outlook:
                    SmtpServer = "smtp.office365.com";
                    SmtpPort = 587;
                    break;
                case(int)Gmail:
                    SmtpServer = "";
                    SmtpPort = 587;
                    break;
            }
        }
    }
}