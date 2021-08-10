using System;

namespace JacobAssistant.Bots.Commands
{
    /// <summary>
    /// 命令执行的结果
    /// </summary>
    [Obsolete("will be removed in next version")]
    public interface IResult
    {
        string Text { get; set; }
    }
}