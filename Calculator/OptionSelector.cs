using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class OptionSelector<T>
    {
        readonly KeyValuePair<string,T>[] options;
        public OptionSelector(IEnumerable<KeyValuePair<string, T>> options)
        {
            this.options = options.ToArray();
        }
        public T Select()
        {
            while (true)
            {
                var i = 0;
                foreach (var option in options)
                    Console.WriteLine($"{++i}. {option.Key}");

                Console.Write($"Select an option (1-{i}): ");
                var response = Console.ReadLine();
                int selected;
                if (int.TryParse(response, out selected))
                    if (selected > 0 && selected <= options.Length)
                        return options[selected - 1].Value;
            }
        }
    }
    class OptionSelector
    {
        public static OptionSelector<T> Create<T>(IEnumerable<KeyValuePair<string, T>> options) => new OptionSelector<T>(options);
        public static OptionSelector<string> Create(IEnumerable<string> options) => new OptionSelector<string>(options.Select(x => new KeyValuePair<string, string>(x, x)));
        public static OptionSelector<string> Create(params string[] options) => Create(options.AsEnumerable());
        public static OptionSelector<T> Create<T>(IEnumerable<string> optionNames, IEnumerable<T> optionValues)
            => Create(optionNames.Zip(optionValues, (a, b) => new KeyValuePair<string, T>(a, b)));
    }
}
