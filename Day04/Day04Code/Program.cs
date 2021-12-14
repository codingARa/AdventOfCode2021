using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04Code
{

    public class BingoCard {
        public virtual List<List<int>> Number { get; set; }
        public virtual List<List<int>> Count { get; set; }
        public BingoCard(List<string> init_data) {
            Number = new List<List<int>>();
            Count = new List<List<int>>();
            InitializeCard(init_data);
        }
        public void InitializeCard(List<string> init_data) {
            foreach (var line in init_data) {
                List<int> split_line = line.Split(" ")
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(x => Int32.Parse(x))
                    .ToList();
                Number.Add(split_line);
                Count.Add(new List<int>(new int[split_line.Count]));
            }
        }
        public void SearchNumber(int chosen_number)
        {
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    if (Number[i][j] == chosen_number && Count[i][j] == 0) {
                        Count[i][j] = 1;
                    }
                }
            }
        }
        public bool FindBingo()
        {
            for (int i = 0; i < 5; i++) {
                int product_row = 1;
                int product_column = 1;
                for (int j = 0; j < 5; j++) {
                    product_row *= Count[i][j];
                    product_column *= Count[j][i];
                }
                if (product_row == 1 || product_column == 1)  return true;
            }
            return false; 
        }
        public int EvaluateBingoCard(int winning_number) {
            int sum_of_unmarked_numbers = 0;
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    if (Count[i][j] == 0) {
                        sum_of_unmarked_numbers += Number[i][j]; 
                    }
                }
            }
            return sum_of_unmarked_numbers * winning_number;
        }
    }

    public class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Let's solve Day 04!");
            var inputStrings = File.ReadAllLines("input.txt")
                .ToList();

            int answer1 = PlayBingoToWin(inputStrings);
            Console.WriteLine($"answer to part 1: {answer1}");

            int answer2 = PlayBingoToLoose(inputStrings);
            Console.WriteLine($"answer to part 2: {answer2}");
        }

        public static int PlayBingoToWin(List<string> inputStrings) {
            List<int> chosen_numbers = ParseChosenNumbers(inputStrings[0]);
            List <BingoCard> bingoCards = ParseBingoNumbers(inputStrings.Skip(2).ToList());

            List<(BingoCard, int)> wonCards = new();
            // draw a number one by one
            foreach (int next_number in chosen_numbers) {
                // play as long as no winner is found ...
                if (wonCards.Count == 0) {
                    foreach (var card in bingoCards) {
                        card.SearchNumber(next_number);
                        if (card.FindBingo()) {
                            wonCards.Add((card, card.EvaluateBingoCard(next_number)));
                        }
                    } 
                } else { break; }
            }

            // check for the winner with the highest score in the last round
            int winning_sum = 0;
            if (wonCards.Count > 1) {
                foreach (var winner in wonCards) {
                    if (winner.Item2 > winning_sum) { winning_sum = winner.Item2; }
                }
            }
            else {
                winning_sum = wonCards[0].Item2;
            }
            return winning_sum; 
        }
        public static int PlayBingoToLoose(List<string> inputStrings) {
            List<int> chosen_numbers = ParseChosenNumbers(inputStrings[0]);
            List<BingoCard> bingoCards = ParseBingoNumbers(inputStrings.Skip(2).ToList());

            List<(BingoCard, int)> wonCards = new();
            // draw the numbers one by one
            foreach (int next_number in chosen_numbers) {
                List<int> index_to_remove = new();
                for (int i = 0; i < bingoCards.Count; i++) {
                    bingoCards[i].SearchNumber(next_number);
                    bool bingoFound = bingoCards[i].FindBingo();
                    if (bingoFound) {
                        wonCards.Add((bingoCards[i], bingoCards[i].EvaluateBingoCard(next_number)));
                        // keep track of the index on the new winner
                        index_to_remove.Add(i);
                    }
                }
                // remove the last winner(s) from the last round
                index_to_remove.Reverse();
                foreach (int index in index_to_remove) {
                    bingoCards.RemoveAt(index);
                }
                index_to_remove = new();
            }
            int last_to_win = wonCards.Last().Item2;
            return last_to_win;
        }
        public static List<int> ParseChosenNumbers(string inputString) {
            return inputString.Split(",").Select(s => int.Parse(s)).ToList();
        }
        public static List<BingoCard> ParseBingoNumbers(List<string> inputStrings) {
            List<BingoCard> answer = new();
            for (int i = 0; i <= inputStrings.Count/6; i++) {
                List<string> cardNumbers = inputStrings.Skip(6*i).Take(5).ToList();
                answer.Add(new BingoCard(init_data: cardNumbers));
            }
            return answer;
        }
    }
}
