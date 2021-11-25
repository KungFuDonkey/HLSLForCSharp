using System;
using HLSLForCSharp.Direct3D11;
namespace HLSLForCSharpExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ComputeShader cs = new ComputeShader("../../ShaderExample/Shader.hlsl", 2);

            //creating input
            int[] inputData = new int[10];
            int[] multipliers = new int[10];
            for (int i = 0; i < inputData.Length; i++)
            {
                inputData[i] = i;
                multipliers[i] = 9 -    i;
                Console.WriteLine(i);
            }
            Console.WriteLine();

            //use compute shader
            cs.Stage(); // only needed once when this is the only shader that you use
            cs.SetRWStructuredBuffer(inputData, sizeof(int), 0); // Create read write buffer
            cs.SetStructuredBuffer(multipliers, sizeof(int), 1); // Create read-only buffer
            cs.SetUniformVariable(2, sizeof(int), 0); // Set uniform variable
            cs.DispatchCompute(5, 1, 1); // as the groupsize is 2 we only need to instance 5 groups on the x axis
            int[] outputData = cs.GetParsedRWBuffer<int>(0); // get output from GPU
            cs.UnStage(); // not needed when this is the only shader that you use
            cs.Dispose(); // not needed for a program that runs every frame

            //read back output
            for (int i = 0; i < outputData.Length; i++)
            {
                Console.WriteLine(outputData[i]);
            }
            Console.ReadKey();
        }
    }
}
