namespace audit.twitter
{
    using System.Threading.Tasks;

    using audit.twitter.DTO.Search;
    using audit.twitter.OAuth;

    public interface ITwitterClient
    {
        void Authenticate(OauthAuthorization authorization);

        Task<SearchResponse> GetHashTag(string hashtag);

        void Tweet(string message);
    }
}