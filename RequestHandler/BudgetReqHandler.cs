/*************************************************
 * FileName     = UserInterface.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = Request Handler for Budget request 
 *************************************************/
using RequestType;
using Credentials;
using MessageLog;
using RequestHandler;

namespace RequestHandler
    {
        public class BudgetReqHandler : IRequestHandler
        {
            private IRequestHandler _nextHandler;

            public IRequestHandler SetNext(IRequestHandler handler)
            {
                _nextHandler = handler;
                return _nextHandler;
            }
        /// <summary>
        /// Handle that takes care of budget approval.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="user"></param>
            public void Handle(IRequest request, User user)
            {
                if (request is BudgetRequest budgetRequest)
                {
                    var message = new MLog.MItem
                    {
                        Description = request.Description,
                        Status = "Pending",
                        Remarks = ""
                    };

                    if (budgetRequest.Budget < 2000)
                    {
                        // Add to Student Head's and Executive's logs
                        MessageLogManager.LogMessage("studenthead", message);
                        MessageLogManager.LogMessage("executive", message);
                    }
                    else
                    {
                        // Add to Student Head's, Executive's, and Faculty In-Charge's logs
                        MessageLogManager.LogMessage("studenthead", message);
                        MessageLogManager.LogMessage("executive", message);
                        MessageLogManager.LogMessage("facultyincharge", message);
                    }

                    // Add to Requester's log
                    MessageLogManager.LogMessage(user.Position, message);

                    request.Status = "Handled by Budget Handlers";
                }
                else
                {
                    _nextHandler?.Handle(request, user);
                }
            }
        }
    }
