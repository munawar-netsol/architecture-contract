namespace ContractModificationService.Contracts
{
    public interface IContractModification
    {
        int ContractId { get; set; }
        string ContractNumber { get; set; }
    }
}