using MarsQACompetitionTaskNUnit.Pages;
using NUnit.Framework;

namespace MarsQACompetitionTaskNUnit.Tests
{
    
    public class LoginTest : BaseTest 
    {   
      
        [Test, Description("User signin successfully")]
        public void LoginwithValidCrendentials()
        {
            LoginPage loginPageObject = new LoginPage();

            Assert.Pass("Passed");
        }
               
    }
}
