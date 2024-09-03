/*************************************************
 * FileName     = EventIdeaReqHandler.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = Request Handler for Event Idea request 
 *************************************************/
using RequestType;
using Credentials;
using MessageLog;
using RequestHandler;

namespace RequestHandler
{
    public class EventIdeaReqHandler : IRequestHandler
    {
        private IRequestHandler _nextHandler;
        /// <summary>
        /// sets next handler if the current handler isn't related to the request
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public IRequestHandler SetNext(IRequestHandler handler)
        {
            _nextHandler = handler;
            return _nextHandler;
        }
        /// <summary>
        /// forwards requests to approver
        /// </summary>
        /// <param name="request"></param>
        /// <param name="user"></param>
        public void Handle(IRequest request, User user)
        {
            if (request is EventIdeaRequest)
            {
                // Add to Student Head's log
                var studentHead = User.UList.Find(u => u.Position == "studenthead");
                var message = new MLog.MItem
                {
                    Description = request.Description,
                    Status = "Pending",
                    Remarks = ""
                };
                MessageLogManager.LogMessage("studenthead", message);

                // Add to Requester's log
                MessageLogManager.LogMessage(user.Position, message);

                request.Status = "Handled by Student Head";
            }
            else
            {
                _nextHandler?.Handle(request, user);
            }
        }
    }
}

