using System.Text.Json;
using GeminiHubApi.DTOs;
using GeminiHubApi.Exceptions;
using Google.GenAI;
using Google.GenAI.Types;

public class GeminiService
{
    public Client client = new Client();

    public string basePath =  AppContext.BaseDirectory;

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

    public async Task<MaterialDataResDto> ExtractMaterialInfosFromPdfAsync(byte[] fileBytes, string mimeType)
    {
        if(mimeType != "application/pdf")
        {
            throw new InvalidFormatException();
        }

        string promptFilePath = Path.Combine(
            basePath,
            "Prompts",
            "ExtractMaterialInfosFromPdfPrompt.txt"
        );

        var prompt = System.IO.File.ReadAllText(promptFilePath);

        var res = await client.Models.GenerateContentAsync(
            model: "gemini-3.1-flash-lite",
            contents: new List<Content>
            {
                new Content
                {
                    Parts = new List<Part>
                    {
                        new Part 
                        {
                            InlineData = new Blob
                            {
                                Data = fileBytes,
                                MimeType = mimeType
                            }
                        },
                        new Part
                        {
                            Text = prompt
                        }
                    }
                }
            },
            config: new GenerateContentConfig
            {
                ResponseMimeType = "application/json"
            }
        );

        var dtoRes = JsonSerializer.Deserialize<MaterialDataResDto>(res.Text! , new JsonSerializerOptions
        {
           PropertyNameCaseInsensitive = true 
        });

        if(dtoRes?.RequiredMaterials!.Count == 0)
        {
            throw new NullException("This PDF file is not a Supply List");
        }

        return dtoRes!;
    }

}