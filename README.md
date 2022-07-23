# Diabase.StrongTypes
C# source generator for creating strong types.

The **Diabase.StrongTypes.Generators** project contains the source generator and also the various attribute classes needed to invoke the generator. 
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
A reference to the Diabase.StrongTypes.Generator project must be included with the attribute *OutputItemType="Analyzer"*.

The **Diabase.StrongTypes.TemplateDesign** project is used to code the design side of the partial structs and classes used. 
When the project is built, it simply copies the source files to the \Templates folder of the Diabase.StrongTypes.Generators project. 
That project uses those source files as embedded resources. 
The classes contained in the template source files are not available to be used as classes directly. 

This is a work-in-progress and needs addition work before reaching version 1.0 status.