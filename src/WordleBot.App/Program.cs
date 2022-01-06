using System;
using System.Collections.Generic;

namespace WordleBot.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string word = "tiger";

            var WordListRepo = new WordListRepository();
            var WordSortingSvc = new WordSortingService();
            var WordFilterSvc = new WordFilterService();

            var possibleWords = WordListRepo.GetAllPossibleWordsFromFile();
            var sortedWords = WordSortingSvc.GetSortedWords(possibleWords);

            string guessedWord = "fiber";

            //var filteredWords = WordFilterSvc.FilterWordsBasedOnResult(
            //    sortedWords, 
            //    new char[] {'f', 'b'},
            //    new Dictionary<int, char> { { 1, 'i'}, { 3, 'e'}, { 4, 'r'} },
            //    new Dictionary<int, char>()
            //);

            var filteredWords = WordFilterSvc.FilterWordsBasedOnResult(
                sortedWords,
                new char[] { 'f', 'b', 'a', 'z', 'o' },
                new Dictionary<int, char> (),
                new Dictionary<int, char>()
            );

            Console.WriteLine("Hello World!");
        }

        private GuessResult MakeGuess(string wordToBeGuessed, string correctGuess)
        {
            return new GuessResult() 
            {
                CharacterGuesses = new List<CharacterGuess>()
                {

                }
            };
        }
    }
}
