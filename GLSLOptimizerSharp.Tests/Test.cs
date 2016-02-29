using NUnit.Framework;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GLSLOptimizerSharp.Tests
{
	[TestFixture ()]
	public class Test
	{
		[Test]
		public void TestCase ()
		{
            var sb = new StringBuilder ();
            sb.Append("uniform mat4 _mv;");
            sb.Append("void main () {");
            sb.Append("  gl_Position = ftransform();");
            sb.Append("}");
            sb.Append("void vs_main () {");
            sb.Append("}");

            using (var optimizer = new GLSLOptimizer(Target.OpenGL))
            {
                var result = optimizer.Optimize(ShaderType.Vertex, sb.ToString(), OptimizationOptions.None);
            }
		}

        [Test]
        public void TestFragment()
        {
            var code = @"
uniform sampler2D tex;
 
void main()
{
    vec4 color = texture2D(tex,gl_TexCoord[0].st);
    gl_FragColor = color;
}";

            using (var optimizer = new GLSLOptimizer(Target.OpenGL))
            {
                var result = optimizer.Optimize(ShaderType.Fragment, code, OptimizationOptions.None);
            }
        }
    }
}

