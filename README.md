# Pdf Api

Code demonstrates several methods of generating one or more PDF files either as a consolidated PDF or as a ZIP file with individual files

|Endpoint|Parameters|Description|
|:--|:--|:--|
|`GET /static-pdf`|`generate` - number of sample documents to generate|Loads an existing PDF file into a zip file without using an intermediate file|
|`GET /dynamic-pdf`|`generate` - number of sample documents to generate|Generates one or more personalised documents using [iText](https://api.itextpdf.com/iText7/dotnet/7.2.1/) and zips them without using any intermediate files|
|`GET /dynamic-pdf-single`|`generate` - number of sample documents to generate|Generates a single PDF with one or more personalised documents using [iText](https://api.itextpdf.com/iText7/dotnet/7.2.1/) without using any intermediate files|

## Licences

This code uses iText7 to generate PDFs. It is available under a [AGPL licence](https://itextpdf.com/en/how-buy/agpl-license) for community use. Commercial licences are available for closed-source projects.
