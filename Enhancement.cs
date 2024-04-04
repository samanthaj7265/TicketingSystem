public class Enhancement : Ticket
{
    public string software { get; set; }
    public decimal cost { get; set; }
    public string reason { get; set; } 
    public decimal estimate { get; set; }
     public override string Display()
    {
      return $"ID: {ticketId}\nSummary: {summary}\nPriority: {priority}\nSubmitted by: {submitter}\nAssigned to: {assigned}\nWatching: {string.Join(", ", watching)}\nSoftware: {software}\nCost: {cost}\nReason: {reason}\nEstimate: {estimate}";
    }
}