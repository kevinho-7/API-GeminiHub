namespace GeminiHubApi.DTOs;

public class MissingMaterialDataReqDto
{
    public string? GradeAndYear {get; set;}
    public List<MissingMaterialItems>? RequiredMaterials {get; set;}
}

public class MissingMaterialItems
{
    public string? Topic {get; set;}
    public List<Material>? Materials {get; set;}
}

public class MissingMaterial
{
    public string? Title {get; set;}
    public string? Quantity {get; set;}
}
