using Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contact.Domain.Tests
{
    public class DocumentLogTests
    {
        [Fact]
        public void Success_WhenCreateDocumentLog_WithValidParameters()
        {
            var documentLog = new DocumentLog(DocumentLog.Status.Processing);
            Assert.NotNull(documentLog);
        }
        [Fact]
        public void ThrowsArgumentException_WhenCreateDocumentLog_WithInvalidParameters()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new DocumentLog((DocumentLog.Status)int.MaxValue);
            });
        }
    }
}
