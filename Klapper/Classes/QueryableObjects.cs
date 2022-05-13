// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using System.Runtime.Serialization;

namespace Klapper.Classes;

public class Tmc2209Extruder
{
    public double phase_offset_position { get; set; }
    public double hold_current { get; set; }
    public object drv_status { get; set; }
    public int mcu_phase_offset { get; set; }
    public double run_current { get; set; }
    public string uart_pin { get; set; }
    public string uart_address { get; set; }
    public string interpolate { get; set; }
    public int driver_tbl { get; set; }
    public int driver_pwm_ofs { get; set; }
    public int driver_hstrt { get; set; }
    public int driver_toff { get; set; }
    public double sense_resistor { get; set; }
    public int driver_hend { get; set; }
    public int driver_pwm_reg { get; set; }
    public int driver_iholddelay { get; set; }
    public int driver_sgthrs { get; set; }
    public double stealthchop_threshold { get; set; }
    public bool driver_pwm_autoscale { get; set; }
    public bool driver_pwm_autograd { get; set; }
    public int driver_pwm_grad { get; set; }
    public int driver_tpowerdown { get; set; }
    public int driver_pwm_lim { get; set; }
    public int driver_pwm_freq { get; set; }
}

public class PauseResume
{
    public bool is_paused { get; set; }
    public double recover_velocity { get; set; }
}

public class StepperY1
{
    public string position_endstop { get; set; }
    public object full_steps_per_rotation { get; set; }
    public string endstop_pin { get; set; }
    public string rotation_distance { get; set; }
    public string step_pin { get; set; }
    public string position_min { get; set; }
    public string microsteps { get; set; }
    public string homing_speed { get; set; }
    public string dir_pin { get; set; }
    public string position_max { get; set; }
    public string enable_pin { get; set; }
    public bool homing_positive_dir { get; set; }
    public double homing_retract_dist { get; set; }
    public List<object> gear_ratio { get; set; }
    public double second_homing_speed { get; set; }
    public double homing_retract_speed { get; set; }
}

public class HeaterBed
{
    public string control { get; set; }
    public string pid_kp { get; set; }
    public string sensor_pin { get; set; }
    public string pid_kd { get; set; }
    public string heater_pin { get; set; }
    public string sensor_type { get; set; }
    public string max_power { get; set; }
    public string pid_ki { get; set; }
    public string min_temp { get; set; }
    public string max_temp { get; set; }
    public double pullup_resistor { get; set; }
    public double min_extrude_temp { get; set; }
    public double inline_resistor { get; set; }
    public double pwm_cycle_time { get; set; }
    public double smooth_time { get; set; }
    public double temperature { get; set; }
    public double power { get; set; }
    public double target { get; set; }
}

public class VirtualSdcard
{
    public string path { get; set; }
    public double progress { get; set; }
    public int GetProgress => (int)(progress * 100);
    public double file_position { get; set; }
    public bool is_active { get; set; }
    public object file_path { get; set; }
    public double file_size { get; set; }
}

public class StepperZ
{
    public string position_endstop { get; set; }
    public object full_steps_per_rotation { get; set; }
    public string endstop_pin { get; set; }
    public string rotation_distance { get; set; }
    public string step_pin { get; set; }
    public string position_min { get; set; }
    public string microsteps { get; set; }
    public string dir_pin { get; set; }
    public string position_max { get; set; }
    public string enable_pin { get; set; }
    public bool homing_positive_dir { get; set; }
    public double homing_retract_dist { get; set; }
    public List<object> gear_ratio { get; set; }
    public double second_homing_speed { get; set; }
    public double homing_speed { get; set; }
    public double homing_retract_speed { get; set; }
}

