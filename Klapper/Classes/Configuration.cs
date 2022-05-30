using System.Text.RegularExpressions;
using Newtonsoft.Json;

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

    public class Config
    {
        [JsonProperty("tmc2209 extruder")]
        public Tmc2209Extruder Tmc2209Extruder { get; set; }
        public PauseResume pause_resume { get; set; }
        public StepperY1 stepper_y1 { get; set; }
        public HeaterBed heater_bed { get; set; }
        public VirtualSdcard virtual_sdcard { get; set; }
        public StepperZ stepper_z { get; set; }
        public StepperY stepper_y { get; set; }
        public StepperX stepper_x { get; set; }
        public InputShaper input_shaper { get; set; }
        public Mcu mcu { get; set; }
        public Printer printer { get; set; }

        [JsonProperty("temperature_sensor mcu_temp")]
        public TemperatureSensorMcuTemp TemperatureSensorMcuTemp { get; set; }
        public Fan fan { get; set; }

        [JsonProperty("tmc2209 stepper_z")]
        public Tmc2209StepperZ Tmc2209StepperZ { get; set; }

        [JsonProperty("tmc2209 stepper_x")]
        public Tmc2209StepperX Tmc2209StepperX { get; set; }

        [JsonProperty("tmc2209 stepper_y")]
        public Tmc2209StepperY Tmc2209StepperY { get; set; }
        public BedScrews bed_screws { get; set; }

        [JsonProperty("tmc2209 stepper_y1")]
        public Tmc2209StepperY1 Tmc2209StepperY1 { get; set; }
        public Extruder extruder { get; set; }
    }

    public class Configfile
    {
        public List<object> warnings { get; set; }
        public Config config { get; set; }
        public Settings settings { get; set; }
        public bool save_config_pending { get; set; }
    }














