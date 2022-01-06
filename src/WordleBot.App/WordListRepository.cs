using System;
using System.Collections.Generic;
using System.IO;

namespace WordleBot.App
{
    public class WordListRepository
    {
        public List<string> GetAllPossibleWordsFromFile()
        {
            try
            {
                using (var reader = new StreamReader("~\\..\\..\\..\\..\\..\\..\\all-5-letter-words.txt"))
                {
                    List<string> listOfWords = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        listOfWords.Add(line);
                    }
                    return listOfWords;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred parsing file, exception {e}");
            }
            return new List<string>();
        }
    }
}