public class StepperY
{
    public string position_endstop { get; set; }
    public object full_steps_per_rotation { get; set; }
    public string endstop_pin { get; set; }
    public string rotation_distance { get; set; }
    public string step_pin { get; set; }
    public string position_min { get; set; }
    public string microsteps { get; set; }
    public string homing_speed { get; set; }
    public string dir_pin { get; set; }
    public string position_max { get; set; }
    public string enable_pin { get; set; }
    public bool homing_positive_dir { get; set; }
    public double homing_retract_dist { get; set; }
    public List<object> gear_ratio { get; set; }
    public double second_homing_speed { get; set; }
    public double homing_retract_speed { get; set; }
}

public class StepperX
{
    public string position_endstop { get; set; }
    public object full_steps_per_rotation { get; set; }
    public string endstop_pin { get; set; }
    public string rotation_distance { get; set; }
    public string step_pin { get; set; }
    public string position_min { get; set; }
    public string microsteps { get; set; }
    public string homing_speed { get; set; }
    public string dir_pin { get; set; }
    public string position_max { get; set; }
    public string enable_pin { get; set; }
    public bool homing_positive_dir { get; set; }
    public double homing_retract_dist { get; set; }
    public List<object> gear_ratio { get; set; }
    public double second_homing_speed { get; set; }
    public double homing_retract_speed { get; set; }
}

public class InputShaper
{
    public string shaper_freq_x { get; set; }
    public string shaper_freq_y { get; set; }
    public string shaper_type { get; set; }
    public double damping_ratio_x { get; set; }
    public double damping_ratio_y { get; set; }
    public string shaper_type_x { get; set; }
    public string shaper_type_y { get; set; }
}

public class HomingOverride
{
    public string gcode { get; set; }
    public string set_position_z { get; set; }
    public string axes { get; set; }
}

public class Mcu
{
    public string baud { get; set; }
    public string restart_method { get; set; }
    public string serial { get; set; }
    public double max_stepper_error { get; set; }
    public string mcu_build_versions { get; set; }
    public string mcu_version { get; set; }
    public LastStats last_stats { get; set; }
    public McuConstants mcu_constants { get; set; }
}

public class Printer
{
    public string max_accel { get; set; }
    public string max_z_accel { get; set; }
    public string max_velocity { get; set; }
    public string max_z_velocity { get; set; }
    public string kinematics { get; set; }
    public string max_accel_to_decel { get; set; }
    public double square_corner_velocity { get; set; }
    public double move_flush_time { get; set; }
    public double buffer_time_start { get; set; }
    public double buffer_time_low { get; set; }
    public double buffer_time_high { get; set; }
}

public class TemperatureSensorMcuTemp
{
    public string min_temp { get; set; }
    public string sensor_type { get; set; }
    public string max_temp { get; set; }
    public string sensor_mcu { get; set; }
    public double measured_min_temp { get; set; }
    public double measured_max_temp { get; set; }
    public double temperature { get; set; }
}

public class Fan
{
    public string pin { get; set; }
    public double cycle_time { get; set; }
    public double off_below { get; set; }
    public double shutdown_speed { get; set; }
    public double max_power { get; set; }
    public double kick_start_time { get; set; }
    public bool hardware_pwm { get; set; }
    public double speed { get; set; }
    public object rpm { get; set; }
}

public class Tmc2209StepperZ
{
    public string uart_pin { get; set; }
    public string uart_address { get; set; }
    public string stealthchop_threshold { get; set; }
    public string run_current { get; set; }
    public string interpolate { get; set; }
    public int driver_tbl { get; set; }
    public int driver_pwm_ofs { get; set; }
    public int driver_hstrt { get; set; }
    public int driver_toff { get; set; }
    public double sense_resistor { get; set; }
    public int driver_pwm_reg { get; set; }
    public int driver_iholddelay { get; set; }
    public int driver_pwm_freq { get; set; }
    public bool driver_pwm_autoscale { get; set; }
    public bool driver_pwm_autograd { get; set; }
    public int driver_pwm_grad { get; set; }
    public int driver_hend { get; set; }
    public double hold_current { get; set; }
    public int driver_tpowerdown { get; set; }
    public int driver_pwm_lim { get; set; }
    public int driver_sgthrs { get; set; }
    public double phase_offset_position { get; set; }
    public object drv_status { get; set; }
    public int mcu_phase_offset { get; set; }
}

