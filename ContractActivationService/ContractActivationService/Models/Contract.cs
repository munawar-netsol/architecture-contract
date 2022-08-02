using Contracts;
using System.Collections;

namespace Publisher.Models
{
    public class Contract : IContract
    {
        public int ContractId { get; set; }
        public string ContractNumber { get; set; }
        IEnumerable<IContractAsset> _contractAssets;
        public IEnumerable<IContractAsset> ContractAssets
        {
            get { return _contractAssets; }
            set { _contractAssets = value; }
        }
    }
}
