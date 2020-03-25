using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlalaAPI.Mapper.Attributes
{
    //STATUT : OK
    public class MapperPropertyAttribute : Attribute
    {
        public string PropertyName { get; private set; }
        public MapperPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}