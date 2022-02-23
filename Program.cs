using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }        
    }   
    

    class DataLoader
    {
        static void Main()
        {
            string directory = @"C:\Users\INTENSO\Desktop\Students\";
            string binaryFile = @"E:\Test\Students.dat";
            DirectoryInfo dir = new DirectoryInfo(directory);
            if (!dir.Exists)
            {
                dir.Create();
                Console.WriteLine("Папка \"Students\" создана" + Environment.NewLine);
            }  
            
            try
            {
                using (FileStream st = new FileStream(binaryFile, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011
                    var newPerson = (Student[])formatter.Deserialize(st);
#pragma warning restore SYSLIB0011

                    string group = null;
                    string student = null;
                    foreach (Student s in newPerson)
                    {
                        group = s.Group;
                        student = $"Имя: {s.Name}\t Дата рождения: {s.DateOfBirth.ToShortDateString()}";
                        File.AppendAllText(directory + $"{group}.txt", student + Environment.NewLine);
                        
                        Console.WriteLine($"Имя: {s.Name}\t |\tГруппа: {s.Group}\t  |\tДата рождения: {s.DateOfBirth.ToShortDateString()} |");                        
                    }
                    Console.WriteLine(new string('-', 75));
                }
            }
            catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }

            

            Console.ReadKey();
        }
    }
}
