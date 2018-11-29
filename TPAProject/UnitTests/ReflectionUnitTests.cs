using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Library;
using Library.Reflection;
using Library.Tracing;
using Library.TreeView;
using Library.TreeView.ReflectionTreeItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ReflectionUnitTests
    {
        public static TreeViewModel viewModel { get; set; } = new TreeViewModel()
        {
            GetPath = new TestPath(),
            Logger = new FileLogger("Logs.txt", "Console"),
            Serializer = new XMLSerializer.XMLSerializer("test.xml")
        };
        public class TestPath : IOpenDialogPath
        {
            public string GetPath()
            {
                return @"..\..\..\LibraryForTests\bin\Debug\LibraryForTests.dll";
            }
        }
        [TestMethod]
        public void CheckNamespacesTestMethod()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces.Count, 2);
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[0].m_NamespaceName, "LibraryForTests");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[1].m_NamespaceName, "LibraryForTests.Recursion");
        }
        [TestMethod]
        public void CheckClassesTestMethod()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            int numberOfClasses = viewModel.assemblyMetadata.m_Namespaces[0].m_Types.Count +
                                  viewModel.assemblyMetadata.m_Namespaces[1].m_Types.Count;
            Assert.AreEqual(numberOfClasses, 4);
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[0].m_Types[0].TypeName, "Class");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[1].m_Types[0].TypeName, "ClassA");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[1].m_Types[1].TypeName, "ClassB");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[1].m_Types[2].TypeName, "ClassC");
        }
        [TestMethod]
        public void CheckFieldsTestMethod()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[0].m_Types[0].Fields[0].m_ParameterName, "name");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[1].m_Types[0].Fields[0].m_ParameterName, "classB");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[1].m_Types[1].Fields[0].m_ParameterName, "classC");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[1].m_Types[2].Fields[0].m_ParameterName, "classA");
        }
    }
}
