using PdfGenerator.Properties;

namespace PdfGenerator.Models.Reports.Grievance.LetterStepOne
{
    public class CarbonCopy
    {
        public string PersonOne => Resources.CcPersonOne;
        public string PersonTwo => Resources.CcPersonTwo;
        public string PersonThree { get; set; }
    }
}