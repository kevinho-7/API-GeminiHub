using GeminiHubApi.DTOs;
using GeminiHubApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/geminiHub")]
public class AskGeminiController : ControllerBase
{

    private readonly AskGeminiService _genMatCheckService;

    public AskGeminiController(AskGeminiService genMatCheckService)
    {
        _genMatCheckService = genMatCheckService;
    }

    [HttpPost("ask")]
    public async Task<ActionResult<AskGeminiResDto>> AskSomethingToGemini(AskGeminiReqDto prompt)
    {
        var res = await _genMatCheckService.AsktSomethingToGeminiAsync(prompt);

        return Ok(new
        {
            success = true,
            response = res
        });
    }

}