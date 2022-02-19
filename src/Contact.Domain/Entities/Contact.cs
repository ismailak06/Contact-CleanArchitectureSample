using Mermas.Domain.Common;
using Mermas.Domain.Interfaces;


namespace Contact.Domain.Entities
{
    public class Contact : AuditableEntity, ISoftDelete
    {
        public Contact(string name, string surname, string companyName)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"'{nameof(Name)}' cannot be null or empty.", nameof(Name));
            }

            if (string.IsNullOrEmpty(surname))
            {
                throw new ArgumentNullException($"'{nameof(Surname)}' cannot be null or empty.", nameof(Surname));
            }

            Name = name;
            Surname = surname;
            CompanyName = companyName;
        }
        public Contact()
        {

        }

        public string Name { get; private set; } 
        public string Surname { get; private set; } 
        public string CompanyName { get; private set; }

        public List<ContactInformation> ContactInformations { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now;
        }
    }
}
