using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    // # Fields
    private readonly FileContext _context;

    // ¤ Constructor
    public PostFileDao(FileContext context)
    {
        _context = context;
    }
    
    // ¤ Create a new post
    public Task<Post> CreateAsync(Post post)
    {
        int postId = 1;
        // # Check if other posts exists and count them.
        if (_context.Posts.Any())
        {
            postId = _context.Posts.Max(u => u.PostId);
            postId++;
        }
        post.PostId = postId;
        
        // # Add post to list of posts.
        _context.Posts.Add(post);
        // # Save changes in database.
        _context.SaveChanges();
        return Task.FromResult(post);
    }

    public Task<Post?> GetByIdAsync(int postId)
    {
        Post? existing = _context.Posts.FirstOrDefault(post => post.PostId == postId);
        return Task.FromResult(existing);
    }
    
    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        // # Get the list of posts
        IEnumerable<Post> result = _context.Posts.AsEnumerable();

        // # Compare Username
        if (!string.IsNullOrEmpty(searchParameters.UserName))
        {
            result = result.Where(post => post.Owner.UserName.Contains(searchParameters.UserName,StringComparison.OrdinalIgnoreCase));
        }
        // # Compare OwnerId
        if (searchParameters.UserId != null)
        {
            result = result.Where(post => post.Owner.UserID == searchParameters.UserId);
        }
        
        // # Compare Title
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            result = result.Where(post =>
                post.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
        }
        
        

        return Task.FromResult(result);
    }

    public Task UpdateAsync(Post toUpdate)
    {
        Post? existing = _context.Posts.FirstOrDefault(post => post.PostId == toUpdate.PostId);
        if (existing == null)
        {
            throw new Exception($"Post with id {toUpdate.PostId} does not exist!");
        }
        _context.Posts.Remove(existing);
        _context.Posts.Add(toUpdate);
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int postId)
    {
        Post? existing = _context.Posts.FirstOrDefault(post => post.PostId == postId);
        if (existing == null)
        {
            throw new Exception($"Post with id {postId} does not exist!");
        }

        _context.Posts.Remove(existing); 
        _context.SaveChanges();
    
        return Task.CompletedTask;
    }
}