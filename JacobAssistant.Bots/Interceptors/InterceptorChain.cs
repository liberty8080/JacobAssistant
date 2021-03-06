using System;
using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Bots.Messages;

namespace JacobAssistant.Bots.Interceptors
{
    public class InterceptorChain
    {

        private readonly List<IMsgInterceptor> _interceptorList;
        private int _index;

        
        public InterceptorChain(IEnumerable<IMsgInterceptor> interceptors)
        {
            _interceptorList = interceptors.ToList();
            _index = _interceptorList.Count;
        }

        public bool ApplyPreHandle(ref BotMsgRequest request, ref BotMsgResponse response)
        {
            // 没有就不管了
            if (_interceptorList.Count == 0)
            {
                return true;
            }

            for (var i = 0; i < _interceptorList.Count; i++)
            {
                var msgInterceptor = _interceptorList[i];
                if (msgInterceptor.PreHandle(ref request, ref response)) continue;
                _index = i;
                return false;
            }

            return true;
        }

        public void ApplyCompletion(ref BotMsgRequest request,ref BotMsgResponse response)
        {
            if (_interceptorList.Count <= 0) return;
            if (_index == 0)
            {
                
            }
            for (var i = 0; i < _index; i++)
            {
                _interceptorList[i].AfterCompletion(ref request,ref response);
            }
        }
    }
}