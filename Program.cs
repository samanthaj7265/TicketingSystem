
 using NLog;
using System.Collections.Immutable;
using Microsoft.Win32.SafeHandles;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");

string ticketFilePath = Directory.GetCurrentDirectory() + "\\Tickets.csv";

TicketFile ticketFile = new TicketFile(ticketFilePath);

string choice = "";
do
{
  // display choices to user
  Console.WriteLine("1) New Ticket");
  Console.WriteLine("2) Display All Tickets");
  Console.WriteLine("Enter to quit");
  // input selection
  choice = Console.ReadLine();
  logger.Info("User choice: {Choice}", choice);
     if (choice == "1")
    {
        Ticket ticket = new Ticket();
        // ask user for ticket summary
        Console.WriteLine("Ticket Summary: ");
        // input 
        ticket.summary = Console.ReadLine();
        // new ticket automaticatlly assigned to open
        ticket.status = "open";
        // ask for ticket priority 
        Console.WriteLine("Ticket Priority: ");
        // input
        ticket.priority = Console.ReadLine();
        // ask for submitter name
        Console.WriteLine("Your Name: ");
        // input
        ticket.submitter = Console.ReadLine();
        // ask who ticket will be assigned to
        Console.WriteLine("Assigned to: ");
        //input
        ticket.assigned = Console.ReadLine();

        string input;
        do
        {
        // ask user to enter others watching ticket
        Console.WriteLine("Enter others watching ticket (or done to quit)");
        // input watchers
        input = Console.ReadLine();
        // if user enters "done"
            if (input != "done" && input.Length > 0)
            {
            ticket.watching.Add(ticket.assigned);
            ticket.watching.Add(ticket.submitter);  
            ticket.watching.Add(input);
            }
        } while (input != "done");
        // specify if no other watchers were entered
        if (ticket.watching.Count == 0)
        {
            ticket.watching.Add(ticket.assigned);
            ticket.watching.Add(ticket.submitter);
        }
        ticketFile.AddTicket(ticket);
    }
        else if(choice == "2")
        {
         // Display All Tickets
        foreach(Ticket t in ticketFile.Tickets)
        {
            Console.WriteLine(t.Display());
        }
        
        }
     } while (choice == "1" || choice == "2");

 logger.Info("Program ended");