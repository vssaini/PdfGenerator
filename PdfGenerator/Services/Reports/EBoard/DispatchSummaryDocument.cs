﻿using PdfGenerator.Components.EBoard;
using PdfGenerator.Models.Reports.EBoard;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Services.Reports.EBoard;

public class DispatchSumDocument(DispatchSumModel model) : IDocument
{
    private const int DefaultFontSize = 9;
    private const string DefaultFont = "Arial";
    private const string ArialFont = "Arial";

    public DocumentMetadata GetMetadata()
    {
        return new DocumentMetadata
        {
            Title = model.Header.Title,
            Author = model.Footer.CurrentUserName,
            Subject = $"{model.Header.Title} {model.Header.DateRange}"
        };
    }

    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        var pageStyle = TextStyle.Default
            .FontSize(DefaultFontSize)
            .FontFamily(DefaultFont)
            .FontColor(Colors.Grey.Darken4);

        const float margin = 0.25f;

        container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(margin, Unit.Inch);
                page.DefaultTextStyle(pageStyle);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
    }

    private void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default
            .FontSize(18)
            .FontFamily(ArialFont)
            .SemiBold()
            .Italic();

        var dateStyle = TextStyle.Default
            .FontSize(12)
            .FontFamily(ArialFont)
            .SemiBold();

        container.Column(column =>
        {
            column.Item().Text(text => text.Span(model.Header.Title).Style(titleStyle));
            column.Item().BorderBottom(1).PaddingVertical(2).PaddingBottom(5).Text(text => text.Span(model.Header.DateRange).Style(dateStyle));
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            var lastItem = model.DispatchSumResponses.LastOrDefault();
            foreach (var disSumResp in model.DispatchSumResponses)
            {
                ComposePagesDateWise(disSumResp, column);
                ComposeEmployerWiseDispatchTable(disSumResp, column);

                if (disSumResp != lastItem)
                    column.Item().PageBreak();
            }
        });
    }

    private static void ComposePagesDateWise(DispatchSumResponse disSumResp, ColumnDescriptor column)
    {
        // Header date
        var date = disSumResp.Date.ToString("dddd, MMM dd, yyyy");
        column.Item()
            .PaddingTop(12)
            .PaddingLeft(25)
            .Text(date)
            .FontSize(12)
            .SemiBold();

        // Summary rows
        foreach (var empRow in disSumResp.DispatchSumEmpRows)
        {
            column.Item().Component(new ReportComponent(empRow.Employer, empRow.SummaryRows));
        }

        // Footer total
        var totalDispatchesOnDate = disSumResp.DispatchSumEmpRows
            .Sum(e => e.SummaryRows.Sum(x => x.DispatchCount));
        column.Item()
            .PaddingTop(3)
            .PaddingLeft(75)
            .Text($"{date} - {totalDispatchesOnDate}")
            .FontSize(9)
            .Bold();
    }

    private static void ComposeEmployerWiseDispatchTable(DispatchSumResponse disSumResp, ColumnDescriptor column)

    {
        var employerDispatches = disSumResp.DispatchSumEmpRows
            .Select(r => new EmployerDispatch
            {
                EmployerName = r.Employer,
                TotalDispatch = r.SummaryRows.Sum(s => s.DispatchCount)
            })
            .ToList();

        column.Item().Component(new SummaryTableComponent(employerDispatches));
    }

    private void ComposeFooter(IContainer container)
    {
        var fontStyle = TextStyle.Default
            .FontSize(8)
            .FontFamily(ArialFont);

        container.PaddingVertical(20).BorderTop(1).PaddingBottom(1).Row(row =>
        {
            row.RelativeItem().PaddingTop(5).Column(c =>
            {
                c.Item().Text(model.Footer.CurrentUserName).Style(fontStyle);
            });

            row.RelativeItem().PaddingTop(5).Column(c =>
            {
                c.Item().Text(model.Footer.CurrentDateTime).Style(fontStyle);
            });

            row.RelativeItem().PaddingLeft(50).PaddingTop(5).Column(c =>
            {
                c.Item().Text(text =>
                {
                    text.DefaultTextStyle(fontStyle);
                    text.Span("Page ");
                    text.CurrentPageNumber();
                    text.Span(" of ");
                    text.TotalPages();
                });
            });

            row.RelativeItem().PaddingTop(5).AlignRight().Column(c =>
            {
                c.Item().Text(model.Footer.PropertyMessage).Style(fontStyle);
            });
        });
    }
}