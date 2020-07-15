using System;
using System.Xml;

namespace ConsoleToXml
{
	internal class Program
	{
		public static XmlDocument Document = new XmlDocument();

		// Keeps track of which element we are referencing right now - start with null, that means the document is empty
		public static XmlElement CurrentlySelected;

		// Add a new node to the currently selected node, then selects the added node
		public static void AddNode()
		{
			Console.WriteLine();
			Console.Write("Name: ");
			string input = Console.ReadLine();

			var elem = Document.CreateElement(input);

			if (CurrentlySelected == null)
			{
				Document.AppendChild(elem);
			}
			else
			{
				CurrentlySelected.AppendChild(elem);
			}

			CurrentlySelected = elem;

			Console.WriteLine();
			Console.WriteLine("Press any key to go back to menu");
			Console.ReadKey();
		}

		// Add an attribute to the currently selected node
		public static void AddAttribute()
		{
			if (CurrentlySelected == null)
			{
				Console.WriteLine("There is no node!");
				Console.WriteLine("Press any key to go back to menu");
				Console.ReadKey();
				return;
			}

			Console.WriteLine();
			Console.Write("Name: ");
			string name = Console.ReadLine();

			Console.WriteLine();
			Console.Write("Value: ");
			string value = Console.ReadLine();

			var attr = Document.CreateAttribute(name);
			attr.InnerText = value;
			Document.LastChild.Attributes.Append(attr);

			Console.WriteLine();
			Console.WriteLine("Press any key to go back to menu");
			Console.ReadKey();
		}

		// Add text to the currently selected node
		public static void AddTextToNode()
		{
			if (CurrentlySelected == null)
			{
				Console.WriteLine("You have to add a node first!");
				Console.WriteLine("Press any key to go back to menu");
				Console.ReadKey();
				return;
			}

			Console.WriteLine();
			Console.Write("Text: ");
			string text = Console.ReadLine();

			var textNode = Document.CreateTextNode(text);

			CurrentlySelected.AppendChild(textNode);

			Console.WriteLine();
			Console.WriteLine("Press any key to go back to menu");
			Console.ReadKey();
		}

		// Output the entire file to the console
		public static void OutputToConsole()
		{
			Console.Clear();
			Document.Save(Console.Out);

			Console.WriteLine();
			Console.WriteLine("Press any key to go back to menu");
			Console.ReadKey();
		}

		// Ask for a path, then if it exists output the XML file there
		public static void OutputToFile()
		{
			Console.WriteLine();
			Console.Write("File path: ");

			var path = "XML " + DateTime.Now.ToString("HH mm") + ".xml";
			Document.Save(path);

			Console.WriteLine();
			Console.WriteLine("Press any key to go back to menu");
			Console.ReadKey();
		}

		private static void Main()
		{
			while (true)
			{
				Console.WriteLine("Welcome to the XML creation tool!");

				if (CurrentlySelected != null)
				{
					Console.WriteLine("Currently selected node: " + CurrentlySelected.OuterXml);
				}

				Console.WriteLine("1 - Create node");
				Console.WriteLine("2 - Add attribute");
				Console.WriteLine("3 - Add text to node");
				Console.WriteLine("4 - Go up a node");
				Console.WriteLine("5 - Go down a node");
				Console.WriteLine("6 - Output to console");
				Console.WriteLine("7 - Save to file");
				Console.WriteLine("8 - Quit");

				char c = Console.ReadKey().KeyChar;

				switch (c)
				{
					case '1':
						AddNode();
						break;

					case '2':
						AddAttribute();
						break;

					case '3':
						AddTextToNode();
						break;

					case '4':
						if (CurrentlySelected == null)
						{
							Console.WriteLine("Cannot go up!");
							Console.WriteLine("Press any key to go back to menu");
							Console.ReadKey();
							break;
						}
						if (CurrentlySelected.ParentNode == null)
						{
							Console.WriteLine("Cannot go up!");
						}
						else
						{
							CurrentlySelected = CurrentlySelected.ParentNode as XmlElement;
						}
						break;

					case '5':
						if (CurrentlySelected == null)
						{
							Console.WriteLine("Cannot go down!");
							Console.WriteLine("Press any key to go back to menu");
							Console.ReadKey();
							break;
						}
						if (!CurrentlySelected.HasChildNodes)
						{
							Console.WriteLine("Cannot go down!");
						}
						else
						{
							CurrentlySelected = CurrentlySelected.FirstChild as XmlElement;
						}
						break;

					case '6':
						OutputToConsole();
						break;

					case '7':
						OutputToFile();
						break;

					default:
						Console.WriteLine();
						return;
				}

				Console.Clear();
				Console.Out.Flush();
			}
		}
	}
}
