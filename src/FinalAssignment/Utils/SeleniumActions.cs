using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using FluentAssertions;
using WindowsInput.Native;
using WindowsInput;

namespace FinalAssignment.Utils
{
    class SeleniumActions
    {
        private IWebDriver _driver;
        private WebDriverWait Wait;
        
        public SeleniumActions()
        {
            this._driver = DriverFactory.Driver;
            Log.Debug("Actions.class instance created...");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }        

        //private void WaitFor(string locator, bool isCollection = false)
        //{           

        //    if (Wait == null)
        //    {
        //        this.Wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        //        Log.Debug($"Wait instance created with timeout {this.Wait.Timeout.ToString()}");
        //    }

        //    if (isCollection)
        //    {
        //        Wait.Until<IWebElement>((d) => {
        //            SetLogs($"Searching a collection of elements by locator {locator}");                    
        //            return d.FindElements(ProcessLocator(locator)).First();
        //        });

        //    }
        //    else
        //    {
        //        Wait.Until<IWebElement>((d) =>
        //        {
        //            SetLogs($"Searching for a single element by locator {locator})");
        //            return d.FindElement(ProcessLocator(locator));
        //        });
        //    }

        //}

        public void WaitForElementToUpdate(string locator)
        {

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));


            By byLocator = ProcessLocator(locator);

            SetLogs($"Waiting for an element by the following locator: {locator}");

            IWebElement element = wait.Until((d) =>
            {
                if (d.FindElement(byLocator).GetAttribute("value") != "")
                    return d.FindElement(byLocator);
                else
                    return null;
            });