public class Tmc2209StepperX
{
    public string uart_pin { get; set; }
    public string uart_address { get; set; }
    public string stealthchop_threshold { get; set; }
    public string run_current { get; set; }
    public string interpolate { get; set; }
    public int driver_pwm_ofs { get; set; }
    public int driver_hstrt { get; set; }
    public int driver_toff { get; set; }
    public double sense_resistor { get; set; }
    public int driver_hend { get; set; }
    public int driver_pwm_reg { get; set; }
    public int driver_iholddelay { get; set; }
    public double hold_current { get; set; }
    public int driver_tbl { get; set; }
    public bool driver_pwm_autograd { get; set; }
    public int driver_pwm_grad { get; set; }
    public int driver_sgthrs { get; set; }
    public bool driver_pwm_autoscale { get; set; }
    public int driver_tpowerdown { get; set; }
    public int driver_pwm_lim { get; set; }
    public int driver_pwm_freq { get; set; }
    public double phase_offset_position { get; set; }
    public object drv_status { get; set; }
    public int mcu_phase_offset { get; set; }
}

public class Tmc2209StepperY
{
    public string uart_pin { get; set; }
    public string uart_address { get; set; }
    public string stealthchop_threshold { get; set; }
    public string run_current { get; set; }
    public string interpolate { get; set; }
    public int driver_pwm_ofs { get; set; }
    public int driver_hstrt { get; set; }
    public int driver_sgthrs { get; set; }
    public double sense_resistor { get; set; }
    public int driver_hend { get; set; }
    public int driver_pwm_reg { get; set; }
    public int driver_iholddelay { get; set; }
    public bool driver_pwm_autoscale { get; set; }
    public int driver_tbl { get; set; }
    public bool driver_pwm_autograd { get; set; }
    public int driver_pwm_grad { get; set; }
    public double hold_current { get; set; }
    public int driver_tpowerdown { get; set; }
    public int driver_toff { get; set; }
    public int driver_pwm_lim { get; set; }
    public int driver_pwm_freq { get; set; }
    public double phase_offset_position { get; set; }
    public object drv_status { get; set; }
    public int mcu_phase_offset { get; set; }
}

public class BedScrews
{
    public string screw2 { get; set; }
    public string screw3 { get; set; }
    public string screw1 { get; set; }
    public string screw4 { get; set; }
    public string screw1_name { get; set; }
    public double probe_speed { get; set; }
    public string screw4_name { get; set; }
    public double probe_height { get; set; }
    public double horizontal_move_z { get; set; }
    public string screw2_name { get; set; }
    public double speed { get; set; }
    public string screw3_name { get; set; }
}

public class DisplayStatus
{
    public double progress { get; set; }
    public object message { get; set; }
}

public class GcodeMacroCANCELPRINT
{
    public string rename_existing { get; set; }
    public string description { get; set; }
    public string gcode { get; set; }
}

public class Tmc2209StepperY1
{
    public string uart_pin { get; set; }
    public string uart_address { get; set; }
    public string stealthchop_threshold { get; set; }
    public string run_current { get; set; }
    public string interpolate { get; set; }
    public int driver_pwm_ofs { get; set; }
    public int driver_hstrt { get; set; }
    public int driver_toff { get; set; }
    public double sense_resistor { get; set; }
    public int driver_hend { get; set; }
    public int driver_pwm_reg { get; set; }
    public int driver_iholddelay { get; set; }
    public bool driver_pwm_autoscale { get; set; }
    public int driver_tbl { get; set; }
    public bool driver_pwm_autograd { get; set; }
    public int driver_pwm_grad { get; set; }
    public int driver_sgthrs { get; set; }
    public double hold_current { get; set; }
    public int driver_tpowerdown { get; set; }
    public int driver_pwm_lim { get; set; }
    public int driver_pwm_freq { get; set; }
    public double phase_offset_position { get; set; }
    public object drv_status { get; set; }
    public int mcu_phase_offset { get; set; }
}

