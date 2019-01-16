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
    public class DatabaseLoggerUnitTests
    {
        public static TreeViewModel viewModel { get; set; } = new TreeViewModel()
        {
            GetPath = new TestPath(),
            Logger = new DatabaseLogger.Logger(),
            Serializer = new DatabaseSerializer(),
            InAssembly = new DatabaseData.Model.DatabaseAssembly()
        };
        public class TestPath : IOpenDialogPath
        {
            public string GetPath()
            {
                return @"..\..\..\DatabaseData\TPASerializationDB.mdf";
            }
        }
        [TestMethod]
        public void DatabaseLoggerTest()
        {
            viewModel.Logger.Log("Test", LevelEnum.Error, DateTime.Now);
        }
    }
}
