using System;
using System.Collections.Generic;

namespace ContractDataAccessLibrary
{
    public partial class ContAsetDto
    {
        public string VinNumb { get; set; } = null!;
        public string ModlNme { get; set; } = null!;
    }
    public static class ContAssetDtoHelper
    {
        public static ContAsetDto AsDto(this ContAset c)
        {
            var dto = new ContAsetDto()
            {
                VinNumb = c.VinNumb,
                ModlNme = c.ModlNme
            };
            return dto;
        }
        public static ContAset AsEntity(this ContAsetDto c)
        {
            var dto = new ContAset()
            {
                VinNumb = c.VinNumb,
                ModlNme = c.ModlNme
            };
            return dto;
        }
    }
}
