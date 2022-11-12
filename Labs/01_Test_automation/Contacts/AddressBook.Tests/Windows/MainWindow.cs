using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.PatternElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook.Tests.Windows
{
    public class MainWindow : Window
    {
        public MainWindow(FrameworkAutomationElementBase frameworkAutomationElement)
            :base(frameworkAutomationElement)
        {
        }

        public Menu Menu =>
            FindFirstDescendant(cf => cf.ByControlType(ControlType.Menu))
            .AsMenu();

        public IEnumerable<ContactItem> Contacts =>
            FindFirstDescendant(cf => cf.ByAutomationId("_contactPanel"))?
                .FindAllChildren(cf => cf.ByClassName("ListBoxItem"))?
                .Select(e => 
                    e.As<ContactItem>()) ?? Enumerable.Empty<ContactItem>();
    }

    public class ContactItem : SelectionItemAutomationElement
    {

        public ContactItem(FrameworkAutomationElementBase automationElement) : base(automationElement)
        {
        }

        public string ContactName
        {
            get
            {
                // Здесь приходится идти на ухищрение
                // надпись с именем не доступна как контрол
                // и достучаться до неё можно только в Raw Mode
                var walker = Automation.TreeWalkerFactory.GetRawViewWalker();
                var currentElement = walker.GetFirstChild(this);

                while (currentElement != null && 
                    currentElement.ClassName != "TextBlock")
                {
                    currentElement = walker.GetNextSibling(currentElement);
                }

                return currentElement?.Name ?? " --- --- ";
            }
        }

        public ContactWindow OpenContactWindow()
        {
            // Почему-то простой двойной щелчек не срабатывает
            Select();
            DoubleClick();

            // Теперь ждем, когда появится окно, от того же процесса,
            // но с нужным нам именем
            var windowResult = Retry.WhileNull(
                () => Automation
                    .GetDesktop()
                    .FindFirstChild(cf => 
                        cf.ByProcessId(Properties.ProcessId)
                        .And(cf.ByName("Windows Contacts"))));

            return windowResult.Success
                ? windowResult.Result.As<ContactWindow>()
                : null;
        }
    }
}
