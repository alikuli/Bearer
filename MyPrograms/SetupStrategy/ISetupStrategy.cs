using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.Setup;
using System;
namespace Bearer.MyPrograms.SetupStrategy
{
    public interface ISetupStrategy
    {
        SetUp AddInfo(SetUp s);
        string Memory { get; set; }
        string NameFmDb();
        string ValueDb();
        string Validate(SetUp s);
    }
}
