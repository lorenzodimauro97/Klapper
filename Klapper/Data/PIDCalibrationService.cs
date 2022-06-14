using System.Globalization;
using System.Text;
using Klapper.Classes;
using Klapper.Shared;
using Radzen;

namespace Klapper.Data;

public class PIDCalibrationService
{
    public PIDCalibrationService(MoonrakerApiService api)
    {
        Api = api;
    }

    public MoonrakerApiService Api { get; set; }

    public List<string> Heaters { get; set; }
    public bool pidIsCalibrating;

    public List<PID> PidList { get; set; } = new();
    public PID CalibrationResult;

    public string selectedHeater;
    public int targetTemperature;
    public int numberOfRuns;

    public void CalculateMeanPID()
    {
        float ki = 0;
        float kp = 0;
        float kd = 0;
        foreach (var pid in PidList)
        {
            ki += pid.Ki;
            kp += pid.Kp;
            kd += pid.Kd;
        }
        ki /= PidList.Count;
        kp /= PidList.Count;
        kd /= PidList.Count;

        CalibrationResult = new PID
        {
            Ki = ki,
            Kp = kp,
            Kd = kd
        };
    }

    public async Task UpdateConfig(string selectedHeater, NotificationService Toast)
    {
        await Api.RunGCode("SAVE_CONFIG");  //This guarantees there is at least one baseline for us to find inside the printer.cfg file to edit
        var configFile = Encoding.UTF8.GetString(await Api.GetFile("printer.cfg", "config"));
        var splitConfigFile = configFile.Split("\n");

        var index = Array.FindIndex(splitConfigFile, x => x.Contains($"#*# [{selectedHeater}]"));

        splitConfigFile[index + 2] = $"#*# pid_kp = {CalibrationResult.Kp.ToString(CultureInfo.InvariantCulture)}\r";
        splitConfigFile[index + 3] = $"#*# pid_ki = {CalibrationResult.Ki.ToString(CultureInfo.InvariantCulture)}\r";
        splitConfigFile[index + 4] = $"#*# pid_kd = {CalibrationResult.Kd.ToString(CultureInfo.InvariantCulture)}\r";

        var updatedConfigFile = splitConfigFile.Aggregate(string.Empty, (current, line) => current + (line + "\n"));

        await UploadFiles(Encoding.UTF8.GetBytes(updatedConfigFile), "printer.cfg", "config", Toast);
    }

    private async Task UploadFiles(byte[] getBytes, string fileName, string root, NotificationService Toast)
    {
        var result = await Api.UploadFile(getBytes, fileName, root);
        ToastNotification.Notificate(Toast, result.Item1, $"PID Calibration Settings saved successfully!", $"Failed to Upload {fileName}, Error: {result.Item2}");
        Reset();
    }

    public void Reset()
    {
        CalibrationResult = null;
        PidList.Clear();
        pidIsCalibrating = false;
    }
    
}