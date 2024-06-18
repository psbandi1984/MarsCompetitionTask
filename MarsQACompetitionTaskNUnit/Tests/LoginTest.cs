using NUnit.Framework;

namespace MarsQACompetitionTaskNUnit.Tests
{
    
    public class LoginTest : BaseTest 
    {   
      
        [Test, Description("User signin successfully")]
        public void LoginwithValidCrendentials()
        {
            
            Assert.Pass("Passed");
        }
                
    }
}
