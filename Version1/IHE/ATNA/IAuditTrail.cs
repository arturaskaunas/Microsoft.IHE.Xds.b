using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.ATNA
{
    public interface IAuditTrail
    {
        void RecordAuditTrail(AuditMessage message);
    }
}
