﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace TestGeneratorLib
{
    public class ClassLoader
    {
        public static FileInfo GetFileInfo(string code)
        {
            CompilationUnitSyntax root = CSharpSyntaxTree.ParseText(code).GetCompilationUnitRoot();
            var classes = new List<ClassInfo>();
            foreach (ClassDeclarationSyntax classDeclaration in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                classes.Add(GetClassInfo(classDeclaration));
            }

            return new FileInfo(classes);
        }

        private static MethodInfo GetMethodInfo(MethodDeclarationSyntax method)
        {
            var parameters = new Dictionary<string, string>();
            foreach (var parameter in method.ParameterList.Parameters)
            {
                parameters.Add(parameter.Identifier.Text, parameter.Type.ToString());
            }

            return new MethodInfo(parameters, method.Identifier.ValueText, method.ReturnType.ToString());
        }

        private static ConstructorInfo GetConstructorInfo(ConstructorDeclarationSyntax constructor)
        {
            var parameters = new Dictionary<string, string>();
            foreach (var parameter in constructor.ParameterList.Parameters)
            {
                parameters.Add(parameter.Identifier.Text, parameter.Type.ToString());
            }

            return new ConstructorInfo(parameters);
        }

        private static ClassInfo GetClassInfo(ClassDeclarationSyntax classDeclaration)
        {
            var methods = new List<MethodInfo>();
            foreach (var method in classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>().Where((methodDeclaration) => methodDeclaration.Modifiers.Any((modifier) => modifier.IsKind(SyntaxKind.PublicKeyword))))
            {
                methods.Add(GetMethodInfo(method));
            }

            var constructors = new List<ConstructorInfo>();
            foreach (var constrctor in classDeclaration.DescendantNodes().OfType<ConstructorDeclarationSyntax>().Where((constructorDeclaration) => constructorDeclaration.Modifiers.Any((modifier) => modifier.IsKind(SyntaxKind.PublicKeyword))))
            {
                constructors.Add(GetConstructorInfo(constrctor));
            }

            return new ClassInfo(classDeclaration.Identifier.ValueText, methods, constructors);
        }
    }
}