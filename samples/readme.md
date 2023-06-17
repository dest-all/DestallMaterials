The general CodeGeneration process of work is the following (theory):
1. A ProjectCodeRenderer instance gets configured to map a template (by its type) to a project (by its name as string).
2. Then it runs a Roslyn (.Net compiler tool) routine to get info about the projects syntax and semantics (the latter is the main thing).
3. Then it instantiates a template object specified and renders it as a Blazor component, passing Compilation object obtained as a Parameter.
4. The content generated then gets analyzed as a string and all contents between <File Path="<relativeFilePath>">***</File> are written to relativeFilePath-s
5. If anything is off - no files are created.

The basic usage of the package is pretty simple.

To get familiar with it (practice):
1. Open the CodeGenerationExample.sln file
2. Program.cs - this is where and how you configure code generation
3. Template.razor - describe generated source code content
4. PropsIterator.cs - the result of running CodeGenerationExample.Generator.Launch project.

 
