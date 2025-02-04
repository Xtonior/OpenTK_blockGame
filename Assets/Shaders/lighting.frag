#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec2 TexUV;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform sampler2D texture0;

void main()
{
    vec4 col = mix(vec4(lightColor * objectColor, 1.0), vec4(Normal, 1.0), 0.1);
    FragColor = col * texture(texture0, TexUV);
}