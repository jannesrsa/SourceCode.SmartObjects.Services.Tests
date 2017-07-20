using System;
using System.Data;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Services.Tests.Helpers;
using SourceCode.SmartObjects.Services.Tests.Interfaces;
using SourceCode.SmartObjects.Services.Tests.Managers;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.Extensions
{
    public static class SmartObjectClientServerExtensions
    {
        /// <summary>
        /// Deserializes <paramref name="value"/> to a SmartObject
        /// </summary>
        /// <param name="clientServer"></param>
        /// <param name="serviceObjectName"></param>
        /// <param name="value">The serialized string version of the object</param>
        /// <returns>The SmartObject containing the return properties of the Deserialize call</returns>
        public static SmartObject Deserialize(this SmartObjectClientServer clientServer, string serviceObjectName,
            ServiceInstanceSettings serviceInstanceSettings, String value)
        {
            return Deserialize(new SmartObjectClientServerWrapper(clientServer), serviceObjectName, serviceInstanceSettings, value);
        }

        /// <summary>
        /// Deserializes <paramref name="value"/> to a DataTable
        /// </summary>
        /// <param name="clientServer"></param>
        /// <param name="serviceObjectName"></param>
        /// <param name="value">The string to deserializd to a datatable</param>
        /// <returns>A DataTable containing the deserialized data for the object</returns>
        public static DataTable DeserializeTypedArray(this SmartObjectClientServer clientServer, string serviceObjectName,
            ServiceInstanceSettings serviceInstanceSettings, string value)
        {
            return DeserializeTypedArray(new SmartObjectClientServerWrapper(clientServer), serviceObjectName, serviceInstanceSettings, value);
        }

        /// <summary>
        /// Executes the Serialize method of the <paramref name="serviceObjectName"/>
        /// </summary>
        /// <param name="clientServer"></param>
        /// <param name="serviceObjectName"></param>
        /// <param name="actions">
        ///		One or more custom actions to perform on the SmartObject associated with the specified
        ///		<paramref name="serviceObjectName"/>. This will most commonly take the form of setting
        ///		input values. See the accompanying example.
        /// </param>
        /// <returns>
        ///		The serialized string representation of the specified <paramref name="serviceObjectName"/>
        /// </returns>
        ///	<example>
        ///		<code>
        ///		clientServer.Serialize("Person",
        ///			so => so.SetInputPropertyValue("FirstName", firstName),
        ///			so => so.SetInputPropertyValue("LastName", lastName));
        ///		</code>
        ///	</example>
        public static string Serialize(this SmartObjectClientServer clientServer, string serviceObjectName,
            ServiceInstanceSettings serviceInstanceSettings, params Action<SmartObject>[] actions)
        {
            return Serialize(new SmartObjectClientServerWrapper(clientServer), serviceObjectName, serviceInstanceSettings, actions);
        }

        /// <summary>
        /// Executes the SerializeAddItemToArray method on the <paramref name="serviceObjectName"/>
        /// </summary>
        /// <param name="clientServer"></param>
        /// <param name="serviceObjectName"></param>
        /// <param name="existingSerializedArray">
        ///		An existing serialized array to add the item to
        /// </param>
        ///		One or more custom actions to perform on the SmartObject associated with the specified
        ///		<paramref name="serviceObjectName"/>. This will most commonly take the form of setting
        ///		input values. See the accompanying example.
        /// <returns>
        ///		A string representing the serialized form of the <paramref name="existingSerializedArray"/> with the item created by the actions used to set the input properties
        /// </returns>
        public static string SerializeAddItemToArray(this SmartObjectClientServer clientServer, string serviceObjectName, string existingSerializedArray,
            ServiceInstanceSettings serviceInstanceSettings, params Action<SmartObject>[] actions)
        {
            return SerializeAddItemToArray(new SmartObjectClientServerWrapper(clientServer), serviceObjectName, existingSerializedArray, serviceInstanceSettings, actions);
        }

        /// <summary>
        /// Executes the SerializeItemToArray method on the <paramref name="serviceObjectName"/>
        /// </summary>
        /// <param name="clientServer"></param>
        /// <param name="serviceObjectName"></param>
        /// <param name="existingSerializedArray">
        ///		An existing serialized array to add the item to
        /// </param>
        ///		One or more custom actions to perform on the SmartObject associated with the specified
        ///		<paramref name="serviceObjectName"/>. This will most commonly take the form of setting
        ///		input values. See the accompanying example.
        /// <returns>
        ///		A string representing the serialized array containing the item created by the actions used to set the input properties
        /// </returns>
        public static string SerializeItemToArray(this SmartObjectClientServer clientServer, string serviceObjectName,
            ServiceInstanceSettings serviceInstanceSettings, params Action<SmartObject>[] actions)
        {
            return SerializeItemToArray(new SmartObjectClientServerWrapper(clientServer), serviceObjectName, serviceInstanceSettings, actions);
        }

        internal static SmartObject Deserialize(ISmartObjectClientServer clientServer, string serviceObjectName, ServiceInstanceSettings serviceInstanceSettings, string value)
        {
            var smartObject = SmartObjectHelper.GetSmartObject(clientServer, serviceObjectName, serviceInstanceSettings);

            smartObject.MethodToExecute = "Deserialize";
            smartObject.SetInputPropertyValue("Serialized_Item__String_", value);

            SmartObjectHelper.ExecuteScalar(clientServer, smartObject);

            return smartObject;
        }

        internal static DataTable DeserializeTypedArray(this ISmartObjectClientServer clientServer, string serviceObjectName,
                                            ServiceInstanceSettings serviceInstanceSettings, string value)
        {
            var smartObject = SmartObjectHelper.GetSmartObject(clientServer, serviceObjectName, serviceInstanceSettings);

            smartObject.MethodToExecute = "DeserializeTypedArray";
            smartObject.SetInputPropertyValue("Serialized_Array", value);

            var dataTable = SmartObjectHelper.ExecuteListDataTable(clientServer, smartObject);

            return dataTable;
        }

        internal static string Serialize(this ISmartObjectClientServer clientServer, string serviceObjectName,
            ServiceInstanceSettings serviceInstanceSettings, params Action<SmartObject>[] actions)
        {
            actions.ThrowIfNull("actions");

            var smartObject = SmartObjectHelper.GetSmartObject(clientServer, serviceObjectName, serviceInstanceSettings);
            smartObject.MethodToExecute = "Serialize";

            foreach (var action in actions)
            {
                action(smartObject);
            }

            var serialized = SmartObjectHelper.ExecuteScalar(clientServer, smartObject);
            return serialized.GetReturnPropertyValue("Serialized_Item__String_");
        }

        internal static string SerializeAddItemToArray(this ISmartObjectClientServer clientServer, string serviceObjectName, string existingSerializedArray,
            ServiceInstanceSettings serviceInstanceSettings, params Action<SmartObject>[] actions)
        {
            actions.ThrowIfNull("actions");

            var smartObject = SmartObjectHelper.GetSmartObject(clientServer, serviceObjectName, serviceInstanceSettings);
            smartObject.MethodToExecute = "SerializeAddItemToArray";
            smartObject.SetInputPropertyValue("Serialized_Array", existingSerializedArray);

            foreach (var action in actions)
            {
                action(smartObject);
            }

            SmartObjectHelper.ExecuteScalar(clientServer, smartObject);

            return smartObject.Properties["Serialized_Array"].Value;
        }

        internal static string SerializeItemToArray(this ISmartObjectClientServer clientServer, string serviceObjectName,
           ServiceInstanceSettings serviceInstanceSettings, params Action<SmartObject>[] actions)
        {
            actions.ThrowIfNull("actions");

            var smartObject = SmartObjectHelper.GetSmartObject(clientServer, serviceObjectName, serviceInstanceSettings);
            smartObject.MethodToExecute = "SerializeItemToArray";

            foreach (var action in actions)
            {
                action(smartObject);
            }

            SmartObjectHelper.ExecuteScalar(clientServer, smartObject);

            return smartObject.Properties["Serialized_Array"].Value;
        }
    }
}