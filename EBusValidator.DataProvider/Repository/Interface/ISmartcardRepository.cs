﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBusValidator.DataProvider.Repository.Interface
{
    public interface ISmartcardRepository : IGenericRepository<Smartcard>
    {
        Smartcard GetByCardNumber(string smartcardid);
    }
}
