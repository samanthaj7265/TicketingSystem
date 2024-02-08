namespace Validater
{
    internal class Inputs
    {
        public static char GetChar(string prompt, char[] possibleAnswers)
        {
            while (true)
            {
                Console.Write(prompt);
                string? userInput = Console.ReadLine();

                if (String.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Please make sure to enter a character.");
                    continue;
                }
                else
                {
                    if (userInput.Length > 1)
                    {
                        Console.WriteLine("Please enter a single character.");
                        continue;
                    }
                }

                if (possibleAnswers.Contains(userInput[0]))
                {
                    return userInput[0];
                }
                else
                {
                    Console.WriteLine("Make sure to enter one of the following: "
                        + String.Join(", ", possibleAnswers));
                    continue;
                }
            }
        }
    }
}
