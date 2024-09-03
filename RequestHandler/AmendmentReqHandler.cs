/*************************************************
 * FileName     = EventIdeaReqHandler.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = Request Handler for Amendment Request
 *************************************************/
using RequestType;
using Credentials;
using MessageLog;
using RequestHandler;

namespace RequestHandler
{
    public class AmendmentReqHandler : IRequestHandler
    {
        private IRequestHandler _nextHandler;

        public IRequestHandler SetNext(IRequestHandler handler)
        {
            _nextHandler = handler;
            return _nextHandler;
        }
        /// <summary>
        /// handle for amendment request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="user"></param>
        public void Handle(IRequest request, User user)
        {
            if (request is AmendmentRequest)
            {
                var message = new MLog.MItem
                {
                    Description = request.Description,
                    Status = "Pending",
                    Remarks = ""
                };

                // Add to Student Head's, Executive's, and Faculty In-Charge's logs
                MessageLogManager.LogMessage("studenthead", message);
                MessageLogManager.LogMessage("executive", message);
                MessageLogManager.LogMessage("facultyincharge", message);

                // Add to Requester's log
                MessageLogManager.LogMessage(user.Position, message);

                request.Status = "Handled by Amendment Handlers";
            }
            else
            {
                _nextHandler?.Handle(request, user);
            }
        }
    }
}
