public class Task : Ticket
{
    public string projectName { get; set; }
    public DateTime dueDate { get; set; }
     public override string Display()
    {
      return $"ID: {ticketId}\nSummary: {summary}\nPriority: {priority}\nSubmitted by: {submitter}\nAssigned to: {assigned}\nWatching: {string.Join(", ", watching)}\nProject Name: {projectName}\nDue Date: {dueDate}";
    }
}