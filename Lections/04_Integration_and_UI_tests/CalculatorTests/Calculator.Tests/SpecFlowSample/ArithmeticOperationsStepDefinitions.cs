using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Calculator.Tests
{
    [Binding]
    public class ArithmeticOperationsStepDefinitions
    {
        private AutomationBase automation;
        private const string CalculatorAppName
            = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
        private Calculator calculator;
        private Application app;

        [Before]
        public void InitCalculator()
        {
            automation = new UIA3Automation();
            app = FlaUI.Core.Application
                .LaunchStoreApp(CalculatorAppName);
            calculator = app.GetMainWindow(automation).As<Calculator>();
            Thread.Sleep(1000);
        }

        [After]
        public void CloseCalculator()
        {
            app?.Close();
            automation?.Dispose();
            Thread.Sleep(1000);
        }

        [Given(@"Enter number (.*)", Culture = "en-US")]
        [Given(@"Введено число (.*)", Culture ="ru-Ru")]
        public void GivenEnterNumber(int num)
        {
            var strNum = num.ToString();
            foreach (char strDigit in strNum)
            {
                var digit = (uint)strDigit - '0';
                calculator.NumButtons[digit].Click();
            }
        }

        [Given(@"Press button (\+|-|\*|/)", Culture = "en-US")]
        [Given(@"Нажата клавиша (\+|-|\*|/)", Culture = "ru-RU")]
        public void GivenPressButton(string button)
        {
            switch (button)
            {
                case "+":
                    calculator.Plus.Click();
                    break;
                case "-":
                    calculator.Minus.Click();
                    break;
                case "*":
                    calculator.Mult.Click();
                    break;
                case "/":
                    calculator.Div.Click();
                    break;
            };
        }


        [When(@"Press button =", Culture = "en-US")]
        [When(@"Нажата клавиша =", Culture = "ru-RU")]
        public void WhenPressButton()
        {
            calculator.Equal.Click();
        }

        [Then(@"Display (.*)", Culture = "en-US")]
        [Then(@"Отобразится (.*)", Culture = "ru-RU")]
        public void ThenDisplay(int expectedResult)
        {
            var result = int.Parse(calculator.DisplayResult.Text.Replace("Display is ", ""));
            result.Should().Be(expectedResult);
        }
    }
}
