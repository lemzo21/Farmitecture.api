using AutoMapper;
using Farmitecture.Api.Data;
using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Data.Models;
using Farmitecture.Api.Services.Interfaces;
using Farmitecture.Api.Utils;
using Microsoft.EntityFrameworkCore;

namespace Farmitecture.Api.Services.Providers;

public class BlogPostService(ApplicationDbContext dbContext, ILogger<BlogPostService> logger, IMapper mapper)
    : IBlogPostService
{
    public async Task<ApiResponse<BlogpostDto>> AddBlogPostAsync(CreateBlogPostRequest blogpost)
{
    try
    {
        logger.LogInformation("Adding blog post");

        var newBlogpost = new Blogpost
        {
            Id = Guid.NewGuid(),
            Topic = blogpost.Topic,
            Content = blogpost.Content,
            Category = blogpost.Category,
            Status = blogpost.Status,
            MetaDescription = blogpost.MetaDescription,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.Blogposts.Add(newBlogpost);
        await dbContext.SaveChangesAsync();

        return new ApiResponse<BlogpostDto>
        {
            Data = mapper.Map<BlogpostDto>(newBlogpost),
            Message = "Blog post added successfully",
            Code = StatusCodes.Status201Created,
            IsSuccessful = true
        };
    }
    catch (Exception e)
    {
        logger.LogError(e, "Error adding blog post");
        return new ApiResponse<BlogpostDto>
        {
            Code = StatusCodes.Status500InternalServerError,
            Message = "Error adding blog post",
            IsSuccessful = false
        };
    }
}

        public async Task<ApiResponse<BlogpostDto>> GetBlogPostByIdAsync(Guid id)
        {
            try
            {
                logger.LogInformation("Retrieving blog post");
                
                var blogpost = await dbContext.Blogposts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (blogpost == null)
                {
                    logger.LogDebug("Blog post not found");

                    return new ApiResponse<BlogpostDto>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Blog post not found",
                        IsSuccessful = false
                    };
                }

                return new ApiResponse<BlogpostDto>
                {
                    Data = mapper.Map<BlogpostDto>(blogpost),
                    Message = "Blog post retrieved successfully",
                    Code = StatusCodes.Status200OK,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error retrieving blog post");
              
                return new ApiResponse<BlogpostDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving blog post",
                    IsSuccessful = false
                };
            }
        }

        public async Task<ApiResponse<PagedResult<IEnumerable<BlogpostDto>>>> GetBlogPostsAsync(BaseFilter filter)
        {
            try
            {
                logger.LogInformation("Retrieving blog posts");

                var query = dbContext.Blogposts.AsNoTracking();

                var totalCount = await query.CountAsync();

                logger.LogInformation("Total applications found {TotalCount}", totalCount);

                var res = await PaginationHelper.GetPaginatedResultAsync(
                    query,
                    filter.Page,
                    filter.PageSize,
                    mapper.Map<BlogpostDto>
                );

                return new ApiResponse<PagedResult<IEnumerable<BlogpostDto>>>
                {
                    Data = res,
                    Message = "Blog posts retrieved successfully",
                    Code = StatusCodes.Status200OK,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error retrieving blog posts");

                return new ApiResponse<PagedResult<IEnumerable<BlogpostDto>>>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving blog posts",
                    IsSuccessful = false
                };
            }
        }
        
        public async Task<ApiResponse<string>> DeleteBlogPostAsync(Guid id)
        {
            try
            {
                var blogpost = await dbContext.Blogposts.FindAsync(id);
                if (blogpost == null)
                {
                    return new ApiResponse<string>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Blog post not found",
                        IsSuccessful = false
                    };
                }

                dbContext.Blogposts.Remove(blogpost);
                await dbContext.SaveChangesAsync();

                return new ApiResponse<string>
                {
                    Message = "Blog post deleted successfully",
                    Code = StatusCodes.Status200OK,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error deleting blog post");
                return new ApiResponse<string>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Error deleting blog post",
                    IsSuccessful = false
                };
            }
        }

        public async Task<ApiResponse<BlogpostDto>> UpdateBlogPostAsync(Guid id, CreateBlogPostRequest request)
        {
            try
            {
                var blogpost = await dbContext.Blogposts.FindAsync(id);
                if (blogpost == null)
                {
                    return new ApiResponse<BlogpostDto>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Blog post not found",
                        IsSuccessful = false
                    };
                }

                blogpost.Topic = request.Topic;
                blogpost.Content = request.Content;
                blogpost.Category = request.Category;
                blogpost.Status = request.Status;
                blogpost.MetaDescription = request.MetaDescription;
                blogpost.UpdatedAt = DateTime.UtcNow;

                dbContext.Blogposts.Update(blogpost);
                await dbContext.SaveChangesAsync();

                return new ApiResponse<BlogpostDto>
                {
                    Data = mapper.Map<BlogpostDto>(blogpost),
                    Message = "Blog post updated successfully",
                    Code = StatusCodes.Status200OK,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error updating blog post");
                return new ApiResponse<BlogpostDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Error updating blog post",
                    IsSuccessful = false
                };
            }
        }
    }