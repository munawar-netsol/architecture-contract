namespace Contracts
{
    public interface IContract
    {
        int ContractId { get; set; }
        string ContractNumber { get; set; }
        IEnumerable<IContractAsset> ContractAssets { get; set; }
    }
}