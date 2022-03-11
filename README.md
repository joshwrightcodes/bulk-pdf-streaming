# Pdf Api

Code demonstrates several methods of generating one or more PDF files either as a consolidated PDF or as a ZIP file with individual files

|Endpoint|Parameters|Description|
|:--|:--|:--|
|`GET /static-pdf`|`generate` - number of sample documents to generate|Loads an existing PDF file into a zip file without using an intermediate file|
|`GET /static-s3`|`generate` - number of sample documents to generate|Loads an existing PDF file from S3 into a zip file without using an intermediate file|
|`GET /dynamic-pdf`|`generate` - number of sample documents to generate|Generates one or more personalised documents using [iText](https://api.itextpdf.com/iText7/dotnet/7.2.1/) and zips them without using any intermediate files|
|`GET /dynamic-pdf-single`|`generate` - number of sample documents to generate|Generates a single PDF with one or more personalised documents using [iText](https://api.itextpdf.com/iText7/dotnet/7.2.1/) without using any intermediate files|

## Configuring HTTPS with Docker & .NET 6

Its good practice to use HTTPS everywhere, private networks included.

To allow this project to run with HTTPS locally, you'll need to complete these steps:

1. Remove your existing development certificate and create a new one using the following`dotnet`
   commands:  
   
   **MacOS:**
   ```shell
   dotnet dev-certs https --clean
   dotnet dev-certs https --verbose --export-path ${HOME}/.aspnet/https/aspnetapp.pfx --password "SuperSecretPassword" --trust
   ```
   **Windows (PowerShell)**:
   ```shell
   dotnet dev-certs https --clean
   dotnet dev-certs https --verbose --export-path $env:USERPROFILE/.aspnet/https/aspnetapp.pfx --password "SuperSecretPassword" --trust
   ```
   
   Make sure to replace `SuperSecretPassword` with something secure.  

   You may get prompts to enter your password to trust the certificate depending on your OS.  

3. Add the certificate password into an `.env` file for docker to use in the variable `ASPNET_CERT_PASS`. make sure to exclude your `.env` file from
   being committed so your super secret password is not exposed.
4. Add your `.env` file to your docker compose command using `--env-file` if you have a non-default `.env` file name

## Configuring LocalStack for S3 emulation

LocalStack emulates the S3 connectivity for AWS and is automatically loaded when you run he included `docker-compose.yml` file.

LocalStack also allows you to set a pre-configured state for demonstration or testing. For example in this
project we are preloading a sample file to the path `s3://demo/a/folder/SamplePDF.pdf`. Additional steps can be configured and recorded
to the file in `/src/stubs/s3/data/recorded_api_calls.json`.

The LocalStack instance in this example has been configured with a single sample pdf.

## Getting Started - Rider

For a better debugging experience, start the PdfStreaming API in Rider and LocalStack in Docker

2. Start LocalStack by running the following Docker Compose command from `/src`.
   ```shell
   docker-compose -f "docker-compose.yml" -f "docker-compose.override.yml" --env-file ".env" up -d
   ```
   This will preload a single sample PDF to S3 as well
3. Start the PdfStreaming Api in Rider. This should expose the service at `https://localhost:7100`
4. The service has 4 sample endpoints which provide different examples of the PDF streaming process. They all expose a query
   string parameter of `generate` which allows you to specify how many documents to generate in the example. If not supplied
   the service defaults to `1`.
   1. `GET /static-pdf` retrieves one or more PDF files that have been generated ahead of time and are stored on the server or a file server
       the application can access.
   2. `GET /static-s3` retrieves one or more PDF files that have been generated ahead of time and are stored in a S3 bucket the
       API can access. For local demonstrations this uses LocalStack. You can configure the service to use a real instance of S3 by adjusting
       the application config in the `LocalStack` and `AWS` configs
   3. `GET /dynamic-pdf` generates one or more individual PDFs on the fly using iText7 and zips them up.
   4. `GET /dynamic-pdf-single` generates a single PDF with multiple individual documents inside it using iText7. This generates a smaller
       payload size than the other endpoints as it can deduplicate shared items like fonts and images which the other endpoints can't 

## Licences

### iText7

This code uses iText7 to generate PDFs. It is available under a [AGPL licence](https://itextpdf.com/en/how-buy/agpl-license) for community use. Commercial licences are available for closed-source projects.

### LocalStack

This code uses LocalStack Community Edition to emulate calls to AWS s3. LocalStack CE is available under a [Apache License 2.0](https://github.com/localstack/localstack/blob/master/LICENSE.txt).
Pro and Enterprise versions are available for purchase [here](https://localstack.cloud/pricing/).

