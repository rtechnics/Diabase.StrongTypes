# Diabase.StrongTypes
C# source generator for creating strong types.

The library ships as a single NuGet package, **Diabase.StrongTypes**, which contains both the runtime types (attributes, enums, and constraint helpers) and the source generator.

Strong types are generated when the coder addes a partial struct or class then addes a supported attribute to the struct or class, such as **[StrongIntId]**.
Doing this causes the design-side code for the strong type to be generated.

There are two primary categories of strong types in this project, *StrongType* and *StrongId*. 
The StrongId structs are (relatively) light-weight with the intended use to of providing a strong types for ID fields. 
The StrongType structs and classes are intended to be broader replacements their backing type. 
A StrongIntType, for example, should behave as a primitive int in most cases except that it cannot be directly assigned to another StrongIntType.

The attributes used to declare a strong type also provide options to automatically create a JsonConverter, TypeConverter, and ValueConverter for those types.
The JsonConverter and TypeConverter are also automatically attached to the strong type itself. 
However, the ValueConverter will need to be registered with Entity Framework to be used.

The **Diabase.StrongTypes.Tests** project contains some tests but also is an example for how to use the strong types generator. 
Within the repository the generator is referenced as a project with *OutputItemType="Analyzer"* and *ReferenceOutputAssembly="false"*, alongside a normal project reference to the runtime types.

The **Diabase.StrongTypes.TemplateDesign** project is used to code the design side of the partial structs and classes used. 
The Diabase.StrongTypes.Generators project embeds the source files in the TemplateDesign \Templates folder as embedded resources at build time. 
The classes contained in the template source files are not available to be used as classes directly. 

This is a work-in-progress and needs addition work before reaching version 1.0 status.
