/*************************************************
 * FileName     = AmendmentRequest.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = Amendment Request class
 *************************************************/
using System.Reflection.Metadata;
using Credentials;

namespace RequestType
{
    public class AmendmentRequest : IRequest
    {
        public string Description { get; private set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public AmendmentRequest(string description)
        {
            Description = description;
            Status = "Pending";
        }
    }
}
