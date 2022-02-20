using Contact.Domain.Common;
using Contact.Domain.Interfaces;


namespace Contact.Domain.Entities
{
    public class Contact : AuditableEntity, ISoftDelete
    {
        public Contact(string name, string surname, string companyName=null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"'{nameof(FirstName)}' cannot be null or empty.", nameof(FirstName));
            }

            if (string.IsNullOrEmpty(surname))
            {
                throw new ArgumentNullException($"'{nameof(LastName)}' cannot be null or empty.", nameof(LastName));
            }

            FirstName = name;
            LastName = surname;
            CompanyName = companyName;
        }
        public Contact()
        {

        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string CompanyName { get; private set; }

        public ICollection<ContactInformation> ContactInformations { get; set; } = new List<ContactInformation>();
        public IReadOnlyCollection<ContactInformation> PhoneNumberInformations => ContactInformations.Where(m => m.InformationType == ContactInformation.Type.PhoneNumber).ToList();
        public IReadOnlyCollection<ContactInformation> EmailInformations => ContactInformations.Where(m => m.InformationType == ContactInformation.Type.Email).ToList();
        public IReadOnlyCollection<ContactInformation> LocationInformations => ContactInformations.Where(m => m.InformationType == ContactInformation.Type.Location).ToList();


        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now;
        }
    }
}
