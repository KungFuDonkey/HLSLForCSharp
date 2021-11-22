

RWStructuredBuffer<int> buffer;
uniform int multiplier;
[numthreads(2, 1, 1)]
void CSMain(uint3 id : SV_DispatchThreadID)
{
    buffer[id.x] = buffer[id.x] * multiplier;
}