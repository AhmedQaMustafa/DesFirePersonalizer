using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesFirePersonalizer
{
    class WrongPiccSettingException : Exception
    {
        public WrongPiccSettingException(String message) : base(message)
        {
        }
    }

    class PiccSettingsNotFoundException : Exception
    {
        public PiccSettingsNotFoundException(String message) : base(message)
        {
        }
    }

    class AppSettingsNotFoundException : Exception
    {
        public AppSettingsNotFoundException(String message) : base(message)
        {
        }
    }

    class WrongFileAttributesException : Exception
    {
        public WrongFileAttributesException(String message) : base(message)
        {
        }
    }

    class FileAttributesNotFoundException : Exception
    {
        public FileAttributesNotFoundException(String message) : base(message)
        {
        }
    }

    class CountryFileNotFoundException : Exception
    {
        public CountryFileNotFoundException(String message) : base(message)
        {
        }
    }

    class StringNullOrEmptyException : Exception
    {
        public StringNullOrEmptyException(String message) : base(message)
        {
        }
    }

    class RuntimeErrorException : Exception
    {
        public RuntimeErrorException(String message) : base(message)
        {
        }
    }

    class FileSizeException : Exception
    {
        public FileSizeException(String message) : base(message)
        {
        }
    }
}
