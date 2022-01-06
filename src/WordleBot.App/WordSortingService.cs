using System.Collections.Generic;
using System.Linq;

namespace WordleBot.App
{
    public class WordSortingService
    {
        public List<string> GetSortedWords(List<string> possibleWords)
        {
            var characterFrequencyDictionary = new Dictionary<char, int>();

            foreach(string word in possibleWords)
            {
                foreach(char curChar in word)
                {
                    characterFrequencyDictionary.TryGetValue(curChar, out int count);
                    if (count == 0)
                    {
                        characterFrequencyDictionary.Add(curChar, 1);
                    }
                    else
                        characterFrequencyDictionary[curChar] = count + 1;
                }
            }

            var sortedCharacters = characterFrequencyDictionary.OrderByDescending(c => c.Value).Select(c => c.Key);

            var wordValuesDictionary = new Dictionary<string, int>();

            foreach (string word in possibleWords)
            {
                var wordValue = 0;
                foreach (char curChar in word)
                {
                    wordValue += characterFrequencyDictionary[curChar];
                }
                wordValuesDictionary.Add(word, wordValue);
            }

            return wordValuesDictionary.OrderByDescending(c => c.Value).Select(c => c.Key).ToList();
        }
    }
}
