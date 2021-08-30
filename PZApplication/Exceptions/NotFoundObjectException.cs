using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.Exceptions
{
    public class NotFoundObjectException:Exception
    {
        public NotFoundObjectException(string entity) : base($"{entity} is not found")
        {

        }
    }
}
