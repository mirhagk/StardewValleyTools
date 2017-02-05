using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        public class Args
        {
            public bool Shard { get; set; }
            public bool Mushroom { get; set; }
        }
        static void Main(string[] args)
        {
            var arguments = PowerCommandParser.Parser.ParseArguments<Args>(args);
            if (arguments == null)
                return;
            if (arguments.Shard)
                RunShard(arguments);
            else if (arguments.Mushroom)
                RunMushroom(arguments);
            else
            {
                OptionSelector.Create<Action<Args>>("Calculate when you'll find prismatic shards", RunShard)
                    .AddOption("Calculate when you'll find a mushroom level in the mines", RunMushroom)
                    .Select()(arguments);
            }
        }
        static void RunShard(Args args)=> new PrismaticShardCalculator(new SaveFileManager()).Run(args);
        static void RunMushroom(Args args) => new MushroomLevelCalculator(new SaveFileManager()).Run(args);
    }
}
