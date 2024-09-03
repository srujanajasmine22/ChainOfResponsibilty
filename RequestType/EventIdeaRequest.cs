/*************************************************
 * FileName     = EventIdeaRequest.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  =  Class for EventIdeaRequest
 *************************************************/
using System.Reflection.Metadata;
using Credentials;

namespace RequestType
{
    public class EventIdeaRequest : IRequest
    {
        public string Description { get; private set; }
        public string Status { get; set; }

        public string Remarks { get; set; }


        public EventIdeaRequest(string description)
        {
            Description = description;
            Status = "Pending";
        }
    }
}
