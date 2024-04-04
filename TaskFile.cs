using NLog;
public class TaskFile

{
  // public property
  public string taskTicketFilePath { get; set; }
  public List<Task> TaskTickets { get; set; }
  private static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() +  
  "\\nlog.config").GetCurrentClassLogger();

  public TaskFile(string taskFilePath)
  {
    taskTicketFilePath = taskFilePath;

    TaskTickets = new List<Task>();

    // to populate the list with data, read from the data file
    try
    {
      StreamReader sr = new StreamReader(taskFilePath);
      // first line contains column headers
      sr.ReadLine();
      while (!sr.EndOfStream)
      {
        // create instance of Ticket class
          Task ticket = new Task();
          string line = sr.ReadLine();


          string[] TicketDetails = line.Split(',');
          ticket.ticketId = UInt64.Parse(TicketDetails[0]);
          ticket.summary = TicketDetails[1];
          ticket.status = TicketDetails[2];
          ticket.submitter = TicketDetails[3];
          ticket.assigned = TicketDetails[4];
          ticket.watching = TicketDetails[6].Split('|').ToList();
          ticket.projectName = TicketDetails[7];
          ticket.dueDate = DateTime.Parse(TicketDetails[8]);

          TaskTickets.Add(ticket);
      }
      // close file when done
      sr.Close();
      logger.Info("Tickets in file {Count}", TaskTickets.Count);
    }
    catch (Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
  
  public void AddTicket(Task ticket)
  {
    try
    {
      //generate ticket id
      ticket.ticketId = TaskTickets.Max(t => t.ticketId) + 1;
      StreamWriter sw = new StreamWriter(taskTicketFilePath, true);
      sw.WriteLine($"{ticket.ticketId},{ticket.summary},{ticket.status},{ticket.priority},{ticket.submitter},{ticket.assigned}{string.Join("|", ticket.watching)},{ticket.projectName},{ticket.dueDate}");
      sw.Close();

      TaskTickets.Add(ticket);
      // log transaction
      logger.Info("Ticket id {Id} added", ticket.ticketId);
    } 
    catch(Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
}