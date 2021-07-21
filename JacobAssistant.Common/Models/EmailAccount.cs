namespace JacobAssistant.Common.Models
{
    public partial class EmailAccount
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? State { get; set; }
        public int? Type { get; set; }
    }

    public enum EmailAccountState
    {
        InActive=0,Active=1
    }

    public enum EmailAccountType
    {
        Custom=0,Outlook=1,Gmail=2
    }
}