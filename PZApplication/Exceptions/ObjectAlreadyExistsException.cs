﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.Exceptions
{
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException(string entity):base($"{entity} already exists")
        {

        }
    }
}
