using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Calificaciones.App
{
    class Alumno
    {
        public string Nombre { get; set; }
        public int Asistencias { get; set; }
        public double Nota { get; set; }
        public double PorcentajeAsistencia { get; set; }
        public string Aprobado { get; set; }
        public string CausaReprobacion { get; set; }

        public Alumno(string nombre, int asistencias, double nota, int totalClases)
        {
            this.Nombre = nombre;
            this.Asistencias = asistencias;
            this.Nota = nota;
            this.PorcentajeAsistencia = (double)asistencias / totalClases * 100;
            this.Aprobado = this.PorcentajeAsistencia >= 85 && this.Nota >= 7 ? "Sí" : "No";

            if (PorcentajeAsistencia < 85 && Nota < 7)
                CausaReprobacion = "Faltas y Nota";
            else if (PorcentajeAsistencia < 85)
                CausaReprobacion = "Faltas";
            else if (Nota < 7)
                CausaReprobacion = "Nota";
            else
                CausaReprobacion = "Ninguna";
        }
    }
}
