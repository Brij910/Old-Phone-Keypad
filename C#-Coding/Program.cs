using System;

namespace OldPhoneKeypad
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Old Phone Keypad Simulator");
            Console.WriteLine("Enter a sequence of numbers (use * for backspace, # for send):");
            string inputSequence = Console.ReadLine();

            while (!string.IsNullOrEmpty(inputSequence))
            {
                string result = OldPhonePad(inputSequence);
                Console.WriteLine("Translated Text: " + result);

                Console.WriteLine("Enter another sequence (or press Enter to exit):");
                inputSequence = Console.ReadLine();
            }

            Console.WriteLine("Goodbye!");
        }

        static string OldPhonePad(string inputSequence)
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

            string result = "";
            string currentString = "";
            char lastChar = '\0';

            foreach (char c in inputSequence)
            {
                if (c == '*')
                {
                    // Simulate backspace by removing the last character
                    if (result.Length > 0)
                    {
                        result = result.Substring(0, result.Length);
                    }
                    currentString = "";
                    lastChar = '\0';
                    continue;
                }

                if (c >= '0' && c <= '9')
                {
                    if (c == lastChar)
                    {
                        currentString += c;  // Append to current string for multiple presses
                    }
                    else
                    {
                        if (currentString.Length > 0)
                        {
                            int letterIndex = currentString.Length - 1;
                            int digit = lastChar - '0';
                            if (digit >= 0 && digit <= 9 && keypad[digit] != null)
                            {
                                char[] mappedLetters = keypad[digit];
                                if (letterIndex < mappedLetters.Length)
                                {
                                    result += mappedLetters[letterIndex];
                                }
                            }
                        }
                        currentString = c.ToString();  // Start new string for new button press
                    }

                    lastChar = c;
                }
                else
                {
                    // When encountering a space, process the current sequence
                    if (currentString.Length > 0)
                    {
                        int letterIndex = currentString.Length - 1;
                        int digit = lastChar - '0';
                        if (digit >= 0 && digit <= 9 && keypad[digit] != null)
                        {
                            char[] mappedLetters = keypad[digit];
                            if (letterIndex < mappedLetters.Length)
                            {
                                result += mappedLetters[letterIndex];
                            }
                        }
                        currentString = "";  // Reset currentString after processing
                    }
                }
            }

            // Handle the final character sequence
            if (currentString.Length > 0)
            {
                int letterIndex = currentString.Length - 1;
                int digit = lastChar - '0';
                if (digit >= 0 && digit <= 9 && keypad[digit] != null)
                {
                    char[] mappedLetters = keypad[digit];
                    if (letterIndex < mappedLetters.Length)
                    {
                        result += mappedLetters[letterIndex];
                    }
                }
            }

            return result;
        }
    }
}
