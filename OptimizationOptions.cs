namespace GLSLOptimizerSharp
{
    // Options flags for glsl_optimize, based on glslopt_options
    public enum OptimizationOptions
    {
        None = 0,

        SkipPreprocessor = (1 << 0), // Skip preprocessing shader source. Saves some time if you know you don't need it.
        NotFullShader = (1 << 1), // Passed shader is not the full shader source. This makes some optimizations weaker.
    }
}