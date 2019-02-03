using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using WindowsInput.Native;
using WindowsInput;

namespace FinalAssignment.Utils
{
    class SeleniumActions
    {
        private IWebDriver _driver;        
        
        public SeleniumActions()
        {
            this._driver = DriverFactory.Driver;
            Log.Debug("Actions.class instance created...");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }       


        public void WaitForElementToUpdate(string locator)
        {

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));


            By byLocator = ProcessLocator(locator);

            Reporter.SetLogs($"Waiting for an element by the following locator: {locator}");

            IWebElement element = wait.Until((d) =>
            {
                if (d.FindElement(byLocator).GetAttribute("value") != "")
                    return d.FindElement(byLocator);
                else
                    return null;
            });

            Reporter.SetLogs($"Element by the following locator: {locator} has been found. Proceeding...");
        }

        private void SetDefaultWaitFor(int duration)
        {
            Reporter.SetLogs($"Setting default time to wait for an element to {duration}");            
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(duration);
        }

        private By ProcessLocator(string locator)
        {

            if (Regex.IsMatch(locator, "^tag=.+$"))
            {
                Log.Debug($"The collection of elements will be searched by tag, using the following locator {locator}");
                return By.TagName(locator.Substring(4));
            }

            else if (Regex.IsMatch(locator, "^class=.+$"))
            {
                Log.Debug($"An element/elements will be searched by class name, using the following locator {locator}");
                return By.ClassName(locator.Substring(7));
            }

            else if (Regex.IsMatch(locator, "^id=.+$"))
            {
                Log.Debug($"An element/elements will be searched by id, using the following locator {locator}");
                return By.Id(locator.Substring(3));
            }

            else if (Regex.IsMatch(locator, "^name=.+$"))
            {
                Log.Debug($"An element/elements will be searched by name, using the following locator {locator}");
                return By.Name(locator.Substring(5));
            }

            else if (Regex.IsMatch(locator, "^xpath=.+$"))
            {
                Log.Debug($"An element/elements will be searched by xpath, using the following locator {locator}");
                return By.XPath(locator.Substring(6));
            }

            else if (Regex.IsMatch(locator, "^css=.+$"))
            {
                Log.Debug($"An element/elements will be searched by css selector, using the following locator {locator}");
                return By.CssSelector(locator.Substring(4));
            }

            else if (Regex.IsMatch(locator, "^link=.+$"))
            {
                Log.Debug($"An element/elements will be searched by link text, using the following locator {locator}");
                return By.LinkText(locator.Substring(5));
            }

            return null;
        }

        public void Click(string locator, int index)
        {
            Reporter.SetLogs($"Clicking on the element by its locator: { locator } and index in the collection of similar elements {index}");            
            _driver.FindElements(ProcessLocator(locator)).ElementAt(index).Click();
        }

        public void Click(string locator)
        {
            Reporter.SetLogs($"Clicking on the element by its locator: {locator}");            
            _driver.FindElement(ProcessLocator(locator)).Click();
        }

        public IWebElement SetText(string locator, string text)
        {
            Reporter.SetLogs($"Setting the text in the element by its locator: {locator}");
            var element = _driver.FindElement(ProcessLocator(locator));
            element.Clear();
            element.SendKeys(text);

            return element;
        }

        public void ClearText(string locator, int index)
        {
            Reporter.SetLogs($"Clearing the text in the element by its locator: {locator}");
            _driver.FindElements(ProcessLocator(locator)).ElementAt(index).Clear();
        }

        public bool CheckIfEnabled(string locator)
        {            
            var element = _driver.FindElement(ProcessLocator(locator));

            if (element.Enabled)
            {
                Reporter.SetLogs($"Element by the following locator: {locator} is enabled");
                return true;
            }
            else
            {
                Reporter.SetLogs($"Element by the following locator: {locator} is disabled");
                return false;
            }
        }

        public bool CheckIfSelected(string locator, int index)
        {            
            var element = _driver.FindElements(ProcessLocator(locator)).ElementAt(index);

            if (element.Selected)
            {
                Reporter.SetLogs($"Element by the following locator: {locator} is selected");
                return true;
            }
            else
            {
                Reporter.SetLogs($"Element by the following locator: {locator} is NOT selected");
                return false;
            }
        }

        public bool CheckIfSelected(string locator)
        {            
            var element = _driver.FindElement(ProcessLocator(locator));

            if (element.Selected)
            {
                Reporter.SetLogs($"Element by the following locator: {locator} is selected");
                return true;
            }
            else
            {
                Reporter.SetLogs($"Element by the following locator: {locator} is NOT selected");
                return false;
            }
        }

        public void GetCssValue(string locator, string cssAttribute)
        {
            Reporter.SetLogs($"Getting CSS value from the element by its locator: {locator}");
            var cssValue = _driver.FindElement(ProcessLocator(locator)).GetCssValue(cssAttribute);
            Reporter.SetLogs($"The CSS value of the element by locator: {locator} is: {cssValue}");
        }

        public string GetText(string locator, int index)
        {            
            var text = _driver.FindElements(ProcessLocator(locator)).ElementAt(index).Text;
            Reporter.SetLogs($"Getting text from the collection of elements by its locator: {locator} and position in the collection {index}. Text value is {text}");
            return text;

        }

        public string GetText(string locator)
        {            
            var text = _driver.FindElement(ProcessLocator(locator)).Text;
            Reporter.SetLogs($"Getting text from the element by its locator: {locator}. Text value is {text}");
            return text;

        }        

        public string GetAttribute(string locator, string attribute, int index)
        {            
            var value = _driver.FindElements(ProcessLocator(locator)).ElementAt(index).GetAttribute(attribute);

            Reporter.SetLogs($"Getting attribute {attribute} from the element {locator} in the {index} position in the collection. The value of the requested attribute is {value}");

            return value;

        }

        public string GetAttribute(string locator, string attribute)
        {            

            var value = _driver.FindElement(ProcessLocator(locator)).GetAttribute(attribute);
            Reporter.SetLogs($"Getting attribute {attribute} from the element {locator}. The value of the requested attribute is {value}");

            return value;

        }        

        public void SelectDropdownElement(string locator, int index)
        {            

            SelectElement selectElement = new SelectElement(_driver.FindElement(ProcessLocator(locator)));
            Log.Debug($"Select instance created by the following locator of the dropdown: {locator}");

            selectElement.SelectByIndex(index);

            Reporter.SetLogs($"Dropdown element was selected by index: {index}");
        }

        public void SelectDropdownElement(string locator, string value)
        {            

            SelectElement selectElement = new SelectElement(_driver.FindElement(ProcessLocator(locator)));
            Log.Debug($"Select instance created by the following locator of the dropdown: {locator}");

            selectElement.SelectByValue(value);

            Reporter.SetLogs($"Dropdown element was selected by value: {value}");
        }

        public void SelectDropdownElementByText(string locator, string text)
        {            

            SelectElement selectElement = new SelectElement(_driver.FindElement(ProcessLocator(locator)));
            Log.Debug($"Select instance created by the following locator of the dropdown: {locator}");

            selectElement.SelectByText(text);

            Reporter.SetLogs($"Dropdown element was selected by value: {text}");
        }

        public void SubmitForm(string locator)
        {            

            var element = _driver.FindElement(ProcessLocator(locator));
            Reporter.SetLogs($"Submitting the form that contains the following element: {locator}");

            element.Submit();
        }

        public void ToPreviousPage()
        {
            Reporter.SetLogs("Navigating back to the previous page");
            _driver.Navigate().Back();
        }

        public void RefreshPage()
        {
            Reporter.SetLogs("Refreshing the page");
            _driver.Navigate().Refresh();
        }        

        public void ValidateText(string locator, string expectedValue)
        {            
            var element = _driver.FindElement(ProcessLocator(locator)).Text;            
            element.Should().Contain(expectedValue);
            Reporter.SetLogs($"Text {element} of the element {locator} is equal to expected value: {expectedValue}");
        }

        public string GetCurrentUrl()
        {
            var thisUrl = _driver.Url;
            Reporter.SetLogs($"Getting current URL. It is {thisUrl}");
            return thisUrl;
        }
        
        public void Hover(string locator)
        {
            Reporter.SetLogs($"Moving and hovering over the element by its locator {locator}");
            new Actions(_driver).MoveToElement(_driver.FindElement(ProcessLocator(locator))).Perform();
        }        

        public void SwitchToWindow(int windowIndex)
        {
            Reporter.SetLogs($"Switching to window/tab by its index: {windowIndex}");
            _driver.SwitchTo().Window(_driver.WindowHandles[windowIndex]);
        }

        public void SwitchToDefaultContent()
        {
            Reporter.SetLogs("Switching to default window...");
            _driver.SwitchTo().DefaultContent();
        }

        public void CloseCurrentWindow()
        {
            Reporter.SetLogs("Closing current window...");
            _driver.Close();
        }

        public void SwitchToFrame(string locator)
        {
            Reporter.SetLogs($"Switching to frame by its locator: {locator}");
            _driver.SwitchTo().Frame(_driver.FindElement(ProcessLocator(locator)));
        }

        public void AcceptAlert()
        {
            Reporter.SetLogs($"Switching to alert and accepting it.");
            _driver.SwitchTo().Alert().Accept();
        }

        public void DragAndDrop(string draggableElement, string targetElement)
        {
            Reporter.SetLogs($"Dragging element by locator: {draggableElement} to its target element by locator: {targetElement}");
            new Actions(_driver).DragAndDrop(_driver.FindElement(ProcessLocator(draggableElement)), _driver.FindElement(ProcessLocator(targetElement))).Perform();
        }

        public void PressKey(VirtualKeyCode keycode)
        {
            Reporter.SetLogs($"Pressing key by the following keycode {keycode}");
            new InputSimulator().Keyboard.KeyPress(keycode);
        }

    }
}