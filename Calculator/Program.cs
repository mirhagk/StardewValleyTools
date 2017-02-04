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
        }
        static void Main(string[] args)
        {
            var arguments = PowerCommandParser.Parser.ParseArguments<Args>(args);
            if (arguments == null)
                return;
            if (arguments.Shard)
                new PrismaticShardCalculator(new SaveFileManager()).Run(arguments);
        }
    }
}
