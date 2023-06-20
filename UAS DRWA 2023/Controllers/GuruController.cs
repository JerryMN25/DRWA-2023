
using BookStoreApi.Models;
using BookStoreApi.Services;
using BookStoreApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuruController : ControllerBase
{
    /// <response code="400">If bad request</response>
    /// <response code="401">If unauthorized</response>
    /// <response code="404">If the item is not found</response>
    /// <response code="500">If the server error</response>

    private readonly GuruService _guruService;

    public GuruController(GuruService guruService) => _guruService = guruService;

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<Guru>> Get() =>
        await _guruService.GetAsync();



    [HttpGet("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Guru>> Get(string id)
    {
        var book = await _guruService.GetAsync(id);

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
    public async Task<IActionResult> Post(Guru newGuru)
    {
        await _guruService.CreateAsync(newGuru);

        return CreatedAtAction(nameof(Get), new { id = newGuru.Id }, newGuru);
    }

    [HttpPut("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(string id, Guru updatedGuru)
    {
        var book = await _guruService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedGuru.Id = book.Id;

        await _guruService.UpdateAsync(id, updatedGuru);

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
        var book = await _guruService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _guruService.RemoveAsync(id);

        return NoContent();
    }
}