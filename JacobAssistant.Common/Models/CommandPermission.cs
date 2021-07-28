using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace JacobAssistant.Common.Models
{
    public class CommandPermission
    {
        [Key]
        public int Id { get; set; }
        public string CommandName { get; set; }
        [Comment("0: disable, 1:enable")]
        public int Permission { get; set; }
        public string UserId { get; set; }
    }

    public static class PermissionType
    {
        public const int Disable = 0;
        public const int Enable = 1;
    }
}