using Core.Entities.Abstract;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.CorporateDtos.CorporateAddDtos
{
    public class CorporateAddRequestDto:IDto
    {
        public int Id { get; set; }
        public int CorporateCode { get; set; }
        public string CorporateName { get; set; }
        public int CorporateAccountNo { get; set; }
        public MoneyTypeEnum MoneyType { get; set; }
        public string Prefix { get; set; }
        public ComissionTypeEnum ComissionType { get; set; }
        public MoneyTypeEnum ComissionMoneyType { get; set; }
        public int Comission { get; set; }
        public int ComissionAccountNo { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}
