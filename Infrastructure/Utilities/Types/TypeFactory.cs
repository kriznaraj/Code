using System;

namespace Controls.Types
{
    public static class TypeFactory
    {
        public static Object CreateGenericInstance(Type type, Type[] genericArgument, params object[] args)
        {
            return Activator.CreateInstance(type.MakeGenericType(genericArgument), args);
        }

        public static T CreateInstance<T>(Type type)
        {
            return (T)Activator.CreateInstance(type);
        }

        public static T CreateInstance<T>(Type type, params object[] args)
        {
            return (T)Activator.CreateInstance(type, args);
        }

        public static T CreateInstance<T>(String fullyQualifiedTypeName)
        {
            return (T)Activator.CreateInstance(Type.GetType(fullyQualifiedTypeName, true));
        }

        public static T CreateInstance<T>(String fullyQualifiedTypeName, params object[] args)
        {
            return (T)Activator.CreateInstance(Type.GetType(fullyQualifiedTypeName, true), args);
        }

        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static object CreateInstance(Type type, params object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        public static object CreateInstance(String fullyQualifiedTypeName)
        {
            return Activator.CreateInstance(Type.GetType(fullyQualifiedTypeName, true));
        }

        public static object CreateInstance(String fullyQualifiedTypeName, params object[] args)
        {
            return Activator.CreateInstance(Type.GetType(fullyQualifiedTypeName, true), args);
        }

        public static Type GetGenericType(Type type, Type[] genericArgument)
        {
            return type.MakeGenericType(genericArgument);
        }
    }
}