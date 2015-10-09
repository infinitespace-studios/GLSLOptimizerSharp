using System;
using System.Runtime.InteropServices;

namespace GLSLOptomizerSharp
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
	public static class GLSLOptimizer {
		
		struct glslopt_shader {}
		struct glslopt_ctx {}

		public enum glslopt_shader_type {
			kGlslOptShaderVertex = 0,
			kGlslOptShaderFragment,
		};

		// Options flags for glsl_optimize
		public enum glslopt_options {
			kGlslOptionSkipPreprocessor = (1<<0), // Skip preprocessing shader source. Saves some time if you know you don't need it.
			kGlslOptionNotFullShader = (1<<1), // Passed shader is not the full shader source. This makes some optimizations weaker.
		};

		// Optimizer target language
		public enum glslopt_target {
			kGlslTargetOpenGL = 0,
			kGlslTargetOpenGLES20 = 1,
			kGlslTargetOpenGLES30 = 2,
			kGlslTargetMetal = 3,
		};

		// Type info
		public enum glslopt_basic_type {
			kGlslTypeFloat = 0,
			kGlslTypeInt,
			kGlslTypeBool,
			kGlslTypeTex2D,
			kGlslTypeTex3D,
			kGlslTypeTexCube,
			kGlslTypeOther,
			kGlslTypeCount
		};
		public enum glslopt_precision {
			kGlslPrecHigh = 0,
			kGlslPrecMedium,
			kGlslPrecLow,
			kGlslPrecCount
		};

		const string DllName = "glsl_optimizer.dll";

		[DllImport(DllName,CharSet = CharSet.Ansi)]
		public static extern IntPtr glslopt_initialize (glslopt_target target);

		[DllImport(DllName,CharSet = CharSet.Ansi)]
		public static extern void glslopt_cleanup (IntPtr ctx);

		[DllImport (DllName,CharSet = CharSet.Ansi)]
		public static extern IntPtr glslopt_optimize (IntPtr ctx,
			glslopt_shader_type type,
			IntPtr shaderSource,
			uint options);

		[DllImport (DllName,CharSet = CharSet.Ansi)]
		public static extern bool glslopt_get_status (IntPtr shader);

		[DllImport(DllName,CharSet = CharSet.Ansi)]
		public static extern IntPtr glslopt_get_log (IntPtr shader);

		[DllImport(DllName, CharSet = CharSet.Ansi)]
		public static extern IntPtr glslopt_get_output (IntPtr shader);

		[DllImport(DllName,CharSet = CharSet.Ansi)]
		public static extern void glslopt_shader_delete (IntPtr shader);

		[DllImport(DllName,CharSet = CharSet.Ansi)]
		public static extern int glslopt_shader_get_input_count (IntPtr shader);

		[DllImport(DllName,CharSet = CharSet.Ansi)]
		public static extern void glslopt_shader_get_input_desc (IntPtr shader,
			int index,
			out IntPtr outName,
			out glslopt_basic_type outType,
			out glslopt_precision outPrec,
			out int outVecSize,
			out int outMatSize,
			out int outArraySize,
			out int outLocation);

		[DllImport(DllName,CharSet = CharSet.Ansi)]
		public static extern int glslopt_shader_get_uniform_count (IntPtr shader);

		[DllImport(DllName,CharSet = CharSet.Ansi)]
		public static extern int glslopt_shader_get_uniform_total_size (IntPtr shader);

		[DllImport(DllName,CharSet = CharSet.Ansi)]
		public static extern void glslopt_shader_get_uniform_desc (IntPtr shader,
			int index,
			out IntPtr outName,
			out glslopt_basic_type outType,
			out glslopt_precision outPrec,
			out int outVecSize,
			out int outMatSize,
			out int outArraySize,
			out int outLocation);

		[DllImport(DllName,CharSet = CharSet.Ansi)]
		public static extern int glslopt_shader_get_texture_count (IntPtr shader);

		[DllImport(DllName,CharSet = CharSet.Ansi)]
		public static extern void glslopt_shader_get_texture_desc (IntPtr shader,
			int index,
			out IntPtr outName,
			out glslopt_basic_type outType,
			out glslopt_precision outPrec,
			out int outVecSize,
			out int outMatSize,
			out int outArraySize,
			out int outLocation);
		/*
		void glslopt_set_max_unroll_iterations (glslopt_ctx* ctx, unsigned iterations);

		const char* glslopt_get_raw_output (glslopt_shader* shader);

		int glslopt_shader_get_texture_count (glslopt_shader* shader);
		void glslopt_shader_get_texture_desc (glslopt_shader* shader, int index, const char** outName, glslopt_basic_type* outType, glslopt_precision* outPrec, int* outVecSize, int* outMatSize, int* outArraySize, int* outLocation);

		// Get *very* approximate shader stats:
		// Number of math, texture and flow control instructions.
		void glslopt_shader_get_stats (glslopt_shader* shader, int* approxMath, int* approxTex, int* approxFlow);
		*/
	}
}

