using GraphQL.Server.Ui.Voyager;
using GraphQLOne.Data;
using GraphQLOne.GraphQLTypes;
using GraphQLOne.GraphQLTypes.Mutations;
using GraphQLOne.GraphQLTypes.Subscriptions;
using GraphQLOne.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace GraphQLOne
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration _config)
        {
            configuration = _config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string path = Directory.GetCurrentDirectory();
            // instead of AddDbContext for the multi threading / concurrent GraphQL recuests
            services.AddPooledDbContextFactory<AppDBContext>((serviceProvider, optBuilder) =>
            {
                optBuilder.UseSqlite(configuration.GetConnectionString("sqllite").Replace("|DataDirectory|", path));
                //optBuilder.UseInternalServiceProvider(serviceProvider);

            });
            services
                    .AddGraphQLServer()
                    .AddQueryType<QLQuery>()
                    .AddMutationType<Mutation>()
                    .AddSubscriptionType<Subscription>()
                    .AddType<PlatformType>()
                    .AddFiltering()
                    .AddSorting()
                    // allow to query a child object
                    .AddProjections()
                    .AddInMemorySubscriptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // for the GraphQL Subscriptions 
            app.UseWebSockets();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGraphQL();
            });
            app.UseGraphQLVoyager(new VoyagerOptions { 
                GraphQLEndPoint = "/graphql"
            },"/ql/voyager");
        }
    }
}
