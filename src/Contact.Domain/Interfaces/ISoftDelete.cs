﻿namespace Contact.Domain.Interfaces
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; }
        DateTime? DeletionDate { get; }
        void SoftDelete();

    }
}
