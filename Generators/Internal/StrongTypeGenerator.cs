//#define INTEGRATED_DEBUGGING // use this for integrated debugging

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
#if INTEGRATED_DEBUGGING
using System.Diagnostics;
#endif
using System.IO;
using System.Linq;
using System.Reflection;

namespace Diabase.StrongTypes.Generators.Internal
{
    [Generator]
    internal class StrongTypeGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            SyntaxReceiver syntaxReceiver = (SyntaxReceiver)context.SyntaxContextReceiver!;

            // Generate code for each class and struct that has a StrongType attribute
            foreach (var entry in syntaxReceiver.Entries)
            {
                string filename = $"StrongType_{entry.NamespaceIdentifier}_{entry.TypeIdentifier}.g.cs";
                string source = GenerateCode(entry);
                context.AddSource(filename, source);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if INTEGRATED_DEBUGGING
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        // Value options for the ImplicitNullConversionMode parameter of a StrongType attribute
        readonly Dictionary<ImplicitNullConversionMode, string> implicitConversionModeSuffix = new()
        {
            { ImplicitNullConversionMode.NotAllowed, "NONE" },
            { ImplicitNullConversionMode.ToEmptyString, "EMPTY" },
            { ImplicitNullConversionMode.ToNullValue, "NULL" },
        };

        // Generates the partial class source code for a class or struct entry
        string GenerateCode(Entry entry)
        {
            // Get template code from embedded resources
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"Diabase.StrongTypes.Templates.{entry.Parameters.TemplateName}.cs";
            string template;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new(stream))
            {
                template = reader.ReadToEnd();
            }

            // Functional transformations to apply to the template based on entry parameters
            // This is done by, primarily, removing code by undefining preprocessor directives
            // There are a few keyword replacements as well
            Func<string, string>[] transformations =
            {
                (s) => s.RemoveLinesStartingWith("using BackingType = "),
                (s) => s.Undefine("DESIGN_MODE"),
                (s) => s.Replace("IMPLICIT_NULL_CONVERSION_DESIGN", $"IMPLICIT_NULL_CONVERSION_{implicitConversionModeSuffix[entry.Parameters.ImplicitNullConversionMode]}"),
                (s) => entry.Parameters.StringConstraints?.Contains(StringConstraint.Required)??false ? s : s.Undefine("CONSTRAINT_REQUIRED"),
                (s) => entry.Parameters.StringConstraints?.Contains(StringConstraint.Regex)??false ? s : s.Undefine("CONSTRAINT_REGEX"),
                (s) => entry.Parameters.NumericConstraints?.Contains(NumericConstraint.MinimumValue)??false ? s : s.Undefine("CONSTRAINT_MIN_VALUE"),
                (s) => entry.Parameters.NumericConstraints?.Contains(NumericConstraint.MaximumValue)??false ? s : s.Undefine("CONSTRAINT_MAX_VALUE"),
                (s) => (entry.Parameters.StringConstraints?.Contains(StringConstraint.Custom)??false) || (entry.Parameters.NumericConstraints?.Contains(NumericConstraint.Custom)??false) ? s : s.Undefine("CONSTRAINT_CUSTOM"),
                (s) => entry.Parameters.ValidationRequired ? s : s.Undefine("VALIDATION_REQUIRED"),
                (s) => entry.Parameters.IncludePublicIdSupport ? s : s.Undefine("INCLUDE_PUBLIC_ID"),
                (s) => !string.IsNullOrEmpty(entry.Parameters.PublicIdAesKey) ? s.Replace("\"--AES-KEY--\"", entry.Parameters.PublicIdAesKey) : s,
                (s) => !string.IsNullOrEmpty(entry.Parameters.PublicIdAesIv) ? s.Replace("\"--AES-IV--\"", entry.Parameters.PublicIdAesIv) : s,
                (s) => entry.Parameters.IncludeImplicitStringConversion ? s : s.Undefine("INCLUDE_IMPLICIT_STRING_CONVERSION"),
                (s) => entry.Parameters.Converters?.Contains(Converter.JsonConverter)??false ? s : s.Undefine("INCLUDE_JSON_CONVERTER"),
                (s) => entry.Parameters.Converters?.Contains(Converter.TypeConverter)??false ? s : s.Undefine("INCLUDE_TYPE_CONVERTER"),
                (s) => entry.Parameters.Converters?.Contains(Converter.ValueConverter)??false ? s : s.Undefine("INCLUDE_VALUE_CONVERTER"),
                (s) => entry.Parameters.Converters?.Contains(Converter.Customize)??false ? s : s.Undefine("USE_CUSTOM_CONVERTER"),
                (s) => entry.Parameters.UseCustomEncryption ? s : s.Undefine("USE_CUSTOM_ENCRYPTION"),
                (s) => s.Replace("internal", "public"),
                (s) => s.Replace("Diabase.StrongTypes.Templates", entry.NamespaceIdentifier),
                (s) => s.Replace(entry.Parameters.TemplateName, entry.TypeIdentifier),
                (s) => s.Replace("BackingType", entry.Parameters.BackingTypeName),
            };
            string code = transformations.Aggregate(template, (acc, transformation) => transformation(acc));

            return code;
        }

