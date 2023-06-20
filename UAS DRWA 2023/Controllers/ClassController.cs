
using BookStoreApi.Models;
using BookStoreApi.Services;
using BookStoreApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassController : ControllerBase
{
    /// <response code="400">If bad request</response>
    /// <response code="401">If unauthorized</response>
    /// <response code="404">If the item is not found</response>
    /// <response code="500">If the server error</response>

    private readonly ClassService _classService;

    public ClassController(ClassService classService) => _classService = classService;

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<Class>> Get() =>
        await _classService.GetAsync();



    [HttpGet("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Class>> Get(string id)
    {
        var book = await _classService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    [Authorize]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(Class newClass)
    {
        await _classService.CreateAsync(newClass);

        return CreatedAtAction(nameof(Get), new { id = newClass.Id }, newClass);
    }

    [HttpPut("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(string id, Class updatedClass)
    {
        var book = await _classService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedClass.Id = book.Id;

        await _classService.UpdateAsync(id, updatedClass);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _classService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _classService.RemoveAsync(id);

        return NoContent();
    }
}