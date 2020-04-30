using System;

namespace Examen_Bus_station
{
    class Bus
    {
        string nombre { get; set; }
        char ruta { get; set; }

       public Bus(string nombre, char ruta)
        {
            this.nombre = nombre;
            this.ruta = ruta;
        } 

         public string Tomarnombre()
        {
            return nombre;
        }

        public Char Tomarruta()
        {
            return ruta;
        }

    }
}