using System.Collections.Generic;
using System.Linq;

namespace WordleBot.App
{
    public class WordSortingService
    {
        private const int DuplicateMultiplierConst = 10;

        public List<string> GetSortedWords(List<string> possibleWords, bool applyDuplicateMultiplier)
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

            var wordValuesDictionary = new Dictionary<string, int>();

            foreach (string word in possibleWords)
            {
                var wordValue = 0;
                foreach (char curChar in word)
                {
                    wordValue += characterFrequencyDictionary[curChar];
                }
                if (applyDuplicateMultiplier)
                {
                    var duplicateMultiplier = ((word.Length - word.Distinct().Count()) * DuplicateMultiplierConst) + 1;
                    wordValue /= duplicateMultiplier;
                }
                wordValuesDictionary.Add(word, wordValue);
            }

            return wordValuesDictionary.OrderByDescending(c => c.Value).Select(c => c.Key).ToList();
        }
    }
}
