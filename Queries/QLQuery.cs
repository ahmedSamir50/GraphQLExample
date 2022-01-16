using GraphQLOne.Data;
using GraphQLOne.Models;
using HotChocolate;
using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLOne.Queries
{
    public class QLQuery
    {
        // service ATTR is coming from chocolate to inject // here supports method injections
        // this attr also for the pooled db context multi threading / concurrent queries
        [UseDbContext(typeof(AppDBContext))]
        // allow to query a child object
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatform([ScopedService]AppDBContext _ctox)
        {
            return _ctox.Platforms;
        }
        
        [UseDbContext(typeof(AppDBContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommand([ScopedService] AppDBContext _ctox)
        {
            return _ctox.Commands;
        }
    }
}
