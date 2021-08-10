namespace JacobAssistant.Bots.Messages
{
    public interface IMessageDispatcher
    {
        void DoDispatch(ref BotMsgRequest request,ref BotMsgResponse response);
    }
}