public class Extruder
{
    public string control { get; set; }
    public string pid_kp { get; set; }
    public string pressure_advance { get; set; }
    public string sensor_pin { get; set; }
    public string nozzle_diameter { get; set; }
    public string max_extrude_cross_section { get; set; }
    public object full_steps_per_rotation { get; set; }
    public string rotation_distance { get; set; }
    public string gear_ratio { get; set; }
    public string max_extrude_only_distance { get; set; }
    public string sensor_type { get; set; }
    public string step_pin { get; set; }
    public string min_temp { get; set; }
    public string pid_kd { get; set; }
    public string microsteps { get; set; }
    public string pid_ki { get; set; }
    public string filament_diameter { get; set; }
    public string dir_pin { get; set; }
    public string max_temp { get; set; }
    public string heater_pin { get; set; }
    public string enable_pin { get; set; }
    public double pullup_resistor { get; set; }
    public double max_extrude_only_velocity { get; set; }
    public double pwm_cycle_time { get; set; }
    public double instantaneous_corner_velocity { get; set; }
    public double pressure_advance_smooth_time { get; set; }
    public double smooth_time { get; set; }
    public double inline_resistor { get; set; }
    public double max_power { get; set; }
    public double max_extrude_only_accel { get; set; }
    public double min_extrude_temp { get; set; }
    public double target { get; set; }
    public double power { get; set; }
    public bool can_extrude { get; set; }
    public double temperature { get; set; }
}

public class Config
{
    [DataMember(Name = "tmc2209 extruder")]
    public Tmc2209Extruder Tmc2209Extruder { get; set; }

    public PauseResume pause_resume { get; set; }
    public StepperY1 stepper_y1 { get; set; }
    public HeaterBed heater_bed { get; set; }
    public VirtualSdcard virtual_sdcard { get; set; }
    public StepperZ stepper_z { get; set; }
    public StepperY stepper_y { get; set; }
    public StepperX stepper_x { get; set; }
    public InputShaper input_shaper { get; set; }
    public HomingOverride homing_override { get; set; }
    public Mcu mcu { get; set; }
    public Printer printer { get; set; }

    [DataMember(Name = "temperature_sensor mcu_temp")]
    public TemperatureSensorMcuTemp TemperatureSensorMcuTemp { get; set; }

    public Fan fan { get; set; }

    [DataMember(Name = "tmc2209 stepper_z")]
    public Tmc2209StepperZ Tmc2209StepperZ { get; set; }

    [DataMember(Name = "tmc2209 stepper_x")]
    public Tmc2209StepperX Tmc2209StepperX { get; set; }

    [DataMember(Name = "tmc2209 stepper_y")]
    public Tmc2209StepperY Tmc2209StepperY { get; set; }

    public BedScrews bed_screws { get; set; }
    public DisplayStatus display_status { get; set; }

    [DataMember(Name = "gcode_macro CANCEL_PRINT")]
    public GcodeMacroCANCELPRINT GcodeMacroCANCELPRINT { get; set; }

    [DataMember(Name = "tmc2209 stepper_y1")]
    public Tmc2209StepperY1 Tmc2209StepperY1 { get; set; }

    public Extruder extruder { get; set; }
}

public class ForceMove
{
    public bool enable_force_move { get; set; }
}

public class GcodeMacroCancelPrint2
{
    public string rename_existing { get; set; }
    public string description { get; set; }
    public string gcode { get; set; }
}

public class VerifyHeaterExtruder
{
    public double max_error { get; set; }
    public double check_gain_time { get; set; }
    public double hysteresis { get; set; }
    public double heating_gain { get; set; }
}

