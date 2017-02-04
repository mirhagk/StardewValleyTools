using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class SaveFile
    {
        public string Name { get; set; }
        public int UniqueID { get; set; }
        public string Date { get; set; }
        public int GeodesCracked { get; set; }
    }
    class SaveFileManager
    {
        public IEnumerable<SaveFile> GetSaveFiles()
        {
            var savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StardewValley", "saves");
            foreach(var folder in Directory.EnumerateDirectories(savePath))
            {
                var saveName = Path.GetFileName(folder);
                var data = File.ReadAllText(Path.Combine(folder, saveName));
                yield return new SaveFile
                {
                    Name = saveName.Split('_')[0],
                    UniqueID = int.Parse(saveName.Split('_')[1]),
                    Date = GetDataByElement(data, "dateStringForSaveGame"),
                    GeodesCracked = int.Parse(GetDataByElement(data, "geodesCracked"))
                };
            }
        }
        public SaveFile PromptToSelectSave()
        {
            while (true)
            {
                var saves = GetSaveFiles().ToList();
                var i = 0;
                foreach (var save in saves)
                {
                    Console.WriteLine($"{++i}. {save.Name} - {save.Date}");
                }
                Console.Write($"Choose which save file (1-{i}): ");
                var response = Console.ReadLine();
                int selected;
                if (int.TryParse(response, out selected))
                    if (selected > 0 && selected <= saves.Count)
                        return saves[selected - 1];
            }
        }
        string GetDataByElement(string data, string elementName)
        {
            var start = data.IndexOf($"<{elementName}>");
            start += elementName.Length + 2;
            var end = data.IndexOf($"</{elementName}>", start);
            return data.Substring(start, end - start);
        }
    }
}
