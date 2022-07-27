using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
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
            SyntaxReceiver rx = (SyntaxReceiver)context.SyntaxContextReceiver!;
            foreach (var entry in rx.Entries)
            {
                string source = GenerateCode(entry);
                context.AddSource($"StrongType_{entry.NamespaceIdentifier}_{entry.StructIdentifier}.g.cs", source);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG_GENERATOR
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        Dictionary<ImplicitNullConversionMode, string> implicitConversionModeSuffix = new()
        {
            { ImplicitNullConversionMode.NotAllowed, "NONE" },
            { ImplicitNullConversionMode.ToEmptyString, "EMPTY" },
            { ImplicitNullConversionMode.ToNullValue, "NULL" },
        };

        string GenerateCode(Entry entry)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"Diabase.StrongTypes.Generators.Templates.{entry.Parameters.TemplateName}.cs";

            string template;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                template = reader.ReadToEnd();
            }
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
                (s) => entry.Parameters.Converters?.Contains(Converter.JsonConverter)??false ? s : s.Undefine("INCLUDE_JSON_CONVERTER"),
                (s) => entry.Parameters.Converters?.Contains(Converter.TypeConverter)??false ? s : s.Undefine("INCLUDE_TYPE_CONVERTER"),
                (s) => entry.Parameters.Converters?.Contains(Converter.ValueConverter)??false ? s : s.Undefine("INCLUDE_VALUE_CONVERTER"),
                (s) => entry.Parameters.Converters?.Contains(Converter.Customize)??false ? s : s.Undefine("USE_CUSTOM_CONVERTER"),
                (s) => s.Replace("internal", "public"),
                (s) => s.Replace("Diabase.StrongTypes.Templates", entry.NamespaceIdentifier),
                (s) => s.Replace(entry.Parameters.TemplateName, entry.StructIdentifier),
                (s) => s.Replace("BackingType", entry.Parameters.BackingType),
            };

            var code = transformations.Aggregate(template, (acc, transformation) => transformation(acc));

            return code;
        }

        struct BackingInfo
        {
            public BackingInfo(string typeText, string templateName)
            {
                TypeText = typeText;
                TemplateName = templateName;
            }
            public string TypeText;
            public string TemplateName;
        }

        const string StrongBoolType = nameof(StrongBoolType);
        const string StrongFloatType = nameof(StrongFloatType);
        const string StrongIntType = nameof(StrongIntType);
        const string StrongStringType = nameof(StrongStringType);

        struct Entry
        {
            public string NamespaceIdentifier;
            public string StructIdentifier;
            public Parameters Parameters;
        }

        struct Parameters
        {
            public bool IsStrongType;
            public string TemplateName;
            public string BackingType;
            public ImplicitNullConversionMode ImplicitNullConversionMode;
            public StringConstraint[] StringConstraints;
            public NumericConstraint[] NumericConstraints;
            public bool ValidationRequired;
            public Converter[] Converters;
        }

        const string StrongStringTypeTemplate = "StrongStringType";
        const string StrongIntTypeTemplate = "StrongIntType";
        const string StrongFloatTypeTemplate = "StrongFloatType";
        const string StrongDateTypeTemplate = "StrongDateType";
        const string StrongBoolTypeTemplate = "StrongBoolType";
        const string StrongReferenceIdTemplate = "StrongReferenceId";
        const string StrongValueIdTemplate = "StrongValueId";

        static List<(string AttributeName, string TemplateName, string BackingTypeName)> generatorList = new()
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
        };

        public static Dictionary<string, (string AttributeName, string TemplateName, string BackingTypeName)> generatorMap = generatorList.ToDictionary(x => x.AttributeName, x => x);

        class SyntaxReceiver : ISyntaxContextReceiver
        {
            public List<Entry> Entries = new();
            string namespaceIdentifier;

            (StringConstraint? StringConstraint, NumericConstraint? NumericConstraint) ParseConstraint(string constraintValue)
            {
                Enum.TryParse<StringConstraint>(constraintValue, out var stringConstraint);
                Enum.TryParse<NumericConstraint>(constraintValue, out var numericConstraint);
                return (stringConstraint, numericConstraint);
            }

            Parameters GetParams(GeneratorSyntaxContext context, SyntaxList<AttributeListSyntax> list)
            {
                Parameters result = new();

                var attributeEntries = list.Aggregate(
                    new List<AttributeSyntax>().AsEnumerable(),
                    (acc, list) => acc.Concat(list.Attributes))
                    .Select(attribute => new { attribute, attributeName = context.SemanticModel.GetTypeInfo(attribute).Type?.ToDisplayString() })
                    .Where(x => generatorMap.ContainsKey(x.attributeName));

                var attributeEntry = attributeEntries.FirstOrDefault();

                if (attributeEntry is not null)
                {
                    result.IsStrongType = true;

                    var generatorInfo = generatorMap[attributeEntry.attributeName];
                    result.BackingType = generatorInfo.BackingTypeName;
                    result.TemplateName = generatorInfo.TemplateName;

                    if (attributeEntry.attribute.ArgumentList is not null)
                    {
                        foreach (AttributeArgumentSyntax attributeArgumentSyntax in attributeEntry.attribute.ArgumentList.Arguments)
                        {
                            if (attributeArgumentSyntax.NameEquals is not null)
                            {
                                var argumentName = attributeArgumentSyntax.NameEquals.Name.Identifier.Text;
                                //var expression = attributeArgumentSyntax.Expression.ToString().Split('.').Last();
                                var expression = attributeArgumentSyntax.Expression.ToString();
                                if (attributeArgumentSyntax.NameEquals.Name.Identifier.Text == "ImplicitNullConversionMode")
                                {
                                    var enumIdentifier = attributeArgumentSyntax.Expression.ToString().Split('.').Last();
                                    if (Enum.TryParse<ImplicitNullConversionMode>(enumIdentifier, out var implicitNullConversionMode))
                                    {
                                        result.ImplicitNullConversionMode = implicitNullConversionMode;
                                    }
                                }
                                else if (argumentName == "Constraints")
                                {
                                    var enumsExpressions = expression.Split('|').Select(x => x.Trim()).Select(x => x.Split('.').Last());
                                    var enums = enumsExpressions.Select(enumExpression => ParseConstraint(enumExpression));
                                    result.StringConstraints = enums.Where(x => x.StringConstraint is not null).Select(x => x.StringConstraint.Value).ToArray();
                                    result.NumericConstraints = enums.Where(x => x.NumericConstraint is not null).Select(x => x.NumericConstraint.Value).ToArray();
                                }
                                else if (argumentName == "Converters")
                                {
                                    var enumsExpressions = expression.Split('|').Select(x => x.Trim()).Select(x => x.Split('.').Last());
                                    var enums = enumsExpressions.Select(enumExpression => (Converter)Enum.Parse(typeof(Converter), enumExpression));
                                    result.Converters = enums.ToArray();
                                }
                                else if (argumentName == "ValidationRequired")
                                {
                                    result.ValidationRequired = expression == "true";
                                }
                            }
                        }
                    }
                }

                return result;
            }

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if (context.Node is NamespaceDeclarationSyntax namespaceDecl)
                {
                    namespaceIdentifier = namespaceDecl.Name.ToString();
                }
                else if (context.Node is AttributeSyntax attrib)
                {
                    var attributeName = context.SemanticModel.GetTypeInfo(attrib).Type?.ToDisplayString();
                }
                else if (context.Node is StructDeclarationSyntax structdecl)
                {
                    var attributeParams = GetParams(context, structdecl.AttributeLists);
                    if (attributeParams.IsStrongType)
                    {
                        var identifier = structdecl.Identifier.Text;
                        var entry = new Entry
                        {
                            NamespaceIdentifier = namespaceIdentifier,
                            StructIdentifier = identifier,
                            Parameters = attributeParams,
                        };
                        Entries.Add(entry);
                    }
                }
                else if (context.Node is ClassDeclarationSyntax classdecl)
                {
                    var attributeParams = GetParams(context, classdecl.AttributeLists);
                    if (attributeParams.IsStrongType)
                    {
                        var identifier = classdecl.Identifier.Text;
                        var entry = new Entry
                        {
                            NamespaceIdentifier = namespaceIdentifier,
                            StructIdentifier = identifier,
                            Parameters = attributeParams,
                        };
                        Entries.Add(entry);
                    }
                }
            }
        }
    }
}
