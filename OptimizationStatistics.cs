namespace GLSLOptimizerSharp
{
    public class OptimizationStatistics
    {
        public int ApproxMathInstructions { get; internal set; }
        public int ApproxTextureInstructions { get; internal set; }
        public int ApproxControlFlowInstructions { get; internal set; }
    }
}