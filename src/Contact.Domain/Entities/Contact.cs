using Contact.Domain.Common;
using Contact.Domain.Interfaces;


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
