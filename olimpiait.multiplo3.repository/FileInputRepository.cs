using Microsoft.Extensions.Logging;
using olimpiait.multiplo3.entity;
using olimpiait.multiplo3.repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace olimpiait.multiplo3.repository
{
    public class FileInputRepository : IFileInputRepository
    {
        private readonly ILogger<FileInputRepository> Logger;

        public FileInputRepository(ILogger<FileInputRepository> Logger)
        {
            this.Logger = Logger;
        }

        public string fileInputProcess(string pathRead, string pathOut)
        {            

            try
            {
                StreamReader reader = new StreamReader(pathRead);
                string lineReady;
                string input = string.Empty;

                using (var stream = new FileStream(pathOut, FileMode.Create))
                {
                    while ((lineReady = reader.ReadLine()) != null)
                    {
                        //Se valida cada registro con expresiones regulares.
                        if (Regex.IsMatch(lineReady, @"^[0-9]+$"))
                        {                            
                            if ((double.Parse(lineReady) % 3) == 0)
                            {                                
                                input += "Si\n";
                            }
                            else
                            {                                
                                input += "No\n";
                            }
                        }
                        else
                        {                            
                            input += "No\n";
                        }                        
                    }
                }

                using (StreamWriter writetext = new StreamWriter(pathOut))
                {
                    writetext.WriteLine(input);
                }

                reader.Close();

                return "OK";    
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return ex.Message;
            }            
        }
    }
}
