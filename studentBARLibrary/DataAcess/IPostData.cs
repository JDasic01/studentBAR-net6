
namespace studentBARLibrary.DataAcess
{
    public interface IPostData
    {
        Task CreatePost(PostModel post);
        Task<List<PostModel>> GetAllPosts();
        Task<PostModel> GetPosts(string id);
        Task UpdatePost(PostModel post);
        Task UpvotePost(string postId, string userId);
    }
}