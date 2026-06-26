using GeminiHubApi.DTOs;
using GeminiHubApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/geminiHub")]
public class AskGeminiController : ControllerBase
{

    private readonly AskGeminiService _askGeminiService;

    public AskGeminiController(AskGeminiService askGeminiService)
    {
        _askGeminiService = askGeminiService;
    }

    [HttpPost("ask")]
    public async Task<ActionResult<AskGeminiResDto>> AskSomethingToGemini(AskGeminiReqDto prompt)
    {
        var res = await _askGeminiService.AsktSomethingToGeminiAsync(prompt);

        return Ok(new
        {
            success = true,
            response = res
        });
    }

}