using System;
namespace Bearer.MyPrograms.ValidatorStrategy
{
    public interface IValidator
    {
        string Validator(string item, string incoming);

    }
}
