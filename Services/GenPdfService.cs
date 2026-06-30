using GeminiHubApi.DTOs;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class GenPdfService
{
    public byte[] GenPdf(MissingMaterialDataReqDto dto)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var doc = Document.Create(container =>
        {
            container.Page(page =>
            {   
                // Default page settings
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));
                page.DefaultTextStyle(x => x.FontFamily("Arial"));

                // Pdf Header
                page.Header()
                    .Padding(10)
                    .Row(row =>
                    {
                        row.ConstantItem(120)
                            .Image("./Images/logo_santanna.png");

                        row.RelativeItem(300)
                            .Padding(15)
                            .Column(col =>
                            {
                                col.Item()
                                    .Text($"{dto.GradeAndYear}")
                                    .FontSize(18)
                                    .LineHeight(2);
                                    
                                col.Item()
                                    .Text("Lista de Materiais")
                                    .FontSize(28)
                                    .Bold();

                                col.Item()
                                    .Text("Supply List")
                                    .FontSize(20)
                                    .Italic();
                            });
                    });

                // Pdf Body
                page.Content()
                    .BorderTop(3)
                    .BorderColor("#00368a")
                    .Column(col =>
                    {
                        col.Item()
                            .PaddingTop(25)
                            .Text("ITENS FALTANTES")
                            .FontSize(24)
                            .Bold()
                            .LineHeight(1)
                            .AlignCenter();

                        col.Item()
                            .Text("MISSING ITEMS")    
                            .FontSize(16)
                            .Bold()
                            .Italic()
                            .AlignCenter();

                        // Table start // 
                        foreach(var data in dto.RequiredMaterials!)
                        {
                            col.Item()
                                .Padding(15)    
                                .Table(table =>
                                {
                                    table.ColumnsDefinition(col =>
                                    {
                                        col.RelativeColumn();
                                        col.ConstantColumn(150);
                                    });

                                    table.Cell()
                                        .ColumnSpan(2)
                                        .Border(1)
                                        .Background(Colors.Grey.Lighten2)
                                        .Padding(8)
                                        .AlignCenter()
                                        .Text($"{data.Topic}")
                                        .FontSize(18)
                                        .Bold();

                                    table.Cell()
                                        .Border(1)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text("Título / Title")
                                        .FontSize(16)
                                        .Bold();

                                    table.Cell()
                                        .Border(1)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text("Qtd / Qty")
                                        .FontSize(16)
                                        .Bold();
                                    
                                    foreach(var material in data.Materials!)
                                    {
                                        table.Cell()
                                            .Border(1)
                                            .Padding(5)
                                            .AlignCenter()
                                            .Text($"{material.Title}")
                                            .FontSize(16);
                                        
                                        table.Cell()
                                            .Border(1)
                                            .Padding(5)
                                            .AlignCenter()
                                            .Text($"{material.Quantity}")
                                            .FontSize(16);
                                    }
                                });
                            // Table End //
                        }

                        col.Item()
                            .Padding(30)
                            .Text("Obs: ")
                            .FontSize(16)
                            .Bold()
                            .Italic();
                    });  
            });
        });

        //doc.ShowInCompanion();
        return doc.GeneratePdf();
    }
}