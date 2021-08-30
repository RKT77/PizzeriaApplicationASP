using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaCommands
{
    public abstract class BaseCommand
    {
        protected PizzeriaContext context { get; }
        protected BaseCommand(PizzeriaContext ctx) => context = ctx;
    }
}
