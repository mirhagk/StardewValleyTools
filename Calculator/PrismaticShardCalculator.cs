using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class PrismaticShardCalculator
    {
        readonly SaveFileManager saveFileManager;
        public PrismaticShardCalculator(SaveFileManager saveFileManager)
        {
            this.saveFileManager = saveFileManager;
        }
        public void Run(Program.Args args)
        {
            var save = saveFileManager.PromptToSelectSave();
            Console.WriteLine($"You have cracked {save.GeodesCracked} geodes so far");
            var shardsSoFar = new List<int>();
            for(int i = 0; i < save.GeodesCracked; i++)
                if (GetsShard(i, save.UniqueID))
                    shardsSoFar.Add(i);

            if (shardsSoFar.Count == 0)
                Console.WriteLine("You've had no opportunities for shards so far");
            else
                Console.WriteLine($"You've had {shardsSoFar.Count} opportunities for shards");

            var shardsCouldGet = new List<int>();
            var maxToCheck = save.GeodesCracked + 10000;
            for(int i = save.GeodesCracked; i < maxToCheck; i++)
                if (GetsShard(i, save.UniqueID))
                    shardsCouldGet.Add(i);

            if (shardsCouldGet.Count == 0)
                Console.WriteLine("Absolutely no shards in your forseeable future");
            else
            {
                Console.WriteLine($"The next shard you could get is the {shardsCouldGet[0] + 1}th geode you crack (wait until you have {shardsCouldGet[0]} geodes cracked then crack an omni geode)");
                Console.WriteLine($"The next {Math.Min(5, shardsCouldGet.Count - 1)} shards after that are at {string.Join(",", shardsCouldGet.Skip(1).Take(5))} geodes cracked");
            }
        }
        bool GetsShard(int geodesCracked, ulong uniqueIDForThisGame)
        {

            Random random = new Random((int)(geodesCracked + (uint)((int)uniqueIDForThisGame / 2)));
            if (!(random.NextDouble() < 0.5))
            {
                random.Next();
                if (random.NextDouble() < 0.008)
                    return true;
            }
            return false;
        }
    }
}
