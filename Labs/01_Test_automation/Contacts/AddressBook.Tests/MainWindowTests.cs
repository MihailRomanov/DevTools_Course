using AddressBook.Tests.Windows;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using FluentAssertions;
using Microsoft.Communications.Contacts;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AddressBook.Tests
{
    [Apartment(ApartmentState.STA)]
    public class MainWindowTests
    {
        ContactManager contactManager;
        private AutomationBase automation;
        private Application app;


        [SetUp]
        public void Setup()
        {
            contactManager = new ContactManager();
            ClearContacts();

            automation = new UIA3Automation();
            app = FlaUI.Core.Application
                .Launch(@"..\..\..\..\AddressBook\AddressBook.exe");
        }

        [TearDown]
        public void Clear()
        {
            app?.Close();
            automation?.Dispose();

            ClearContacts();
        }

        [Test]
        public void ReadContactList()
        {
            var contacts = GenerateContacts(10).ToList();
            var mainWindow = app.GetMainWindow(automation).As<MainWindow>();

            var contactNames = mainWindow.Contacts.Select(c => c.ContactName);
            var expectedNames = contacts.Select(c => c.Names.First().FormattedName);

            contactNames.Should().BeEquivalentTo(expectedNames);
        }

        [Test]
        public void OpenFirstContact()
        {
            var contacts = GenerateContacts(10).ToList();
            var mainWindow = app.GetMainWindow(automation).As<MainWindow>();

            var contactWindow = mainWindow.Contacts.First().OpenContactWindow();
            contactWindow.Close();
        }

        private void ClearContacts()
        {
            var contacts = contactManager.GetContactCollection();
            foreach (var contact in contacts)
            {
                contactManager.Remove(contact.Id);
            }
        }

        private IEnumerable<Contact> GenerateContacts(int count)
        {
            for (uint i = 0; i < count; i++)
            {
                var person = new Bogus.Person();

                var contact = new Contact();
                contact.Names.Add(new Name(person.FirstName, "", person.LastName,
                    NameCatenationOrder.GivenFamily));

                contactManager.AddContact(contact);

                yield return contact;
            }

        }
    }
}