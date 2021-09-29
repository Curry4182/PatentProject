using BoardProject.Models;
using BoardProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardProject.Repository
{
    public interface IPostRepository : IDisposable
    {
        public Post AddPost(Post model);

        public Task<List<Post>> GetPostAsync();

        public Post GetPostById(int id);

        public Post EditPost(Post model);

        public Post DeletePost(int id);

        public Task<PagingResult<Post>> GetPagePostAsync(int pageIndex, int pageSize);

        public Task SavePostChagesAsync();

        public Task<List<Post>> GetMatchingTitle(string word);

        public Task<List<Post>> GetMatchingContent(string word);

        public Task<List<Post>> GetMatchingUser(string word);
    }
}
