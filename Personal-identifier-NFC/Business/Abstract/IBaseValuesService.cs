﻿using Core.Results;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBaseValuesService
    {
        IResult Add(BaseValues baseValues);
        IDataResult<List<BaseValues>> GetAllActive();
    }
}
