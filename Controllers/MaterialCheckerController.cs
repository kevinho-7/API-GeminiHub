using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/geminiHub")]
public class MaterialCheckerController : ControllerBase
{
    private readonly GeminiService _geminiService;

    public MaterialCheckerController(GeminiService geminiService)
    {
        _geminiService = geminiService;
    }

    [HttpPost("extractDataFromPdf")]
    public async Task<ActionResult<MaterialDataResDto>> ExtractDataFromPDf(IFormFile pdfFile)
    {
        var memoryStream = new MemoryStream();
        await pdfFile.CopyToAsync(memoryStream);
        byte[] fileBytes = memoryStream.ToArray();
        var mimeType = pdfFile.ContentType;
        

        var res = await _geminiService.ExtractMaterialInfosFromPdfAsync(fileBytes, mimeType);

        return Ok(new
        {
            success = true,
            response = res
        });
    }
}