using System;
using System.IO;
using Library;
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
            Logger = new FileLogger.FileLogger("Logs.txt", "Tests"),
            Serializer = new XMLSerializer.XMLSerializer("test.xml"),
            AssemblyModel = new XMLSerializer.Model.XmlAssembly()

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
            viewModel.Serialize();
            Assert.IsTrue(File.Exists(viewModel.Serializer.GetPath()));
        }

        [TestMethod]
        public void DeserializationTest()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            viewModel.Serialize();
            Assert.IsTrue(File.Exists(viewModel.Serializer.GetPath()));
            viewModel.Deserialize();
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
