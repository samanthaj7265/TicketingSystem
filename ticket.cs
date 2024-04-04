public abstract class Ticket
{
  public UInt64 ticketId { get; set; }
  public string summary { get; set; }
  public string status { get; set; }
  public string  priority { get; set; }
  public string submitter { get; set; }
  public string  assigned { get; set; }
  public List<string> watching { get; set; }

    public Ticket()
  {
    watching = new List<string>();
  }
  public virtual string Display()
    {
      return $"ID: {ticketId}\nSummary: {summary}\nPriority: {priority}\nSubmitted by: {submitter}\nAssigned to: {assigned}\nWatching: {string.Join(", ", watching)}";
    }
    
}