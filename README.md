# HLSLForCSharp

## Dependencies

The HLSLForCSharp libary uses the SharpDX libary. This can be downloaded from the nuget package manager. It uses the following specific libaries:
	
- SharpDX
- SharpDX.D3DCompiler
- SharpDX.Direct3D11
- SharpDX.DXGI
- System.Drawing 

## What can you do with this libary?

Please note that this libary in in the early stages of development 

This is a libary that allows the programmer to easily use HLSL shaders in C#. The goal of this libary is that a programmer can easily use their HLSL shaders in C# without knowing all the options, functions or settings from the DirectX framework. Currently the libary allows the user to perform the following:
- Compile and use Compute shaders
- Send StructuredBuffer to a Compute Shader
- Send RWStructuredBuffer to a Compute Shader

## Usage

For an example on how to use this libary please look at the HLSLForCSharpExample project in this repo

## Issues and Pull requests

Issues and Pull requests are welcome, but please note the following:

When making an issue please:
- Clarify what the idea, or problem is 
- In case of an error please provide source code so I can help you with identifying the problem
- In case of an idea please make sure that the idea can be made so that a programmer can easily use it

When making a pull request please:
- Make sure that functions are easy to use for other programmers
- Make readable code with comments

## Future updates
- Add support for uniform, texture2D and sampler
- Add support for creating a window
- Add support for a render pipeline