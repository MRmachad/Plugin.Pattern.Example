using Plugin.Pattern.Example.Interfaces;

namespace Plugin.Pattern.Example.APP_UM
{
    public class APP : IPlugin
    {
        public string Execute()
        {
            return "Eu sou o primeiro app e estou aqui fazendo minhas coisas \n";
        }
    }
}