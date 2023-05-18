using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class Result<T>
    {
        public string MSG { get; set; }
        public bool IsSuccess { get; set; }

        public T Data { get; set; }

    }

   
}