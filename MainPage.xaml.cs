using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;

namespace UwpDataGridSample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.dataGrid.Columns.Clear();

            {
                DataGridTextColumn col = new DataGridTextColumn();
                col.Header = "Column 0";
                col.Binding = new Binding() { Path = new PropertyPath("[0]") };
                col.Binding.Mode = BindingMode.TwoWay;
                col.IsReadOnly = false;
                this.dataGrid.Columns.Add(col);
            }

            {
                DataGridCheckBoxColumn col = new DataGridCheckBoxColumn();
                col.Header = "boolean col";
                col.Binding = new Binding() { Path = new PropertyPath("[1]") };
                col.Binding.Mode = BindingMode.TwoWay;
                col.IsThreeState = false;
                col.IsReadOnly = false;
                this.dataGrid.Columns.Add(col);
            }

            {
                // Create the DataTemplates dynamically so we can change bindings for the column

                int dataColumnIndex = 2;
                int itemListIndex = 3;

                string cellTemplateXaml =
                    "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"> " +
                        "<TextBlock " +
                            "HorizontalAlignment = \"Stretch\" " +
                            "VerticalAlignment = \"Center\" " +
                            "TextAlignment = \"Left\" " +
                            "TextWrapping = \"Wrap\" " +
                            $"Text = \"{{Binding Path=[{dataColumnIndex}]}}\" /> " +
                    "</DataTemplate>";
                DataTemplate template = (DataTemplate)XamlReader.Load(cellTemplateXaml);

                string cellEditingTemplateXaml =
                    "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"> " +
                        "<ComboBox " +
                            "HorizontalAlignment=\"Stretch\" " +
                            "VerticalAlignment=\"Stretch\" " +
                            $"ItemsSource=\"{{Binding Path=[{itemListIndex}]}}\" " +
                            $"SelectedValue=\"{{Binding Path=[{dataColumnIndex}], Mode=TwoWay}}\" " +
                            "SelectedValuePath=\"[0]\" " +
                            "DisplayMemberPath=\"[1]\" " +
                            "/> " +
                    "</DataTemplate>";
                DataTemplate editingTemplate = (DataTemplate)XamlReader.Load(cellEditingTemplateXaml);

                var col = new DataGridTemplateColumn();
                col.Header = "Templated Column";
                col.CellTemplate = template;
                col.CellEditingTemplate = editingTemplate;
                col.IsReadOnly = false;
                dataGrid.Columns.Add(col);
            }

            object[] comboOptions = new object[]
            {
                new object[] { "A", "Abel" },
                new object[] { "B", "Baker" },
                new object[] { "C", "Charlie" },
            };

            object[] data = new object[]
            {
                new object[] { "hello", false, "A", comboOptions},
                new object[] { "there", true, "B", comboOptions },
                new object[] { "you", true, "C", comboOptions },
            };

            this.dataGrid.IsReadOnly = false;
            this.dataGrid.ItemsSource = data;
        }

    }
}
