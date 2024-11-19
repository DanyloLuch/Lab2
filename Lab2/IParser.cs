using System.Collections.Generic;

namespace Lab2
{
    public interface IParser
    {
        List<PersonViewModel> Parse(string filePath);
    }
}
