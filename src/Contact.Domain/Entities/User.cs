using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Entities
{
    public class User
    {
        public Contact CreateContact(string firstName, string lastName, string companyName)
        {
            return new Contact(firstName, lastName, companyName);
        }
    }
}
