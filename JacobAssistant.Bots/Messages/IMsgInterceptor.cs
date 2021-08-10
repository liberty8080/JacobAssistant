namespace JacobAssistant.Bots.Messages
{
    public interface IMsgInterceptor
    {
        // before command invoke
        bool PreHandle(ref BotMsgRequest request, ref BotMsgResponse response);
        // after command execute
        void AfterCompletion(ref BotMsgRequest request, ref BotMsgResponse response);
    }
}