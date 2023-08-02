namespace GraduationThesis_CarServices.Enum
{
    public class MechanicLevel
    {
        private MechanicLevel(string value) { Value = value; }

        public string Value { get; private set; }

        public static MechanicLevel Level1 { get { return new MechanicLevel("Trình độ: Bậc thợ 1/3 (Sơ cấp)"); } }
        public static MechanicLevel Level2 { get { return new MechanicLevel("Trình độ: Bậc thợ 2/3 (Trung cấp)"); } }
        public static MechanicLevel Level3 { get { return new MechanicLevel("Trình độ: Bậc thợ 3/3 (Chuyên nghiệp)"); } }

        public override string ToString()
        {
            return Value;
        }
    }
}