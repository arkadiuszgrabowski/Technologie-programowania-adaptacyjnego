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
                return @"..\..\..\LibraryForTests\bin\Debug\LibraryForTests.dll";
            }
        }
        [TestMethod]
        public void CheckNamespacesTestMethod()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels.Count, 2);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Name, "Tests.LibraryForTests");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Name, "Tests.LibraryForTests.Recursion");
        }
        [TestMethod]
        public void CheckClassesTestMethod()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            int numberOfClasses = viewModel.assemblyMetadata.NamespaceModels[0].Types.Count +
                                  viewModel.assemblyMetadata.NamespaceModels[1].Types.Count;
            Assert.AreEqual(numberOfClasses, 4);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Name, "Class");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[0].Name, "ClassA");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[1].Name, "ClassB");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[2].Name, "ClassC");
        }
        [TestMethod]
        public void CheckFieldsTestMethod()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[0].Types[0].Fields[0].Name, "name");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[0].Fields[0].Name, "classB");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[1].Fields[0].Name, "classC");
            Assert.AreEqual(viewModel.assemblyMetadata.NamespaceModels[1].Types[2].Fields[0].Name, "classA");
        }
    }
}
