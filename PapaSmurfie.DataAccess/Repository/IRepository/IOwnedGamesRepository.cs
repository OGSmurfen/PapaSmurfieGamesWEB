﻿using PapaSmurfie.Models;
using PapaSmurfie.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.DataAccess.Repository.IRepository
{
    public interface IOwnedGamesRepository : IRepositoryGeneric<OwnedGameModel> 
    {
    }
}
