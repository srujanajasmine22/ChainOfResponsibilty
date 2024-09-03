/*************************************************
 * FileName     = BudgetRequest.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = Budget request class 
 *************************************************/
using System.Reflection.Metadata;
using Credentials;

namespace RequestType
{
    public class BudgetRequest : IRequest
    {
        public string Description { get; set; }
        public double Budget { get; }
        public string Status { get; set; }
        public string Remarks { get; set; }

        public BudgetRequest(string description, double amount)
        {
            Description = description;
            Budget = amount;
            Status = "Pending";
        }
    }
}
