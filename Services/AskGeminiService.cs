using GeminiHubApi.DTOs;
using Microsoft.VisualBasic;

namespace GeminiHubApi.Services;

public class AskGeminiService
{
    private readonly GeminiService _geminiService;

    public AskGeminiService(GeminiService geminiService)
    {
        _geminiService = geminiService;
    }

    public async Task<AskGeminiResDto> AsktSomethingToGeminiAsync(AskGeminiReqDto prompt)
    {
        var res = await _geminiService.GenSomethingAsync(prompt);

        return res;
    }
};