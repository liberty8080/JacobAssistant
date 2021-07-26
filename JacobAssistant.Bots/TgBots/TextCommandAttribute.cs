using System;

namespace JacobAssistant.Bots.Bots
{
    //Bot权限
    public enum BotPermission
    {
        Admin,
        AnyOne
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class TextCommandAttribute : Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Method)]
    public class Cmd : Attribute
    {
        public Cmd(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public BotPermission Permission { get; set; } = BotPermission.Admin;
        public int Order { get; set; }
        public string Desc { get; set; }
    }
}