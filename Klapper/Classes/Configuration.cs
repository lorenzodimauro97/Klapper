using System.Text.RegularExpressions;

namespace Klapper.Classes;

public class Configuration
{
    public List<ConfigurationSection> Sections { get; set; } = new();
    public string ConfigurationString { get; set; }
    
    public bool RestartPrinterOnSave { get; set; }

    public Configuration(string configurationString)
    {
        ConfigurationString = configurationString;
        var configurationArray = configurationString.Split("\n");

        List<string> section = new();
        var sectionTitle = string.Empty;

        foreach (var row in configurationArray)
        {
            if (row.Contains('[', StringComparison.InvariantCultureIgnoreCase))
            {
                sectionTitle = row.Replace("[", "").Replace("]", "");
                continue;
            }

            if (string.IsNullOrEmpty(row) || row == "\n")
            {
                if (section.Count == 0) continue;
                Sections.Add(new ConfigurationSection(section, sectionTitle));
                section.Clear();
                sectionTitle = string.Empty;
            }

            if (!string.IsNullOrEmpty(sectionTitle))
            {
                section.Add(row);
            }
        }
    }

    public void UpdateConfiguration(ConfigurationRow row, string updatedRow)
    {
        ConfigurationString = ConfigurationString.Replace(row.GetString(), row.GetString(updatedRow));
    }
}

public class ConfigurationSection
{
    public string Title { get; set; }
    public List<ConfigurationRow> Rows { get; set; } = new();

    public ConfigurationSection(List<string> rows, string title)
    {
        Title = title;

        foreach (var row in rows)
        {
            var separator = string.Empty;

            if (row.Contains(':', StringComparison.InvariantCultureIgnoreCase))
                separator = ":";

            else if (row.Contains('=', StringComparison.InvariantCultureIgnoreCase))
                separator = "=";
            else continue;

            var splitRow = row.Split(separator);
            Rows.Add(new ConfigurationRow(splitRow[0], splitRow[1], separator));
        }
    }
}

public class ConfigurationRow
{
    public ConfigurationRow(string description, string value, string separator)
    {
        Description = description;
        Value = value;
        Separator = separator;
    }

    public string Description { get; set; }
    public string Value { get; set; }
    public string Separator { get; set; }

    public string GetString()
    {
        return $"{Description}{Separator}{Value}";
    }
    
    public string GetString(string updatedValue)
    {
        return $"{Description}{Separator}{updatedValue}";
    }
}