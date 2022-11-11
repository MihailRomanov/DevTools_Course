using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace Calculator.Tests
{
    public class Calculator : Window
    {
        public Calculator(FrameworkAutomationElementBase frameworkAutomationElement) 
            : base(frameworkAutomationElement)
        {
        }

        public Label DisplayResult =>
            FindFirstDescendant(cf => cf.ByAutomationId("CalculatorResults"))
            .AsLabel();

        public NumButtons NumButtons =>
            FindFirstDescendant(cf => cf.ByAutomationId("NumberPad"))
            .As<NumButtons>();

        public Button Plus =>
            FindFirstDescendant(cf => cf.ByAutomationId("plusButton"))
            .AsButton();
        public Button Minus =>
            FindFirstDescendant(cf => cf.ByAutomationId("minusButton"))
            .AsButton();
        public Button Mult =>
            FindFirstDescendant(cf => cf.ByAutomationId("multiplyButton"))
            .AsButton();
        public Button Div =>
            FindFirstDescendant(cf => cf.ByAutomationId("divideButton"))
            .AsButton();

        public Button Equal =>
            FindFirstDescendant(cf => cf.ByAutomationId("equalButton"))
            .AsButton();
    }

    public class NumButtons : AutomationElement
    {
        public NumButtons(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public Button this[uint num] 
        {
            get 
            {
                if (num > 9)
                    throw new IndexOutOfRangeException();
                return FindFirstDescendant(cf => cf.ByAutomationId($"num{num}Button"))
                    .AsButton();
            }
        }
    }
}
