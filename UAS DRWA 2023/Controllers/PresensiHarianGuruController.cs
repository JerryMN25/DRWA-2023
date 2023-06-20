
using BookStoreApi.Models;
using BookStoreApi.Services;
using BookStoreApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresensiHarianGuruController : ControllerBase
{
    /// <response code="400">If bad request</response>
    /// <response code="401">If unauthorized</response>
    /// <response code="404">If the item is not found</response>
    /// <response code="500">If the server error</response>

    private readonly PresensiHarianGuruService _presensiHarianGuruService;

    public PresensiHarianGuruController(PresensiHarianGuruService presensiHarianGuruService) => _presensiHarianGuruService = presensiHarianGuruService;

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<PresensiHarianGuru>> Get() =>
        await _presensiHarianGuruService.GetAsync();



    [HttpGet("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PresensiHarianGuru>> Get(string id)
    {
        var book = await _presensiHarianGuruService.GetAsync(id);

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
    public async Task<IActionResult> Post(PresensiHarianGuru newPresensiHarianGuru)
    {
        await _presensiHarianGuruService.CreateAsync(newPresensiHarianGuru);

        return CreatedAtAction(nameof(Get), new { id = newPresensiHarianGuru.Id }, newPresensiHarianGuru);
    }

    [HttpPut("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(string id, PresensiHarianGuru updatedPresensiHarianGuru)
    {
        var book = await _presensiHarianGuruService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedPresensiHarianGuru.Id = book.Id;

        await _presensiHarianGuruService.UpdateAsync(id, updatedPresensiHarianGuru);

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
        var book = await _presensiHarianGuruService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _presensiHarianGuruService.RemoveAsync(id);

        return NoContent();
    }
}