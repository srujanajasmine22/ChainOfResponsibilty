/******************************************************************************
 * Filename    = Utest.cs
 *
 * Author      = Komarabathina Srujana Jasmine
 *
 * Product     = SoftwareDesignPatterns
 * 
 * Project     = ChainOfResponsibility
 *
 * Description = Unit tests for message logging functionality.
 *****************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLog;
using RequestType;
using Credentials;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class Utest
    {
        /// <summary>
        /// Tests that a request is correctly logged in the message log.
        /// </summary>
        [TestMethod]
        public void TestRequestLogging()
        {
            // Arrange
            User requester = new User("lead1", "password1");
            IRequest request = new EventIdeaRequest("Test Event Idea");
            var message = new MLog.MItem
            {
                Position = "lead1",
                Description = request.Description,
                Status = request.Status,
                Remarks = request.Remarks
            };

            // Act
            MessageLogManager.LogMessage(requester.Position, message);
            var loggedMessages = MessageLogManager.GetMessages(requester.Position);

            // Assert
            Assert.IsTrue(loggedMessages.Any(m => m.Description == "Test Event Idea"));
        }

        /// <summary>
        /// Tests that the status of a request is correctly updated in the message log.
        /// </summary>
        [TestMethod]
        public void TestRequestStatusUpdate()
        {
            // Arrange
            User requester = new User("lead1", "password1");
            IRequest request = new BudgetRequest("Test Budget", 1500);
            var message = new MLog.MItem
            {
                Position = "lead1",
                Description = request.Description,
                Status = "Pending",
                Remarks = request.Remarks
            };
            MessageLogManager.LogMessage(requester.Position, message);

            // Act
            message.Status = "Approved";
            MessageLogManager.UpdateMessage(requester.Position, message);
            var loggedMessages = MessageLogManager.GetMessages(requester.Position);

            // Assert
            var updatedMessage = loggedMessages.FirstOrDefault(m => m.Description == "Test Budget");
            Assert.IsNotNull(updatedMessage);
            Assert.AreEqual("Approved", updatedMessage.Status);
        }
    }
}
