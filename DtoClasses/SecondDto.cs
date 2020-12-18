using DtoClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoProject
{
    public class SecondDto : IDto
    {
        /*public SecondDto(List<int> listIntValue, DateTime dateTimeValue, List<string> listStringValue, FirstDto dtoValue)
        {
            ListInt = listIntValue;
            DateTime = dateTimeValue;
            ListString = listStringValue;
            Dto = dtoValue;
        }*/

        public List<int> ListInt;
        public DateTime DateTime;
        public List<string> ListString { get; set; }
        public FirstDto Dto;
    }
}
