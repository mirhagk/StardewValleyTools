using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class MushroomLevelCalculator
    {
        readonly SaveFileManager saveFileManager;
        readonly InputHelper inputHelper = new InputHelper();
        public MushroomLevelCalculator(SaveFileManager saveFileManager)
        {
            this.saveFileManager = saveFileManager;
        }
        public void Run(Program.Args args)
        {
            var save = saveFileManager.PromptToSelectSave();
            var numDaysToCheck = inputHelper.GetInteger("Please enter how many days you want to look for (defaults to 7)", defaultNum: 7);
            for (uint i = 0; i < numDaysToCheck; i++)
            {
                var day = i + save.DaysPlayed;
                for(int level = 81; level < 120; level++)
                {
                    if (IsLevelAMushroomLevel(save.UniqueID, day, level))
                    {
                        if (i == 0)
                            Console.WriteLine($"You will find a mushroom level at level {level} today");
                        else
                            Console.WriteLine($"You will find a mushroom level at level {level} in {i} days");
                    }
                }
            }
        }
        bool IsLevelAMushroomLevel(ulong gameID, uint daysPlayed, int level)
        {
            var random = new Random((int)(daysPlayed + (uint)level + (uint)((int)gameID / 2)));
            if (random.NextDouble() < 0.3)
            {
                random.NextDouble();
            }
            if (random.NextDouble() < 0.15 && level > 5 && level != 120) { }
            if (random.NextDouble() < 0.035 && (level >= 80 && level <= 120) && level % 5 != 0)
            {
                return true;
            }
            return false;
        }
    }
}
