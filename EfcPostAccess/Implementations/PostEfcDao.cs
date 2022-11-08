using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcPostAccess.Implementations;

public class PostEfcDao : IPostDao
{
    private readonly PostContext _context;
    
    // # Constructor
    public PostEfcDao(PostContext context)
    {
        _context = context;
    }
    
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Post?> GetByIdAsync(int postId)
    {
        Post? found = await _context.Posts.FindAsync(postId);
        return found;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> query = _context.Posts.Include(post => post.Owner).AsQueryable();

        // ¤ Username
        if (!string.IsNullOrEmpty(searchParameters.UserName))
        {
            query = query.Where(post => post.Owner.UserName.ToLower().Equals(searchParameters.UserName.ToLower()));
        }
        
        // ¤ UserId
        if (searchParameters.UserId != null)
        {
            query = query.Where(post => post.Owner.UserID == searchParameters.UserId);
        }
        
        // ¤ Title
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            query = query.Where(post => post.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
        }

        List<Post> result = await query.ToListAsync();
        return result;
    }

    public async Task UpdateAsync(Post toUpdate)
    {
        _context.ChangeTracker.Clear();
        _context.Posts.Update(toUpdate);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int postId)
    {
        Post? existing = await GetByIdAsync(postId);
        if (existing == null)
        {
            throw new Exception($"Todo with id {postId} not found!");
        }

        _context.Posts.Remove(existing);
        await _context.SaveChangesAsync();
    }
}