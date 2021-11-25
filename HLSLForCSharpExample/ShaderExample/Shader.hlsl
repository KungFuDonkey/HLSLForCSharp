

RWStructuredBuffer<int> buffer;
uniform int multiplier : register(t0);
StructuredBuffer<int> buffer2 : register(t1);

[numthreads(2, 1, 1)]
void CSMain(uint3 id : SV_DispatchThreadID)
{
    buffer[id.x] = buffer[id.x] * buffer2[id.x] * multiplier;
}