public class IdleTimeout
{
    public string gcode { get; set; }
    public double timeout { get; set; }
    public string state { get; set; }
    public double printing_time { get; set; }
}

public class VerifyHeaterHeaterBed
{
    public double heating_gain { get; set; }
    public double max_error { get; set; }
    public double check_gain_time { get; set; }
    public double hysteresis { get; set; }
}

public class Settings
{
    [DataMember(Name = "tmc2209 extruder")]
    public Tmc2209Extruder Tmc2209Extruder { get; set; }

    public ForceMove force_move { get; set; }
    public PauseResume pause_resume { get; set; }

    //[DataMember(Name="gcode_macro cancel_print")]
    //public GcodeMacroCancelPrint GcodeMacroCancelPrint { get; set; }
    public StepperY1 stepper_y1 { get; set; }
    public HeaterBed heater_bed { get; set; }
    public VirtualSdcard virtual_sdcard { get; set; }

    [DataMember(Name = "verify_heater extruder")]
    public VerifyHeaterExtruder VerifyHeaterExtruder { get; set; }

    public StepperZ stepper_z { get; set; }
    public StepperY stepper_y { get; set; }
    public StepperX stepper_x { get; set; }
    public InputShaper input_shaper { get; set; }
    public HomingOverride homing_override { get; set; }
    public Mcu mcu { get; set; }
    public Printer printer { get; set; }

    [DataMember(Name = "temperature_sensor mcu_temp")]
    public TemperatureSensorMcuTemp TemperatureSensorMcuTemp { get; set; }

    public IdleTimeout idle_timeout { get; set; }
    public Fan fan { get; set; }

    [DataMember(Name = "tmc2209 stepper_z")]
    public Tmc2209StepperZ Tmc2209StepperZ { get; set; }

    [DataMember(Name = "tmc2209 stepper_x")]
    public Tmc2209StepperX Tmc2209StepperX { get; set; }

    [DataMember(Name = "tmc2209 stepper_y")]
    public Tmc2209StepperY Tmc2209StepperY { get; set; }

    public BedScrews bed_screws { get; set; }

    [DataMember(Name = "verify_heater heater_bed")]
    public VerifyHeaterHeaterBed VerifyHeaterHeaterBed { get; set; }

