using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using System.Windows.Automation;

namespace Calculator.Tests
{
    [TestFixture]
    public class UIAutomationSample
    {
        [Test]
        [STAThread]
        public void SwitchToScientificAndRevert()
        {
            var calcPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.System),
                "calc.exe");

            var process = Process.Start(calcPath);
            process.WaitForInputIdle();

            var desktop = AutomationElement.RootElement.GetUpdatedCache(
                new CacheRequest() { TreeScope = TreeScope.Subtree });

            var window = desktop.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(
                    AutomationElement.NameProperty, 
                    "Calculator"));

            var toggleMenuButton = window.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(
                    AutomationElement.AutomationIdProperty,
                    "TogglePaneButton"))
                .GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;

            toggleMenuButton.Invoke();

            window = window.GetUpdatedCache(
                new CacheRequest() { TreeScope = TreeScope.Subtree });

            var paneRoot = window.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(
                    AutomationElement.AutomationIdProperty,
                    "PaneRoot"));

            var scientificCalculatorMenu = paneRoot.FindFirst(
                TreeScope.Descendants,
                new AndCondition(
                    new PropertyCondition(
                        AutomationElement.ControlTypeProperty, 
                        ControlType.ListItem),
                    new PropertyCondition(
                        AutomationElement.NameProperty, "Scientific Calculator")))
                .GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;

            scientificCalculatorMenu.Invoke();

            Thread.Sleep(4000);

            toggleMenuButton.Invoke();
            paneRoot = paneRoot.GetUpdatedCache(
                new CacheRequest() { TreeScope = TreeScope.Subtree });

            var standardCalculatorMenu = paneRoot.FindFirst(
                TreeScope.Descendants,
                new AndCondition(
                    new PropertyCondition(
                        AutomationElement.ControlTypeProperty,
                        ControlType.ListItem),
                    new PropertyCondition(
                        AutomationElement.NameProperty, "Standard Calculator")))
                .GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;

            standardCalculatorMenu.Invoke();

            Thread.Sleep(4000);

            (window.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern)
                .Close();
        }
    }
}