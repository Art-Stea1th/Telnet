using System.Windows.Data;


namespace ASD.Telnet.Converters {

    [ValueConversion(typeof(bool), typeof(bool))]
    public sealed class BooleanInverseConverter : BinaryLogicBooleanConverter<bool> {
        public BooleanInverseConverter() : base(false, true) { }
    }
}