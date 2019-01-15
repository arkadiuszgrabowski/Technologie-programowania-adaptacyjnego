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
                return @"..\..\..\LibraryForTests\TPA.ApplicationArchitecture.dll";
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
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels.Count, 4);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Name, "TPA.ApplicationArchitecture.BusinessLogic");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Name, "TPA.ApplicationArchitecture.Data");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[2].Name, "TPA.ApplicationArchitecture.Data.CircularReference");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Name, "TPA.ApplicationArchitecture.Presentation");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Name, "Model");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[1].Name, "ClassWithAttribute");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[2].Name, "DerivedClass");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[2].Types[0].Name, "ClassA");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Types[0].Name, "View");
        }
    }
}
