/*************************************************
 * FileName     = MLog.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = Class to store requests
 *************************************************/
namespace MessageLog
{
    public class MLog
    {
        public List<MItem> Messages { get; }

        public class MItem
        {
            public string Position;
            public string Description { get; set; }
            public string Status { get; set; }
            public string Remarks { get; set; }
        }
    }
}
