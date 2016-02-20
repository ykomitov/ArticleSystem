﻿namespace ArticleSystem.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ArticleSystem.Data.Models;

    public interface IVotesService
    {
        Vote GetById(int id);
    }
}
