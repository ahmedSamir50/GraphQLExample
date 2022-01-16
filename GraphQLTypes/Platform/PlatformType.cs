using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLOne.Models;
using HotChocolate;
using GraphQLOne.Data;

namespace GraphQLOne.GraphQLTypes
{
    public class PlatformType: ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents any Software or services that has a command line interface . \"CLI\"");
            descriptor.Field(x => x.Name).Description("Represents Platform Name ");
            // don't expose a field 
            descriptor.Field(x => x.LicenseKey).Ignore();
            descriptor.Field(x => x.Commands)
                       .ResolveWith<Resolvers>(x => x.GetCommands(default, default))
                       .UseDbContext<AppDBContext>()
                       .Description("Represents Collection of Platform Related Known available Commands");
        }
        private class Resolvers {
           public IQueryable<Command> GetCommands([Parent]Platform platform, [ScopedService] AppDBContext _ctox) {
                return _ctox.Commands.Where(cmd => cmd.PlateformID == platform.Id);
            }
        }
    }
}
