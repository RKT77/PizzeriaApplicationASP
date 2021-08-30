using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.Exceptions
{
    public class ObjectDoesntExistException:Exception
    {
        public ObjectDoesntExistException(string entity):base($"{entity} does not exist")
        {

        }
    }
}
