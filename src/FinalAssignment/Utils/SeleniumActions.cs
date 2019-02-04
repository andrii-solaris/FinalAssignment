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
    //Provides wrappers and logic for Selenium actions.
    class SeleniumActions
    {
        private IWebDriver _driver;        
        
        public SeleniumActions()
        {
            this._driver = DriverFactory.Driver;
            Log.Debug("Actions.class instance created...");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }       

        //Provides custom waits.
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

        //Sets default waits for web elements.
        private void SetDefaultWaitFor(int duration)
        {
            Reporter.SetLogs($"Setting default time to wait for an element to {duration}");            
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(duration);
        }

        //Decomposes a string to locator and decides on which By instance to use.
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

        //Standard click in case the locator returns an array of web elements.
        public void Click(string locator, int index)
        {
            Reporter.SetLogs($"Clicking on the element by its locator: { locator } and index in the collection of similar elements {index}");            
            _driver.FindElements(ProcessLocator(locator)).ElementAt(index).Click();
        }
        
        //Standard click for single element.
        public void Click(string locator)
        {
            Reporter.SetLogs($"Clicking on the element by its locator: {locator}");            
            _driver.FindElement(ProcessLocator(locator)).Click();
        }

        //Clears and sets text to input field.
        public IWebElement SetText(string locator, string text)
        {
            Reporter.SetLogs($"Setting the text in the element by its locator: {locator}");
            var element = _driver.FindElement(ProcessLocator(locator));
            element.Clear();
            element.SendKeys(text);

            return element;
        }

        //Clears text from an input field.
        public void ClearText(string locator, int index)
        {
            Reporter.SetLogs($"Clearing the text in the element by its locator: {locator}");
            _driver.FindElements(ProcessLocator(locator)).ElementAt(index).Clear();
        }

        //Checks if a web element is enabled.
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

        //Checks whether checkbox is selected, if a locator returns an array of web elements.
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

        //Checks whether checkbox is selected.
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

        //Gets css value of a web element.
        public void GetCssValue(string locator, string cssAttribute)
        {
            Reporter.SetLogs($"Getting CSS value from the element by its locator: {locator}");
            var cssValue = _driver.FindElement(ProcessLocator(locator)).GetCssValue(cssAttribute);
            Reporter.SetLogs($"The CSS value of the element by locator: {locator} is: {cssValue}");
        }

        //Gets text of a web element, if a locator returns an array of web elements.
        public string GetText(string locator, int index)
        {            
            var text = _driver.FindElements(ProcessLocator(locator)).ElementAt(index).Text;
            Reporter.SetLogs($"Getting text from the collection of elements by its locator: {locator} and position in the collection {index}. Text value is {text}");
            return text;

        }

        //Gets text of a web element.
        public string GetText(string locator)
        {            
            var text = _driver.FindElement(ProcessLocator(locator)).Text;
            Reporter.SetLogs($"Getting text from the element by its locator: {locator}. Text value is {text}");
            return text;

        }

        //Gets a value of a specified web element's attribute, if a locator returns an array of web elements.
        public string GetAttribute(string locator, string attribute, int index)
        {            
            var value = _driver.FindElements(ProcessLocator(locator)).ElementAt(index).GetAttribute(attribute);

            Reporter.SetLogs($"Getting attribute {attribute} from the element {locator} in the {index} position in the collection. The value of the requested attribute is {value}");

            return value;

        }

        //Gets a value of a specified web element's attribute.
        public string GetAttribute(string locator, string attribute)
        {            

            var value = _driver.FindElement(ProcessLocator(locator)).GetAttribute(attribute);
            Reporter.SetLogs($"Getting attribute {attribute} from the element {locator}. The value of the requested attribute is {value}");

            return value;

        }        


        //Selects an element from a dropdown by index.
        public void SelectDropdownElement(string locator, int index)
        {            

            SelectElement selectElement = new SelectElement(_driver.FindElement(ProcessLocator(locator)));
            Log.Debug($"Select instance created by the following locator of the dropdown: {locator}");

            selectElement.SelectByIndex(index);

            Reporter.SetLogs($"Dropdown element was selected by index: {index}");
        }

        //Selects an element from a dropdown by value.
        public void SelectDropdownElement(string locator, string value)
        {            

            SelectElement selectElement = new SelectElement(_driver.FindElement(ProcessLocator(locator)));
            Log.Debug($"Select instance created by the following locator of the dropdown: {locator}");

            selectElement.SelectByValue(value);

            Reporter.SetLogs($"Dropdown element was selected by value: {value}");
        }

        //Selects an element from a dropdown by text.
        public void SelectDropdownElementByText(string locator, string text)
        {            

            SelectElement selectElement = new SelectElement(_driver.FindElement(ProcessLocator(locator)));
            Log.Debug($"Select instance created by the following locator of the dropdown: {locator}");

            selectElement.SelectByText(text);

            Reporter.SetLogs($"Dropdown element was selected by value: {text}");
        }

        //Submits form if a specified element is within a form element.
        public void SubmitForm(string locator)
        {            

            var element = _driver.FindElement(ProcessLocator(locator));
            Reporter.SetLogs($"Submitting the form that contains the following element: {locator}");

            element.Submit();
        }

        //Navigates back to previous page in a browser.
        public void ToPreviousPage()
        {
            Reporter.SetLogs("Navigating back to the previous page");
            _driver.Navigate().Back();
        }

        //Refreshes a page in a browser.
        public void RefreshPage()
        {
            Reporter.SetLogs("Refreshing the page");
            _driver.Navigate().Refresh();
        }        

        //Provides implementation for validate text assert.
        public void ValidateText(string locator, string expectedValue)
        {            
            var element = _driver.FindElement(ProcessLocator(locator)).Text;            
            element.Should().Contain(expectedValue);
            Reporter.SetLogs($"Text {element} of the element {locator} is equal to expected value: {expectedValue}");
        }

        //Gets url of a current web page.
        public string GetCurrentUrl()
        {
            var thisUrl = _driver.Url;
            Reporter.SetLogs($"Getting current URL. It is {thisUrl}");
            return thisUrl;
        }
        
        //Provides implementation for hover action.
        public void Hover(string locator)
        {
            Reporter.SetLogs($"Moving and hovering over the element by its locator {locator}");
            new Actions(_driver).MoveToElement(_driver.FindElement(ProcessLocator(locator))).Perform();
        }        

        //Switches to window by index.
        public void SwitchToWindow(int windowIndex)
        {
            Reporter.SetLogs($"Switching to window/tab by its index: {windowIndex}");
            _driver.SwitchTo().Window(_driver.WindowHandles[windowIndex]);
        }

        //Switches to default content.
        public void SwitchToDefaultContent()
        {
            Reporter.SetLogs("Switching to default window...");
            _driver.SwitchTo().DefaultContent();
        }

        //Closes active window.
        public void CloseCurrentWindow()
        {
            Reporter.SetLogs("Closing current window...");
            _driver.Close();
        }

        //Switches to iframe by locator.
        public void SwitchToFrame(string locator)
        {
            Reporter.SetLogs($"Switching to frame by its locator: {locator}");
            _driver.SwitchTo().Frame(_driver.FindElement(ProcessLocator(locator)));
        }

        //Accepts alert.
        public void AcceptAlert()
        {
            Reporter.SetLogs($"Switching to alert and accepting it.");
            _driver.SwitchTo().Alert().Accept();
        }

        //Provides implementation for drag and drop action.
        public void DragAndDrop(string draggableElement, string targetElement)
        {
            Reporter.SetLogs($"Dragging element by locator: {draggableElement} to its target element by locator: {targetElement}");
            new Actions(_driver).DragAndDrop(_driver.FindElement(ProcessLocator(draggableElement)), _driver.FindElement(ProcessLocator(targetElement))).Perform();
        }

        //Simulates key press and release.
        public void PressKey(VirtualKeyCode keycode)
        {
            Reporter.SetLogs($"Pressing key by the following keycode {keycode}");
            new InputSimulator().Keyboard.KeyPress(keycode);
        }

    }
}