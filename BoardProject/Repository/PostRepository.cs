using BoardProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardProject.Repository
{
    public class PostRepository : IPostRepository, IDisposable
    {
        private readonly PostDbContext _context;
        private bool _added = false;

        public PostRepository(PostDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 입력을 받은 데이터를 context에 저장, SavePostChanges를 콜해야 데이터베이스에 저장
        /// 입력에 성공하면 입력받은 model를 반환
        /// 실패하면 null반환 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Post AddPost(Post model)
        {
            if (_added) return null;
            _added = false;

            _context.Posts.Add(model);
            return model;
        }


        //post테이블에서 id로 검색
        //일치하는 id에 해당하는 행 삭제 
        public Post DeletePost(int id)
        {
            var item = _context.Posts.Find(id);
            if(item != null)
            {
                _context.Posts.Remove(item);
                return item;
            }

            return null;
        }

        //수정된 모델을 입력으로 받고 context에 저장
        //SavePostChanges를 콜해야 데이터베이스에 저장
        public Post EditPost(Post model)
        {
            _context.Entry(model).State = EntityState.Modified;
            return model;
        }

        //모든 Post행을 List로 반환 
        public async Task<List<Post>> GetPostAsync()
        {
            return await _context.Posts.Include(p => p.Comments).ToListAsync();
        }

        //id와 일치하는 post와 comment까지 반환 
        public Post GetPostById(int id)
        {
            return _context.Posts.Where(p => p.PostId == id)
                    .Include(p => p.Comments)
                    .First();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// 
        /// 수정된 데이터들을 데이터베이스에 저장 
        /// </summary>
        /// <returns></returns>
        public async Task SavePostChagesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<PagingResult<Post>> GetPagePostAsync(int pageIndex, int pageSize)
        {
            var totalRecords = await _context.Posts.CountAsync();
            var posts = await _context
                .Posts
                .OrderByDescending(m => m.PostId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Include(p => p.Comments)
                .ToListAsync();

            return new PagingResult<Post>(posts, totalRecords);
        }

        public async Task<List<Post>> GetMatchingContent(string word)
        {
            var pattern = "%" + word + "%";
            var matchingPosts = await _context.Posts.
                Where(post => EF.Functions.Like(post.Content, pattern)).OrderByDescending(post => post.PostId)
                .ToListAsync();

            return matchingPosts;
        }

        public async Task<List<Post>> GetMatchingTitle(string word)
        {
            var pattern = "%" + word + "%";
            var matchingPosts = await _context.Posts.
                Where(post => EF.Functions.Like(post.Title, pattern)).OrderByDescending(post => post.PostId)
                .ToListAsync();

            return matchingPosts;
        }

        public async Task<List<Post>> GetMatchingUser(string user)
        {
            var matchingPosts = await _context.Posts.
                Where(post => post.Author == user).OrderByDescending(post => post.PostId)
                .ToListAsync();

            return matchingPosts;
        }
    }
}
