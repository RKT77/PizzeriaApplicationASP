﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.Interfaces
{
    public interface ICommand<TReq, TRes>
    {
        TRes Execute(TReq req);
    }
    public interface ICommand<TReq>
    {
        void Execute(TReq req);
    }
}
