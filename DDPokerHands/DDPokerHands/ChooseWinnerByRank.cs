using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDPokerHands
{
   public  class ChooseWinnerByRank
    {
        /// <summary>
        /// Run through the ranks from 2- 10
        /// </summary>
        /// <param name="player"></param>
        /// <param name="numbers"></param>
        /// <param name="suits"></param>
        /// <returns></returns>
        public Dictionary<int, int> CheckRanks(Dictionary<int, int> player, List<int> numbers, List<string> suits)
        {
            Dictionary<int, int> ranks = new Dictionary<int, int>();

            List<string> list = suits.ToList();
            bool allAreSame = list.All(x => x == list.First());

            var pair = player.ContainsValue(2);
            if (pair == true)
            {
                var no = player.Where(c => c.Value == 2);
                if (no.ToList().Count == 1)
                { ranks.Add(2, player.Where(c => c.Value == 2).Select(c => c.Key).FirstOrDefault()); }
            }
            var twoPair = player.Where(c => c.Value == 2);
            if (twoPair.ToList().Count == 2)
            {
                var pairlist = player.Where(c => c.Value == 2).Select(c => c.Key).ToList();
                ranks.Add(3, pairlist.Max());
            }
            var ThreeOfKind = player.Where(c => c.Value == 3);
            if (ThreeOfKind.ToList().Count == 1)
            {
                ranks.Add(4, player.Where(c => c.Value == 3).Select(c => c.Key).FirstOrDefault());
            }
            //Check for the consecutive numbers
            bool straight = !numbers.Select((i, j) => i - j).Distinct().Skip(1).Any();
            if (straight == true)
            {
                ranks.Add(5, 0);
            }
            //Check all the same in the list
            if (allAreSame == true)
            {
                ranks.Add(6, 0);
            }
            bool fulHouse = player.ContainsValue(3) && player.ContainsValue(2);
            if (fulHouse == true)
            {
                ranks.Add(7, player.Where(c => c.Value == 3).Select(c => c.Key).FirstOrDefault());
            }
            bool fourOfKind = player.ContainsValue(4);
            //var fourOfKind = player.Where(c => c.Value == 4);
            if (fourOfKind == true)
            {
                ranks.Add(8, player.Where(c => c.Value == 4).Select(c => c.Key).FirstOrDefault());
            }
            ///Check for the same suit and consecutive number
            bool strightFlush = !numbers.Select((i, j) => i - j).Distinct().Skip(1).Any();
            if (strightFlush == true
                && allAreSame == true)
            {
                ranks.Add(9, 0);
            }
            bool royalFlush = numbers.All(i => i >= 10);
            bool checkConse = !numbers.Select((i, j) => i - j).Distinct().Skip(1).Any();
            if (royalFlush == true && checkConse == true
                && allAreSame == true)
            {
                ranks.Add(10, 0);
            }
            return ranks;
        }
    }
}
