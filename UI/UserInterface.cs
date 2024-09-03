/*************************************************
 * FileName     = UserInterface.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = Takes inputs for logging in as user and requesting/approving requests 
 *************************************************/
using System;
using RequestType;
using RequestHandler;
using Credentials;
using MessageLog;

namespace UI
{
    public class UserInterface
    {
        /// <summary>
        /// Start function takes user's position and password , 
        /// validate them with existing user list and 
        /// navigates to ShowOptions function for further process 
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Enter position:");
            var position = Console.ReadLine()?.Trim().ToLower();
            Console.WriteLine("Enter password:");
            var password = Console.ReadLine()?.Trim();

            var user = User.UList.Find(u => u.Position.ToLower() == position && u.Password == password);
            if (user != null)
            {
                ShowOptions(user);
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
            }
        }

        /// <summary>
        /// According to user's position ShowOptions gives 2 choices
        /// For sending/approving recieved requests and to view all past requests
        /// </summary>
        /// <param name="user"></param>
        private void ShowOptions(User user)
        {
            var userLog = user.Log;
            Console.WriteLine("Choose an option:");
            // For user is of lead type
            if (user.Position.StartsWith("lead"))
            {
                Console.WriteLine("1. Send approval requests");
                Console.WriteLine("2. See request log");

                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    SendApprovalRequest(user);  //send requests
                }
                else if (choice == "2")
                {
                    DisplayLog(user);          // displays all the messages sent/received by particular user
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }
            else
            {
                Console.WriteLine("1. View received requests and approve/reject");
                Console.WriteLine("2. View request log");

                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    HandleReceivedRequests(user);    // handles received requests
                }
                else if (choice == "2")
                {
                    DisplayLog(user);               // displays all the messages sent/received by particular user
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }
        }

        /// <summary>
        /// Used to send 3 types requests
        /// </summary>
        /// <param name="user"></param>
        private void SendApprovalRequest(User user)
        {
            //Takes input from user
            Console.WriteLine("Enter request type (EventIdea/Budget/Amendment):");     
            var requestType = Console.ReadLine()?.Trim();
            Console.WriteLine("Enter description:");
            var description = Console.ReadLine()?.Trim();

            IRequest request = null;

            switch (requestType)
            {
                case "EventIdea":
                    request = new EventIdeaRequest(description);
                    break;
                case "Budget":
                    Console.WriteLine("Enter budget:");
                    var budget = double.Parse(Console.ReadLine() ?? "0");
                    request = new BudgetRequest(description, budget);
                    break;
                case "Amendment":
                    request = new AmendmentRequest(description);
                    break;
                default:
                    Console.WriteLine("Invalid request type.");
                    break;
            }

            //Using ChainOfResponsiblity to handle different types of requests
           if (request != null)
            {
                IRequestHandler handler = new EventIdeaReqHandler();
                handler.SetNext(new BudgetReqHandler()).SetNext(new AmendmentReqHandler());

                handler.Handle(request, user);
           
                var message = new MLog.MItem
                {
                    Description = description,
                    Status = "Pending",
                    Remarks = ""
                };

                //Saves Requests for requester and approver in json file using LogMessage
                MessageLogManager.LogMessage(user.Position, message);

                if (request is EventIdeaRequest)
                {
                    MessageLogManager.LogMessage("student head", message);
                }
                else if (request is BudgetRequest || request is AmendmentRequest)
                {
                    MessageLogManager.LogMessage("student head", message);
                    MessageLogManager.LogMessage("executive", message);
                    MessageLogManager.LogMessage("faculty in-charge", message);
                }

                Console.WriteLine("Request sent.");
            }
        }

        /// <summary>
        /// Shows received requests and updates approval or rejection
        /// </summary>
        /// <param name="user"></param>
        private void HandleReceivedRequests(User user)
        {
            var messages = MessageLogManager.GetMessages(user.Position);    //gets messages from the json file where all the messages are saved
            foreach (var request in messages)
            {
                Console.WriteLine($"Request: {request.Description}");
                Console.WriteLine("Approve or Reject? (a/r)");
                var choice = Console.ReadLine()?.Trim().ToLower();

                if (choice == "a")
                {
                    request.Status = "Approved";
                    Console.WriteLine("Enter remarks:");
                    request.Remarks = Console.ReadLine()?.Trim();

                    MessageLogManager.UpdateMessage(user.Position, request);    //updating the received requests

                    MessageLogManager.UpdateMessage(request.Position, request);

                    Console.WriteLine("\nRequest approved.");
                }
                else if (choice == "r")
                {
                    request.Status = "Rejected";
                    Console.WriteLine("Enter remarks:");
                    request.Remarks = Console.ReadLine()?.Trim();

                    MessageLogManager.UpdateMessage(user.Position, request);

                    MessageLogManager.UpdateMessage(request.Position, request);

                    Console.WriteLine("\nRequest rejected.");
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }
        /// <summary>
        /// Displays all the requests of particular user
        /// </summary>
        /// <param name="user"></param>
        private void DisplayLog(User user)
        {
            var messages = MessageLogManager.GetMessages(user.Position);
            foreach (var message in messages)
            {
                Console.WriteLine($"Description: {message.Description}, Status: {message.Status}, Remarks: {message.Remarks}");
            }
        }
    }
}