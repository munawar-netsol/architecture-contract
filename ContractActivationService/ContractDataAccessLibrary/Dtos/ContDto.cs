using System;
using System.Collections.Generic;

namespace ContractDataAccessLibrary
{
    public partial class ContDto
    {
        public ContDto()
        {

        }        
        public string ContNumb { get; set; } = null!;
        public DateTime? ContStrtDte { get; set; }
        public DateTime? ContEndDte { get; set; }
        public List<ContAsetDto> ContAsets { get; set; } = new List<ContAsetDto>();        
    }
    public static class ContDtoHelper
    {
        public static ContDto AsDto(this Cont c)
        {
            var dto = new ContDto()
            {
                ContNumb = c.ContNumb,
                ContStrtDte = c.ContStrtDte,
                ContEndDte = c.ContEndDte
            };
            foreach (var contAset in c.ContAsets)
            {
                dto.ContAsets.Add(contAset.AsDto());
            }
            return dto;
        }

        public static Cont AsEntity(this ContDto c)
        {
            var cont = new Cont()
            {
                ContNumb = c.ContNumb,
                ContStrtDte = c.ContStrtDte,
                ContEndDte = c.ContEndDte
            };
            foreach (var contAsetDto in c.ContAsets)
            {                
                var contAset = contAsetDto.AsEntity();
                contAset.Cont = cont;
                cont.ContAsets.Add(contAset);
            }
            return cont;
        }
    }
}
