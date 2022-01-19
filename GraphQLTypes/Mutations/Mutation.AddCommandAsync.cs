using GraphQLOne.Data;
using GraphQLOne.GraphQLTypes.Subscriptions;
using GraphQLOne.Models;
using GraphQLOne.Models.Mutations;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLOne.GraphQLTypes.Mutations
{
    public partial class Mutation
    {
        // adding platform
        [UseDbContext(typeof(AppDBContext))]
        public async Task<AddCommandPayLoad> AddCommandAsync(
            AddCommanInput input , 
            [ScopedService] AppDBContext _ctx ,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken
            )
        {
            Command command = new Command{ HowTo = input.HowTo , CommandLine = input.CMD ,PlateformID =input.platformID};
            await _ctx.Commands.AddAsync(command,cancellationToken);
            await _ctx.SaveChangesAsync(cancellationToken);
            command.Platform = await _ctx.Platforms.FindAsync(input.platformID);
            // notify the subscribers 
            await eventSender.SendAsync(nameof(Subscription.OnCommandAdded),command , cancellationToken);
            return new AddCommandPayLoad(command);
        }
    }
}
