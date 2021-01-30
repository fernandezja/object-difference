using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectDiffFinder
{
    public static class ReflectionExtensions
    {
        public static TModel Difference<TModel>(this TModel model1, TModel model2) where TModel : class, new() =>
            Difference(typeof(TModel), model1, model2);

        public static Dictionary<string, string> DifferenceToDictionary<TModel>(this TModel model1, TModel model2) where TModel : class, new() =>
            DifferenceToDictionary(typeof(TModel), model1, model2);


        public static List<PropertyCompareResult> DifferenceToList<TModel>(this TModel model1, TModel model2) where TModel : class, new() =>
            DifferenceToList(typeof(TModel), model1, model2);
        


        private static TModel Difference<TModel>(Type type, TModel model1, TModel model2) where TModel : class, new()
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            var result = type.GetConstructor(new Type[] { }).Invoke(null) as TModel;

            var fieldsDiff = new Dictionary<string, string>();

            result = properties.Aggregate(result, (seed, property) =>
            {
                var value1 = model1 == null ? null : property.GetValue(model1);
                var value2 = model2 == null ? null : property.GetValue(model2);

                if (!AreEqual(value1, value2))
                {
                    // Take the one which is not default.
                    var value = IsDefault(value1) ? value2 : value1;
                    property.SetValue(seed, value);

                    fieldsDiff.Add(property.Name, value2.ToString());
                }

                return seed;
            });

            return result;
        }

        private static Dictionary<string, string> DifferenceToDictionary<TModel>(Type type, TModel model1, TModel model2) where TModel : class, new()
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            var result = type.GetConstructor(new Type[] { }).Invoke(null) as TModel;

            var fieldsDiff = new Dictionary<string, string>();

            result = properties.Aggregate(result, (seed, property) =>
            {
                var value1 = model1 == null ? null : property.GetValue(model1);
                var value2 = model2 == null ? null : property.GetValue(model2);

                if (!AreEqual(value1, value2))
                {
                    // Take the one which is not default.
                    var value = IsDefault(value1) ? value2 : value1;
                    property.SetValue(seed, value);

                    fieldsDiff.Add(property.Name, value2.ToString());
                }

                return seed;
            });

            return fieldsDiff;
        }


        private static List<PropertyCompareResult> DifferenceToList<TModel>(Type type, TModel model1, TModel model2) where TModel : class, new()
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            var result = type.GetConstructor(new Type[] { }).Invoke(null) as TModel;

            var listDiff = new List<PropertyCompareResult>();

            result = properties.Aggregate(result, (seed, property) =>
            {
                var value1 = model1 == null ? null : property.GetValue(model1);
                var value2 = model2 == null ? null : property.GetValue(model2);

                if (!AreEqual(value1, value2))
                {
                    // Take the one which is not default.
                    var value = IsDefault(value1) ? value2 : value1;
                    property.SetValue(seed, value);

                    listDiff.Add(new PropertyCompareResult(property.Name, value1.ToString(), value2.ToString()));
                }

                return seed;
            });

            return listDiff;
        }

        private static bool IsDefault<TValue>(TValue value)
        {
            if (value==null)
            {
                return true;
            }
            return value.Equals(default(TValue));
        }

        private static bool AreEqual(object value1, object value2)
        {
            // Add nullchecks for the sake of simplicity
            if (value1 == null && value2 == null) return true;
            if (value1 == null && value2 != null) return false;
            if (value1 != null && value2 == null) return false;

            // Check special case string
            if (value1 is string str1 && value2 is string str2)
            {
                return str1 == str2;
            }

            // Provide the actual type of the property.
            Type type = value1.GetType();
            // Distinguish between primitive types and classes
            return type.IsValueType
                ? value1.Equals(value2)
                : AreEqualClass(type, value1, value2);
        }

        private static bool AreEqualClass<TValue>(Type type, TValue value1, TValue value2) where TValue : class, new()
        {
            TValue difference = Difference(type, value1, value2);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            return properties.All(property =>
            {
                var value = property.GetValue(difference);
                return IsDefault(value);
            });
        }
    }
}
