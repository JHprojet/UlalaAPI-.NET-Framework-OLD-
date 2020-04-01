using System;

namespace UlalaAPI.Mapper.Attributes
{
    public class MapperPropertyAttribute : Attribute
    {
        public string PropertyName { get; private set; }
        public MapperPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}