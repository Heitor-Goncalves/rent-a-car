using System;
using System. Collections.Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace fnSBRentProcess
{
    internal class RentModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public string TempoAluguel { get; set; }
        public DateTime Data { get; set; }
    }
}
