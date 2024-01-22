using System.Reflection;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace WordWithOpenXml;

public class DocumentModify
{
    public async Task Doit()
    {
        var template = "employee-profile.docx";
        await using var templateStream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream($"{GetType().Namespace}.Resources.{template}");

        try
        {
            var memoryStream = new MemoryStream();
            templateStream.CopyTo(memoryStream);

            var wordprocessingDocument = WordprocessingDocument.Open(memoryStream, true);
            var document = wordprocessingDocument.MainDocumentPart.Document;

            IEnumerable<TableProperties> tableProperties =
                document.Descendants<TableProperties>().Where(tp => tp.TableCaption != null).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        // var tables = body.Descendants<Table>().Where(table => table.TableCaption != null);.ToList();

        // foreach (TableProperties tProp in tableProperties)
        // {
        //     if (tProp.TableCaption.Val.Equals("myCaption")) // see comment, this is actually StringValue
        //     {
        //         // do something for table with myCaption
        //         Table table = (Table)tProp.Parent;
        //     }
        // }
    }


}