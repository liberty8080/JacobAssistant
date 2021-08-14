using JacobAssistant.Bots.Messages;
using JacobAssistant.Services;
using Serilog;

namespace JacobAssistant.Bots.Interceptors
{
    public class PermissionInterceptor : IMsgInterceptor
    {
        private readonly PermissionService _service;

        public PermissionInterceptor(PermissionService service)
        {
            _service = service;
        }

        public bool PreHandle(ref BotMsgRequest request, ref BotMsgResponse response)
        {
            var permitted = _service.CheckPermission(request.From, request.Command);
            if (permitted) return true;
            response.Text = "Permission Denied";
            return false;
        }

        public void AfterCompletion(ref BotMsgRequest request, ref BotMsgResponse response)
        {
            Log.Information("Permit AfterCompletion , Do Nothing");
        }
    }
}