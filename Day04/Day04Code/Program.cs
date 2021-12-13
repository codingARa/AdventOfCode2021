using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04Code
{


    public class BingoCard
    {
        public virtual List<List<int>> Number { get; set; }
        public virtual List<List<int>> Count { get; set; }
        public BingoCard(List<string> init_data)
        {
            Number = new List<List<int>>();
            Count = new List<List<int>>();
            InitializeCard(init_data);
        }
        public void InitializeCard(List<string> init_data)
        {
            foreach (var line in init_data)
            {
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
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Number[i][j] == chosen_number && Count[i][j] == 0)
                    {
                        Count[i][j] = 1;
                    }
                }
            }
        }
        public bool FindBingo()
        {
            for (int i = 0; i < 5; i++)
            {
                int product_row = 1;
                int product_column = 1;
                for (int j = 0; j < 5; j++)
                {
                    product_row *= Count[i][j];
                    product_column *= Count[j][i];
                }
                if (product_row == 1 || product_column == 1) {
                    return true;
                }
            }
            return false; 
        }
        public int EvaluateBingoCard(int winning_number)
        {
            int sum_of_unmarked_numbers = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Count[i][j] == 0)
                    {
                        sum_of_unmarked_numbers += Number[i][j]; 
                    }
                }
            }
            return sum_of_unmarked_numbers * winning_number;
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's solve Day 04!");
            var inputStrings = File.ReadAllLines("input.txt")
                .ToList();

            int answer1 = PlayBingo(inputStrings);
            Console.WriteLine($"answer to part 1: {answer1}");

        }
        public static int PlayBingo(List<string> inputStrings)
        {
            List<int> chosen_numbers = ParseChosenNumbers(inputStrings[0]);
            List <BingoCard> bingoCards = ParseBingoNumbers(inputStrings.Skip(2).ToList());
            List<(BingoCard, int)> wonCards = new();
            foreach (int next_number in chosen_numbers)
            {
                //Console.WriteLine($"next_number: {next_number}");
                if (wonCards.Count == 0)
                {
                    foreach (var card in bingoCards)
                    {
                        card.SearchNumber(next_number);
                        bool bingoFound = card.FindBingo();
                        //Console.WriteLine($"won yet? {bingoFound}");
                        //string nums = "";
                        //for (int i = 0; i < 5; i++)
                        //{
                        //    for (int j = 0; j < 5; j++)
                        //    {
                        //        nums = string.Concat(nums, card.Number[i][j].ToString() + " ");
                        //    }
                        //    Console.WriteLine(nums);
                        //    nums = "";
                        //}
                        //Console.WriteLine("---");
                        //string cnts = "";
                        //for (int i = 0; i < 5; i++)
                        //{
                        //    for (int j = 0; j < 5; j++)
                        //    {
                        //        cnts = string.Concat(cnts, card.Count[i][j].ToString() + " ");
                        //    }
                        //    Console.WriteLine(cnts);
                        //    cnts = "";
                        //}
                        //Console.WriteLine("===");
                        if (bingoFound) {
                            wonCards.Add((card, card.EvaluateBingoCard(next_number)));
                            Console.WriteLine($"last number: {next_number}");
                        }
                    } 
                } else { 
                    break;
                }
            }

            int winning_sum = 0;
            foreach (var winner in wonCards)
            {
                if (winner.Item2 > winning_sum) { winning_sum = winner.Item2; }
            }
            return winning_sum; 
        }
        public static List<int> ParseChosenNumbers(string inputString)
        {
            return inputString.Split(",").Select(s => int.Parse(s)).ToList();
        }
        public static List<BingoCard> ParseBingoNumbers(List<string> inputStrings)
        {
            List<BingoCard> answer = new();
            for (int i = 0; i <= inputStrings.Count/6; i++)
            {
                List<string> cardNumbers = inputStrings.Take(5).ToList();
                _ = inputStrings.Take(1);
                answer.Add(new BingoCard(init_data: cardNumbers));
            }
            return answer;
        }
    }
}
