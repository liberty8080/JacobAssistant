using System.Collections.Generic;
using JacobAssistant.Bots.Commands;
using JacobAssistant.Bots.Interceptors;

namespace JacobAssistant.Bots.Messages
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IEnumerable<IMsgInterceptor> _interceptors;

        public MessageDispatcher(IEnumerable<IMsgInterceptor> interceptors)
        {
            _interceptors = interceptors;
        }

        public void DoDispatch(ref BotMsgRequest request, ref BotMsgResponse response)
        {
            if (request.Command == null)
            {
                response.Text = "Command Not Found";
                return;
            }

            var chain = GetInterceptorChain();
            // do intercept
            var preHandle = chain.ApplyPreHandle(ref request, ref response);
            if (!preHandle)
            {
                return;
            }
            // resolve and execute
            var command = ICommand.GetCommand(request.Command);
            command.Execute(ref request, ref response);
            
            chain.ApplyCompletion(ref request,ref response);
        }

        private InterceptorChain GetInterceptorChain()
        {
            return new(_interceptors);
        }
    }
}