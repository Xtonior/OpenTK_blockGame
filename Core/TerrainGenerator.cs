using System.Collections.Generic;
using Engine.Core;
using OpenTK.Mathematics;

public class TerrainGenerator
{
    private int worldX = 1;
    private int worldy = 1;

    private List<Chunk> chunks = new List<Chunk>();

    public void Generate(Shader shader, World world)
    {
        for (int x = 0; x < worldX; x++)
        {
            for (int y = 0; y < worldy; y++)
            {
                Chunk chunk = new Chunk(new Vector2i(x, y));
                chunk.Generate(shader);
                chunks.Add(chunk);

                world.AddChunk(chunk);
            }
        }
    }
}