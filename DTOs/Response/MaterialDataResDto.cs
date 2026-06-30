public class MaterialDataResDto
{
    public string? GradeAndYear {get; set;}
    public string? Obs {get; set;}
    public List<MaterialItems>? RequiredMaterials {get; set;}
}

public class MaterialItems
{
    public string? Topic {get; set;}
    public List<Material>? Materials {get; set;}
}

public class Material
{
    public string? Title {get; set;}
    public string? Quantity {get; set;}
}
