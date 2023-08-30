﻿using Microsoft.Extensions.DependencyInjection;
using PdfGenerator;
using PdfGenerator.Services;
using Serilog;

var host = Startup.ConfigureHostBuilder();

var pdfSvc = ActivatorUtilities.CreateInstance<PdfService>(host.Services);
pdfSvc.Run();

Log.CloseAndFlush();