using System.Collections.Generic;
using System.Linq;

namespace WordleBot.App
{
    public class WordFilterService
    {
        public List<string> FilterOutDuplicateLetters(List<string> inputWords)
        {
            var remainingStrings = new List<string>();
            foreach (string word in inputWords)
            {
                if (word.Distinct().Count() == word.Length)
                    remainingStrings.Add(word);
            }
            return remainingStrings;
        }

        public List<string> FilterWordsBasedOnResult(
            List<string> words,
            GuessResult result)
        {
            var includedList = new List<string>();
            foreach(string word in words)
            {
                if (!word.Intersect(result.IncorrectChars).Any())
                {
                    var numberOfCharsOutOfPos = 0;

                    for (int index = 0; index < 5; index++)
                    {
                        var curChar = word[index];
                        if (result.CorrectCharsInPosition.TryGetValue(index, out char resChar) && (resChar != curChar))
                            break;
                        if (result.CorrectCharsOutOfPosition.TryGetValue(index, out resChar) && resChar == curChar)
                                break;
                        if (result.CorrectCharsOutOfPosition.Values.Contains(curChar))
                        {
                            numberOfCharsOutOfPos++;
                        }
                        if (index == 4 && numberOfCharsOutOfPos == result.CorrectCharsOutOfPosition.Count)
                        {
                            includedList.Add(word);
                        }
                    }
                }
            }
            return includedList;
        }
    }
}
