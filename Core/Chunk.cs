using System;
using System.Collections.Generic;
using Engine.Core;
using OpenTK.Mathematics;

public class Chunk
{
    public readonly Vector2i ChunkPosition;

    private Vector3i chunkSize = new Vector3i(4, 4, 4);
    private List<Block> blocks;

    public Chunk(Vector2i position)
    {
        blocks = new List<Block>();
        ChunkPosition = position;
    }

    public void Generate(Shader shader)
    {
        for (int x = 0; x < chunkSize.X; x++)
        {
            for (int y = 0; y < chunkSize.X; y++)
            {
                for (int z = 0; z < chunkSize.X; z++)
                {
                    blocks.Add(new Block(new Vector3(ChunkPosition.X * chunkSize.X, 0.0f, ChunkPosition.Y * chunkSize.Y) + new Vector3(x, y, z), shader, "Assets/Textures/atl_blocks.png"));
                }
            }
        }
    }

    public void Render(Shader shader)
    {
        foreach (var block in blocks)
        {
            block.Render(shader);
        }
    }
}