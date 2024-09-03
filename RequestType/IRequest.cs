/*************************************************
 * FileName     = IRequest.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = defines the request type  
 *************************************************/
using System.Reflection.Metadata;
using Credentials;

namespace RequestType
{
    public interface IRequest
    {
        string Description { get; }      // request description
        string Status { get; set; }      // request Status
        string Remarks { get; set; }     // remarks on request
    }
}
