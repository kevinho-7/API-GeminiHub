using GeminiHubApi.DTOs;
using Google.GenAI;

public class GeminiService
{
    public Client client = new Client();

    public async Task<AskGeminiResDto> GenSomethingAsync(AskGeminiReqDto prompt)
    {
        var text = prompt.Prompt;

        var geminiReq = await client.Models.GenerateContentAsync(
            model: "gemini-3.1-flash-lite", contents: text!
        );

        var geminiRes = new AskGeminiResDto
        {
            Prompt = geminiReq.Candidates![0].Content!.Parts![0].Text
        };

        return geminiRes;
    }
}