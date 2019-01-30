using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using FinalAssignment.Utils;

namespace FinalAssignment.PageObjectLibrary
{
    class AtataSampleAppPage : SeleniumActions
    {
        public void Logout()
        {

            Log.Information("Logging out from Atata web app...");
            base.Click("xpath=//a[contains(text(),'Account')]");
            base.Click("xpath=//a[text()='Sign Out']");
        }

        public void ViewUserDetails(int userIndex)
        {
            Log.Information($"Navigating to view user ({userIndex}) details page");
            base.Click($@"css=tbody>tr:nth-child({userIndex}) a");
        }

        public void GoToCalculationsTab()
        {
            Log.Information("Navigating to calculations tab...");
            base.Click("xpath=//a[text()='Calculations']");
        }

        public void GoToPlansTab()
        {
            Log.Information("Navigating to plans tab...");
            base.Click("xpath=//a[text()='Plans']");
        }

        public void GoToProductsTab()
        {
            Log.Information("Navigating to products tab...");
            base.Click("xpath=//a[text()='Products']");
        }

        public string ConductCalculations(int firstOperand, int secondOperand)
        {
            Log.Information($"Adding first operand: {firstOperand} to the second operand: {secondOperand}");
            base.SetText("id=addition-value-1", firstOperand.ToString());
            base.SetText("id=addition-value-2", secondOperand.ToString());

            base.WaitForElementToUpdate("id=addition-result");

            var result = base.GetAttribute("id=addition-result", "value");
            Log.Information($"The resulting value is {result}");

            return result;
        }
    }
}
