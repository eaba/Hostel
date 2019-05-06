using System;

namespace Hostel.Model.Tenant
{
    public enum IssueType
    {
        Fight,
        Rent,
        Illness,
        Warning,
        Drug
    }
    public class Issue
    {
        public DateTime IssueDate { get; }
        public IssueType IssueType { get; }
        public string Comment { get; }
        public Issue(DateTime date, IssueType issue, string comment)
        {
            IssueDate = date;
            IssueType = issue;
            Comment = comment;
        }
    }
}
