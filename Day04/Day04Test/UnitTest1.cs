using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Day04Code;

namespace Day04Test
{
    public class Tests
    {

        [Test]
        public void Test1_Sanity_Check()
        {
            var inputStrings = File.ReadAllLines("testInput.txt")
                .ToList();
            // Count lines, to see that reading file went okay
            Assert.AreEqual(19, inputStrings.Count);
        }

        [Test]
        public void Test2_Part1_truncated_test_input()
        {
            var inputStrings = File.ReadAllLines("truncatedTestInput.txt")
                .ToList();
            int answer = Program.PlayBingo(inputStrings);
            Assert.AreEqual(4512, answer);
        }

        [Test]
        public void Test3_ComparingBingoCards()
        {
            var inputNormal = File.ReadAllLines("testInput.txt")
                .ToList();
            var inputTruncated = File.ReadAllLines("truncatedTestInput.txt")
                .ToList();

            List<BingoCard> normalCards = Program.ParseBingoNumbers(inputNormal.Skip(2).ToList());
            List<BingoCard> truncatedCards = Program.ParseBingoNumbers(inputTruncated.Skip(2).ToList());

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; i++)
                {
                    Assert.AreEqual(normalCards[2].Number[i][j], truncatedCards[0].Number[i][j]);
                    Assert.AreEqual(normalCards[2].Count[i][j], truncatedCards[0].Count[i][j]);
                }
            }
        }


        [Test]
        public void Test4_Part1_Full_test_Input()
        {
            var inputStrings = File.ReadAllLines("testInput.txt")
                .ToList();
            int answer = Day04Code.Program.PlayBingo(inputStrings);
            Assert.AreEqual(4512, answer);
        }
    }
}