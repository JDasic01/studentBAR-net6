using Microsoft.Extensions.Caching.Memory;

namespace studentBARLibrary.DataAcess;

public class MongoPostData : IPostData
{
    private readonly IDbConnection _db;
    private readonly IMemoryCache _cache;
    private readonly IUserData _userData;
    private readonly IMongoCollection<PostModel> _posts;
    private const string CacheName = "postData";

    public MongoPostData(IDbConnection db, IUserData userData, IMemoryCache cache)
    {
        _db = db;
        _userData = userData;
        _cache = cache;
        _posts = db.PostCollection;
    }

    public async Task<List<PostModel>> GetAllPosts()
    {
        var output = _cache.Get<List<PostModel>>(CacheName);
        if (output is null)
        {
            var results = await _posts.FindAsync(s => s.Archived == false);
            output = results.ToList();

            _cache.Set(CacheName, output, TimeSpan.FromMinutes(1));
        }
        return output;
    }

    public async Task<PostModel> GetPosts(string id)
    {
        var result = await _posts.FindAsync(s => s.Id == id);
        return result.FirstOrDefault();
    }

    public async Task UpdatePost(PostModel post)
    {
        await _posts.ReplaceOneAsync(s => s.Id == post.Id, post);
        _cache.Remove(CacheName);
    }

    public async Task UpvotePost(string postId, string userId)
    {
        var client = _db.Client;

        using var session = await client.StartSessionAsync();
        session.StartTransaction();
        try
        {
            var db = client.GetDatabase(_db.DbName);
            var postsInTransactions = db.GetCollection<PostModel>(_db.PostCollectionName);
            var post = (await postsInTransactions.FindAsync(s => s.Id == postId)).First();

            bool isUpvote = post.UserVotes.Add(userId);

            if (isUpvote == false)
            {
                post.UserVotes.Remove(userId);
            }

            await postsInTransactions.ReplaceOneAsync(s => s.Id == postId, post);
            var usersInTransaction = db.GetCollection<UserModel>(_db.UserCollectionName);
            var user = await _userData.GetUser(post.Author.Id);

            if (isUpvote)
            {
                user.VotedOnPosts.Add(new BasicPostModel(post));
            }
            else
            {
                var postToRemove = user.VotedOnPosts.Where(s => s.Id == postId).First();
                user.VotedOnPosts.Remove(postToRemove);
            }
            await usersInTransaction.ReplaceOneAsync(u => u.Id == userId, user);
            await session.CommitTransactionAsync();

            _cache.Remove(CacheName);
        }
        catch (Exception ex)
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }

    public async Task CreatePost(PostModel post)
    {
        var client = _db.Client;
        using var session = await client.StartSessionAsync();

        session.StartTransaction();

        try
        {
            var db = client.GetDatabase(_db.DbName);
            var postsInTransactions = db.GetCollection<PostModel>(_db.PostCollectionName);
            await postsInTransactions.InsertOneAsync(post);

            var usersInTransactions = db.GetCollection<UserModel>(_db.UserCollectionName);
            var user = await _userData.GetUser(post.Author.Id);
            user.AuthoredPosts.Add(new BasicPostModel(post));
            await usersInTransactions.ReplaceOneAsync(u => u.Id == user.Id, user);

            await session.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }
}
