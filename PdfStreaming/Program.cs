using System.IO.Compression;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapGet("/static-pdf", async (
    HttpContext context,
    int? generate,
    CancellationToken cancellationToken) =>
{
    var staticPdfPath = Path.Combine(app.Environment.ContentRootPath, "Documents", "SamplePDF.pdf");

    var docCount = generate ?? 1;

    context.Response.ContentType = "application/octet-stream";
    context.Response.Headers.Add("Content-Disposition", "attachment; filename=\"document.zip\"");

    using var archive = new ZipArchive(context.Response.BodyWriter.AsStream(), ZipArchiveMode.Create);

    for (var i = 1; i <= docCount; i++)
    {
        var filenameWithoutExtension = Path.GetFileNameWithoutExtension(staticPdfPath);
        var filename = $"{filenameWithoutExtension}_{i:0000000000}.pdf";
        var entry = archive.CreateEntry(filename);
        await using var entryStream = entry.Open();
        await using var fileStream = File.OpenRead(staticPdfPath);
        await fileStream.CopyToAsync(entryStream, cancellationToken);
    }
});

app.MapGet("/dynamic-pdf", async (
    HttpContext context,
    int? generate) =>
{
    var docCount = generate ?? 1;

    context.Response.ContentType = "application/octet-stream";
    context.Response.Headers.Add("Content-Disposition", "attachment; filename=\"document.zip\"");

    using var archive = new ZipArchive(context.Response.BodyWriter.AsStream(), ZipArchiveMode.Create);

    for (var i = 1; i <= docCount; i++)
    {
        var filename = $"dynamic-pdf_{i,0000000000}.pdf";
        var entry = archive.CreateEntry(filename);
        await using var entryStream = entry.Open();
        var pdfWriter = new PdfWriter(entryStream);
        var pdfDocument = new PdfDocument(pdfWriter);
        var document = new Document(pdfDocument);
        var header = new Paragraph($"Document {i,0000000000}")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

        document.Add(header);
        document.Close();
    }
});

app.MapGet("/dynamic-pdf-single", async (
    HttpContext context,
    int? generate) =>
{
    var docCount = generate ?? 1;

    context.Response.ContentType = "application/octet-stream";
    context.Response.Headers.Add("Content-Disposition", "attachment; filename=\"document.pdf\"");

    await using var pdfWriter = new PdfWriter(context.Response.BodyWriter.AsStream());
    var pdfDocument = new PdfDocument(pdfWriter);
    var document = new Document(pdfDocument);

    for (var i = 1; i <= docCount; i++)
    {
        if (i > 1) document.Add(new AreaBreak()); // add new page
        var para = new Paragraph($"Document {i,0000000000}")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

        document.Add(para);
    }

    document.Close();
});

app.Run();