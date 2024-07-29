using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestDownloadDtos
{
    public class ExcelDto:IDto
    {
        public int RowId { get; set; }
        public object ColumnZero { get; set; }
        public object ColumnOne { get; set; }
        public object ColumnTwo { get; set; }
        public object ColumnThree { get; set; }
        public object ColumnFour { get; set; }
        public object ColumnFive { get; set; }
        public object ColumnSix { get; set; }
        public object ColumnSeven { get; set; }
        public object ColumnEight { get; set; }
        public object ColumnNine { get; set; }
        public object ColumnTen { get; set; }
    }
}
