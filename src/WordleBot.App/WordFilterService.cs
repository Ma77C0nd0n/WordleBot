using System;
using System.Collections.Generic;
using System.Linq;

namespace WordleBot.App
{
    public class WordFilterService
    {
        public List<string> FilterWordsBasedOnResult(
            List<string> words,
            char[] incorrectChars,
            Dictionary<int, char> correctCharsInPosition,  
            Dictionary<int, char> correctCharsOutOfPosition)
        {
            var includedList = new List<string>();
            var excludedList = new List<string>(); //for testing
            foreach(string word in words)
            {
                if (word.Intersect(incorrectChars).Any())
                {
                    excludedList.Add(word);
                }
                else
                {
                    for (int index = 0; index < 5; index++)
                    {
                        var curChar = word[index];
                        if (correctCharsInPosition.TryGetValue(index, out char resChar) && (resChar != curChar))
                        {
                            excludedList.Add(word);
                            break;
                        }
                        if (correctCharsOutOfPosition.TryGetValue(index, out resChar) && resChar == curChar)
                        {
                            excludedList.Add(word);
                            break;
                        }
                        if (index == 4)
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
