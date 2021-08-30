using PizzeriaApplication.DTO;
using PizzeriaApplication.Interfaces;
using PizzeriaApplication.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsAttendant
{
    public interface IGetAttendants:ICommand<AttendantSearch,IEnumerable<AttendantDTO>>
    {
    }
}
