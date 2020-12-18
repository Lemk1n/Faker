using DtoClasses;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DtoProject
{
    public class Faker : IFaker
    {
        private Random random;

        public Faker()
        {
            random = new Random();
        }

        private List<object> GetConstructorParameters(ConstructorInfo constructor)
        {
            var parametersValues = new List<object>();

            foreach (var parameter in constructor.GetParameters())
            {
                var parameterType = parameter.ParameterType;
                var parameterObjectValue = GenerateValue(parameterType);

                if (parameterObjectValue is List<object>)
                {
                    var parameterListObject = (List<object>)parameterObjectValue;

                    if (parameterListObject[0] is int)
                    {
                        parametersValues.Add(parameterListObject.Cast<int>().ToList());
                    }

                    if (parameterListObject[0] is double)
                    {
                        parametersValues.Add(parameterListObject.Cast<double>().ToList());
                    }

                    if (parameterListObject[0] is float)
                    {
                        parametersValues.Add(parameterListObject.Cast<float>().ToList());
                    }

                    if (parameterListObject[0] is long)
                    {
                        parametersValues.Add(parameterListObject.Cast<long>().ToList());
                    }

                    if (parameterListObject[0] is string)
                    {
                        parametersValues.Add(parameterListObject.Cast<string>().ToList());
                    }
                }
                else
                {
                    parametersValues.Add(parameterObjectValue);
                }
            }

            return parametersValues;
        }

        public DtoObjectType Create<DtoObjectType>()
        {
            var objectType = typeof(DtoObjectType);

            if (objectType.GetMethods(BindingFlags.DeclaredOnly).Length > 0 || !objectType.GetInterfaces().Contains(typeof(IDto)))
            {
                return default;
            }

            var constructorsCount = objectType.GetConstructors(BindingFlags.DeclaredOnly).Length;

            if (constructorsCount == 0)
            {
                var dtoObject = (DtoObjectType)Activator.CreateInstance(objectType);

                foreach (var field in objectType.GetFields())
                {
                    HandleFieldFill(dtoObject, field);
                }

                foreach (var property in objectType.GetProperties())
                {
                    HandlePropertyFill(dtoObject, property);
                }

                return dtoObject;
            }
            else if (constructorsCount == 1)
            {
                var constructor = objectType.GetConstructors()[0];
                var @object = constructor.Invoke(GetConstructorParameters(constructor).ToArray());
                return (DtoObjectType)@object;
            }
            else
            {
                var constructors = objectType.GetConstructors();
                var constructor = GetConstructor(constructors.ToList());
                return (DtoObjectType)constructor.Invoke(GetConstructorParameters(constructor).ToArray());
            }
        }

        private ConstructorInfo GetConstructor(List<ConstructorInfo> constructors)
        {
            ConstructorInfo buffer = null;

            foreach (var constructor in constructors)
            {
                var parametersLength = constructor.GetParameters().Length;

                if (buffer == null)
                {
                    buffer = constructor;
                    continue;
                }

                if (parametersLength > buffer.GetParameters().Length)
                {
                    buffer = constructor;
                }
            }

            return buffer;
        }

        private void HandlePropertyFill<DtoObjectType>(DtoObjectType dtoObject, PropertyInfo property)
        {
            if (property.CanWrite)
            {
                var type = property.PropertyType;
                var objectValue = GenerateValue(type);

                if (objectValue is List<object>)
                {
                    var listObject = (List<object>)objectValue;

                    if (listObject[0] is int)
                    {
                        property.SetValue(dtoObject, listObject.Cast<int>().ToList());
                        return;
                    }

                    if (listObject[0] is double)
                    {
                        property.SetValue(dtoObject, listObject.Cast<double>().ToList());
                        return;
                    }

                    if (listObject[0] is float)
                    {
                        property.SetValue(dtoObject, listObject.Cast<float>().ToList());
                        return;
                    }

                    if (listObject[0] is long)
                    {
                        property.SetValue(dtoObject, listObject.Cast<long>().ToList());
                        return;
                    }

                    if (listObject[0] is string)
                    {
                        property.SetValue(dtoObject, listObject.Cast<string>().ToList());
                        return;
                    }
                }
                else
                {
                    property.SetValue(dtoObject, objectValue);
                    return;
                }

                property.SetValue(dtoObject, default);
            }
        }

        private void HandleFieldFill<DtoObjectType>(DtoObjectType dtoObject, FieldInfo field)
        {
            var type = field.FieldType;
            object objectValue;

            objectValue = GenerateValue(type);

            if (objectValue is List<object>)
            {
                var listObject = (List<object>)objectValue;

                if (listObject[0] is int)
                {
                    field.SetValue(dtoObject, listObject.Cast<int>().ToList());
                    return;
                }

                if (listObject[0] is double)
                {
                    field.SetValue(dtoObject, listObject.Cast<double>().ToList());
                    return;
                }

                if (listObject[0] is float)
                {
                    field.SetValue(dtoObject, listObject.Cast<float>().ToList());
                    return;
                }

                if (listObject[0] is long)
                {
                    field.SetValue(dtoObject, listObject.Cast<long>().ToList());
                    return;
                }

                if (listObject[0] is string)
                {
                    field.SetValue(dtoObject, listObject.Cast<string>().ToList());
                    return;
                }

                field.SetValue(dtoObject, default);
            }
            else
            {
                field.SetValue(dtoObject, objectValue);
                return;
            }
        }

        private List<object> GetList(Type type)
        {
            var size = random.Next(100) + 1;

            var list = new List<object>();

            for (int i = 0; i < size; i++)
            {
                list.Add(GenerateValue(type));
            }

            return list;
        }

        private object GenerateValue(Type type)
        {
            if (type == typeof(int))
            {
                return Program.GetIntValue();
            }

            if (type == typeof(long))
            {
                return Program.GetLongValue();
            }

            if (type == typeof(double))
            {
                return Program.GetDoubleValue();
            }

            if (type == typeof(float))
            {
                return Program.GetFloatValue();
            }

            if (type == typeof(DateTime))
            {
                var value = new DateTime();
                value = value.AddYears(random.Next(2020) + 1);
                value = value.AddDays(random.Next(365) + 1);
                value = value.AddHours(random.Next(24) + 1);
                value = value.AddMinutes(random.Next(60) + 1);
                value = value.AddSeconds(random.Next(60) + 1);
                return value;
            }

            if (type == typeof(string))
            {
                var size = random.Next(1000) + 1;
                return GetRandomString(size, false);
            }

            if (type.GetInterfaces().Contains(typeof(IList)))
            {
                var baseType = type.GetGenericArguments()[0];
                return GetList(baseType);
            }

            if (type.GetInterfaces().Contains(typeof(IDto)))
            {
                var frameCount = new StackTrace().FrameCount;

                if (frameCount < 10)
                {
                    var faker = new Faker();

                    if (type == typeof(FirstDto))
                    {
                        return faker.Create<FirstDto>();
                    }

                    if (type == typeof(SecondDto))
                    {
                        return faker.Create<SecondDto>();
                    }
                }
            }

            return default;
        }

        private string GetRandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;

            for (var i = 0; i < size; i++)
            {
                var @char = (char)random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
