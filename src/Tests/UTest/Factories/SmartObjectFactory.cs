using SourceCode.SmartObjects.Client;

namespace SourceCode.SmartObjects.Services.Tests.UTest.Factories
{
    internal static class SmartObjectFactory
    {
        public static SmartObject GetSmartObject(SmartObjectOption option = SmartObjectOption.Empty)
        {
            var smartObject = new SmartObject();

            switch (option)
            {
                case SmartObjectOption.ProcessInfo:
                    smartObject.Load(typeof(SmartObjectFactory).Assembly.GetManifestResourceStream("SourceCode.SmartObjects.Services.Tests.UTest.Resources.SmartObject_ProcessInfo.xml"));
                    break;

                case SmartObjectOption.Users_and_Groups:
                    smartObject.Load(typeof(SmartObjectFactory).Assembly.GetManifestResourceStream("SourceCode.SmartObjects.Services.Tests.UTest.Resources.SmartObject_Users_and_Groups.xml"));
                    break;

                case SmartObjectOption.Empty:
                default:
                    break;
            }

            return smartObject;
        }
    }
}