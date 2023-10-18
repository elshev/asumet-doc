using System.Reflection;

namespace Asumet.Common
{
    public class ReflectionHelper
    {
        /// <summary>
        /// Gets the value of the member with the specified name.
        /// </summary>
        /// <param name="sourceObject">An object instance.</param>
        /// <param name="memberName">The member name. Could be separated by "." to access internal members: "ParentProperty.ChildProperty.SomeMember"</param>
        /// <returns>The member value.</returns>
        public static object GetMemberValue(object sourceObject, string memberName)
        {
            string[] propertyChain = memberName.Split('.');
            object? currentObject = sourceObject;
            foreach (string propertyName in propertyChain)
            {
                currentObject = GetPropertyValue(currentObject, propertyName, out var propertyInfo); 
            }
            return currentObject;
        }

        /// <summary>
        /// Gets the value of the property with the specified name.
        /// </summary>
        /// <param name="sourceObject">An object instance.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyInfo">A reference to the <see cref="PropertyInfo"/> object.</param>
        /// <returns>The property value.</returns>
        public static object? GetPropertyValue(object? sourceObject, string propertyName, out PropertyInfo? propertyInfo)
        {
            object? value = null;
            propertyInfo = GetPropertyInfo(sourceObject, propertyName);

            if (propertyInfo != null && propertyInfo.CanRead)
            {
                value = propertyInfo.GetValue(sourceObject, null);
            }

            return value;
        }

        /// <summary>
        /// Gets a <see cref="PropertyInfo"/> object representing the property
        /// belonging to the object having the specified name.
        /// </summary>
        /// <param name="sourceObject">An object instance.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>A <see cref="PropertyInfo"/> object, or null if the object
        /// instance does not have a property with the specified name.</returns>
        public static PropertyInfo? GetPropertyInfo(object? sourceObject, string propertyName)
        {
            PropertyInfo? propertyInfo = null;

            if (sourceObject != null)
            {
                propertyInfo = GetPropertyInfo(sourceObject.GetType(), propertyName);
            }

            return propertyInfo;
        }

        /// <summary>
        /// Gets a <see cref="PropertyInfo"/> object representing the property
        /// belonging to the runtime type having the specified name.
        /// </summary>
        /// <param name="type">The runtime type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>A <see cref="PropertyInfo"/> object, or null if the runtime
        /// type does not have a property with the specified name.</returns>
        public static PropertyInfo? GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo? propertyInfo = null;

            if (type != null && !string.IsNullOrEmpty(propertyName))
            {
                propertyInfo = type.GetProperty(propertyName);
            }

            return propertyInfo;
        }
    }
}
