 using Validater;
 /*Complete and submit first phase of project
 Build data file with initial system tickets and data in a CSV
 Write Console application to process and add records to the CSV file
 Tickets.csv

 TicketID, Summary, Status, Priority, Submitter, Assigned, Watching

 1,This is a bug ticket,Open,High,Drew Kjell, Jane Doe,Drew Kjell| John Smith | Bill Jones */
 string ticketFile = "Ticket.csv";


 // Check if file already exists. If yes, delete it.
 if (File.Exists(ticketFile))
 {
     File.Delete(ticketFile);
 }

 // Create a new file
 using (StreamWriter sw = File.CreateText(ticketFile))
 {
     sw.WriteLine("TicketID, Summary, Status, Priority, Submitter, Assigned, Watching");
     sw.WriteLine("1,This is a bug ticket,Open,High,Drew Kjell, Jane Doe,Drew Kjell| John Smith | Bill Jones");
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
         Console.Write("Add Ticket(TicketID, Summary, Status, Priority, Submitter, Assigned, Watching):\n");
         string appendText = Console.ReadLine();
         File.AppendAllText(ticketFile, appendText);
     }
 } while (menuOption != 'E');