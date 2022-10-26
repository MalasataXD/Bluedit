using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.LogicImpl;

public class PostLogicImpl : IPostLogic
{
    // # Fields
    private readonly IPostDao _postDao;
    private readonly IUserDao _userDao;
    
    // ¤ Constructor
    public PostLogicImpl(IPostDao postDao, IUserDao userDao)
    {
        _postDao = postDao;
        _userDao = userDao;
    }

    // ¤ Create mehtod
    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        // # Check if owner exist
        User? user = await _userDao.GetByUserNameAsync(dto.OwnerName);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerName} was not found!");
        }
        
        // # Validate DTO
        ValidatePost(dto);
        
        // # Create new post
        Post post = new Post(user, dto.Title, dto.Description) {DateTime = DateTime.Now.ToString()};
        Post created = await _postDao.CreateAsync(post);
        return created;
    }

    // ¤ Get method
    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IEnumerable<Post> posts =  await _postDao.GetAsync(searchParameters);
        return posts;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? found = await _postDao.GetByIdAsync(id);

        if (found == null)
        {
            throw new Exception($"Post with ID {id} not found!");
        }

        return found;
    }


    public async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await _postDao.GetByIdAsync(dto.PostId);

        if (existing == null)
        {
            throw new Exception($"Post with ID {dto.PostId} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await _userDao.GetByUserIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Tilte ?? existing.Title;
        string description = dto.Description ?? existing.Description;
    
        Post updated = new (userToUse, titleToUse,description)
        {
            PostId = existing.PostId,
            DateTime = DateTime.Now.ToString()
        };

        ValidatePost(updated);

        await _postDao.UpdateAsync(updated);
    }
    public async Task DeleteAsync(int id)
    {
        Post? post = await _postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }
        
        await _postDao.DeleteAsync(id);
    }
    

    // ¤ Validation of DTO/Post
    private void ValidatePost(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title))
        {
            throw new Exception("Title cannot be empty!");
        }
        else if (string.IsNullOrEmpty(dto.Description))
        {
            throw new Exception("Description cannot be empty!");
        }
        
    }
    private void ValidatePost(Post post)
    {
        if (string.IsNullOrEmpty(post.Title))
        {
            throw new Exception("Title cannot be empty!");
        }
        else if (string.IsNullOrEmpty(post.Description))
        {
            throw new Exception("Description cannot be empty!");
        }
        
    }
    
}