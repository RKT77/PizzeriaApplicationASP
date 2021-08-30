using PizzeriaApplication.DTO;
using PizzeriaApplication.Interfaces;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsAttendant
{
    public interface IAddAttendant:ICommand<AttendantRequest,AttendantDTO>
    {
    }
}
