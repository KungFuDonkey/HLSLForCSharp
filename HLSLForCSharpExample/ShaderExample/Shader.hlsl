RWStructuredBuffer<int> Result;

[numthreads(2, 1, 1)]
void CSMain(uint3 id : SV_DispatchThreadID)
{
    Result[id.x] = Result[id.x] * 8;
}