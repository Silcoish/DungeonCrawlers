using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MidiStripExcess
{
	public partial class Form1 : Form
	{
		string fileName, fileTitle, fileDir;
		string previousLine = "somerandomstringthatwillneverbeinthefileok";
		bool opened = false;
		List<string> output;

		public Form1()
		{
			output = new List<string>();
			InitializeComponent();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.InitialDirectory = "c:\\";
			openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				Console.WriteLine("File Opened");
				fileName = openFileDialog1.FileName;
				fileTitle = Path.GetFileNameWithoutExtension(fileName);
				fileDir = Path.GetDirectoryName(fileName);
				opened = true;
				button1.Enabled = true;
				Console.WriteLine(fileName);
			}
			else
			{
				opened = false;
				button1.Enabled = false;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if(opened)
			{
				LoadFile();
			}
		}

		private void LoadFile()
		{
			try
			{
				using (StreamReader sr = new StreamReader(fileName))
				{
					while (!sr.EndOfStream)
					{
						String line = sr.ReadLine();
						if (!line.Contains(previousLine))
						{
							output.Add(line);
						}
						previousLine = line;
					}

					SaveFile();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(ex.Message);
			}
		}

		private void SaveFile()
		{
			string[] lines = output.ToArray();

			System.IO.File.WriteAllLines(fileDir + "/" + fileTitle + "_stripped.miditext", lines);

			MessageBox.Show("Stripping complete!");
			Application.Exit();
		}
	}
}
