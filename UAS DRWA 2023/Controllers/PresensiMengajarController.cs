
using BookStoreApi.Models;
using BookStoreApi.Services;
using BookStoreApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresensiMengajarController : ControllerBase
{
    /// <response code="400">If bad request</response>
    /// <response code="401">If unauthorized</response>
    /// <response code="404">If the item is not found</response>
    /// <response code="500">If the server error</response>

    private readonly PresensiMengajarService _presensiMengajarService;

    public PresensiMengajarController(PresensiMengajarService presensiMengajarService) => _presensiMengajarService = presensiMengajarService;

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<PresensiMengajar>> Get() =>
        await _presensiMengajarService.GetAsync();



    [HttpGet("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PresensiMengajar>> Get(string id)
    {
        var book = await _presensiMengajarService.GetAsync(id);

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
    public async Task<IActionResult> Post(PresensiMengajar newPresensiMengajar)
    {
        await _presensiMengajarService.CreateAsync(newPresensiMengajar);

        return CreatedAtAction(nameof(Get), new { id = newPresensiMengajar.Id }, newPresensiMengajar);
    }

    [HttpPut("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(string id, PresensiMengajar updatedPresensiMengajar)
    {
        var book = await _presensiMengajarService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedPresensiMengajar.Id = book.Id;

        await _presensiMengajarService.UpdateAsync(id, updatedPresensiMengajar);

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
        var book = await _presensiMengajarService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _presensiMengajarService.RemoveAsync(id);

        return NoContent();
    }
}