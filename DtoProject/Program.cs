using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DtoProject
{
    class Program
    {
        private static MethodInfo getIntMethod;
        private static MethodInfo getLongMethod;
        private static MethodInfo getDoubleMethod;
        private static MethodInfo getFloatMethod;

        public static int GetIntValue()
        {
            return (int)getIntMethod.Invoke(null, null);
        }

        public static long GetLongValue()
        {
            return (long)getLongMethod.Invoke(null, null);
        }

        public static float GetFloatValue()
        {
            return (float)getFloatMethod.Invoke(null, null);
        }

        public static double GetDoubleValue()
        {
            return (double)getDoubleMethod.Invoke(null, null);
        }

        static void Main(string[] args)
        {
            var pluginAssembly = Assembly.LoadFrom("plugin/DtoPlugin.dll");
            var type = pluginAssembly.GetTypes()[0];
            getIntMethod = type.GetMethod("GetIntValue");
            getLongMethod = type.GetMethod("GetLongValue");
            getFloatMethod = type.GetMethod("GetFloatValue");
            getDoubleMethod = type.GetMethod("GetDoubleValue");

            var faker = new Faker();
            var firstDto = faker.Create<FirstDto>();
            var secondDto = faker.Create<SecondDto>();
            var notDto = faker.Create<int>();
        }
    }
}