        const string StrongBoolType = nameof(StrongBoolType);
        const string StrongFloatType = nameof(StrongFloatType);
        const string StrongIntType = nameof(StrongIntType);
        const string StrongStringType = nameof(StrongStringType);

        // Each entry represents a class or struct that will need to have a strong type partial class generated
        struct Entry
        {
            public string NamespaceIdentifier;
            public string TypeIdentifier;
            public Parameters Parameters;
        }

        // These parameters specify which features should be enabled in the generated code
        struct Parameters
        {
            public string TemplateName;
            public string BackingTypeName;
            public ImplicitNullConversionMode ImplicitNullConversionMode;
            public StringConstraint[] StringConstraints;
            public NumericConstraint[] NumericConstraints;
            public bool ValidationRequired;
            public bool IncludePublicIdSupport;
            public string PublicIdAesKey;
            public string PublicIdAesIv;
            public bool IncludeImplicitStringConversion;
            public bool UseCustomEncryption;
            public Converter[] Converters;
        }

        const string StrongStringTypeTemplate = "StrongStringType";
        const string StrongIntTypeTemplate = "StrongIntType";
        const string StrongFloatTypeTemplate = "StrongFloatType";
        const string StrongDateTypeTemplate = "StrongDateType";
        const string StrongBoolTypeTemplate = "StrongBoolType";
        const string StrongReferenceIdTemplate = "StrongReferenceId";
        const string StrongValueIdTemplate = "StrongValueId";
        const string StrongBytesSizeUnit = "StrongBytesSizeUnit";

        // Map attribute names to template names and backing types
        static readonly List<(string AttributeName, string TemplateName, string BackingTypeName)> generatorList = new()
        {
            (typeof(StrongStringTypeAttribute).FullName, StrongStringTypeTemplate, typeof(string).Name),
            (typeof(StrongIntTypeAttribute).FullName, StrongIntTypeTemplate, typeof(int).Name),
            (typeof(StrongLongTypeAttribute).FullName, StrongIntTypeTemplate, typeof(long).Name),
            (typeof(StrongDoubleTypeAttribute).FullName, StrongFloatTypeTemplate, typeof(double).Name),
            (typeof(StrongFloatTypeAttribute).FullName, StrongFloatTypeTemplate, typeof(float).Name),
            (typeof(StrongDecimalTypeAttribute).FullName, StrongFloatTypeTemplate, typeof(decimal).Name),
            (typeof(StrongDateTypeAttribute).FullName, StrongDateTypeTemplate, typeof(DateTime).Name),
            (typeof(StrongBoolTypeAttribute).FullName, StrongBoolTypeTemplate, typeof(bool).Name),

            (typeof(StrongIdAttribute).FullName, StrongValueIdTemplate, typeof(int).Name),
            (typeof(StrongStringIdAttribute).FullName, StrongReferenceIdTemplate, typeof(string).Name),
            (typeof(StrongGuidIdAttribute).FullName, StrongValueIdTemplate, typeof(Guid).Name),
            (typeof(StrongIntIdAttribute).FullName, StrongValueIdTemplate, typeof(int).Name),

            (typeof(StrongBytesSizeUnitAttribute).FullName, StrongBytesSizeUnit, typeof(decimal).Name),

        };

        public static Dictionary<string, (string AttributeName, string TemplateName, string BackingTypeName)> generatorMap = generatorList.ToDictionary(x => x.AttributeName, x => x);

        class SyntaxReceiver : ISyntaxContextReceiver
        {
            // Names of parameters in the StrongType attribute
            private const string implicitNullConversionMode = "ImplicitNullConversionMode";
            private const string constraints = "Constraints";
            private const string converters = "Converters";
            private const string validationRequired = "ValidationRequired";
            private const string includePublicIdSupport = "IncludePublicIdSupport";
            private const string publicIdAesKey = "PublicIdAesKey";
            private const string publicIdAesIv = "PublicIdAesIv";
            private const string includeImplicitStringConversion = "IncludeImplicitStringConversion";
            private const string useCustomEncryption = "UseCustomEncryption";
            // Parameter values in the StrongType attribute
            private const string booleanTrue = "true";

            public List<Entry> Entries = new(); // List of classes and structs that have the StrongType attribute
            string namespaceIdentifier; // The namespace of the current class or struct being parsed

            (StringConstraint? StringConstraint, NumericConstraint? NumericConstraint) ParseConstraint(string constraintValue)
            {
                Enum.TryParse<StringConstraint>(constraintValue, out var stringConstraint);
                Enum.TryParse<NumericConstraint>(constraintValue, out var numericConstraint);
                return (stringConstraint, numericConstraint);
            }

