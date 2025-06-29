﻿using PdfGenerator.Models.Reports.Common;

namespace PdfGenerator.Models.Reports.EmpDispatch;

public class EmpDispatchReportModel
{
    public Header Header { get; set; }
    public Footer Footer { get; set; }
    public List<EmpDispatchResponse> EmpDispatchResponses { get; set; }
}