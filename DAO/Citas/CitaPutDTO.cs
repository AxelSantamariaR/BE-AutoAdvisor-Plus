using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Citas
{
    public class CitaPutDTO
    {
        public int Id_Cita { get; set; }

        public long Id_Horario { get; set; }

        public DateTime Fecha { get; set; }
    }
}
