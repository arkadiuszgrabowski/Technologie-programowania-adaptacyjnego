﻿using System.Collections.Generic;
using System.ComponentModel;
using Library;
using Library.TreeView;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.MVVMUnitTests
{
    [TestClass]
    public class MVVMTests
    {
        public static TreeViewModel viewModel { get; set; } = new TreeViewModel()
        {
            GetPath = new TestPath(),
            Logger = new FileLogger.FileLogger("Logs.txt", "Console")
        };
        public class TestPath : IOpenDialogPath
        {
            public string GetPath()
            {
                return @"..\..\..\LibraryForTests\TPA.ApplicationArchitecture.dll";
            }
        }
        [TestMethod]
        public void ExecuteCommandTestMethod()
        {
            Assert.IsNull(viewModel.assemblyMetadata);
            Assert.IsNull(viewModel.assemblyTi);
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            Assert.IsNotNull(viewModel.assemblyMetadata);
            Assert.IsNotNull(viewModel.assemblyTi);
        }
        [TestMethod]
        public void GetPathTestMethod()
        {
            viewModel.Click_Browse.Execute(null);
            viewModel.Click_Open.Execute(null);
            Assert.AreEqual(viewModel.GetPath.GetPath(), @"..\..\..\LibraryForTests\TPA.ApplicationArchitecture.dll");
            Assert.IsNotNull(viewModel.PathVariable);
        }
        [TestMethod]
        public void RaisePropertyTestMethod()
        {
            List<string> recievedEvents = new List<string>();
            viewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                recievedEvents.Add(e.PropertyName);
            };
            viewModel.Click_Browse.Execute(null);
            Assert.AreEqual("PathVariable", recievedEvents[0]);
        }
    }
}
