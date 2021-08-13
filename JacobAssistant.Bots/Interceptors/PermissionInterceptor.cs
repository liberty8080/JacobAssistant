using JacobAssistant.Bots.Messages;
using JacobAssistant.Services;

namespace JacobAssistant.Bots.Interceptors
{
    public class PermissionInterceptor:IMsgInterceptor
    {
        private readonly PermissionService _service;

        public PermissionInterceptor(PermissionService service)
        {
            _service = service;
        }
        public bool PreHandle(ref BotMsgRequest request, ref BotMsgResponse response)
        {
           return _service.CheckPermission(request.From,request.Command);
        }

        public void AfterCompletion(ref BotMsgRequest request, ref BotMsgResponse response)
        {
            throw new System.NotImplementedException();
        }
    }
}