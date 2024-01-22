// See https://aka.ms/new-console-template for more information

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using RazorLight;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using WordPdfConvert.Models;
using PageSize = PdfSharpCore.PageSize;

Console.WriteLine("Hello, World!");

var engine = new RazorLightEngineBuilder()
    // required to have a default RazorLightProject type,
    // but not required to create a template from string.
    .UseEmbeddedResourcesProject(typeof(Employee))
    .SetOperatingAssembly(typeof(Employee).Assembly)
    .UseMemoryCachingProvider()
    .Build();

string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template.cshtml");
var template = File.ReadAllText(filePath);

Employee model = new Employee
{
    Id = "miph",
    Name = "Pham Duc Minh",
    Title = "Advanced Software Engineer",
    ImageUrl = "https://pdminh.dev/images/me.jpg",
    Projects = new List<string>()
    {
        "Project 1",
        "Project 2",
        "Project 3"
    }
};

string html = await engine.CompileRenderStringAsync("employee-profile", template, model);
Console.WriteLine(html);

// PDF generate
PdfDocument pdfDocument = PdfGenerator.GeneratePdf(html, PageSize.A4);
await using var stream = File.Create("miph.pdf");
pdfDocument.Save(stream);

// Word generate
await using var docStream = File.Create("miph.docx");
using var package = WordprocessingDocument.Create(docStream, WordprocessingDocumentType.Document);
MainDocumentPart mainPart = package.MainDocumentPart;
if (mainPart == null)
{
    mainPart = package.AddMainDocumentPart();
    new Document(new Body()).Save(mainPart);
}

var converter = new HtmlConverter(mainPart)
{
    ConsiderDivAsParagraph = true
};
converter.ParseHtml(html);
mainPart.Document.Save();