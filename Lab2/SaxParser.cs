using System.Collections.Generic;
using System.Xml;

namespace Lab2
{
    public class SaxParser : IParser
    {
        public List<PersonViewModel> Parse(string filePath)
        {
            var result = new List<PersonViewModel>();
            XmlReader reader = XmlReader.Create(filePath);

            PersonViewModel person = null;
            List<string> subjects = new List<string>();
            List<string> tasks = new List<string>();
            List<string> courses = new List<string>();

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "Student":
                            person = new PersonViewModel
                            {
                                Id = reader.GetAttribute("id"),
                                FullName = reader.GetAttribute("fullName"),
                                Faculty = reader.GetAttribute("faculty"),
                                Department = reader.GetAttribute("department"),
                                EducationLevel = reader.GetAttribute("educationLevel"),
                                Institution = reader.GetAttribute("institution"),
                                StartDate = reader.GetAttribute("startDate"),
                                Subjects = reader.GetAttribute("subjects")
                            };
                            break;
                        case "Subject":
                            if (person != null)
                            {
                                subjects.Add(reader.GetAttribute("name"));
                            }
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    if (reader.Name == "Student")
                    {
                        person.Subjects = string.Join(", ", subjects);
                        person.Courses = string.Join(", ", courses);
                        result.Add(person);
                        subjects.Clear();
                        tasks.Clear();
                        courses.Clear();
                    }
                }
            }
            return result;
        }
    }
}
