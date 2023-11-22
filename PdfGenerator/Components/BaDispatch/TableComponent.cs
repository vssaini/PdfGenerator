﻿using PdfGenerator.Models.Reports.BaDispatch;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.BaDispatch
{
    internal class TableComponent : IComponent
    {
        private readonly List<DispatchRow> _dispatches;

        public TableComponent(List<DispatchRow> dispatches)
        {
            _dispatches = dispatches;
        }

        public void Compose(IContainer container)
        {
            container.Element(ComposeTable);
        }

        private void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(.3f,Unit.Inch);
                    columns.ConstantColumn(.7f, Unit.Inch);
                    columns.ConstantColumn(.7f, Unit.Inch);
                    columns.ConstantColumn(2f, Unit.Inch);
                    columns.ConstantColumn(.7f, Unit.Inch);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("");
                    header.Cell().Element(CellStyle).Text("Report"); 
                    header.Cell().Element(CellStyle).Text("Skill");
                    header.Cell().Element(CellStyle).Text("Worker Name");
                    header.Cell().Element(CellStyle).Text("ID No.");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("Status");

                    IContainer CellStyle(IContainer headerContainer)
                    {
                        return headerContainer.DefaultTextStyle(x => x.SemiBold())
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .Background(Colors.Grey.Lighten1)
                            .AlignCenter();
                    }  
                });

                // step 3
                foreach (var item in _dispatches)
                {
                    var slNo = Convert.ToString(_dispatches.IndexOf(item) + 1);

                    table.Cell().Element(CellStyle).Text(slNo);
                    table.Cell().Element(CellStyle).Text(item.ReportTime);
                    table.Cell().Element(CellStyle).Text(item.Skill);
                    table.Cell().Element(CellLeftStyle).Text(item.WorkerName);
                    table.Cell().Element(CellStyle).Text(item.WorkerId.ToString());

                    table.Cell().Element(CellStyle).Text(item.Status.Member);
                    table.Cell().Element(CellStyle).Text(item.Status.Lor);

                    IContainer CellStyle(IContainer cellContainer)
                    {
                        return cellContainer
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .AlignCenter();
                    }

                    IContainer CellLeftStyle(IContainer cellContainer)
                    {
                        return cellContainer
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .AlignLeft();
                    }
                }
            });
        }
    }
}