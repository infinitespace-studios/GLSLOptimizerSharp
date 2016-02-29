namespace GLSLOptimizerSharp
{
    public class VariableInfo
    {
        public string Name { get; internal set; }
        public BasicType Type { get; internal set; }
        public Precision Precision { get; internal set; }
        public int VectorSize { get; internal set; }
        public int MatrixSize { get; internal set; }
        public int ArraySize { get; internal set; }
        public int Location { get; internal set; }
    }
}