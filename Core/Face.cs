using Engine.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class Face
{
    private readonly int vao;
    private readonly int vbo;

    public Face(float[] vertices, Shader shader)
    {
        vao = GL.GenVertexArray();
        GL.BindVertexArray(vao);

        vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        var positionLocation = shader.GetAttribLocation("aPos");
        GL.EnableVertexAttribArray(positionLocation);
        GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

        var normalLocation = shader.GetAttribLocation("aNormal");
        GL.EnableVertexAttribArray(normalLocation);
        GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

        var uvLocation = shader.GetAttribLocation("inUV");
        GL.EnableVertexAttribArray(uvLocation);
        GL.VertexAttribPointer(uvLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
    }

    public void Render(Matrix4 mat, Shader shader)
    {
        GL.BindVertexArray(vao);

        shader.SetMatrix4("model", mat);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

        GL.BindVertexArray(0);
    }
}