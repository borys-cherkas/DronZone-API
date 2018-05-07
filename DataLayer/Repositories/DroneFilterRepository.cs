using System;
using System.Collections.Generic;
using System.Text;
using Common.Models;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class DroneFilterRepository  : RepositoryBase<DroneFilter>, IDroneFilterRepository
    {
        public DroneFilterRepository(AppDbContext dbContext) 
            : base(dbContext, dbContext.DroneFilters) { }
    }
}
