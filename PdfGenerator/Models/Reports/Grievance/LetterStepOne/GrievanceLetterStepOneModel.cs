namespace PdfGenerator.Models.Reports.Grievance.LetterStepOne
{
    public class GrievanceLetterStepOneModel
    {
        public Header Header { get; set; }

        public Address Address { get; set; }
        public string Subject { get; set; }
        public Body Body { get; set; }

        public Signature Signature { get; set; }
        public CarbonCopy CarbonCopy { get; set; }

        public string CertifiedStatement { get; set; }
        public Footer Footer { get; set; }
    }
}