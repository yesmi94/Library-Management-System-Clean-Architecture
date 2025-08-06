namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Borrowing
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public string MemberId { get; set; }

        public virtual Person Person { get; set; }

        public string BookId { get; set; }

        public bool IsReturned { get; set; }

        public virtual Book Book { get; set; }

    }
}
