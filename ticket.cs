public class Ticket
{
  public UInt64 ticketId { get; set; }
  public string summary { get; set; }
  public List<string> status { get; set; }
  public List<string>  priority { get; set; }
  public List<string> submitter { get; set; }
  public List<string>  assigned { get; set; }
  public List<string> watching { get; set; }
}