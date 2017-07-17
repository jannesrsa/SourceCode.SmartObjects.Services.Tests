using Xunit;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    public class ConnectionHelperTests
    {
        [Fact()]
        public void GetCurrentUserTest()
        {
            // Action
            var currentuser = ConnectionHelper.GetCurrentUser();

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(currentuser));
        }
    }
}