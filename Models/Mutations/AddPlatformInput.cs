namespace GraphQLOne.Models.Mutations
{
    public record AddPlatformInput(string NameOfPlatform);
    public record AddCommanInput(string HowTo , string CMD , int platformID);
}