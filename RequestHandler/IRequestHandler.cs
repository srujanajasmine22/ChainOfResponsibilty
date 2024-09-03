/*************************************************
 * FileName     = IRequestHandler.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = RequestHandler interface
 *************************************************/
using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Permissions;
using RequestType;
using Credentials;
namespace RequestHandler
{
    /// <summary>
    /// two methods to implement chain of responsibility desighn pattern
    /// </summary>
    public interface IRequestHandler
    {
        IRequestHandler SetNext(IRequestHandler handler);
        void Handle(IRequest request, User user);
    }
}
