using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Xsl;
using System.Xml;
using Microsoft.Maui.Controls;

namespace Lab2
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<PersonViewModel> allPersons = new ObservableCollection<PersonViewModel>();
        private string selectedFilePath;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSelectFileButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] { ".xml" } },
            { DevicePlatform.MacCatalyst, new[] { "xml" } },
            { DevicePlatform.iOS, new[] { "public.xml" } },
            { DevicePlatform.Android, new[] { "application/xml" } },
        });

                var fileResult = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Виберіть XML файл",
                    FileTypes = customFileType
                });

                if (fileResult != null)
                {
                    selectedFilePath = fileResult.FullPath;
                    await DisplayAlert("Файл вибрано", $"Шлях до файлу: {selectedFilePath}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", $"Не вдалося вибрати файл: {ex.Message}", "OK");
            }
        }


        private void SaveHtml(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedFilePath))
                {
                    DisplayAlert("Помилка", "Спершу виберіть файл для перетворення", "OK");
                    return;
                }

                string xsltFilePath = "C:\\Users\\Данило Кучерук\\source\\repos\\Lab2\\Lab2\\Transform.xslt";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(selectedFilePath);

                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltFilePath);

                string htmlFilePath = "D:\\output.html";
                using (StreamWriter writer = new StreamWriter(htmlFilePath))
                {
                    xslt.Transform(xmlDoc, null, writer);
                }

                DisplayAlert("Успіх", "HTML файл успішно збережено за шляхом, D:\\output.html!", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Помилка", $"Сталася помилка: {ex.Message}", "OK");
            }
        }

        private void UpdateTable(IEnumerable<PersonViewModel> data)
        {
            allPersons.Clear();
            foreach (var person in data)
            {
                allPersons.Add(person);
            }

            ResultCollectionView.ItemsSource = allPersons;
        }

        private void SwitchParser(string parserType)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                DisplayAlert("Помилка", "Спершу виберіть файл для парсингу", "OK");
                return;
            }

            IParser parser;
            switch (parserType)
            {
                case "SAX":
                    parser = new SaxParser();
                    break;
                case "DOM":
                    parser = new DomParser();
                    break;
                case "LINQ":
                    parser = new LinqToXmlParser();
                    break;
                default:
                    throw new ArgumentException("Невідомий метод парсингу");
            }

            var parsedData = parser.Parse(selectedFilePath);
            UpdateTable(parsedData);
        }

        private void OnSaxButtonClicked(object sender, EventArgs e) => SwitchParser("SAX");
        private void OnDomButtonClicked(object sender, EventArgs e) => SwitchParser("DOM");
        private void OnLinqButtonClicked(object sender, EventArgs e) => SwitchParser("LINQ");   




        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string searchQuery = SearchEntry.Text?.ToLower() ?? "";

            var filteredPersons = allPersons.Where(person =>
                person.Id.ToLower().Contains(searchQuery) ||
                person.FullName.ToLower().Contains(searchQuery) ||
                person.Faculty.ToLower().Contains(searchQuery) ||
                person.EducationLevel.ToLower().Contains(searchQuery) ||
                person.Subjects.ToLower().Contains(searchQuery) ||
                person.Institution.ToLower().Contains(searchQuery) ||
                person.StartDate.ToLower().Contains(searchQuery)
            ).ToList();

            UpdateTable(filteredPersons);
        }

        private void OnShowDescriptionButtonClicked(object sender, EventArgs e)
        {
            string nameAndGroup = "                                            Кучерук Данило К-25"; 
            string description = "Цей файл містить дані про студентів, їх факультети, кафедри, рівень освіти, університети та предмети, які вони вивчають.";
            string fullDescription = $"{nameAndGroup}\n\n{description}";

            DisplayAlert("Опис", fullDescription, "OK");
        }
        private void OnClearButtonClicked(object sender, EventArgs e)
        {
            SearchEntry.Text = string.Empty;

            UpdateTable(new List<PersonViewModel>());
        }


        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            bool confirmExit = await DisplayAlert("Підтвердження", "Ви дійсно хочете вийти?", "Так", "Ні");

            if (confirmExit)
            {
                Application.Current.Quit();
            }
        }

    }
}
