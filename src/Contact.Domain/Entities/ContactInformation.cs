using Contact.Domain.Common;
using Contact.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Contact.Domain.Entities
{
    public class ContactInformation : AuditableEntity, ISoftDelete
    {
        public ContactInformation(Type informationType, string content)
        {
            InformationType = informationType;
            Content = content;
        }
        public ContactInformation()
        {}

        public Type InformationType { get; private set; }
        public string InformationTypeDisplayName => InformationType.GetType().GetMember(InformationType.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();
        public string Content { get; private set; }

        public Contact Contact { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now;
        }
        public enum Type
        {
            None,
            [Display(Name = "Telefon Numarası")]
            PhoneNumber,
            [Display(Name = "E-Posta")]
            Email,
            [Display(Name = "Konum")]
            Location
        }
    }
}
