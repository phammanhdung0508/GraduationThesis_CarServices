namespace GraduationThesis_CarServices.Enum
{
    public class ServiceGroup
    {
        private ServiceGroup(string value) { Value = value; }

        public string Value { get; private set; }

        public static ServiceGroup PackageCleaningMaintenance { get { return new ServiceGroup("GÓI DỊCH VỤ VỆ SINH + BẢO DƯỠNG"); } }
        public static ServiceGroup PackageExterior { get { return new ServiceGroup("GÓI DỊCH VỤ NGOẠI THẤT"); } }
        public static ServiceGroup PackageInterior { get { return new ServiceGroup("GÓI DỊCH VỤ NỘI THẤT"); } }

        public override string ToString()
        {
            return Value;
        }
    }

    // public class LogCategory
    // {
    //     private LogCategory(string value) { Value = value; }

    //     public string Value { get; private set; }

    //     public static LogCategory Trace { get { return new LogCategory("Trace"); } }
    //     public static LogCategory Debug { get { return new LogCategory("Debug"); } }
    //     public static LogCategory Info { get { return new LogCategory("Info"); } }
    //     public static LogCategory Warning { get { return new LogCategory("Warning"); } }
    //     public static LogCategory Error { get { return new LogCategory("Error"); } }

    //     public override string ToString()
    //     {
    //         return Value;
    //     }
    // }

    // public static void Write(string message, LogCategory logCategory)
    // {
    //     var log = new LogEntry { Message = message };
    //     Logger.Write(log, logCategory.Value);
    // }
    // Logger.Write("This is almost like an enum.", LogCategory.Info);
}

