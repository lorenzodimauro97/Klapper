namespace Klapper.Classes;

    public class Endstop
    {
        public string y { get; set; }
        public string x { get; set; }
        public string z { get; set; }
    }

    public class EndstopRoot
    {
        public Endstop result { get; set; }
    }
