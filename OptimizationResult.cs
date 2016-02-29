using System.Collections.Generic;

namespace GLSLOptimizerSharp
{
    public class OptimizationResult
    {
        public string OutputCode { get; internal set; }
        public List<VariableInfo> Inputs { get; private set; }
        public List<VariableInfo> Uniforms { get; private set; }
        public OptimizationStatistics Statistics { get; private set; }

        public OptimizationResult()
        {
            Inputs = new List<VariableInfo>();
            Uniforms = new List<VariableInfo>();
            Statistics = new OptimizationStatistics();
        }
    }
}