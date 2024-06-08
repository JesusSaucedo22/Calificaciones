using Calificaciones.App;
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Solicitar al usuario el nombre del archivo de entrada
            Console.Write("Ingrese el nombre del archivo de entrada (con extensión): ");
            string fileName = Console.ReadLine();
            
            // Solicitar al usuario el nombre del archivo de salida
            Console.Write("Ingrese el nombre del archivo de salida (con extensión): ");
            string outputFileName = Console.ReadLine();

            // Leer el archivo especificado por el usuario
            string[] lines = File.ReadAllLines(fileName);

            // Obtener el número total de clases del primer registro
            int totalClases = int.Parse(lines[0]);

            // Crear una lista para almacenar los datos de los alumnos
            List<Alumno> alumnos = new List<Alumno>();

            // Procesar los registros de alumnos a partir del segundo registro
            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                string nombre = data[0].Trim();
                int asistencias = int.Parse(data[1].Trim());
                double nota = double.Parse(data[2].Trim());

                // Crear un objeto Alumno y agregarlo a la lista de alumnos
                Alumno alumno = new Alumno(nombre, asistencias, nota, totalClases);
                alumnos.Add(alumno);
            }

            // Mostrar la tabla con los datos de los alumnos
            MostrarTabla(alumnos);
            MostrarEstadisticas(alumnos, outputFileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error: {ex.Message}");
        }
    }

    static void MostrarTabla(List<Alumno> alumnos)
    {
        // Imprimir encabezados de la tabla
        Console.WriteLine("| Nombre           | Asistencias | % Asistencia | Nota | Aprobado | Causa de Reprobación |");
        Console.WriteLine("|------------------|-------------|--------------|------|----------|----------------------|");

        // Iterar sobre cada alumno para imprimir sus datos en la tabla
        foreach (var alumno in alumnos)
        {
            Console.WriteLine($"| {alumno.Nombre,-16} | {alumno.Asistencias,11} | {alumno.PorcentajeAsistencia,11:F2}% | {alumno.Nota,4} | {alumno.Aprobado,-8} | {alumno.CausaReprobacion,-20} |");
        }
    }

    static void MostrarEstadisticas(List<Alumno> alumnos, string outputFileName)
    {
        double sumaNotas = 0;
        double sumaAsistencias = 0;
        int aprobados = 0;
        int reprobados = 0;
        int reprobadosPorFaltas = 0;
        int reprobadosPorNota = 0;
        int reprobadosPorFaltasYNota = 0;
        double calificacionMaxima = double.MinValue;

        for (int i = 0; i < alumnos.Count; i++)
        {
            Alumno alumno = alumnos[i];
            sumaNotas += alumno.Nota;
            sumaAsistencias += alumno.PorcentajeAsistencia;

            if (alumno.Aprobado == "Sí")
            {
                aprobados++;
            }
            else
            {
                reprobados++;
                if (alumno.CausaReprobacion == "Faltas y Nota")
                {
                    reprobadosPorFaltasYNota++;
                    reprobadosPorFaltas++;
                    reprobadosPorNota++;
                }
                else if (alumno.CausaReprobacion == "Faltas")
                {
                    reprobadosPorFaltas++;
                }
                else if (alumno.CausaReprobacion == "Nota")
                {
                    reprobadosPorNota++;
                }
            }

            if (alumno.Nota > calificacionMaxima)
            {
                calificacionMaxima = alumno.Nota;
            }
        }


        int totalAlumnos = alumnos.Count;
        double calificacionPromedio = sumaNotas / totalAlumnos;
        double asistenciaPromedio = sumaAsistencias / totalAlumnos;
        double porcentajeAprobados = (double)aprobados / totalAlumnos * 100;
        double porcentajeReprobados = (double)reprobados / totalAlumnos * 100;
        double porcentajeReprobadosPorFaltas = (double)reprobadosPorFaltas / totalAlumnos * 100;
        double porcentajeReprobadosPorNota = (double)reprobadosPorNota / totalAlumnos * 100;
        double porcentajeReprobadosPorFaltasYNota = (double)reprobadosPorFaltasYNota / totalAlumnos * 100;

        Console.WriteLine("\nEstadísticas del grupo:");
        Console.WriteLine($"Calificación promedio del grupo: {calificacionPromedio:F2}");
        Console.WriteLine($"Porcentaje de asistencia promedio del grupo: {asistenciaPromedio:F2}%");
        Console.WriteLine($"Número y porcentaje de alumnos aprobados: {aprobados} ({porcentajeAprobados:F2}%)");
        Console.WriteLine($"Número y porcentaje de alumnos reprobados: {reprobados} ({porcentajeReprobados:F2}%)");
        Console.WriteLine($"Número y porcentaje de alumnos reprobados por faltas: {reprobadosPorFaltas} ({porcentajeReprobadosPorFaltas:F2}%)");
        Console.WriteLine($"Número y porcentaje de alumnos reprobados por su nota: {reprobadosPorNota} ({porcentajeReprobadosPorNota:F2}%)");
        Console.WriteLine($"Número y porcentaje de alumnos reprobados por faltas y por nota: {reprobadosPorFaltasYNota} ({porcentajeReprobadosPorFaltasYNota:F2}%)");
        Console.WriteLine($"Calificación máxima del grupo: {calificacionMaxima:F2}");

        string estadisticas = $"\nEstadísticas del grupo:\n" +
            $"Calificación promedio del grupo: {calificacionPromedio:F2}\n" +
            $"Porcentaje de asistencia promedio del grupo: {asistenciaPromedio:F2}%\n" +
            $"Número y porcentaje de alumnos aprobados: {aprobados} ({porcentajeAprobados:F2}%)\n" +
            $"Número y porcentaje de alumnos reprobados: {reprobados} ({porcentajeReprobados:F2}%)\n" +
            $"Número y porcentaje de alumnos reprobados por faltas: {reprobadosPorFaltas} ({porcentajeReprobadosPorFaltas:F2}%)\n" +
            $"Número y porcentaje de alumnos reprobados por su nota: {reprobadosPorNota} ({porcentajeReprobadosPorNota:F2}%)\n" +
            $"Número y porcentaje de alumnos reprobados por faltas y por nota: {reprobadosPorFaltasYNota} ({porcentajeReprobadosPorFaltasYNota:F2}%)\n" +
            $"Calificación máxima del grupo: {calificacionMaxima:F2}";

        // Guardar las estadísticas en el archivo de salida
        File.WriteAllText(outputFileName, estadisticas);
    }
}

