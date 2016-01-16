using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace NOTAM.Service
{
    public abstract class CodeBase: NotamBase 
    {

      // [Column(Name = "Code", DbType = "NVarChar(20) NOT NULL", CanBeNull = false)]
        public abstract string Code{ get; set;}

        //[Column(Name = "Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public abstract string Name { get; set; }
    }
}
