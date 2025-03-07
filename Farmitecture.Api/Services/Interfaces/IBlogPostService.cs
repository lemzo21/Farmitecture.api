using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Data.Models;

namespace Farmitecture.Api.Services.Interfaces;

public interface IBlogPostService
{
    Task<ApiResponse<BlogpostDto>> AddBlogPostAsync(CreateBlogPostRequest blogpost);
    
    Task<ApiResponse<BlogpostDto>> GetBlogPostByIdAsync(Guid id);

    Task<ApiResponse<PagedResult<IEnumerable<BlogpostDto>>>> GetBlogPostsAsync(BaseFilter filter);

    Task<ApiResponse<string>> DeleteBlogPostAsync(Guid id);
    Task<ApiResponse<BlogpostDto>> UpdateBlogPostAsync(Guid id, CreateBlogPostRequest request);
}