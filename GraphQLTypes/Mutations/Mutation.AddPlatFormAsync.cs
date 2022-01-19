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
        public async Task<AddPlatFormPayLoad> AddPlatFormAsync(
            AddPlatformInput input , [ScopedService] AppDBContext _ctx,
             [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken
            )
        {
            Platform platefrm = new Platform{ Name = input.NameOfPlatform};
            await _ctx.Platforms.AddAsync(platefrm , cancellationToken);
            await _ctx.SaveChangesAsync(cancellationToken);
            // notify the subscribers 
            await eventSender.SendAsync(nameof(Subscription.OnPlatFormAdded), platefrm, cancellationToken);
            return new AddPlatFormPayLoad(platefrm);
        }
    }
}
