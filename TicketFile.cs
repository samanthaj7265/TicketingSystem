using NLog;
public class TicketFile

{
  // public property
  public string filePath { get; set; }
  public List<Ticket> Tickets { get; set; }
  

  // constructor is a special method that is invoked
  // when an instance of a class is created
  public TicketFile(string ticketFilePath)
  {
    filePath = ticketFilePath;
       // create instance of Logger
    NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

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
          Ticket ticket = new Ticket();
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
}