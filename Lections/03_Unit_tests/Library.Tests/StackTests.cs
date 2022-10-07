using System;
using System.Collections.Generic;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Principal;
using FluentAssertions;

namespace Wintellect.PowerCollections.Tests
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void DefaultConstructorStack()
        {
            Stack<int> stack = new Stack<int>(1);

            Assert.AreEqual(0, stack.Count);
            Assert.AreEqual(1, stack.Capacity);
        }
        [TestMethod]
        public void CreatedStack_IsEmpty()
        {
            Stack<string> stack = new Stack<string>(3);
            Assert.IsTrue(stack.IsEmpty());
        }

        [TestMethod]
        public void AfterPushElement_Stack_IsNotEmpty()
        {
            Stack<string> stack = new Stack<string>(3);
            stack.Push("hello");
            Assert.IsFalse(stack.IsEmpty());
        }

        [TestMethod]
        public void AfterPopElement_Stack_IsEmpty()
        {
            Stack<string> stack = new Stack<string>(3);
            stack.Push("hello");
            stack.Pop();
            Assert.IsTrue(stack.IsEmpty());
        }

        [TestMethod]
        public void CreatedStack_IsNotFull()
        {
            Stack<string> stack = new Stack<string>(3);
            Assert.IsFalse(stack.IsFull());
        }

        [TestMethod]
        public void AfterPushAllAvailableElements_IsFull()
        {
            Stack<string> stack = new Stack<string>(3);
            stack.Push("hello");
            stack.Push("my");
            stack.Push("friend");
            Assert.IsTrue(stack.IsFull());
        }

        [TestMethod]
        public void AfterPushNotAllAvailableElements_IsNotFull()
        {
            Stack<string> stack = new Stack<string>(3);
            stack.Push("hello");
            stack.Push("my");
            Assert.IsFalse(stack.IsFull());
        }

        [TestMethod]
        public void AfterPopElementFromFilledStack_IsNotFull()
        {
            Stack<string> stack = new Stack<string>(2);
            stack.Push("hello");
            stack.Push("my");
            stack.Pop();
            Assert.IsFalse(stack.IsFull());
        }

        [TestMethod]
        public void Push()
        {
            Stack<int> stack = new Stack<int>(3);
            stack.Push(7);
            Assert.AreEqual(7, stack.Top());
            Assert.AreEqual(1, stack.Count);
        }

        [TestMethod]
        public void Pop()
        {
            Stack<int> stack = new Stack<int>(3);
            stack.Push(7);
            Assert.AreEqual(7, stack.Pop());
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void Top()
        {
            Stack<int> stack = new Stack<int>(3);
            stack.Push(7);
            Assert.AreEqual(7, stack.Top());
        }

        [TestMethod]
        public void TopException()
        {
            Stack<int> stack = new Stack<int>(10);
            Action act = () => stack.Top();
            act.Should().Throw<InvalidOperationException>()
            .WithMessage("Стек пуст. Нет элементов для получения.");
        }

        [TestMethod]
        public void PopException()
        {
            Stack<int> stack = new Stack<int>(10);
            Action act = () => stack.Pop();
            act.Should().Throw<InvalidOperationException>()
            .WithMessage("Стек пуст. Нет элементов для получения.");
        }

        [TestMethod]
        public void PushException()
        {
            Stack<string> stack = new Stack<string>(2);
            Action act = () => {
                stack.Push("Hello");
                stack.Push("my");
                stack.Push("friends");
            };
            act.Should().Throw<InvalidOperationException>()
            .WithMessage("Стек переполнен. Невозможно добавить элемент.");
        }

        [TestMethod]
        public void GetEnumeratorFromTopToBottom()
        {
            Stack<string> stack = new Stack<string>(3);
            IEnumerator numerator = stack.GetEnumerator();
            stack.Push("Hello");
            stack.Push("Viktoria");
            stack.Push("Goodbye");
            numerator.Current.Should().BeNull();
            numerator.MoveNext();
            numerator.Current.Should().Be("Goodbye");
            numerator.MoveNext();
            numerator.Current.Should().Be("Viktoria");
            numerator.MoveNext();
            numerator.Current.Should().Be("Hello");
        }
        [TestMethod]
        public void GetEnumeratorFromEmptyStack()
        {
            Stack<string> stack = new Stack<string>(1);
            IEnumerator numerator = stack.GetEnumerator();
            numerator.MoveNext().Should().BeFalse();
            numerator.Current.Should().BeNull();
        }
        [TestMethod]
        public void GetEnumeratorFromFilledStack()
        {
            Stack<string> stack = new Stack<string>(1);
            IEnumerator numerator = stack.GetEnumerator();
            stack.Push("Hello");
            numerator.MoveNext();
            numerator.MoveNext().Should().BeFalse();
        }
    }
}