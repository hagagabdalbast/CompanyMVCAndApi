﻿using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Interface
{
    public interface ICountryRepo
    {
        IEnumerable<Country> Get();

        Country GetById(int id);

    }
}
