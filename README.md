# Pdf Api

Code demonstrates several methods of generating one or more PDF files either as a consolidated PDF or as a ZIP file with individual files

|Endpoint|Parameters|Description|
|:--|:--|:--|
|`GET /static-pdf`|`generate` - number of sample documents to generate|Loads an existing PDF file into a zip file without using an intermediate file|
|`GET /dynamic-pdf`|`generate` - number of sample documents to generate|Generates one or more personalised documents using [iText](https://api.itextpdf.com/iText7/dotnet/7.2.1/) and zips them without using any intermediate files|
|`GET /dynamic-pdf-single`|`generate` - number of sample documents to generate|Generates a single PDF with one or more personalised documents using [iText](https://api.itextpdf.com/iText7/dotnet/7.2.1/) without using any intermediate files|

## Getting Started with LocalStack

LocalStack emulates the S3 connectivity for AWS and is automatically loaded when you run the included `docker-compose.yml` file.

1. Open a new terminal window and change to the Solution Directory which contains the `docker-compose.yml` file
2. Run the following command to spin up the docker environment `docker compose up` (Docker Compose v2) or `docker-compose up` (Docker Compose v1).
3. Prepare the S3 bucket:
   1. asdfasdfasdf
   2. asdfasdfasdf
   3. asdfasdfasd
4. Call the demo api endpoints as required at the posted address
5. Run the following command to pull down the docker environment `docker compose down` or `docker-compose down`

## Licences

This code uses iText7 to generate PDFs. It is available under a [AGPL licence](https://itextpdf.com/en/how-buy/agpl-license) for community use. Commercial licences are available for closed-source projects.
