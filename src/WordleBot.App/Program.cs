using System;
using System.Linq;

namespace WordleBot.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var WordListRepo = new WordListRepository();
            var WordSortingSvc = new WordSortingService();
            var WordFilterSvc = new WordFilterService();

            var possibleWords = WordListRepo.GetAllPossibleWordsFromFile();
            var sortedWords = WordSortingSvc.GetSortedWords(possibleWords);

            var sortedWordsNoDuplicates = WordFilterSvc.FilterOutDuplicateLetters(sortedWords);

            var guessIsCorrect = false;
            while (!guessIsCorrect)
            {
                if (!sortedWordsNoDuplicates.Any())
                {
                    Console.WriteLine($"No words left to guess. Exiting.");
                    return;
                }

                string guessedWord = sortedWordsNoDuplicates[0];

                Console.WriteLine($"Guessed word: {guessedWord}");

                var result = Console.ReadLine();

                var guessResult = ConvertToGuessResult(guessedWord, result);

                guessIsCorrect = guessResult.IsCorrect();

                sortedWordsNoDuplicates = WordFilterSvc.FilterWordsBasedOnResult(
                    sortedWordsNoDuplicates,
                    guessResult
                );
            }
            Console.WriteLine("Guess was correct");
        }

        private static GuessResult ConvertToGuessResult(string input, string result)
        {
            var guessRes = new GuessResult();
            for (int i=0; i < 5; i++)
            {
                var curChar = input[i];
                var curCharRes = (ResultValue) char.GetNumericValue(result[i]);
                guessRes.Add(curChar, i, curCharRes);
            }
            return guessRes;
        }
    }
}
