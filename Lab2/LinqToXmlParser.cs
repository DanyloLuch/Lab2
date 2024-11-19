using System.Collections.Generic;
using System.Xml.Linq;

namespace Lab2
{
    public class LinqToXmlParser : IParser
    {
        public List<PersonViewModel> Parse(string filePath)
        {
            var result = new List<PersonViewModel>();
            XDocument doc = XDocument.Load(filePath);

            foreach (var personElement in doc.Descendants("Student"))
            {
                var person = new PersonViewModel
                {
                    Id = personElement.Attribute("id")?.Value,
                    FullName = personElement.Attribute("fullName")?.Value,
                    Faculty = personElement.Attribute("faculty")?.Value,
                    Department = personElement.Attribute("department")?.Value,
                    EducationLevel = personElement.Attribute("educationLevel")?.Value,
                    Institution = personElement.Attribute("institution")?.Value,
                    StartDate = personElement.Attribute("startDate")?.Value,
                    Subjects = string.Join(", ", personElement.Descendants("Subject").Select(x => x.Attribute("name")?.Value)),
                    Courses = string.Join(", ", personElement.Descendants("Course").Select(x => x.Attribute("name")?.Value))
                };

                result.Add(person);
            }

            return result;
        }
    }
}
