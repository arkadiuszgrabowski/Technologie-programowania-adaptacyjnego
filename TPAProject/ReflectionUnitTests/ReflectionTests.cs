using Library;
using Library.TreeView;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ReflectionUnitTests
{
    [TestClass]
    public class ReflectionTests
    {
        public static TreeViewModel viewModel { get; set; } = new TreeViewModel()
        {
            GetPath = new TestPath(),
            Logger = new FileLogger.FileLogger("Logs.txt", "Console"),
            Serializer = new XMLSerializer.XMLSerializer("test.xml")
        };
        public class TestPath : IOpenDialogPath
        {
            public string GetPath()
            {
                return @"..\..\..\LibraryForTests\TPA.ApplicationArchitecture.dll";
            }
        }
        [TestMethod]
        public void CheckNamespacesTestMethod()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels.Count, 4);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Name, "TPA.ApplicationArchitecture.BusinessLogic");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Name, "TPA.ApplicationArchitecture.Data");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[2].Name, "TPA.ApplicationArchitecture.Data.CircularReference");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Name, "TPA.ApplicationArchitecture.Presentation");
        }
        [TestMethod]
        public void CheckClassesTestMethod()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            int numberOfClasses = viewModel.assemblyMetadata.NamespaceModels[0].Types.Count +
                                  viewModel.assemblyMetadata.NamespaceModels[1].Types.Count +
                                  viewModel.assemblyMetadata.NamespaceModels[2].Types.Count +
                                  viewModel.assemblyMetadata.NamespaceModels[3].Types.Count;
            Assert.AreEqual(numberOfClasses, 20);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Name, "Model");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[1].Name, "ClassWithAttribute");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[2].Name, "DerivedClass");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[2].Types[0].Name, "ClassA");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Types[0].Name, "View");
        }
        [TestMethod]
        public void CheckFieldsTestMethod()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Fields[0].Name, "<Linq2SQL>k__BackingField");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Types[0].Fields[0].Name, "<ViewModel>k__BackingField");
        }
        [TestMethod]
        public void CheckMethodsTestMethod()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Methods[0].Name, "get_Linq2SQL");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Methods[1].Name, "set_Linq2SQL");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Types[0].Methods[0].Name, "get_ViewModel");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[3].Types[0].Methods[1].Name, "set_ViewModel");
        }
    }
}
