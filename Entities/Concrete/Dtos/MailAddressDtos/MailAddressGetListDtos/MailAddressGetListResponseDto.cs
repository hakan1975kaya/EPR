﻿using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.MailAddressDtos.MailAddressGetListDtos
{
    public class MailAddressGetListResponseDto : IDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public bool IsCC { get; set; }
        public bool IsPtt { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}