            SetLogs($"Element by the following locator: {locator} has been found. Proceeding...");
        }

        private void SetDefaultWaitFor(int duration)
        {
            SetLogs($"Setting default time to wait for an element to {duration}");            
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
            //WaitFor(locator, true);
            SetLogs($"Clicking on the element by its locator: { locator } and index in the collection of similar elements {index}");            
            _driver.FindElements(ProcessLocator(locator)).ElementAt(index).Click();
        }

        public void Click(string locator)
        {
            //WaitFor(locator);
            SetLogs($"Clicking on the element by its locator: {locator}");            
            _driver.FindElement(ProcessLocator(locator)).Click();
        }

        public IWebElement SetText(string locator, string text)
        {
            //WaitFor(locator);
            SetLogs($"Setting the text in the element by its locator: {locator}");
            var element = _driver.FindElement(ProcessLocator(locator));
            element.SendKeys(text);

            return element;
        }

        public void ClearText(string locator, int index)
        {
            //WaitFor(locator, true);
            SetLogs($"Clearing the text in the element by its locator: {locator}");
            _driver.FindElements(ProcessLocator(locator)).ElementAt(index).Clear();
        }

        public bool CheckIfEnabled(string locator)
        {
            //WaitFor(locator);
            var element = _driver.FindElement(ProcessLocator(locator));

            if (element.Enabled)
            {
                SetLogs($"Element by the following locator: {locator} is enabled");
                return true;
            }
            else
            {
                SetLogs($"Element by the following locator: {locator} is disabled");
                return false;
            }
        }

        public bool CheckIfSelected(string locator, int index)
        {
            //WaitFor(locator);
            var element = _driver.FindElements(ProcessLocator(locator)).ElementAt(index);

            if (element.Selected)
            {
                SetLogs($"Element by the following locator: {locator} is selected");
                return true;
            }
            else
            {
                SetLogs($"Element by the following locator: {locator} is NOT selected");
                return false;
            }
        }

        public bool CheckIfSelected(string locator)
        {
            //WaitFor(locator);
            var element = _driver.FindElement(ProcessLocator(locator));

            if (element.Selected)
            {
                SetLogs($"Element by the following locator: {locator} is selected");
                return true;
            }
            else
            {
                SetLogs($"Element by the following locator: {locator} is NOT selected");
                return false;
            }
        }

        public void GetCssValue(string locator, string cssAttribute)
        {
            //WaitFor(locator);
            SetLogs($"Getting CSS value from the element by its locator: {locator}");
            var cssValue = _driver.FindElement(ProcessLocator(locator)).GetCssValue(cssAttribute);
            SetLogs($"The CSS value of the element by locator: {locator} is: {cssValue}");
        }

        public string GetText(string locator, int index)
        {
            //WaitFor(locator);
            var text = _driver.FindElements(ProcessLocator(locator)).ElementAt(index).Text;
            SetLogs($"Getting text from the collection of elements by its locator: {locator} and position in the collection {index}. Text value is {text}");
            return text;

        }

        public string GetText(string locator)
        {
            //WaitFor(locator);
            var text = _driver.FindElement(ProcessLocator(locator)).Text;
            SetLogs($"Getting text from the element by its locator: {locator}. Text value is {text}");
            return text;

        }

        public string GetTextWithImplicitWait(string locator, int time)
        {
            SetDefaultWaitFor(time);

            string text = _driver.FindElement(ProcessLocator(locator)).Text;

            SetDefaultWaitFor(0);

            return text;

        }

        public string GetAttribute(string locator, string attribute, int index)
        {
            //WaitFor(locator);
            var value = _driver.FindElements(ProcessLocator(locator)).ElementAt(index).GetAttribute(attribute);

            SetLogs($"Getting attribute {attribute} from the element {locator} in the {index} position in the collection. The value of the requested attribute is {value}");

            return value;

        }

        public string GetAttribute(string locator, string attribute)
        {
            //WaitFor(locator);

            var value = _driver.FindElement(ProcessLocator(locator)).GetAttribute(attribute);
            SetLogs($"Getting attribute {attribute} from the element {locator}. The value of the requested attribute is {value}");

            return value;

        }

        public List<IWebElement> ReturnAndFilterElements(string locator)
        {
            var unfilteredList = _driver.FindElements(ProcessLocator(locator)).ToList();

            return unfilteredList.Where(x => x.Text.Contains("5")).ToList();

        }

        public void SelectDropdownElement(string locator, int index)
        {
            //WaitFor(locator);

            SelectElement selectElement = new SelectElement(_driver.FindElement(ProcessLocator(locator)));
            Log.Debug($"Select instance created by the following locator of the dropdown: {locator}");

            selectElement.SelectByIndex(index);

            SetLogs($"Dropdown element was selected by index: {index}");
        }

        public void SelectDropdownElement(string locator, string value)
        {
            //WaitFor(locator);

            SelectElement selectElement = new SelectElement(_driver.FindElement(ProcessLocator(locator)));
            Log.Debug($"Select instance created by the following locator of the dropdown: {locator}");

            selectElement.SelectByValue(value);

            SetLogs($"Dropdown element was selected by value: {value}");
        }

        public void SelectDropdownElementByText(string locator, string text)
        {
            //WaitFor(locator);

            SelectElement selectElement = new SelectElement(_driver.FindElement(ProcessLocator(locator)));
            Log.Debug($"Select instance created by the following locator of the dropdown: {locator}");

            selectElement.SelectByText(text);

            SetLogs($"Dropdown element was selected by value: {text}");
        }

        public void SubmitForm(string locator)
        {
            //WaitFor(locator);

            var element = _driver.FindElement(ProcessLocator(locator));
            SetLogs($"Submitting the form that contains the following element: {locator}");

            element.Submit();
        }

        public void ToPreviousPage()
        {
            SetLogs("Navigating back to the previous page");
            _driver.Navigate().Back();
        }

        public void RefreshPage()
        {
            SetLogs("Refreshing the page");
            _driver.Navigate().Refresh();
        }        

        public void ValidateText(string locator, string expectedValue)
        {
            //WaitFor(locator);
            var element = _driver.FindElement(ProcessLocator(locator)).Text;            
            element.Should().Contain(expectedValue);
            SetLogs($"Text {element} of the element {locator} is equal to expected value: {expectedValue}");
        }

        public string GetCurrentUrl()
        {
            var thisUrl = _driver.Url;
            SetLogs($"Getting current URL. It is {thisUrl}");
            return thisUrl;
        }


        public void PressKey(VirtualKeyCode keycode)
        {
            SetLogs($"Pressing key by the following keycode {keycode}");
            new InputSimulator().Keyboard.KeyPress(keycode);
        }

        public void ReleaseControlKey()
        {
            SetLogs($"Releasing 'Control' key.");
            new Actions(_driver).KeyUp(Keys.Control).Perform();
        }

        public void Hover(string locator)
        {
            SetLogs($"Moving and hovering over the element by its locator {locator}");
            new Actions(_driver).MoveToElement(_driver.FindElement(ProcessLocator(locator))).Perform();
        }        

        public void SwitchToWindow(int windowIndex)
        {
            SetLogs($"Switching to window/tab by its index: {windowIndex}");
            _driver.SwitchTo().Window(_driver.WindowHandles[windowIndex]);
        }

        public void SwitchToDefaultContent()
        {
            SetLogs("Switching to default window...");
            _driver.SwitchTo().DefaultContent();
        }

        public void CloseCurrentWindow()
        {
            SetLogs("Closing current window...");
            _driver.Close();
        }

        public void SwitchToFrame(string locator)
        {
            SetLogs($"Switching to frame by its locator: {locator}");
            _driver.SwitchTo().Frame(_driver.FindElement(ProcessLocator(locator)));
        }

        public void AcceptAlert()
        {
            SetLogs($"Switching to alert and accepting it.");
            _driver.SwitchTo().Alert().Accept();
        }

        public void DragAndDrop(string draggableElement, string targetElement)
        {
            SetLogs($"Dragging element by locator: {draggableElement} to its target element by locator: {targetElement}");
            new Actions(_driver).DragAndDrop(_driver.FindElement(ProcessLocator(draggableElement)), _driver.FindElement(ProcessLocator(targetElement))).Perform();
        }


        public void FluentScroll(string locator)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)_driver;
            IWebElement elementFound;

            while (true)
            {
                if (_driver.FindElements(ProcessLocator(locator)).Count != 0)
                {
                    elementFound = _driver.FindElement(ProcessLocator(locator));
                    Log.Information($"Element by locator {locator} has been found!");
                    break;
                }

                long prevScrollTop = (long)jsExecutor.ExecuteScript("return window.scrollY");

                jsExecutor.ExecuteScript("window.scrollBy(0, 200)");

                Log.Information("Scrolling page down by 200 px");

                long scrollTop = (long)jsExecutor.ExecuteScript("return window.scrollY");

                if (prevScrollTop == scrollTop)
                    throw new NoSuchElementException("List does not contain this element");
            }

            jsExecutor.ExecuteScript("arguments[0].scrollIntoView()", elementFound);
        }

        private void SetLogs(string message)
        {
            Log.Information(message);
            Reporter.Log(message);
        }

    }
}