using Contact.Domain.Common;
using Contact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Entities
{
    public class DocumentReport: AuditableEntity, ISoftDelete
    {
        public DocumentReport(string fileName, Status processStatus)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException($"'{nameof(FileName)}' cannot be null or empty.", nameof(FileName));
            }
            FileName = fileName;
            ProcessStatus = processStatus;
        }
        public string FileName { get; set; }
        public Status ProcessStatus { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now;
        }

        public enum Status
        {
            None,
            Processing,
            Completed
        }
    }
}
