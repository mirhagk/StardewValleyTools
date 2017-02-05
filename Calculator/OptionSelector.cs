using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class InputHelper
    {
        public int GetInteger(string message, int max = int.MaxValue, int min = 0, int? defaultNum = null)
        {
            while (true)
            {
                Console.WriteLine(message);
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) && defaultNum.HasValue)
                    return defaultNum.Value;
                int selected;
                if (int.TryParse(input, out selected))
                    if (selected >= min && selected <= max)
                        return selected;
            }
        }
    }
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
        public static OptionCreator<T> Create<T>(string name, T value) => new OptionCreator<T>().AddOption(name, value);
    }
    class OptionCreator<T>
    {
        List<KeyValuePair<string, T>> options = new List<KeyValuePair<string, T>>();
        public OptionCreator<T> AddOption(string name, T value)
        {
            options.Add(new KeyValuePair<string, T>(name, value));
            return this;
        }
        public T Select() => OptionSelector.Create(options).Select();
    }
    class OptionCreator
    {
    }
}
