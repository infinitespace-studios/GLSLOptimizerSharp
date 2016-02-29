using NUnit.Framework;
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

                Assert.That(result.OutputCode, Is.EqualTo("uniform sampler2D tex;\nvoid main ()\n{\n  gl_FragColor = texture2D (tex, gl_TexCoord[0].xy);\n}\n\n"));

                Assert.That(result.Inputs.Count, Is.EqualTo(1));
                Assert.That(result.Inputs[0].Name, Is.EqualTo("gl_TexCoord"));
                Assert.That(result.Inputs[0].Type, Is.EqualTo(BasicType.Float));
                Assert.That(result.Inputs[0].VectorSize, Is.EqualTo(4));

                Assert.That(result.Uniforms.Count, Is.EqualTo(0));

                Assert.That(result.Textures.Count, Is.EqualTo(1));
                Assert.That(result.Textures[0].Name, Is.EqualTo("tex"));
                Assert.That(result.Textures[0].Type, Is.EqualTo(BasicType.Tex2D));
            }
        }
    }
}

