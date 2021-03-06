using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id);
        Post GetUserPostById(int id, int userProfileId);
        List<Post> GetAllPostsByUser(int userProfileId);
        void DeletePost(int PostId);
        Post GetPostById(int id);
        void UpdatePost(Post post);
    }
}