            Parameters? GetParams(GeneratorSyntaxContext context, SyntaxList<AttributeListSyntax> list)
            {
                var attributeEntries = list.SelectMany(x => x.Attributes);
                var namedAttributeEntries = attributeEntries.Select(attribute =>
                    new {
                        Name = context.SemanticModel.GetTypeInfo(attribute).Type?.ToDisplayString(),
                        Value = attribute
                    });
                var supportedAttributes = namedAttributeEntries.Where(x => generatorMap.ContainsKey(x.Name));
                var attributeEntry = supportedAttributes.FirstOrDefault();

                if (attributeEntry is null)
                {
                    return null; // this class or struct does not have a StrongType attribute
                }

                Parameters result = new();

                var generatorInfo = generatorMap[attributeEntry.Name];
                result.BackingTypeName = generatorInfo.BackingTypeName;
                result.TemplateName = generatorInfo.TemplateName;

                if (attributeEntry.Value.ArgumentList is not null)
                {
                    foreach (AttributeArgumentSyntax attributeArgumentSyntax in attributeEntry.Value.ArgumentList.Arguments)
                    {
                        if (attributeArgumentSyntax.NameEquals is not null)
                        {
                            var argumentName = attributeArgumentSyntax.NameEquals.Name.Identifier.Text;
                            var expression = attributeArgumentSyntax.Expression.ToString();
                            switch (argumentName)
                            {
                                case implicitNullConversionMode:
                                {
                                    var enumIdentifier = attributeArgumentSyntax.Expression.ToString().Split('.').Last();
                                    if (Enum.TryParse<ImplicitNullConversionMode>(enumIdentifier, out var implicitNullConversionMode))
                                    {
                                        result.ImplicitNullConversionMode = implicitNullConversionMode;
                                    }
                                    break;
                                }
                                case constraints:
                                {
                                    var enumsExpressions = expression.Split('|').Select(x => x.Trim()).Select(x => x.Split('.').Last());
                                    var enums = enumsExpressions.Select(enumExpression => ParseConstraint(enumExpression));
                                    result.StringConstraints = enums.Where(x => x.StringConstraint is not null).Select(x => x.StringConstraint.Value).ToArray();
                                    result.NumericConstraints = enums.Where(x => x.NumericConstraint is not null).Select(x => x.NumericConstraint.Value).ToArray();
                                    break;
                                }
                                case converters:
                                {
                                    var enumsExpressions = expression.Split('|').Select(x => x.Trim()).Select(x => x.Split('.').Last());
                                    var enums = enumsExpressions.Select(enumExpression => (Converter)Enum.Parse(typeof(Converter), enumExpression));
                                    result.Converters = enums.ToArray();
                                    break;
                                }
                                case validationRequired:
                                {
                                    result.ValidationRequired = expression == booleanTrue;
                                    break;
                                }
                                case includePublicIdSupport:
                                {
                                    result.IncludePublicIdSupport = expression == booleanTrue;
                                    break;
                                }
                                case publicIdAesKey:
                                {
                                    result.PublicIdAesKey = expression;
                                    break;
                                }
                                case publicIdAesIv:
                                {
                                    result.PublicIdAesIv = expression;
                                    break;
                                }
                                case includeImplicitStringConversion:
                                {
                                    result.IncludeImplicitStringConversion = expression == booleanTrue;
                                    break;
                                }
                                case useCustomEncryption:
                                {
                                    result.UseCustomEncryption = expression == booleanTrue;
                                    break;
                                }

                            }
                        }
                    }
                }

                return result;
            }

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                switch (context.Node)
                {
                    case NamespaceDeclarationSyntax namespaceDecl:
                        {
                            namespaceIdentifier = namespaceDecl.Name.ToString();
                        }
                        break;
#if false // for testing
                    case AttributeSyntax attrib:
                        {
                            var attributeName = context.SemanticModel.GetTypeInfo(attrib).Type?.ToDisplayString();
                        }
                        break;
#endif
                    case StructDeclarationSyntax structdecl:
                        {
                            var attributeParams = GetParams(context, structdecl.AttributeLists);
                            if (attributeParams is not null)
                            {
                                var identifier = structdecl.Identifier.Text;
                                var entry = new Entry
                                {
                                    NamespaceIdentifier = namespaceIdentifier,
                                    TypeIdentifier = identifier,
                                    Parameters = attributeParams.Value,
                                };
                                Entries.Add(entry);
                            }
                        }
                        break;
                    case ClassDeclarationSyntax classdecl:
                        {
                            var attributeParams = GetParams(context, classdecl.AttributeLists);
                            if (attributeParams is not null)
                            {
                                var identifier = classdecl.Identifier.Text;
                                var entry = new Entry
                                {
                                    NamespaceIdentifier = namespaceIdentifier,
                                    TypeIdentifier = identifier,
                                    Parameters = attributeParams.Value,
                                };
                                Entries.Add(entry);
                            }
                        }
                        break;
                }
            }
        }
    }
}
