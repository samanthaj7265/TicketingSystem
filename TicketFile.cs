using NLog;
public class TicketFile

{
  // public property
  public string filePath { get; set; }
  public List<Ticket> Tickets { get; set; }
  private static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() +  
  "\\nlog.config").GetCurrentClassLogger();

  public TicketFile(string ticketFilePath)
  {
    filePath = ticketFilePath;

    Tickets = new List<Ticket>();

    // to populate the list with data, read from the data file
    try
    {
      StreamReader sr = new StreamReader(filePath);
      // first line contains column headers
      sr.ReadLine();
      while (!sr.EndOfStream)
      {
        // create instance of Ticket class
          Ticket ticket = new BugDefect();
          string line = sr.ReadLine();


          string[] TicketDetails = line.Split(',');
          ticket.ticketId = UInt64.Parse(TicketDetails[0]);
          ticket.summary = TicketDetails[1];
          ticket.status = TicketDetails[2];
          ticket.submitter = TicketDetails[3];
          ticket.assigned = TicketDetails[4];
          ticket.watching = TicketDetails[6].Split('|').ToList();

          Tickets.Add(ticket);
      }
      // close file when done
      sr.Close();
      logger.Info("Tickets in file {Count}", Tickets.Count);
    }
    catch (Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
  
  public void AddTicket(Ticket ticket)
  {
    try
    {
      //generate ticket id
      ticket.ticketId = Tickets.Max(t => t.ticketId) + 1;
      StreamWriter sw = new StreamWriter(filePath, true);
      sw.WriteLine($"{ticket.ticketId},{ticket.summary},{ticket.status},{ticket.priority},{ticket.submitter},{ticket.assigned}{string.Join("|", ticket.watching)}");
      sw.Close();

      Tickets.Add(ticket);
      // log transaction
      logger.Info("Ticket id {Id} added", ticket.ticketId);
    } 
    catch(Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
}