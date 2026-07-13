namespace GeminiHubApi.DTOs;

public class MissingMaterialDataReqDto
{
    public string? StudentName {get; set;}
    public string? GradeAndYear {get; set;}
    public string? Obs {get; set;}
    public List<MissingMaterialItems>? RequiredMaterials {get; set;}
}

public class MissingMaterialItems
{
    public string? Topic {get; set;}
    public List<MissingMaterial>? Materials {get; set;}
}

public class MissingMaterial
{
    public bool? IsMissing {get; set;}
    public string? Title {get; set;}
    public string? MissingQty {get; set;}
}
