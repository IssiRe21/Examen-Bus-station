using System;
using System.Collections.Generic;

namespace Examen_Bus_station
{
    class Menu
    {
        /*
        > Ingreso de camión
        > Salida de camión
        > Exportar / Imprimir camiones
        > Ver estadísticas
        > Borrar información
        > Salir
            */
        List<int> mainMenuOptions = new List<int>(new int[] { 1, 2, 3, 4, 5, 9 });
        List<char> opcionesDeRuta = new List<char>(new char[] { 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Ñ', 'Z', 'X', 'C', 'V', 'B', 'N', 'M' });

        private const int MAIN_MENU_EXIT_OPTION = 9;
        int camionesQueHanPasadoEnTotal = 0;

        string cuantasVecesHanPasadoEnRutas = "";
        Dictionary<char, int> countByChar = new Dictionary<char, int>();

        private List<Bus> elements = new List<Bus>();


        private void DisplayWelcomeMessage()
        {
            System.Console.WriteLine("Elige una opción\n");
        }

        private void DisplayMainMenuOptions()
        {
            System.Console.WriteLine("1) Ingreso de camión");
            System.Console.WriteLine("2) Salida de camión");
            System.Console.WriteLine("3) Exportar / Imprimir camiones");
            System.Console.WriteLine("4) Ver estadísticas");
            System.Console.WriteLine("5) Borrar información");
            System.Console.WriteLine();
            System.Console.WriteLine("9) Salir");
        }
        private void DisplayByeMessage()
        {
            System.Console.WriteLine("Adiós");
        }

        private int RequestOption(List<int> validOptions)
        {
            int userInputAsInt = 0;
            bool isUserInputValid = false;

            //Mientras no haya una respuesta válida
            while (!isUserInputValid)
            {
                System.Console.WriteLine("Selecciona una opción:");
                string userInput = System.Console.ReadLine();


                try
                {
                    userInputAsInt = Convert.ToInt32(userInput);
                    isUserInputValid = validOptions.Contains(userInputAsInt);
                }
                catch (System.Exception)
                {
                    isUserInputValid = false;
                }


                if (!isUserInputValid)
                {
                    System.Console.WriteLine("La opción seleccionada no es válida.");
                }
            }

            return userInputAsInt;
        }



        private char VerSiLaRutaEsCorrecta(List<char> okOptions)
        {
            char userInputAsChar = ' ';
            bool isUserInputValid = false;

            //Mientras no haya una respuesta válida
            while (!isUserInputValid)
            {
                System.Console.WriteLine("Escribe una ruta");
                string seleccion = System.Console.ReadLine();


                try
                {
                    userInputAsChar = Convert.ToChar(seleccion);
                    isUserInputValid = okOptions.Contains(userInputAsChar);
                }
                catch (System.Exception)
                {
                    isUserInputValid = false;
                }


                if (!isUserInputValid)
                {
                    System.Console.WriteLine("Esa ruta no existe");
                }
            }
            cuantasVecesHanPasadoEnRutas = cuantasVecesHanPasadoEnRutas + userInputAsChar;
            return userInputAsChar;
        }


        public void IngresoDeCamion()
        {
            System.Console.WriteLine("Escribe el nombre del conductor");
            string nombreIngresar = Convert.ToString(Console.ReadLine());
            // Ver si no tiene apellido
            foreach (var caracter in nombreIngresar)
            {
                if (caracter == ' ')
                {
                    System.Console.WriteLine("Solo debes escribir nombres, sin apellidos");
                    return;
                }
            }


            char rutaIngresar = VerSiLaRutaEsCorrecta(opcionesDeRuta);


            elements.Add(new Bus(nombreIngresar, rutaIngresar));
            camionesQueHanPasadoEnTotal = camionesQueHanPasadoEnTotal + 1;
            System.Console.WriteLine($"Ingresado {nombreIngresar} con la ruta {rutaIngresar}\n");
        }

        public void SalidaDeCamion()
        {
            if (elements.Count <= 0)
            {
                System.Console.WriteLine("No hay camiones para salir");
            }
            else
            {
                //System.Console.WriteLine(elements[0].Tomarnombre());
                System.Console.WriteLine($"Salió el conductor {elements[0].Tomarnombre()} con la ruta {elements[0].Tomarruta()}\n");
                elements.RemoveAt(0);

            }
        }

        public void ExportarImprimir()
        {
            if (elements.Count <= 0)
            {
                System.Console.WriteLine("No hay ningún camión en la estación");
            }
            else
            {
                System.Console.WriteLine("Camiones dentro de la estación:\n");
                for (int i = elements.Count - 1; i >= 0; i--)
                {

                    System.Console.WriteLine($"Camión #{i + 1}: {elements[i].Tomarnombre()} con la ruta {elements[i].Tomarruta()}\n");
                }
            }
        }

        public void Estadisticas()
        {
            System.Console.WriteLine($"camiones que han entrado a la estación en total: {camionesQueHanPasadoEnTotal} ");

            foreach (var caracter in cuantasVecesHanPasadoEnRutas)
            {
                if (countByChar.ContainsKey(caracter))
                {
                    countByChar.TryGetValue(caracter, out int currentCount);

                    int nextCount = currentCount + 1;

                    countByChar.Remove(caracter);
                    countByChar.Add(caracter, nextCount);
                }
                else
                {
                    countByChar.Add(caracter, 1);
                }
            }

            System.Console.WriteLine("Cuantas veces han pasado por cada ruta:");
            foreach (var item in countByChar)
            {
                System.Console.WriteLine($"{item.Key} -> {item.Value}");
            }

            foreach (var item in countByChar.Keys)
            {
                countByChar.Remove(item);
            }

        }

        public void BorrarInfo()
        {

            for (int i = elements.Count - 1; i >= 0; i--)
            {
                elements.RemoveAt(0);
            }
            System.Console.WriteLine("Todo ha sido borrado con éxito");
            camionesQueHanPasadoEnTotal = 0;
            cuantasVecesHanPasadoEnRutas = "";

        }

        public void Display()
        {
            int selectedOption = 0;

            DisplayWelcomeMessage();

            while (selectedOption != MAIN_MENU_EXIT_OPTION)
            {
                DisplayMainMenuOptions();

                selectedOption = RequestOption(mainMenuOptions);

                switch (selectedOption)
                {
                    case 1:
                        IngresoDeCamion();
                        break;

                    case 2:
                        SalidaDeCamion();
                        break;

                    case 3:
                        ExportarImprimir();
                        break;

                    case 4:
                        Estadisticas();
                        break;

                    case 5:
                        BorrarInfo();
                        break;
                }
            }

            DisplayByeMessage();
        }
    }
}



