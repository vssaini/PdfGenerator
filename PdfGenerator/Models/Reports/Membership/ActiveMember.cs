namespace PdfGenerator.Models.Reports.Membership
{
    public class ActiveMember
    {
        public int WorkerId { get; set; }
        public string WorkerName { get; set; }

        public int? VotingCraftId { get; set; }
        public string WorkerStatusAbbrv { get; set; }

        public DateTime? MemberDate { get; set; }
        public string Date => MemberDate.HasValue ? MemberDate.Value.ToString(Constants.DateFormat.MonthDateYear) : string.Empty;

        public string VoterStatus => $"{VotingCraftId} {WorkerStatusAbbrv}";

        public int TotalRows { get; set; }
    }
}