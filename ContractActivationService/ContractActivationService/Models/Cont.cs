using System;
using System.Collections.Generic;

namespace ContractActivationService.Models
{
    public partial class Cont
    {
        public Cont()
        {
            ContAsets = new HashSet<ContAset>();
        }

        public int ContId { get; set; }
        public string ContNumb { get; set; } = null!;
        public DateTime? ContStrtDte { get; set; }
        public DateTime? ContEndDte { get; set; }
        public string? InsrBy { get; set; }
        public string? InsrDte { get; set; }
        public string? UpdtBy { get; set; }
        public string? UpdtDte { get; set; }

        public virtual ICollection<ContAset> ContAsets { get; set; }
    }
}
