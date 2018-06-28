using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ZipApp
{
    enum Operations
    {
        compress,
        decompress
    };
    class Program
    {

        static void Main(string[] args)
        {
            Error error = new Error();

            if (Check(args, error))
            {
                //try
                {
                    Console.WriteLine("Start");
                    ZipWorker zip = new ZipWorker(args, error);
                }
                //catch (Exception ex)
                {
                 //   Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine(error.GetErrors());
            }

            Console.WriteLine("DEBUG!!");
            Console.ReadLine();
        }

        static bool Check(string[] arguments, Error error)
        {
            bool thereAreError = false;

            if (arguments.Length != 3)
            {
                thereAreError = true;
                error.ErrMessage("Неверное количество аргументов");
                return !thereAreError;
            }

            // Checking arguments for errors
            // Check 1 arg           
            if (!Enum.IsDefined(typeof(Operations), arguments[0]))
            {
                thereAreError = true;
                error.ErrMessage("Первый аргумент должен быть <compress> или <decompress>");
            }

            // Check 2 arg
            if (!isTheFileNameValid(arguments[1]))
            {
                thereAreError = true;
                error.ErrMessage("Путь к исходому файлу содержит недопустимые символы.");
            }
            // Does the file exist
            if (!File.Exists(arguments[1]))
            {
                thereAreError = true;
                error.ErrMessage("Исходный файл не найден.");
            }

            // Check 3 arg
            if (!isTheFileNameValid(arguments[2]))
            {
                thereAreError = true;
                error.ErrMessage("Путь к целевому файлу содержит недопустимые символы.");
            }
            // Does the file exist
            if (File.Exists(arguments[2]))
            {
                thereAreError = true;
                error.ErrMessage("Целевой файл уже существует");
            }
            // Create directory
            else if (!Directory.Exists(Path.GetDirectoryName(arguments[2])))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(arguments[2]));
                error.CreateDirectory = true;
            }
            return !thereAreError;
        }



        static private bool isTheFileNameValid(string fileName)
        {
            if ((fileName == null) || (fileName.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                return false;
            return true;
        }
    }

    class Error
    {
        string errors;
        // To store the creation status of the directory
        bool createDirectory = false;

        public Error()
        {
            errors = "";
        }
        // Receiving message
        public void ErrMessage(string err)
        {
            errors += err + "\n";
        }
        // Sending error status
        public string GetErrors()
        {
            return errors;
        }
        public bool CreateDirectory
        {
            get { return createDirectory; }
            set { createDirectory = value; }
        }
    }
}
