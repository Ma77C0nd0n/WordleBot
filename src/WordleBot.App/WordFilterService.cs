using System.Collections.Generic;
using System.Linq;

namespace WordleBot.App
{
    public class WordFilterService
    {
        public List<string> FilterWordsBasedOnResult(
            List<string> words,
            GuessResult result)
        {
            var includedList = new List<string>();
            foreach(string word in words)
            {
                if (!word.Intersect(result.IncorrectChars).Any())
                {
                    var charactersOutOfPos = new List<char>();

                    for (int index = 0; index < 5; index++)
                    {
                        var curChar = word[index];
                        if (result.CorrectCharsInPosition.TryGetValue(index, out char resChar) && (resChar != curChar))
                            break;
                        if (result.CorrectCharsOutOfPosition.TryGetValue(index, out resChar) && resChar == curChar)
                                break;
                        if (result.CorrectCharsOutOfPosition.Values.Contains(curChar))
                        {
                            charactersOutOfPos.Add(curChar);
                        }
                        if (index == 4 && charactersOutOfPos.Distinct().Count() == result.CorrectCharsOutOfPosition.Count)
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
