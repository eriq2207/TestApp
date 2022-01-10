using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestApp.Models;

namespace TestApp.Utils
{
    class CSVReader
    {
        String fileContent = String.Empty;
        String filePath = String.Empty;
        public CSVReader()
        {

        }
        private String ReadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.filePath = openFileDialog.FileName;
                var fileStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    this.fileContent = reader.ReadToEnd();
                }
                return this.fileContent;
            }
            return String.Empty;
        }
        private Towar parseLine(String csvLine)
        {
            String[] csvLineFields = csvLine.Split(';');
            Towar towar = new Towar();
            if(csvLineFields.Length != 4)
            {
                throw new Exception("Błąd parsowania pliku csv. Nieprawidłowa ilość kolumn!");
            }
            towar.Nazwa = csvLineFields[0];
            towar.Cena =  float.Parse(csvLineFields[1]);
            towar.Opis.A = csvLineFields[2];
            towar.Opis.B = csvLineFields[3];
            return towar;
        }
        public List<Towar> readAndParseCSV()
        {
            ReadFile();
            if (this.fileContent == String.Empty)
                throw new Exception("Błąd odczytu pliku!");

            List<Towar> towary = new List<Towar>();
            String[] csvLines = this.fileContent.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var csvLine in csvLines)
            {
                Towar towar = this.parseLine(csvLine);
                towary.Add(towar);
            }
            return towary;
        }
    }
}
