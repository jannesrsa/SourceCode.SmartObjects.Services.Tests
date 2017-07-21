using System;
using System.Data;
using SourceCode.SmartObjects.Client;
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
            return new SmartObjectClientServerWrapper(clientServer).Deserialize(serviceObjectName, serviceInstanceSettings, value);
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
            return new SmartObjectClientServerWrapper(clientServer).DeserializeTypedArray(serviceObjectName, serviceInstanceSettings, value);
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
            return new SmartObjectClientServerWrapper(clientServer).Serialize(serviceObjectName, serviceInstanceSettings, actions);
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
            return new SmartObjectClientServerWrapper(clientServer).SerializeAddItemToArray(serviceObjectName, existingSerializedArray, serviceInstanceSettings, actions);
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
            return new SmartObjectClientServerWrapper(clientServer).SerializeItemToArray(serviceObjectName, serviceInstanceSettings, actions);
        }
    }
}