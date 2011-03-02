namespace Restbucks.Service.Representations
{
    public static class RestbucksRelationExtensions
    {
        public static string ToRelUri(this RestbucksRelation relation)
        {
            return RepresentationBase.RestbucksRelationsUri + "/" + relation.ToString().ToLowerInvariant();
        }
    }
}