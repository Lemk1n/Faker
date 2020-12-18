using DtoClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoProject
{
    public class FirstDto : IDto
    {
        public FirstDto()
        {

        }

        public FirstDto(int intValue)
        {
            IntProperty = intValue;
        }

        public FirstDto(int intValue, long longValue, double doubleValue, float floatValue, string stringValue, List<int> listIntValue, SecondDto secondDtoValue)
        {
            IntProperty = intValue;
            LongProperty = longValue;
            DoubleProperty = doubleValue;
            FloatProperty = floatValue;
            StringProperty = stringValue;
            ListIntProperty = listIntValue;
            Dto = secondDtoValue;
        }

        public int IntProperty { get; set; }
        public long LongProperty { get; set; }
        public double DoubleProperty { get; set; }
        public float FloatProperty { get; set; }
        public string StringProperty { get; set; }
        public List<int> ListIntProperty { get; set; }
        public SecondDto Dto;
    }
}
