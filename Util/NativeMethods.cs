using System;
using System.Runtime.InteropServices;

namespace GLSLOptimizerSharp.Util
{
    /*
 Main GLSL optimizer interface.
 See ../../README.md for more instructions.

 General usage:

 ctx = glslopt_initialize();
 for (lots of shaders) {
   shader = glslopt_optimize (ctx, shaderType, shaderSource, options);
   if (glslopt_get_status (shader)) {
     newSource = glslopt_get_output (shader);
   } else {
     errorLog = glslopt_get_log (shader);
   }
   glslopt_shader_delete (shader);
 }
 glslopt_cleanup (ctx);
*/
    internal static class NativeMethods
    {
        const string DllName = "glsl_optimizer.dll";

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern IntPtr glslopt_initialize(Target target);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern void glslopt_cleanup(IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern IntPtr glslopt_optimize(IntPtr ctx,
            ShaderType type,
            IntPtr shaderSource,
            uint options);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern bool glslopt_get_status(IntPtr shader);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern IntPtr glslopt_get_log(IntPtr shader);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern IntPtr glslopt_get_output(IntPtr shader);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern void glslopt_shader_delete(IntPtr shader);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern int glslopt_shader_get_input_count(IntPtr shader);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern void glslopt_shader_get_input_desc(IntPtr shader,
            int index,
            out IntPtr outName,
            out BasicType outType,
            out Precision outPrec,
            out int outVecSize,
            out int outMatSize,
            out int outArraySize,
            out int outLocation);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern int glslopt_shader_get_uniform_count(IntPtr shader);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern int glslopt_shader_get_uniform_total_size(IntPtr shader);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern void glslopt_shader_get_uniform_desc(IntPtr shader,
            int index,
            out IntPtr outName,
            out BasicType outType,
            out Precision outPrec,
            out int outVecSize,
            out int outMatSize,
            out int outArraySize,
            out int outLocation);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern int glslopt_shader_get_texture_count(IntPtr shader);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern void glslopt_shader_get_texture_desc(IntPtr shader,
            int index,
            out IntPtr outName,
            out BasicType outType,
            out Precision outPrec,
            out int outVecSize,
            out int outMatSize,
            out int outArraySize,
            out int outLocation);

        /*
		void glslopt_set_max_unroll_iterations (glslopt_ctx* ctx, unsigned iterations);

		const char* glslopt_get_raw_output (glslopt_shader* shader);

        */

        // Get *very* approximate shader stats:
        // Number of math, texture and flow control instructions.
        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern void glslopt_shader_get_stats(IntPtr shader, out int approxMath, out int approxTex, out int approxFlow);
    }
}