    [DataMember(Name = "tmc2209 stepper_y1")]
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

public class LastQuery
{
}

public class QueryEndstops
{
    public LastQuery last_query { get; set; }
}

public class Webhooks
{
    public string state { get; set; }
    public string state_message { get; set; }
}

public class GcodeMove
{
    public List<double> homing_origin { get; set; }
    public double speed_factor { get; set; }
    public List<double> gcode_position { get; set; }
    public bool absolute_extrude { get; set; }
    public bool absolute_coordinates { get; set; }
    public List<double> position { get; set; }
    public double speed { get; set; }
    public double extrude_factor { get; set; }
}

public class LastStats
{
    public int retransmit_seq { get; set; }
    public int receive_seq { get; set; }
    public int send_seq { get; set; }
    public int bytes_invalid { get; set; }
    public double rto { get; set; }
    public int freq { get; set; }
    public double srtt { get; set; }
    public int stalled_bytes { get; set; }
    public int bytes_write { get; set; }
    public double mcu_awake { get; set; }
    public double mcu_task_avg { get; set; }
    public double rttvar { get; set; }
    public double mcu_task_stddev { get; set; }
    public int bytes_read { get; set; }
    public int bytes_retransmit { get; set; }
    public int ready_bytes { get; set; }
}

public class McuConstants
{
    public string BUS_PINS_i2c1 { get; set; }
    public string BUS_PINS_i2c2 { get; set; }
    public string BUS_PINS_i2c1a { get; set; }
    public string INITIAL_PINS { get; set; }
    public string RESERVE_PINS_serial { get; set; }
    public string BUS_PINS_spi1a { get; set; }
    public int STATS_SUMSQ_BASE { get; set; }
    public int RECEIVE_WINDOW { get; set; }
    public int STEPPER_BOTH_EDGE { get; set; }
    public int SERIAL_BAUD { get; set; }
    public int ADC_MAX { get; set; }
    public string BUS_PINS_spi3 { get; set; }
    public string BUS_PINS_spi2 { get; set; }
    public string BUS_PINS_spi1 { get; set; }
    public int PWM_MAX { get; set; }
    public string MCU { get; set; }
    public int CLOCK_FREQ { get; set; }
}

public class Heaters
{
    public List<string> available_sensors { get; set; }
    public List<string> available_heaters { get; set; }
}

public class MotionReport
{
    public List<double> live_position { get; set; }
    public List<string> steppers { get; set; }
    public double live_velocity { get; set; }
    public double live_extruder_velocity { get; set; }
    public List<string> trapq { get; set; }
}

public class SystemStats
{
    public double sysload { get; set; }
    public int memavail { get; set; }
    public double cputime { get; set; }
}

public class PrintStats
{
    public double print_duration { get; set; }
    public double total_duration { get; set; }
    public double filament_used { get; set; }
    public string filename { get; set; }
    public string state { get; set; }
    public string message { get; set; }
}

public class Toolhead
{
    public double square_corner_velocity { get; set; }
    public double max_accel { get; set; }
    public string homed_axes { get; set; }
    public double estimated_print_time { get; set; }
    public double max_velocity { get; set; }
    public double print_time { get; set; }
    public double max_accel_to_decel { get; set; }
    public List<double> axis_minimum { get; set; }
    public int stalls { get; set; }
    public List<double> axis_maximum { get; set; }
    public List<double> position { get; set; }
    public string extruder { get; set; }
}

public class QueryResultStatus
{
    [DataMember(Name = "tmc2209 extruder")]
    public Tmc2209Extruder Tmc2209Extruder { get; set; }

    public PauseResume pause_resume { get; set; }
    public Configfile configfile { get; set; }
    public QueryEndstops query_endstops { get; set; }
    public Webhooks webhooks { get; set; }
    public VirtualSdcard virtual_sdcard { get; set; }
    public HeaterBed heater_bed { get; set; }
    public GcodeMove gcode_move { get; set; }
    public Mcu mcu { get; set; }
    public Heaters heaters { get; set; }

    [DataMember(Name = "temperature_sensor mcu_temp")]
    public TemperatureSensorMcuTemp TemperatureSensorMcuTemp { get; set; }

    public MotionReport motion_report { get; set; }
    public SystemStats system_stats { get; set; }
    public IdleTimeout idle_timeout { get; set; }
    public Fan fan { get; set; }

    [DataMember(Name = "tmc2209 stepper_z")]
    public Tmc2209StepperZ Tmc2209StepperZ { get; set; }

    [DataMember(Name = "tmc2209 stepper_x")]
    public Tmc2209StepperX Tmc2209StepperX { get; set; }

    [DataMember(Name = "tmc2209 stepper_y")]
    public Tmc2209StepperY Tmc2209StepperY { get; set; }

    public DisplayStatus display_status { get; set; }

    [DataMember(Name = "gcode_macro CANCEL_PRINT")]
    public GcodeMacroCANCELPRINT GcodeMacroCANCELPRINT { get; set; }

    public PrintStats print_stats { get; set; }
    public Toolhead toolhead { get; set; }

    [DataMember(Name = "tmc2209 stepper_y1")]
    public Tmc2209StepperY1 Tmc2209StepperY1 { get; set; }

    public Extruder extruder { get; set; }
}

public class MoonrakerQueryResult
{
    public QueryResultStatus status { get; set; }
    public double eventtime { get; set; }
}

public class MoonrakerQueryResultObject
{
    public MoonrakerQueryResult result { get; set; }
}