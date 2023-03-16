using Plugin.Pattern.Example.Interfaces;

namespace Plugin.Pattern.Example.APP_DOIS
{
    public class APP : IPlugin
    {
        public string Execute()
        {
            return "Hello World\n";
        }
    }
}