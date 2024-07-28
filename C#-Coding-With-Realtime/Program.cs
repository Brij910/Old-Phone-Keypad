using System;
using System.Threading;

namespace OldPhoneKeypad
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Old Phone Keypad Simulator (Press Esc to exit)");
            Console.WriteLine("Start typing...");

            string result = "";
            string currentString = "";
            char lastChar = '\0';
            bool backspaceFlag = false;
            var timer = new Timer(_ => backspaceFlag = true, null, Timeout.Infinite, Timeout.Infinite);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    char c = key.KeyChar;

                    if (key.Key == ConsoleKey.Escape)
                    {
                        break; // Exit the loop if Esc is pressed
                    }

                    if (char.IsDigit(c))
                    {
                        if (c == '8')
                        {
                            // Simulate backspace
                            if (result.Length > 0)
                            {
                                result = result.Substring(0, result.Length - 1);
                                Console.Clear();
                                Console.WriteLine("Old Phone Keypad Simulator (Press Esc to exit)");
                                Console.WriteLine("Translated Text: " + result);
                            }
                            continue;
                        }

                        if (c == '9')
                        {
                            // Simulate send button
                            continue;
                        }

                        if (c == lastChar && !backspaceFlag)
                        {
                            currentString += c;  // Append to current string for multiple presses
                        }
                        else
                        {
                            if (currentString.Length > 0)
                            {
                                int letterIndex = currentString.Length - 1;
                                int digit = lastChar - '0';
                                char letter = GetMappedLetter(digit, letterIndex);
                                if (letter != '\0')
                                {
                                    result += letter;
                                    Console.Clear();
                                    Console.WriteLine("Old Phone Keypad Simulator (Press Esc to exit)");
                                    Console.WriteLine("Translated Text: " + result);
                                }
                            }
                            currentString = c.ToString();  // Start new string for new button press
                        }

                        lastChar = c;
                        backspaceFlag = false;
                        timer.Change(1000, Timeout.Infinite); // Reset the timer to set backspaceFlag after 1 second
                    }
                }
            }

            // Handle the final character sequence
            if (currentString.Length > 0)
            {
                int letterIndex = currentString.Length - 1;
                int digit = lastChar - '0';
                char letter = GetMappedLetter(digit, letterIndex);
                if (letter != '\0')
                {
                    result += letter;
                }
            }

            Console.WriteLine("Final Translated Text: " + result);
            Console.WriteLine("Goodbye!");
        }

        static char GetMappedLetter(int digit, int letterIndex)
        {
            // Define the mapping of numbers to letters
            char[][] keypad = new char[10][];
            keypad[2] = new char[] { 'A', 'B', 'C' };
            keypad[3] = new char[] { 'D', 'E', 'F' };
            keypad[4] = new char[] { 'G', 'H', 'I' };
            keypad[5] = new char[] { 'J', 'K', 'L' };
            keypad[6] = new char[] { 'M', 'N', 'O' };
            keypad[7] = new char[] { 'P', 'Q', 'R', 'S' };
            keypad[8] = new char[] { 'T', 'U', 'V' };
            keypad[9] = new char[] { 'W', 'X', 'Y', 'Z' };
            keypad[0] = new char[] { ' ' };

            if (digit >= 0 && digit <= 9 && keypad[digit] != null && letterIndex < keypad[digit].Length)
            {
                return keypad[digit][letterIndex];
            }

            return '\0';
        }
    }
}