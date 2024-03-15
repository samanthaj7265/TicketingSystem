 using Validater;
 using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");

 /*Complete and submit first phase of project
 Build data file with initial system tickets and data in a CSV
 Write Console application to process and add records to the CSV file
 Tickets.csv

 TicketID, Summary, Status, Priority, Submitter, Assigned, Watching

 1,This is a bug ticket,Open,High,Drew Kjell, Jane Doe,Drew Kjell| John Smith | Bill Jones */
 Ticket ticket = new Ticket
{
  ticketId = 2,
  summary = "system error ticket",
  status = "open",
  priority = "high",
  submitter = "Samantha Jesmok",
  assigned = "Drew Kjell",
  watching = new List<string> { "Samantha Jesmok", "Drew Kjell"}
};
 
 string ticketFile = "Ticket.csv";

 if (File.Exists(ticketFile))
 {
     File.Delete(ticketFile);
 }

 using (StreamWriter sw = File.CreateText(ticketFile))
 {
     sw.WriteLine("TicketID, Summary, Status, Priority, Submitter, Assigned, Watching");
     sw.WriteLine("1,This is a bug ticket,Open,High,Drew Kjell, Jane Doe,Drew Kjell| John Smith | Bill Jones");
     sw.WriteLine($"{ticket.Display()}");
 }

 char menuOption;

 do
 {
     menuOption = Inputs.GetChar("Do you want to (A - Read File, B - Add to File , E - End): ",
         new char[] { 'A', 'B', 'E' });
     if (menuOption == 'A')
     {
         using (StreamReader sr = File.OpenText(ticketFile))
         {
             string s = "";
             while ((s = sr.ReadLine()) != null)
             {
                 Console.WriteLine(s);
             }
         }
     }
     else if (menuOption == 'B')
     {
         Console.Write("Add Ticket:\n");
         string appendText = Console.ReadLine();
         File.AppendAllText(ticketFile, appendText);
     }
 } while (menuOption != 'E');

 logger.Info("Program ended");