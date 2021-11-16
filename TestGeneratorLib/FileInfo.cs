using System;
using System.Collections.Generic;

namespace TestGeneratorLib
{
    public class FileInfo
    {
        public string Filename { get; private set; }
        public List<ClassInfo> Classes { get; private set; }

        public FileInfo(List<ClassInfo> classes)
        {
            Classes = classes;
        }
    }

    public class ClassInfo
    {
        public List<MethodInfo> Methods { get; private set; }
        public List<ConstructorInfo> Constructors { get; private set; }
        public String Name;

        public ClassInfo(string name, List<MethodInfo> methods, List<ConstructorInfo> constructors)
        {
            Name = name;
            Methods = methods;
            this.Constructors = constructors;
        }
    }

    public class ConstructorInfo
    {
        public Dictionary<string, string> Parameters { get; private set; }

        public ConstructorInfo(Dictionary<string, string> parameters)
        {
            this.Parameters = parameters;
        }
    }

    public class MethodInfo
    {
        public Dictionary<string, string> Parameters { get; private set; }
        public string Name { get; private set; }
        public string ReturnType { get; private set; }

        public MethodInfo(Dictionary<string, string> parameters, string name, string returnType)
        {
            Parameters = parameters;
            Name = name;
            ReturnType = returnType;
        }
    }
}