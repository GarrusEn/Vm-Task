using System;
using System.IO;
using System.Linq;

namespace ZipApp
{
    enum Operation
    {
        compress,
        decompress
    }

    class Properties
    {
        string _sourceFile;
        string _targetFile;
        // 0 - compress, 1 - decompress
        int _statusOperation;
        bool _iChecked;
        ErrorManager _error;

        public string GetSourceFile { get => _sourceFile; }
        public string GetTargetFile { get => _targetFile; }
        public int GetStatusOperation { get => _statusOperation; }

        public Properties(string[] args)
        {
            _error = new ErrorManager();
            if (CheckArgs(args))
            {
                _sourceFile = args[1];
                _targetFile = args[2];
                
                if(args[0] == Enum.GetName(typeof(Operation), 0))
                {
                    _statusOperation = 0;
                }
                else
                {
                    _statusOperation = 1;
                }
                _iChecked = true;
            }
            else
            {
                PrintCheckErrors();
                _iChecked = false;
            }
        }

        public bool ICheckedProperties()
        {
            return _iChecked;
        }

        bool CheckArgs(string[] args)
        {
            if (args.Count() != 3)
            {
                SetErrorLog("Неверное количество аргументов", ErrorType.StartingError);
                return false;
            }

            if (!Enum.IsDefined(typeof(Operation), args[0]))
            {
                SetErrorLog("Первый аргумент должен быть \"compress\" или \"decompress\"",ErrorType.StartingError);
                return false;
            }

            if (!FileNameValid(args[1]))
            {
                SetErrorLog("Неверный путь к исходному файлу", ErrorType.StartingError);
                return false;
            }
            
            if (!File.Exists(args[1]))
            {
                SetErrorLog("Исходный файл не найден", ErrorType.StartingError);
                return false;
            }

            if (!FileNameValid(args[2]))
            {
                SetErrorLog("Неверный путь к целевому файлу", ErrorType.StartingError);
                return false;
            }

            if (File.Exists(args[2]))
            {
                SetErrorLog("Целевой файл уже существует", ErrorType.StartingError);
                return false;
            }

            return true;                        
        }

        bool FileNameValid(string fileName)
        {
            if ((fileName == null) || (fileName.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                return false;
            return true;
        }

        void PrintCheckErrors()
        {
            _error.GetErrors();
        }

        void SetErrorLog(string ErrorMessage, ErrorType ErrorType)
        {
            _error.SetError(new Error(ErrorMessage, ErrorType));
        }
    }
}
