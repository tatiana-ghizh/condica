using System.Reflection;

namespace CVU.CONDICA.Client.Services
{
    public static class Generics
    {
        public static T GetDifferences<T>(T original, T changed) where T : class
        {
            T editModel = (T)Activator.CreateInstance(typeof(T));

            foreach (PropertyInfo property in original.GetType().GetProperties())
            {
                object value1 = property.GetValue(original, null);
                object value2 = property.GetValue(changed, null);

                if (!value1.Equals(value2))
                {
                    editModel.GetType().GetProperty(property.Name).SetValue(editModel, value2);
                }
            }
            return editModel;
        }

        public static IEnumerable<T> Enumerate<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
