using System;
using JacobAssistant.Bots.Commands;
using JacobAssistant.Services;
using Telegram.Bot.Args;

namespace JacobAssistant.Bots.Messages
{
    public class PermissionHandler : BaseMessageHandler
    {
        private readonly PermissionService _permissionService;

        public PermissionHandler(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public override IResult Handle(MsgEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override IResult Handle<T>(T sender, MsgEventArgs e)
        {
            var cmdName = ICommand.ParseCommandName(e.Message);
            if (!ICommand.IsCommand(cmdName)) return null;
            var check = _permissionService.CheckPermission(e.Message.From, cmdName);
            return check ? Next.Handle(sender,e) : new Result{Text = "permission denied"};
        }
    }
}