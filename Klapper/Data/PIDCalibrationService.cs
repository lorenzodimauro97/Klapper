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

    public async Task StartPIDCalibration(int numberOfRuns, string selectedHeater, int targetTemperature, NotificationService Toast)
    {
        CalibrationResult = null;
        pidIsCalibrating = true;
        for (var i = 0; i < numberOfRuns; i++)
        {
            var result = await Api.RunGCode($"PID_CALIBRATE HEATER={selectedHeater} TARGET={targetTemperature}");

            if (!result.Item1)
            {
                ToastNotification.Notificate(Toast, false, badMessage:"Operation Timed out before completion! Please increase the NGINX request timeout for this Moonraker Instance!");
                Reset();
            }
            
            var storedMessages = (await Api.GetGCodeStoredMessages("2")).result.gcode_store.Last();
            if (!storedMessages.message.Contains("=", StringComparison.InvariantCultureIgnoreCase))
                PidList.Add(new PID(storedMessages.message));
        }
        CalculateMeanPID(numberOfRuns);
    }

    private void CalculateMeanPID(int numberOfRuns)
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
        ki /= numberOfRuns;
        kp /= numberOfRuns;
        kd /= numberOfRuns;

        CalibrationResult = new PID
        {
            Ki = ki,
            Kp = kp,
            Kd = kd
        };
    }

    public async Task UpdateConfig(string selectedHeater, NotificationService Toast)
    {
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