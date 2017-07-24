using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class UrlHelperTests
    {
        [TestMethod()]
        public void VanityEncode_AllInvalidCharacters()
        {
            // Arrange
            var builder = new StringBuilder();

            // Add control chars (0x00 - 0x1F)
            int code = 0;
            while (code < 0x20)
            {
                builder.Append((char)code++);
            }
            builder.Append('\x7F'); // DEL Control Char
            builder.Append("\"<>|:*?\\/#%&+");

            // Action
            var actual = UrlHelper.VanityEncode(builder.ToString());

            // Assert
            Assert.AreEqual("_00_01_02_03_04_05_06_07_08_09_0A_0B_0C_0D_0E_0F_10_11_12_13_14_15_16_17_18_19_1A_1B_1C_1D_1E_1F_7F_22_3C_3E_7C_3A_2A_3F_5C_2F_23_25_26_2B", actual);
        }

        [TestMethod()]
        public void VanityEncode_UnderscoreSpaceEndInPeriod()
        {
            // Arrange
            var text = "_ .";

            StringBuilder builder = new StringBuilder();

            // Add control chars (0x00 - 0x1F)
            int code = 0;
            while (code < 0x20)
            {
                builder.Append((char)code++);
            }
            builder.Append('\x7F'); // DEL Control Char

            builder.Append("\"<>|:*?\\/#%&+");

            // Action
            var actual = UrlHelper.VanityEncode(text);

            // Assert
            Assert.AreEqual("__+_2E", actual);
        }

        [TestMethod()]
        public void VanityEncode_UnencodeUrl()
        {
            // Arrange
            var text = "http://www.ietf.org/rfc/rfc2396.txt";

            StringBuilder builder = new StringBuilder();

            // Add control chars (0x00 - 0x1F)
            int code = 0;
            while (code < 0x20)
            {
                builder.Append((char)code++);
            }
            builder.Append('\x7F'); // DEL Control Char

            builder.Append("\"<>|:*?\\/#%&+");

            // Action
            var actual = UrlHelper.VanityEncode(text);

            // Assert
            Assert.AreEqual("http_3A_2F_2Fwww.ietf.org_2Frfc_2Frfc2396.txt", actual);
        }
    }
}