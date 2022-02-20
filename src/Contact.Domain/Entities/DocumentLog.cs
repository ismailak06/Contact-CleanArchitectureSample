using Contact.Domain.Common;
using Contact.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Contact.Domain.Entities
{
    public class DocumentLog : AuditableEntity, ISoftDelete
    {
        public DocumentLog(Status processStatus)
        {
            if (!Enum.IsDefined(typeof(Status), processStatus))
            {
                throw new ArgumentOutOfRangeException(nameof(processStatus));
            }
            ProcessStatus = processStatus;
        }
        public Status ProcessStatus { get; private set; }
        public string ProcessStatusDisplayName => ProcessStatus.GetType().GetMember(ProcessStatus.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();

        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now;
        }
        public void SetStatus(Status processStatus)
        {
            ProcessStatus = processStatus;
        }
        public enum Status
        {
            [Display(Name = "N/A")]
            None,
            [Display(Name = "İşleniyor")]
            Processing,
            [Display(Name = "Tamamlandı")]
            Completed
        }

    }
}
