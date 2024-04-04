public class BugDefect : Ticket
{
    public string severity { get; set; }
   
     public override string Display()
    {
      return $"ID: {ticketId}\nSummary: {summary}\nPriority: {priority}\nSubmitted by: {submitter}\nAssigned to: {assigned}\nWatching: {string.Join(", ", watching)}\nSeverity to: {severity}";
    }
}