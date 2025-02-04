using Engine.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class Block
{
    private Vector3 position;

    private readonly Face front;
    private readonly Face back;
    private readonly Face left;
    private readonly Face right;
    private readonly Face top;
    private readonly Face bottom;

    private Texture texture;

    public Block(Vector3 position, Shader shader, string texturePath)
    {
        this.position = position;

        front = new Face(GenerateFrontFace(3, 0, 16, 16), shader);
        back = new Face(GenerateBackFace(3, 0, 16, 16), shader);
        left = new Face(GenerateLeftFace(3, 0, 16, 16), shader);
        right = new Face(GenerateRightFace(3, 0, 16, 16), shader);

        top = new Face(GenerateTopFace(0, 0, 16, 16), shader);
        bottom = new Face(GenerateBottomFace(0, 0, 16, 16), shader);

        texture = Texture.LoadFromFile(texturePath);
        texture.Use(TextureUnit.Texture0);

        Unbind();
    }

    public void Unbind()
    {
        GL.BindVertexArray(0);
    }

    public void Render(Shader shader)
    {
        Matrix4 mat = Matrix4.CreateTranslation(position);

        front.Render(mat, shader);
        back.Render(mat, shader);
        left.Render(mat, shader);
        right.Render(mat, shader);
        top.Render(mat, shader);
        bottom.Render(mat, shader);
    }

    private Vector2[] GetTileUV(int tileX, int tileY, int atlasCols, int atlasRows)
    {
        float tileWidth = 1.0f / atlasCols;
        float tileHeight = 1.0f / atlasRows;

        float uMin = tileX * tileWidth;
        float vMin = tileY * tileHeight;
        float uMax = uMin + tileWidth;
        float vMax = vMin + tileHeight;

        return new Vector2[]
        {
            new Vector2(uMax, vMax), // Bottom-left
            new Vector2(uMin, vMax), // Bottom-right
            new Vector2(uMin, vMin), // Top-right
            new Vector2(uMax, vMin)  // Top-left
        };
    }

    private float[] GenerateFrontFace(int tileX, int tileY, int atlasCols, int atlasRows)
    {
        Vector2[] uvs = GetTileUV(tileX, tileY, atlasCols, atlasRows);

        return new float[]
        {
        // Positions          Normals              Texture Coords (Using uvs)
        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  uvs[0].X, uvs[0].Y,
         0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  uvs[1].X, uvs[1].Y,
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  uvs[2].X, uvs[2].Y,
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  uvs[2].X, uvs[2].Y,
        -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  uvs[3].X, uvs[3].Y,
        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  uvs[0].X, uvs[0].Y,
        };
    }

    private float[] GenerateBackFace(int tileX, int tileY, int atlasCols, int atlasRows)
    {
        Vector2[] uvs = GetTileUV(tileX, tileY, atlasCols, atlasRows);

        return new float[]
        {
        // Positions              Normals                Texture Coords
         0.5f, -0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   uvs[0].X, uvs[0].Y,
        -0.5f, -0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   uvs[1].X, uvs[1].Y,
        -0.5f,  0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   uvs[2].X, uvs[2].Y,
        -0.5f,  0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   uvs[2].X, uvs[2].Y,
         0.5f,  0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   uvs[3].X, uvs[3].Y,
         0.5f, -0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   uvs[0].X, uvs[0].Y,
        };
    }

    private float[] GenerateLeftFace(int tileX, int tileY, int atlasCols, int atlasRows)
    {
        Vector2[] uvs = GetTileUV(tileX, tileY, atlasCols, atlasRows);

        return new float[]
        {
        // Positions              Normals                Texture Coords
        -0.5f, -0.5f, -0.5f,  -1.0f,  0.0f,  0.0f,   uvs[0].X, uvs[0].Y,
        -0.5f, -0.5f,  0.5f,  -1.0f,  0.0f,  0.0f,   uvs[1].X, uvs[1].Y,
        -0.5f,  0.5f,  0.5f,  -1.0f,  0.0f,  0.0f,   uvs[2].X, uvs[2].Y,
        -0.5f,  0.5f,  0.5f,  -1.0f,  0.0f,  0.0f,   uvs[2].X, uvs[2].Y,
        -0.5f,  0.5f, -0.5f,  -1.0f,  0.0f,  0.0f,   uvs[3].X, uvs[3].Y,
        -0.5f, -0.5f, -0.5f,  -1.0f,  0.0f,  0.0f,   uvs[0].X, uvs[0].Y,
        };
    }

    private float[] GenerateRightFace(int tileX, int tileY, int atlasCols, int atlasRows)
    {
        Vector2[] uvs = GetTileUV(tileX, tileY, atlasCols, atlasRows);

        return new float[]
        {
        // Positions              Normals                Texture Coords
         0.5f, -0.5f,  0.5f,   1.0f,  0.0f,  0.0f,   uvs[0].X, uvs[0].Y,
         0.5f, -0.5f, -0.5f,   1.0f,  0.0f,  0.0f,   uvs[1].X, uvs[1].Y,
         0.5f,  0.5f, -0.5f,   1.0f,  0.0f,  0.0f,   uvs[2].X, uvs[2].Y,
         0.5f,  0.5f, -0.5f,   1.0f,  0.0f,  0.0f,   uvs[2].X, uvs[2].Y,
         0.5f,  0.5f,  0.5f,   1.0f,  0.0f,  0.0f,   uvs[3].X, uvs[3].Y,
         0.5f, -0.5f,  0.5f,   1.0f,  0.0f,  0.0f,   uvs[0].X, uvs[0].Y,
        };
    }

    private float[] GenerateTopFace(int tileX, int tileY, int atlasCols, int atlasRows)
    {
        Vector2[] uvs = GetTileUV(tileX, tileY, atlasCols, atlasRows);

        return new float[]
        {
        // Positions              Normals                Texture Coords
        -0.5f,  0.5f, -0.5f,   0.0f,  1.0f,  0.0f,   uvs[0].X, uvs[0].Y,
         0.5f,  0.5f, -0.5f,   0.0f,  1.0f,  0.0f,   uvs[1].X, uvs[1].Y,
         0.5f,  0.5f,  0.5f,   0.0f,  1.0f,  0.0f,   uvs[2].X, uvs[2].Y,
         0.5f,  0.5f,  0.5f,   0.0f,  1.0f,  0.0f,   uvs[2].X, uvs[2].Y,
        -0.5f,  0.5f,  0.5f,   0.0f,  1.0f,  0.0f,   uvs[3].X, uvs[3].Y,
        -0.5f,  0.5f, -0.5f,   0.0f,  1.0f,  0.0f,   uvs[0].X, uvs[0].Y,
        };
    }

    private float[] GenerateBottomFace(int tileX, int tileY, int atlasCols, int atlasRows)
    {
        Vector2[] uvs = GetTileUV(tileX, tileY, atlasCols, atlasRows);

        return new float[]
        {
        // Positions              Normals                Texture Coords
        -0.5f, -0.5f, -0.5f,   0.0f, -1.0f,  0.0f,   uvs[0].X, uvs[0].Y,
         0.5f, -0.5f, -0.5f,   0.0f, -1.0f,  0.0f,   uvs[1].X, uvs[1].Y,
         0.5f, -0.5f,  0.5f,   0.0f, -1.0f,  0.0f,   uvs[2].X, uvs[2].Y,
         0.5f, -0.5f,  0.5f,   0.0f, -1.0f,  0.0f,   uvs[2].X, uvs[2].Y,
        -0.5f, -0.5f,  0.5f,   0.0f, -1.0f,  0.0f,   uvs[3].X, uvs[3].Y,
        -0.5f, -0.5f, -0.5f,   0.0f, -1.0f,  0.0f,   uvs[0].X, uvs[0].Y,
        };
    }


    private void GetBlockUV(int offsetX, int offsetY, float atlasSize, float blockPixelSize, out float uMin, out float vMin, out float uMax, out float vMax)
    {
        float blockSize = blockPixelSize / atlasSize;
        uMin = offsetX * blockSize;
        vMin = offsetY * blockSize;
        uMax = uMin + blockSize;
        vMax = vMin + blockSize;
    }
}