using Contact.Domain.Entities;
using System;
using Xunit;

namespace Contact.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void Success_WhenCreateContact_WithValidParameters()
        {
            var user = new User();
            var contact = user.CreateContact("Sevinç", "Mutluer");
            var contactWithCompanyName = user.CreateContact("Sevinç", "Mutluer", "Mutluer A.Þ.");

            Assert.NotNull(contact);
            Assert.NotNull(contactWithCompanyName);
        }
        [Fact]
        public void ThrowArgumentException_WhenCreateContact_WithEmptyOrNullParameters()
        {
            var user = new User();

            Assert.Throws<ArgumentNullException>(() =>
            {
                user.CreateContact(string.Empty, "Mutluer");
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                user.CreateContact(null, "Mutluer");
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                user.CreateContact("Sinan", string.Empty);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                user.CreateContact("Sinan", null);
            });
        }

        [Fact]
        public void Success_WhenCreateContactInformation()
        {
            var user = new User();
            var contact = new Entities.Contact("Okan","Demir");
            contact.Id = 3;
            var contactInformation = user.CreateContactInformation(ContactInformation.Type.PhoneNumber,"05390000000", contact);
            
            Assert.NotNull(contactInformation);
        }

        [Fact]
        public void ThrowArgumentExcepiton_WhenCreateContactInformation_WithInvalidParameters()
        {
            var user = new User();
            var contact = new Entities.Contact("Okan", "Demir");
            contact.Id = 3;

            Assert.Throws<ArgumentNullException>(() => {
                user.CreateContactInformation(ContactInformation.Type.Email, string.Empty, contact);
            });

            Assert.Throws<ArgumentNullException>(() => {
                user.CreateContactInformation(ContactInformation.Type.Location, null, contact);
            });

            Assert.Throws<NullReferenceException>(() => {
                user.CreateContactInformation(ContactInformation.Type.Email, "test@test.com", null);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                user.CreateContactInformation((ContactInformation.Type)int.MaxValue, "test@test.com", contact);
            });
        }
    }
}