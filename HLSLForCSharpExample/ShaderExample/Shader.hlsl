

RWStructuredBuffer<int> buffer;
StructuredBuffer<int> buffer2;
//uniform int multiplier;
[numthreads(2, 1, 1)]
void CSMain(uint3 id : SV_DispatchThreadID)
{
    buffer[id.x] = buffer[id.x] * buffer2[id.x];
}