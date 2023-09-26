using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions
{
    internal class ClientValidationException : Xeption
    {
        public ClientValidationException(Xeption innerException)
            : base(message: "Client validation error occured, fix the error and try again",
                  innerException)
        { }
    }
}
