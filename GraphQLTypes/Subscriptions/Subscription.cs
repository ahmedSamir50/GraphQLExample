using GraphQLOne.Models;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLOne.GraphQLTypes.Subscriptions
{
    public class Subscription
    {
        [Subscribe]
        [Topic(nameof(Subscription.OnPlatFormAdded))]
        public Platform OnPlatFormAdded([EventMessage] Platform plateform) => plateform;

        [Subscribe]
        [Topic(nameof(Subscription.OnCommandAdded))]
        public Command OnCommandAdded([EventMessage] Command command) => command;
    }
}
