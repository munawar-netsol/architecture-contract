using ContractModificationService.Contracts;

namespace ContractModificationService.Models
{
    public class ContractModification : IContractModification
    {
        public int ContractId { get; set; }
        public string ContractNumber { get; set; }
    }
}
