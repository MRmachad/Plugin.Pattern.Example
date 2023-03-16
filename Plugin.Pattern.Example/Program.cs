using Plugin.Pattern.Example.Interfaces;
using System.Reflection;
using System.Runtime.Loader;
using System.Runtime.Versioning;

namespace Program
{
    public class Program
    {
        public static string PathPlugins = "PLUGINS_PATH";
        public static Dictionary<string, IPlugin> Plugins = new Dictionary<string, IPlugin>();
        public static void Main(String[] arg)
        {
            LoadPlugins();
            Console.WriteLine("Inicio de Aplicação: Busca e execução de plugins \n");

            foreach (var key in Plugins.Keys)
            {
                Console.WriteLine(Plugins[key].Execute());
            }

            Console.WriteLine("Fim da Aplicação...\n");
        }

        public static void LoadPlugins()
        {
            foreach(var pluginDLL in Directory.GetFiles(Environment.GetEnvironmentVariable(PathPlugins) ?? ""))
            {
                var AssemblyContext = new AssemblyLoadContext(pluginDLL);
                var Assembly = AssemblyContext.LoadFromAssemblyPath(pluginDLL);

                if (Assembly != null)
                    Assembly.GetTypes()
                        .Where(t =>
                            t != null &&
                            t.Namespace != null &&
                            t.BaseType != null &&
                            t.IsClass &&
                            (t.GetInterfaces().Contains(typeof(IPlugin))))
                        .ToList()
                        .ForEach((plg) =>
                        {
                            var plugin = Activator.CreateInstance(plg) as IPlugin;
                            if(plugin != null)
                                Plugins.TryAdd(Path.GetFileNameWithoutExtension(pluginDLL), plugin);
                        });
            }


         }
    }
}