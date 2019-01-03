using System.IO;
using Library;
using Library.TreeView;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.SerializationUnitTests
{
    [TestClass]
    public class SerializationTests
    {
        public static TreeViewModel viewModel { get; set; } = new TreeViewModel()
        {
            GetPath = new TestPath(),
            Logger = new FileLogger.FileLogger("Logs.txt", "Tests"),
            Serializer = new XMLSerializer.XMLSerializer("test.xml"),
            InAssembly = new XMLSerializer.Model.XmlAssembly()

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
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels.Count, 2);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Name, "Tests.LibraryForTests");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Name, "Tests.LibraryForTests.Recursion");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Fields[0].Name, "name");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[0].Fields[0].Name, "classB");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[1].Fields[0].Name, "classC");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[2].Fields[0].Name, "classA");
        }
    }
}
