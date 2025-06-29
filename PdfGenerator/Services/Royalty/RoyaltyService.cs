﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Royalty;
using PdfGenerator.Models.Royalty;
using PdfGenerator.Queries;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;

namespace PdfGenerator.Services.Royalty;

public class RoyaltyService(ILogger<RoyaltyService> logger, ISender sender, IConfiguration config)
    : IRoyaltyDocService, IPdfService
{
    public async Task GenerateRoyaltyDocAsync(RoyaltyFilter filter)
    {
        logger.LogInformation("Generating Royalty document for {AccountNumber} and {Year}", filter.AccountNumber, filter.Year);

        var model = await sender.Send(new GetRoyaltyQuery(filter));

        var fontSize = config.GetValue<int>("Pdf:FontSize");
        var document = new RoyaltyDocument(model, fontSize);

        var showInPreviewer = config.GetValue<bool>("Pdf:ShowInPreviewer");
        if (showInPreviewer)
            await document.ShowInPreviewerAsync();
        else
            GeneratePdf(document, document.FileName);
    }

    public void GeneratePdf(IDocument document, string filePath)
    {
        logger.LogInformation("Generating PDF at {PdfFilePath}", filePath);
        document.GeneratePdf(filePath);

        logger.LogInformation("Opening PDF at {PdfFilePath}", filePath);
        Process.Start("explorer.exe", filePath);
    }
}