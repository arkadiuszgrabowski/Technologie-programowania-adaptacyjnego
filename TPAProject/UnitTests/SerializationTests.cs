using System;
using System.IO;
using Library;
using Library.Tracing;
using Library.TreeView;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class SerializationTests
    {
        public static TreeViewModel viewModel { get; set; } = new TreeViewModel()
        {
            GetPath = new TestPath(),
            Logger = new FileLogger("Logs.txt", "Tests"),
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
        public void SerializationTest()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            viewModel.Click_Serialize.Execute(null);
            Assert.IsTrue(File.Exists(viewModel.Serializer.GetPath()));
        }

        [TestMethod]
        public void DeserializationTest()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            viewModel.Click_Serialize.Execute(null);
            Assert.IsTrue(File.Exists(viewModel.Serializer.GetPath()));
            viewModel.Click_Deserialize.Execute(null);
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces.Count, 2);
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[0].m_NamespaceName, "LibraryForTests");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[1].m_NamespaceName, "LibraryForTests.Recursion");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[0].m_Types[0].Fields[0].m_ParameterName, "name");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[1].m_Types[0].Fields[0].m_ParameterName, "classB");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[1].m_Types[1].Fields[0].m_ParameterName, "classC");
            Assert.AreEqual(viewModel.assemblyMetadata.m_Namespaces[1].m_Types[2].Fields[0].m_ParameterName, "classA");
        }
    }
}
