using Microsoft.EntityFrameworkCore;

namespace AgEntities.CustomEntities
{
    public class AuditEntry2
    {
        public int AuditEntryId { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
    }
}
