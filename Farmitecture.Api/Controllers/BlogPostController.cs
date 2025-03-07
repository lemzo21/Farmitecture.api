using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Models;
using Farmitecture.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farmitecture.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogPostsController(IBlogPostService blogPostService) : ControllerBase
{
    /// <summary>
    /// Adds a new blog post.
    /// </summary>
    /// <param name="blogpost">Blog post request object</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<BlogpostDto>))]
    public async Task<IActionResult> AddBlogPost([FromBody] CreateBlogPostRequest blogpost)
    {
        var result = await blogPostService.AddBlogPostAsync(blogpost);
        return Ok(result);;
    }

    /// <summary>
    /// Retrieves a blog post by ID.
    /// </summary>
    /// <param name="id">Blog post ID</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<BlogpostDto>))]
    public async Task<IActionResult> GetBlogPostById(Guid id)
    {
        var result = await blogPostService.GetBlogPostByIdAsync(id);
        
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a paginated list of blog posts.
    /// </summary>
    /// <param name="filter">Page index</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PagedResult<IEnumerable<BlogpostDto>>>))]
    public async Task<IActionResult> GetBlogPosts([FromQuery] BaseFilter filter)
    {
        var result = await blogPostService.GetBlogPostsAsync(filter);
        return Ok(result);
    }
    
    /// <summary>
    /// Deletes a blog post by ID.
    /// </summary>
    /// <param name="id">Blog post ID</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
    public async Task<IActionResult> DeleteBlogPost(Guid id)
    {
        var result = await blogPostService.DeleteBlogPostAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Updates a blog post by ID.
    /// </summary>
    /// <param name="id">Blog post ID</param>
    /// <param name="request">Update blog post request object</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<BlogpostDto>))]
    public async Task<IActionResult> UpdateBlogPost(Guid id, [FromBody] CreateBlogPostRequest request)
    {
        var result = await blogPostService.UpdateBlogPostAsync(id, request);
        return Ok(result);
    }
}