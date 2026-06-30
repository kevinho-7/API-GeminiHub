using GeminiHubApi.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/geminiHub")]
public class MaterialCheckerController : ControllerBase
{
    private readonly GeminiService _geminiService;
    private readonly GenPdfService _genPdfService;

    public MaterialCheckerController(GeminiService geminiService, GenPdfService genPdfService)
    {
        _geminiService = geminiService;
        _genPdfService = genPdfService;
    }

    // Extracting data from some Supply List pdf
    [HttpPost("extract-data-from-pdf")]
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

    // Generating a pdf
    [HttpPost("gen-pdf")]
    public async Task<ActionResult<MissingMaterialDataReqDto>> GenPdf()
    {
        _genPdfService.GenPdf();

        return Ok(new
        {
            success = true,
            response = "The Pdf file has been generated"
        });
    }

}

                     