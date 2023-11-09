// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using DinkToPdf;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using PdfSharpCore.Pdf;
using RazorLight;
using VetCV.HtmlRendererCore.PdfSharpCore;
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


// string template = "Hello, @Model.Name. Welcome to RazorLight repository";

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
// PdfDocument pdf = PdfGenerator.GeneratePdf(html, PageSize.A4, 10);
// await using var stream = File.Create("miph.pdf");
// pdf.Save(stream);
Path.Combine("libwkhtmltox.dylib");
Path.Combine("libwkhtmltox.dll");
Path.Combine("libwkhtmltox.so");

var context = new CustomAssemblyLoadContext();
string projectRootFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

string path;
string runtimeArchitecture = RuntimeInformation.ProcessArchitecture.ToString().ToLower();

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    path = Path.Combine(projectRootFolder, "libwkhtmltox.dll");
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    path = Path.Combine(projectRootFolder, "libwkhtmltox.so");
else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    path = Path.Combine(projectRootFolder, "libwkhtmltox.dylib");
else
    throw new InvalidOperationException("Supported OS Platform not found");

context.LoadUnmanagedLibrary(path);


var pdfConverter = new BasicConverter(new PdfTools());

var doc = new HtmlToPdfDocument()
{
    GlobalSettings =
    {
        ColorMode = ColorMode.Color,
        Orientation = Orientation.Landscape,
        PaperSize = PaperKind.A4,
    },
    Objects =
    {
        new ObjectSettings()
        {
            PagesCount = true,
            HtmlContent = html,
            WebSettings = { DefaultEncoding = "utf-8" },
            HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
        }
    }
};

var data = pdfConverter.Convert(doc);
File.WriteAllBytes("miph.pdf", data);

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


internal class CustomAssemblyLoadContext : AssemblyLoadContext
{
    public IntPtr LoadUnmanagedLibrary(string absolutePath)
        => this.LoadUnmanagedDll(absolutePath);

    protected override Assembly Load(AssemblyName assemblyName)
    {
        return null;
    }

    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        => this.LoadUnmanagedDllFromPath(unmanagedDllName);
}