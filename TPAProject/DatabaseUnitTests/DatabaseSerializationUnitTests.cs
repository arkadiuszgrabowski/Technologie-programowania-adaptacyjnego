using System;
using DatabaseData;
using Contracts;
using Library.Mappers;
using Library.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.TreeView;
using Library;

namespace DatabaseUnitTests
{
    [TestClass]
    [DeploymentItem("TPASerializationDB.mdf")]
    public class DatabaseSerializationUnitTests
    {
        public static ISerializer Serializer = new DatabaseSerializer();
        public static TreeViewModel viewModel { get; set; } = new TreeViewModel()
        {
            GetPath = new TestPath(),
            Logger = new DatabaseLogger.Logger(),
            Serializer = Serializer,
            InAssembly = new DatabaseData.Model.DatabaseAssembly()
        };
        public class TestPath : IOpenDialogPath
        {
            public string GetPath()
            {
                return @"..\..\..\LibraryForTests\TPA.ApplicationArchitecture.dll";
            }
        }
        [TestMethod]
        public void DatabaseDeserializationPossibilityTest()
        {
            Assert.IsTrue(Serializer.IsDeserializationPossible());
        }
        [TestMethod]
        public void DatabaseDeserializationTest()
        {
            viewModel.Deserialize();
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels.Count, 4);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Name, "TPA.ApplicationArchitecture.Presentation");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Name, "TPA.ApplicationArchitecture.Data.CircularReference");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[2].Name, "TPA.ApplicationArchitecture.Data");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Name, "TPA.ApplicationArchitecture.BusinessLogic");
            int numberOfClasses = viewModel.assemblyMetadata.NamespaceModels[0].Types.Count +
                                 viewModel.assemblyMetadata.NamespaceModels[1].Types.Count +
                                 viewModel.assemblyMetadata.NamespaceModels[2].Types.Count +
                                 viewModel.assemblyMetadata.NamespaceModels[3].Types.Count;
            Assert.AreEqual(numberOfClasses, 20);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Types[0].Name, "Model");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[2].Types[1].Name, "ClassWithAttribute");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[2].Types[2].Name, "DerivedClass");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[0].Name, "ClassA");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Name, "View");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Types[0].Fields[0].Name, "<Linq2SQL>k__BackingField");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Fields[0].Name, "<ViewModel>k__BackingField");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Types[0].Methods[0].Name, "get_Linq2SQL");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Types[0].Methods[1].Name, "set_Linq2SQL");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Methods[0].Name, "get_ViewModel");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Methods[1].Name, "set_ViewModel");
        }
    }
}