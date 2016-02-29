using System;
using System.Runtime.InteropServices;
using GLSLOptimizerSharp.Util;

namespace GLSLOptimizerSharp
{
    public class GLSLOptimizer : IDisposable
    {
        private readonly IntPtr _ctx;

        public GLSLOptimizer(Target target)
        {
            _ctx = NativeMethods.glslopt_initialize(target);
            if (_ctx == IntPtr.Zero)
                throw new Exception("Failed to create context");
        }

        public OptimizationResult Optimize(ShaderType shaderType, string source, OptimizationOptions options)
        {
            var sourcePtr = Marshal.StringToHGlobalAnsi(source);
            var shader = NativeMethods.glslopt_optimize(_ctx, shaderType, sourcePtr, (uint) options);
            if (shader == IntPtr.Zero)
                throw new Exception("Failed to create shader");

            try
            {
                if (!NativeMethods.glslopt_get_status(shader))
                {
                    var log = NativeMethods.glslopt_get_log(shader);
                    var info = Marshal.PtrToStringAnsi(log);
                    throw new Exception("Error parsing shader: " + info);
                }

                var outputPtr = NativeMethods.glslopt_get_output(shader);
                var output = Marshal.PtrToStringAnsi(outputPtr);

                var result = new OptimizationResult();
                result.OutputCode = output;

                var inputCount = NativeMethods.glslopt_shader_get_input_count(shader);
                for (int i = 0; i < inputCount; i++)
                {
                    IntPtr outName;
                    int outLocation, outArraySize, outMatSize, outVecSize;
                    BasicType outType;
                    Precision outPrec;

                    NativeMethods.glslopt_shader_get_input_desc(shader, i,
                        out outName,
                        out outType,
                        out outPrec,
                        out outVecSize,
                        out outMatSize,
                        out outArraySize,
                        out outLocation);
                    var name = Marshal.PtrToStringAnsi(outName);

                    result.Inputs.Add(new VariableInfo
                    {
                        Name = name,
                        Type = outType,
                        Precision = outPrec,
                        VectorSize = outVecSize,
                        MatrixSize = outMatSize,
                        ArraySize = outArraySize,
                        Location = outLocation
                    });
                }

                var uniformCount = NativeMethods.glslopt_shader_get_uniform_count(shader);
                for (int i = 0; i < uniformCount; i++)
                {
                    IntPtr outName;
                    int outLocation, outArraySize, outMatSize, outVecSize;
                    BasicType outType;
                    Precision outPrec;

                    NativeMethods.glslopt_shader_get_uniform_desc(shader, i,
                        out outName,
                        out outType,
                        out outPrec,
                        out outVecSize,
                        out outMatSize,
                        out outArraySize,
                        out outLocation);
                    var name = Marshal.PtrToStringAnsi(outName);

                    result.Uniforms.Add(new VariableInfo
                    {
                        Name = name,
                        Type = outType,
                        Precision = outPrec,
                        VectorSize = outVecSize,
                        MatrixSize = outMatSize,
                        ArraySize = outArraySize,
                        Location = outLocation
                    });
                }

                return result;
            }
            finally
            {
                NativeMethods.glslopt_shader_delete(shader);
            }
        }

        public void Dispose()
        {
            NativeMethods.glslopt_cleanup(_ctx);
        }
	}
}

