using NLog;
public class EnhancementFile

{
  // public property
  public string enhancementTicketFilePath { get; set; }
  public List<Enhancement> EnhancementTickets { get; set; }
  private static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() +  
  "\\nlog.config").GetCurrentClassLogger();

  public EnhancementFile(string enhancementFilePath)
  {
    enhancementTicketFilePath = enhancementFilePath;

    EnhancementTickets = new List<Enhancement>();

    // to populate the list with data, read from the data file
    try
    {
      StreamReader sr = new StreamReader(enhancementFilePath);
      // first line contains column headers
      sr.ReadLine();
      while (!sr.EndOfStream)
      {
        // create instance of Ticket class
          Enhancement ticket = new Enhancement();
          string line = sr.ReadLine();


          string[] TicketDetails = line.Split(',');
          ticket.ticketId = UInt64.Parse(TicketDetails[0]);
          ticket.summary = TicketDetails[1];
          ticket.status = TicketDetails[2];
          ticket.submitter = TicketDetails[3];
          ticket.assigned = TicketDetails[4];
          ticket.watching = TicketDetails[6].Split('|').ToList();
          ticket.software = TicketDetails[7];
          ticket.cost = Decimal.Parse(TicketDetails[8]);
          ticket.reason = TicketDetails[9];
          ticket.estimate = Decimal.Parse(TicketDetails[10]);

          EnhancementTickets.Add(ticket);
      }
      // close file when done
      sr.Close();
      logger.Info("Tickets in file {Count}", EnhancementTickets.Count);
    }
    catch (Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
  
  public void AddTicket(Enhancement ticket)
  {
    try
    {
      //generate ticket id
      ticket.ticketId = EnhancementTickets.Max(t => t.ticketId) + 1;
      StreamWriter sw = new StreamWriter(enhancementTicketFilePath, true);
      sw.WriteLine($"{ticket.ticketId},{ticket.summary},{ticket.status},{ticket.priority},{ticket.submitter},{ticket.assigned}{string.Join("|", ticket.watching)},{ticket.software},{ticket.cost}{ticket.reason}{ticket.estimate}");
      sw.Close();

      EnhancementTickets.Add(ticket);
      // log transaction
      logger.Info("Ticket id {Id} added", ticket.ticketId);
    } 
    catch(Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
}