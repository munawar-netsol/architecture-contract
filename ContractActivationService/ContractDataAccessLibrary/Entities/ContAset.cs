using System;
using System.Collections.Generic;

namespace ContractDataAccessLibrary
{
    public partial class ContAset
    {
        public int ContAsetId { get; set; }
        public int ContId { get; set; }
        public string VinNumb { get; set; } = null!;
        public string ModlNme { get; set; } = null!;
        public string? InsrBy { get; set; }
        public string? InsrDte { get; set; }
        public string? UpdtBy { get; set; }
        public string? UpdtDte { get; set; }
        public virtual Cont Cont { get; set; } = null!;
    }
}
