using NUnit.Framework;
using System;
using GLSLOptomizerSharp;
using System.Runtime.InteropServices;
using System.Text;

namespace GLSLOptimizerSharp.Tests
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestCase ()
		{
			var ctx = GLSLOptimizer.glslopt_initialize (GLSLOptimizer.glslopt_target.kGlslTargetOpenGL);
			Assert.AreNotEqual (ctx, IntPtr.Zero, "Failed to create context");

			var sb = new StringBuilder ();
			sb.Append ("uniform mat4 _mv;");
			sb.Append ("void main () {");
			sb.Append ("  gl_Position = ftransform();");
			sb.Append ("}");
			sb.Append ("void vs_main () {");
			sb.Append ("}");

			IntPtr source = Marshal.StringToHGlobalAuto (sb.ToString());
			var shader = GLSLOptimizer.glslopt_optimize (ctx, GLSLOptimizer.glslopt_shader_type.kGlslOptShaderVertex, source, 0);
			Assert.AreNotEqual (shader, IntPtr.Zero, "Failed to create shader");
			if (GLSLOptimizer.glslopt_get_status (shader)) {
				var o = GLSLOptimizer.glslopt_get_output (shader);
				string opt = Marshal.PtrToStringAuto (o);

				var count = GLSLOptimizer.glslopt_shader_get_input_count (shader);
				for (int i = 0; i < count; i++) {
					IntPtr outName;
					int outLocation, outArraySize, outMatSize, outVecSize;
					GLSLOptimizer.glslopt_basic_type outType;
					GLSLOptimizer.glslopt_precision outPrec;

					GLSLOptimizer.glslopt_shader_get_input_desc (shader, i,
						out outName,
						out outType,
						out outPrec,
						out outVecSize,
						out outMatSize,
						out outArraySize,
						out outLocation);
					var info = Marshal.PtrToStringAuto(outName);
				}

				count = GLSLOptimizer.glslopt_shader_get_uniform_count (shader);
				for (int i = 0; i < count; i++) {
					IntPtr outName;
					int outLocation, outArraySize, outMatSize, outVecSize;
					GLSLOptimizer.glslopt_basic_type outType;
					GLSLOptimizer.glslopt_precision outPrec;

					GLSLOptimizer.glslopt_shader_get_uniform_desc (shader, i,
						out outName,
						out outType,
						out outPrec,
						out outVecSize,
						out outMatSize,
						out outArraySize,
						out outLocation);
					var info = Marshal.PtrToStringAuto(outName);
				}

			} else {
				var log = GLSLOptimizer.glslopt_get_log (shader);
				var info = Marshal.PtrToStringAuto(log);
			}

			GLSLOptimizer.glslopt_shader_delete (shader);
			GLSLOptimizer.glslopt_cleanup (ctx);
			Assert.Pass ();
		}
	}
}

